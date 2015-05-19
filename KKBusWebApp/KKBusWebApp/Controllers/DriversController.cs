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
    public class DriversController : Controller
    {
        private kkbusDBEntities db = new kkbusDBEntities();

        // GET: /Drivers/
        public ActionResult Index()
        {
            var kierowcy = db.KIEROWCY.Include(k => k.OSOBY);
            return View(kierowcy.ToList());
        }

        // GET: /Drivers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KIEROWCY kierowcy = db.KIEROWCY.Find(id);
            if (kierowcy == null)
            {
                return HttpNotFound();
            }
            return View(kierowcy);
        }

        // GET: /Drivers/Create
        public ActionResult Create()
        {
            ViewBag.OSO_ID = new SelectList(db.OSOBY, "OSO_ID", "OSO_IMIE");
            return View();
        }

        // POST: /Drivers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="KIE_ID,OSO_ID")] KIEROWCY kierowcy)
        {
            if (ModelState.IsValid)
            {
                db.KIEROWCY.Add(kierowcy);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OSO_ID = new SelectList(db.OSOBY, "OSO_ID", "OSO_IMIE", kierowcy.OSO_ID);
            return View(kierowcy);
        }

        // GET: /Drivers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KIEROWCY kierowcy = db.KIEROWCY.Find(id);
            if (kierowcy == null)
            {
                return HttpNotFound();
            }
            ViewBag.OSO_ID = new SelectList(db.OSOBY, "OSO_ID", "OSO_IMIE", kierowcy.OSO_ID);
            return View(kierowcy);
        }

        // POST: /Drivers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="KIE_ID,OSO_ID")] KIEROWCY kierowcy)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kierowcy).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OSO_ID = new SelectList(db.OSOBY, "OSO_ID", "OSO_IMIE", kierowcy.OSO_ID);
            return View(kierowcy);
        }

        // GET: /Drivers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KIEROWCY kierowcy = db.KIEROWCY.Find(id);
            if (kierowcy == null)
            {
                return HttpNotFound();
            }
            return View(kierowcy);
        }

        // POST: /Drivers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            KIEROWCY kierowcy = db.KIEROWCY.Find(id);
            db.KIEROWCY.Remove(kierowcy);
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
