using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KKBusWebApp.Models;
using Microsoft.AspNet.Identity;

namespace KKBusWebApp.Controllers
{
    //[Authorize(Roles="CLIENT")]
    public class ReservationController : Controller
    {
        private kkbusDBEntities db = new kkbusDBEntities();

        // GET: /Reservation/
        public ActionResult Index()
        {
            int userId = User.Identity.GetUserId<int>();
            int clientId = db.OSOBY.Find(userId).KLIENCI.FirstOrDefault().KLI_ID;
            var rezerwacje = db.REZERWACJE.Where(p => p.KLI_ID == clientId);
            if (TempData["ErrorMessage"] != null)
            {
                ViewBag.Message = TempData["ErrorMessage"].ToString();
            }
            return View(rezerwacje.ToList());
        }

        public ActionResult AvaiableReservations()
        {
            DateTime offsetMin = DateTime.Now.AddHours(2);
            DateTime offsetMax = DateTime.Now.AddDays(7);
            var przejazdy = db.PRZEJAZDY.Where(p => p.PRZ_ODJAZD > offsetMin && p.PRZ_ODJAZD < offsetMax)
                .Include(p => p.KURSY)
                .OrderBy(p => p.PRZ_ODJAZD);
            //var przejazdy = db.PRZEJAZDY.Where(p => IsCourseReservatonAvaiable(p))
            //    .Include(p => p.KURSY)
            //    .OrderBy(p => p.PRZ_ODJAZD);
            return View(przejazdy.ToList());
        }

