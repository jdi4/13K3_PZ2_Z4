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
    public class EmployeesController : Controller
    {
        private kkbusDBEntities db = new kkbusDBEntities();

        // GET: /Admin/Employees/
        public ActionResult Index()
        {
            var pracownicy = db.PRACOWNICY.Include(p => p.OSOBY);
            return View(pracownicy.ToList());
        }

        // GET: /Admin/Employees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PRACOWNICY pracownicy = db.PRACOWNICY.Find(id);
            if (pracownicy == null)
            {
                return HttpNotFound();
            }
            return View(pracownicy);
        }

        // GET: /Admin/Employees/Create
        public ActionResult Create()
        {
            ViewBag.OSO_ID = new SelectList(db.OSOBY, "OSO_ID", "OSO_IMIE");
            return View();
        }

        // POST: /Admin/Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="PRA_ID,OSO_ID,PRA_UPRAWNIENIA")] PRACOWNICY pracownicy)
        {
            if (ModelState.IsValid)
            {
                db.PRACOWNICY.Add(pracownicy);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OSO_ID = new SelectList(db.OSOBY, "OSO_ID", "OSO_IMIE", pracownicy.OSO_ID);
            return View(pracownicy);
        }

        // GET: /Admin/Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PRACOWNICY pracownicy = db.PRACOWNICY.Find(id);
            if (pracownicy == null)
            {
                return HttpNotFound();
            }
            ViewBag.OSO_ID = new SelectList(db.OSOBY, "OSO_ID", "OSO_IMIE", pracownicy.OSO_ID);
            return View(pracownicy);
        }

        // POST: /Admin/Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="PRA_ID,OSO_ID,PRA_UPRAWNIENIA")] PRACOWNICY pracownicy)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pracownicy).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OSO_ID = new SelectList(db.OSOBY, "OSO_ID", "OSO_IMIE", pracownicy.OSO_ID);
            return View(pracownicy);
        }

        // GET: /Admin/Employees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PRACOWNICY pracownicy = db.PRACOWNICY.Find(id);
            if (pracownicy == null)
            {
                return HttpNotFound();
            }
            return View(pracownicy);
        }

        // POST: /Admin/Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PRACOWNICY pracownicy = db.PRACOWNICY.Find(id);
            db.PRACOWNICY.Remove(pracownicy);
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
