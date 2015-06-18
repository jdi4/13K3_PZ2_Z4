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
    [Authorize(Roles = "CLIENT")]
    public class LPController : Controller
    {
        private kkbusDBEntities db = new kkbusDBEntities();

        // GET: /LP/
        public ActionResult Index()
        {
            int userId = User.Identity.GetUserId<int>();
            var client = db.OSOBY.Find(userId).KLIENCI.First();
            //int clientId = client.KLI_ID;

            var nagrody_klienci = db.NAGRODY_KLIENCI.Where(nk => nk.KLI_ID == client.KLI_ID)
                .Include(n => n.DOSTEPNE_NAGRODY)
                .Include(n => n.KLIENCI);

            LPHomeViewModel lphome = new LPHomeViewModel()
            {
                UserPrizes = nagrody_klienci.ToList(),
                ClientID = client.KLI_ID,
                LPPoints = client.KLI_PUNKTY,
                UserName = String.Format("{0} {1}", client.OSOBY.OSO_IMIE, client.OSOBY.OSO_NAZWISKO),
                AvaiablePrizes = db.DOSTEPNE_NAGRODY.ToList()  //.Where(dn => dn.NAG_AKTYWNA == new byte[1] {1})
            };
            return View(lphome);
        }

        public ActionResult Exchange(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DOSTEPNE_NAGRODY nagroda = db.DOSTEPNE_NAGRODY.Find(id);
            if (nagroda == null)
            {
                return HttpNotFound();
            }
            int userId = User.Identity.GetUserId<int>();
            var client = db.OSOBY.Find(userId).KLIENCI.First();
            if (nagroda.NAG_PUNKTY > client.KLI_PUNKTY)
            {
                return RedirectToAction("Index"); // dodac info o niemozliwosci wykorzystania
            }
            NAGRODY_KLIENCI nk = new NAGRODY_KLIENCI()
            {
                NAG_ID = nagroda.NAG_ID,
                KLI_ID = client.KLI_ID
                //NAK_ADRES_WYSYLKI = "45-435 Kraków"
            };
            return View(nk);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Exchange([Bind(Include = "KLI_ID,NAG_ID,NAK_ADRES_WYSYLKI")] NAGRODY_KLIENCI nagrody_klienci)
        {
            if (ModelState.IsValid)
            {
                DOSTEPNE_NAGRODY nagroda = db.DOSTEPNE_NAGRODY.Find(nagrody_klienci.NAG_ID);
                int userId = User.Identity.GetUserId<int>();
                var client = db.OSOBY.Find(userId).KLIENCI.First();

                db.NAGRODY_KLIENCI.Add(nagrody_klienci);
                client.KLI_PUNKTY -= nagroda.NAG_PUNKTY;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nagrody_klienci);
        }

        // GET: /LP/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NAGRODY_KLIENCI nagrody_klienci = db.NAGRODY_KLIENCI.Find(id);
            if (nagrody_klienci == null)
            {
                return HttpNotFound();
            }
            return View(nagrody_klienci);
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