        public ActionResult MakeReservation(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PRZEJAZDY przejazd = db.PRZEJAZDY.Find(id);
            if (przejazd == null)
            {
                return HttpNotFound();
            }
            REZERWACJE rezerwacja = new REZERWACJE();
            rezerwacja.REZ_CENA = przejazd.KURSY.TRASA.Sum(t => t.TRA_CENA);
            //rezerwacja.KLI_ID = User.Identity.GetUserId<int>(); !! źle
            rezerwacja.PRZ_ID = przejazd.PRZ_ID;

            int userid = User.Identity.GetUserId<int>();
            OSOBY osoba = db.OSOBY.Find(userid);

            //SelectList ticketTypesSelectList = new SelectList(db.RODZAJE_BILETOW, "ROD_ID", "ROD_NAZWA", db.RODZAJE_BILETOW.First().ROD_ID);

            RODZAJE_BILETOW rodzaj_biletu = db.RODZAJE_BILETOW.First();
            TicketType ticket = new TicketType() { TicketID = (int)rodzaj_biletu.ROD_ID, TicketName = rodzaj_biletu.ROD_NAZWA, TicketsNumber = 1 };
            List<TicketType> ticketList = new List<TicketType>();
            ticketList.Add(ticket);

            //;

            SelectList ticketTypesSelectList = new SelectList(db.RODZAJE_BILETOW.Where(r => r.ROD_ID != ticket.TicketID), "ROD_ID", "ROD_NAZWA", db.RODZAJE_BILETOW.First(r => r.ROD_ID != ticket.TicketID).ROD_ID);

            MakeReservationViewModel model = new MakeReservationViewModel() {
                                                        Reservation = rezerwacja,
                                                        CourseId = przejazd.PRZ_ID,
                                                        CourseName = przejazd.KURSY.KUR_RELACJA,
                                                        TicketsTypesDropDownList = ticketTypesSelectList,
                                                        TicketsTypes = ticketList,
                                                        Name = String.Format("{0} {1}", osoba.OSO_IMIE, osoba.OSO_NAZWISKO)
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult MakeReservation(int courseId, IEnumerable<TicketType> tickets)
        {
            PRZEJAZDY course = db.PRZEJAZDY.Find(courseId);
            if (IsCourseReservatonAvaiable(course))
            {
                int userId = User.Identity.GetUserId<int>();
                OSOBY osoba = db.OSOBY.Find(userId);
                int clientId = osoba.KLIENCI.FirstOrDefault().KLI_ID;
                REZERWACJE newReservation = new REZERWACJE()
                {
                    KLI_ID = clientId,
                    PRZ_ID = course.PRZ_ID,
                    REZ_DOKUMENT = 0,
                    REZ_WYKORZYSTANA = new byte[1] {0},
                    REZ_CENA = 0
                };

                db.REZERWACJE.Add(newReservation);
                try
                {
                    db.SaveChanges();
                }
                catch
                {
                    ViewBag.StatusMessage = "Wystąpił błąd podczas przetwarzania rezerwacji";
                    return RedirectToAction("AvaiableReservations");
                }

                double price = 0;
                foreach (TicketType ticket in tickets)
                {
                    ULGI_REZERWACJA reservationTickets = new ULGI_REZERWACJA()
                    {
                        REZ_ID = newReservation.REZ_ID,
                        ROD_ID = ticket.TicketID,
                        ULR_ILOSC = ticket.TicketsNumber
                    };

                    price += CalculatePriceWithRelief((double)course.KURSY.TRASA.Sum(t => t.TRA_CENA), db.RODZAJE_BILETOW.Find(ticket.TicketID));
                    db.ULGI_REZERWACJA.Add(reservationTickets);
                }

                newReservation.REZ_CENA = price;

                try
                {
                    db.SaveChanges();
                }
                catch
                {
                    ViewBag.StatusMessage = "Wystąpił błąd podczas przetwarzania rezerwacji";
                    return RedirectToAction("AvaiableReservations");
                }


            }
            else
            {
                ViewBag.StatusMessage = "Wygasła możliwośc rezerwacji wybranego przejazdu";
                return RedirectToAction("AvaiableReservations");
            }

            ViewBag.StatusMessage = "Pomyślnie zarezerwowano przejazd";
            return RedirectToAction("Index");
        }

        public PartialViewResult AddTicketType(int? ticketId)
        {
            if (ticketId == null)
            {
                return PartialView("_TicketTypesListPartial", null);  // zmienić?
            }
            RODZAJE_BILETOW rodzaj_biletu = db.RODZAJE_BILETOW.Find(ticketId);
            TicketType ticket = new TicketType() { TicketID = (int)ticketId, TicketName = rodzaj_biletu.ROD_NAZWA, TicketsNumber = 1 };
            return PartialView("_TicketTypesListPartial", ticket);
        }

        public ActionResult Cancel(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            REZERWACJE reservation = db.REZERWACJE.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            if (!IsReservationCancelAvaiable(reservation))
            {
                TempData["ErrorMessage"] = "Zbyt poźno, aby odwołać rezerwację (rezerwacje można odwoływać tylko do 24h przed odjazdem).";
                return RedirectToAction("Index");
            }
            return View(reservation);
        }

        [HttpPost, ActionName("Cancel")]
        [ValidateAntiForgeryToken]
        public ActionResult CancelConfirmed(int id)
        {
            REZERWACJE reservation = db.REZERWACJE.Find(id);
            db.REZERWACJE.Remove(reservation);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // testy? testy??
        private bool IsCourseReservatonAvaiable(PRZEJAZDY course)
        {
            DateTime offsetMin = DateTime.Now.AddHours(2);
            DateTime offsetMax = DateTime.Now.AddDays(7);
            return course.PRZ_ODJAZD > offsetMin && course.PRZ_ODJAZD < offsetMax;
        }

        private bool IsReservationCancelAvaiable(REZERWACJE reservation)
        {
            return (reservation.PRZEJAZDY.PRZ_ODJAZD - DateTime.Now).Hours >= 24;
        }

        private double CalculatePriceWithRelief(double initialPrice, RODZAJE_BILETOW ticketRelief)
        {
            return initialPrice * (100.0 - ticketRelief.ROD_ZNIZKA) / 100.0;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
