using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KKBusWebApp.Models;

namespace KKBusWebApp.Areas.Admin.Controllers
{
    [Authorize(Roles = "OWNER")]
    public class PersonalDataController : Controller
    {
        private kkbusDBEntities db = new kkbusDBEntities();

        // GET: /Admin/PersonalData/
        public ActionResult Index()
        {
            return View(db.OSOBY.ToList());
        }

        // GET: /Admin/PersonalData/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OSOBY osoby = db.OSOBY.Find(id);
            if (osoby == null)
            {
                return HttpNotFound();
            }
            return View(osoby);
        }

        // GET: /Admin/PersonalData/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Admin/PersonalData/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="OSO_IMIE,OSO_NAZWISKO,OSO_PESEL,OSO_TELEFON,OSO_DATA_URODZENIA,OSO_LOGIN,OSO_HASLO")] OSOBY osoby)
        {
            if (ModelState.IsValid)
            {
                Microsoft.AspNet.Identity.PasswordHasher ph = new Microsoft.AspNet.Identity.PasswordHasher();
                osoby.OSO_HASLO = ph.HashPassword(osoby.OSO_HASLO);
                db.OSOBY.Add(osoby);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(osoby);
        }

        // GET: /Admin/PersonalData/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OSOBY osoby = db.OSOBY.Find(id);
            if (osoby == null)
            {
                return HttpNotFound();
            }
            return View(osoby);
        }

        // POST: /Admin/PersonalData/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="OSO_ID,OSO_IMIE,OSO_NAZWISKO,OSO_PESEL,OSO_TELEFON,OSO_DATA_URODZENIA,OSO_LOGIN,OSO_HASLO")] OSOBY osoby)
        {
            if (ModelState.IsValid)
            {
                Microsoft.AspNet.Identity.PasswordHasher ph = new Microsoft.AspNet.Identity.PasswordHasher();
                osoby.OSO_HASLO = ph.HashPassword(osoby.OSO_HASLO);
                db.Entry(osoby).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(osoby);
        }

        // GET: /Admin/PersonalData/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OSOBY osoby = db.OSOBY.Find(id);
            if (osoby == null)
            {
                return HttpNotFound();
            }
            return View(osoby);
        }

        // POST: /Admin/PersonalData/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OSOBY osoby = db.OSOBY.Find(id);
            db.OSOBY.Remove(osoby);
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
