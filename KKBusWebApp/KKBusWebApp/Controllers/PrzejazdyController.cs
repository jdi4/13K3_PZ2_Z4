using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KKBusWebApp.Models;

namespace KKBusWebApp.Controllers
{
    public class PrzejazdyController : Controller
    {
        private kkbusDBEntities db = new kkbusDBEntities();

        // GET: /Przejazdy/
        public ActionResult Index(bool past = false)
        {
            DateTime offsetMin = past ? new DateTime() : DateTime.Now.Date; // sam dzień (bez godzin, żeby wyśietlić wszystkie przejazdy danego dnia)
            DateTime offsetMax = past ? DateTime.Now : DateTime.MaxValue;
            
            var przejazdy = db.PRZEJAZDY
                .Where(p => p.PRZ_ODJAZD > offsetMin && p.PRZ_ODJAZD < offsetMax)
                .Include(p => p.KIEROWCY)
                .Include(p => p.KURSY)
                .Include(p => p.PRACOWNICY)
                .Include(p => p.KIEROWCY.OSOBY)
                .Include(p => p.REZERWACJE)
                .Include(p => p.POJAZDY)
                .OrderBy(p => p.PRZ_ODJAZD);
            ViewBag.Title = "Zarządzanie przejazdami";
            ViewBag.HeaderH2 = past ? "Poprzednie przejazdy" : "Aktualne przejazdy";
            ViewBag.PastMode = past;
            return View(przejazdy.ToList());
        }

        // GET: /Przejazdy/Details/5
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

        // GET: /Przejazdy/Create
        public ActionResult Create()
        {
            SetPRZEJAZDYSelectLists();
            PRZEJAZDY nowyPrzejazd = new PRZEJAZDY()
            {
                PRZ_AKTYWNY = false,
                KIE_ID = db.KIEROWCY.FirstOrDefault().KIE_ID
            };
            return View(nowyPrzejazd);
        }

        // POST: /Przejazdy/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="KUR_ID,PRA_ID,KIE_ID,PRZ_AKTYWNY,PRZ_ODJAZD,POJ_ID")] PRZEJAZDY przejazdy)
        {
            if (ModelState.IsValid)
            {
                //przejazdy.KIE_ID = db.KIEROWCY.First(k => k.OSO_ID == db.OSOBY.First(o => o.OSO_ID == przejazdy))
                db.PRZEJAZDY.Add(przejazdy);
                db.SaveChanges();
                db.Entry(przejazdy).Reference("PRACOWNICY").Load();
                KIEROWCY kie = db.KIEROWCY.First(k => k.OSO_ID == przejazdy.PRACOWNICY.OSO_ID);
                if (kie == null)
                {
                    kie = new KIEROWCY() { OSO_ID = przejazdy.PRACOWNICY.OSO_ID };
                    db.KIEROWCY.Add(kie);
                    db.SaveChanges();
                }
                przejazdy.KIE_ID = kie.KIE_ID;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            SetPRZEJAZDYSelectLists();
            return View(przejazdy);
        }

        // GET: /Przejazdy/Edit/5
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
            SetPRZEJAZDYSelectLists();
            return View(przejazdy);
        }

        // POST: /Przejazdy/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="KUR_ID,PRA_ID,KIE_ID,PRZ_AKTYWNY,PRZ_ODJAZD")] PRZEJAZDY przejazdy)
        {
            if (ModelState.IsValid)
            {
                db.Entry(przejazdy).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            SetPRZEJAZDYSelectLists();
            return View(przejazdy);
        }

        // GET: /Przejazdy/Delete/5
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

        // POST: /Przejazdy/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PRZEJAZDY przejazdy = db.PRZEJAZDY.Find(id);
            db.PRZEJAZDY.Remove(przejazdy);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        private void SetPRZEJAZDYSelectLists()
        {
            var pracownicy = db.PRACOWNICY.Where(pra => pra.PRA_UPRAWNIENIA.Contains("DRIVER"));
            //var pracownicy = db.PRACOWNICY.Where(pra => pra.PRA_UPRAWNIENIA == "DIRVER");
            List<object> pracownicySL = new List<object>();
            foreach (var pr in pracownicy)
                pracownicySL.Add(new
                {
                    Id = pr.PRA_ID,
                    Name = pr.OSOBY.OSO_IMIE + " " + pr.OSOBY.OSO_NAZWISKO
                });

            var pojazdy = db.POJAZDY.Where(p => p.POJ_SPRAWNY == new byte[1] { 1 });
            List<object> pojazdySL = new List<object>();
            foreach (var p in pojazdy)
                pojazdySL.Add(new
                {
                    Id = p.POJ_ID,
                    Name = String.Format("{0} {1} [miejsca: {2}]", p.POJ_MARKA, p.POJ_ID, p.POJ_MIEJSCA)
                });

            //ViewBag.KIE_ID = new SelectList(kierowcySL, "Id", "Name");
            //ViewBag.KIE_ID = db.KIEROWCY.FirstOrDefault().KIE_ID;
            ViewBag.KUR_ID = new SelectList(db.KURSY, "KUR_ID", "KUR_RELACJA");
            ViewBag.PRA_ID = new SelectList(pracownicySL, "Id", "Name");
            ViewBag.POJ_ID = new SelectList(pojazdySL, "Id", "Name");
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
