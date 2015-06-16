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
        public ActionResult Index()
        {
            DateTime offsetMin = DateTime.Now.Date; // sam dzień (bez godzin, żeby wyśietlić wszystkie przejazdy danego dnia)

            var przejazdy = db.PRZEJAZDY
                .Where(p => p.PRZ_ODJAZD > offsetMin)
                .Include(p => p.KIEROWCY)
                .Include(p => p.KURSY)
                .Include(p => p.PRACOWNICY)
                .Include(p => p.KIEROWCY.OSOBY)
                .Include(p => p.REZERWACJE)
                .OrderBy(p => p.PRZ_ODJAZD);
            return View(przejazdy.ToList());
        }

        public ActionResult Past()
        {
            DateTime offsetMin = DateTime.Now;

            var przejazdy = db.PRZEJAZDY
                .Where(p => p.PRZ_ODJAZD < offsetMin)
                .Include(p => p.KIEROWCY)
                .Include(p => p.KURSY)
                .Include(p => p.PRACOWNICY)
                .Include(p => p.KIEROWCY.OSOBY)
                .Include(p => p.REZERWACJE)
                .OrderBy(p => p.PRZ_ODJAZD);
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
            ViewBag.KIE_ID = new SelectList(db.KIEROWCY, "KIE_ID", "KIE_ID");
            ViewBag.KUR_ID = new SelectList(db.KURSY, "KUR_ID", "KUR_RELACJA");
            ViewBag.PRA_ID = new SelectList(db.PRACOWNICY, "PRA_ID", "PRA_UPRAWNIENIA");
            return View();
        }

        // POST: /Przejazdy/Create
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
            ViewBag.KIE_ID = new SelectList(db.KIEROWCY, "KIE_ID", "KIE_ID", przejazdy.KIE_ID);
            ViewBag.KUR_ID = new SelectList(db.KURSY, "KUR_ID", "KUR_RELACJA", przejazdy.KUR_ID);
            ViewBag.PRA_ID = new SelectList(db.PRACOWNICY, "PRA_ID", "PRA_UPRAWNIENIA", przejazdy.PRA_ID);
            return View(przejazdy);
        }

        // POST: /Przejazdy/Edit/5
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
