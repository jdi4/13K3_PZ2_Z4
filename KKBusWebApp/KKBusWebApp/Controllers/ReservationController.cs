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
            var rezerwacje = db.REZERWACJE.Where(p => p.KLI_ID == userId);
            return View(rezerwacje.ToList());
        }

        public ActionResult AvaiableReservations()
        {
            DateTime offsetMin = DateTime.Now.AddHours(2);
            DateTime offsetMax = DateTime.Now.AddDays(7);
            var przejazdy = db.PRZEJAZDY.Where(p => p.PRZ_ODJAZD > offsetMin && p.PRZ_ODJAZD < offsetMax)
                .Include(p => p.KURSY)
                .OrderBy(p => p.PRZ_ODJAZD);
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

            SelectList tickettypes = new SelectList(db.RODZAJE_BILETOW, "ROD_ID", "ROD_NAZWA", db.RODZAJE_BILETOW.First().ROD_ID);

            MakeReservationViewModel model = new MakeReservationViewModel() {
                                                        Reservation = rezerwacja,
                                                        CourseName = przejazd.KURSY.KUR_RELACJA,
                                                        TicketsTypesList = tickettypes,
                                                        TicketsTypes = db.RODZAJE_BILETOW.Where(t => t.ROD_NAZWA == "Normalny").ToList(),
                                                        Name = String.Format("{0} {1}", osoba.OSO_IMIE, osoba.OSO_NAZWISKO)
            };

            return View(model);
        }

        public ViewResult AddTicketType(int? ticketId)
        {
            RODZAJE_BILETOW ticketType = db.RODZAJE_BILETOW.Find(ticketId);
            return View("_TicketTypesListPartial", ticketType);
        }

        //public ActionResult MakeReservation(int? courseId, int? ticketId, string t)
        //{

        //    return View();
        //}

        private ActionResult AddTicketType()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Reserve(int TicketsNumber)
        {

            return View();
        }


        public ActionResult ConfirmReservation()
        {

            return View();
        }

        // GET: /Reservation/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PRZEJAZDY przejazdy = db.PRZEJAZDY.Find(id);
            if (przejazdy == null)
            {
                return HttpNotFound();
            }
            return View(przejazdy);
        }

        // GET: /Reservation/Create
        public ActionResult Create()
        {
            ViewBag.KIE_ID = new SelectList(db.KIEROWCY, "KIE_ID", "KIE_ID");
            ViewBag.KUR_ID = new SelectList(db.KURSY, "KUR_ID", "KUR_RELACJA");
            ViewBag.PRA_ID = new SelectList(db.PRACOWNICY, "PRA_ID", "PRA_UPRAWNIENIA");
            return View();
        }

        // POST: /Reservation/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="PRZ_ID,KUR_ID,PRA_ID,KIE_ID,PRZ_AKTYWNY,PRZ_ODJAZD")] PRZEJAZDY przejazdy)
        {
            if (ModelState.IsValid)
            {
                db.PRZEJAZDY.Add(przejazdy);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.KIE_ID = new SelectList(db.KIEROWCY, "KIE_ID", "KIE_ID", przejazdy.KIE_ID);
            ViewBag.KUR_ID = new SelectList(db.KURSY, "KUR_ID", "KUR_RELACJA", przejazdy.KUR_ID);
            ViewBag.PRA_ID = new SelectList(db.PRACOWNICY, "PRA_ID", "PRA_UPRAWNIENIA", przejazdy.PRA_ID);
            return View(przejazdy);
        }

        // GET: /Reservation/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PRZEJAZDY przejazdy = db.PRZEJAZDY.Find(id);
            if (przejazdy == null)
            {
                return HttpNotFound();
            }
            ViewBag.KIE_ID = new SelectList(db.KIEROWCY, "KIE_ID", "KIE_ID", przejazdy.KIE_ID);
            ViewBag.KUR_ID = new SelectList(db.KURSY, "KUR_ID", "KUR_RELACJA", przejazdy.KUR_ID);
            ViewBag.PRA_ID = new SelectList(db.PRACOWNICY, "PRA_ID", "PRA_UPRAWNIENIA", przejazdy.PRA_ID);
            return View(przejazdy);
        }

        // POST: /Reservation/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="PRZ_ID,KUR_ID,PRA_ID,KIE_ID,PRZ_AKTYWNY,PRZ_ODJAZD")] PRZEJAZDY przejazdy)
        {
            if (ModelState.IsValid)
            {
                db.Entry(przejazdy).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.KIE_ID = new SelectList(db.KIEROWCY, "KIE_ID", "KIE_ID", przejazdy.KIE_ID);
            ViewBag.KUR_ID = new SelectList(db.KURSY, "KUR_ID", "KUR_RELACJA", przejazdy.KUR_ID);
            ViewBag.PRA_ID = new SelectList(db.PRACOWNICY, "PRA_ID", "PRA_UPRAWNIENIA", przejazdy.PRA_ID);
            return View(przejazdy);
        }

        // GET: /Reservation/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PRZEJAZDY przejazdy = db.PRZEJAZDY.Find(id);
            if (przejazdy == null)
            {
                return HttpNotFound();
            }
            return View(przejazdy);
        }

        // POST: /Reservation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PRZEJAZDY przejazdy = db.PRZEJAZDY.Find(id);
            db.PRZEJAZDY.Remove(przejazdy);
            db.SaveChanges();
            return RedirectToAction("Index");
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
