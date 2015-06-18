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
    public class PrzystankiController : Controller
    {
        private kkbusDBEntities db = new kkbusDBEntities();

        // GET: /Przystanki/
        public ActionResult Index()
        {
            return View(db.PRZYSTANKI.ToList());
        }

        // GET: /Przystanki/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PRZYSTANKI przystanki = db.PRZYSTANKI.Find(id);
            if (przystanki == null)
            {
                return HttpNotFound();
            }
            return View(przystanki);
        }

        // GET: /Przystanki/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Przystanki/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="PRZY_NAZWA,PRZY_GPS_LONGITUDE,PRZY_GPS_LATITUDE,PRZY_ZADANIE")] PRZYSTANKI przystanki)
        {
            if (ModelState.IsValid)
            {
                db.PRZYSTANKI.Add(przystanki);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(przystanki);
        }

        // GET: /Przystanki/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PRZYSTANKI przystanki = db.PRZYSTANKI.Find(id);
            if (przystanki == null)
            {
                return HttpNotFound();
            }
            return View(przystanki);
        }

        // POST: /Przystanki/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="PRZY_NAZWA,PRZY_GPS_LONGITUDE,PRZY_GPS_LATITUDE,PRZY_ZADANIE")] PRZYSTANKI przystanki)
        {
            if (ModelState.IsValid)
            {
                db.Entry(przystanki).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(przystanki);
        }

        // GET: /Przystanki/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PRZYSTANKI przystanki = db.PRZYSTANKI.Find(id);
            if (przystanki == null)
            {
                return HttpNotFound();
            }
            return View(przystanki);
        }

        // POST: /Przystanki/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PRZYSTANKI przystanki = db.PRZYSTANKI.Find(id);
            db.PRZYSTANKI.Remove(przystanki);
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
