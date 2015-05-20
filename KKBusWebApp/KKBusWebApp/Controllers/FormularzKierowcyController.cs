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
    public class FormularzKierowcyController : Controller
    {
        private kkbusDBEntities db = new kkbusDBEntities();

        // GET: /FormularzKierowcy/
        public ActionResult Index()
        {
            var tankowania = db.TANKOWANIA.Include(t => t.KIEROWCY).Include(t => t.POJAZDY);
            return View(tankowania.ToList());
        }

        // GET: /FormularzKierowcy/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TANKOWANIA tankowania = db.TANKOWANIA.Find(id);
            if (tankowania == null)
            {
                return HttpNotFound();
            }
            return View(tankowania);
        }

        // GET: /FormularzKierowcy/Create
        public ActionResult Create()
        {
            ViewBag.KIE_ID = new SelectList(db.KIEROWCY, "KIE_ID", "KIE_ID");
            ViewBag.POJ_ID = new SelectList(db.POJAZDY, "POJ_ID", "POJ_MARKA");
            return View();
        }

        // POST: /FormularzKierowcy/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="TAN_ID,KIE_ID,POJ_ID,TAN_ILOSC,TAN_KWOTA,TAN_NASTEPNE,TAN_PRZEJECHANO")] TANKOWANIA tankowania)
        {
            if (ModelState.IsValid)
            {
                db.TANKOWANIA.Add(tankowania);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.KIE_ID = new SelectList(db.KIEROWCY, "KIE_ID", "KIE_ID", tankowania.KIE_ID);
            ViewBag.POJ_ID = new SelectList(db.POJAZDY, "POJ_ID", "POJ_MARKA", tankowania.POJ_ID);
            return View(tankowania);
        }

        // GET: /FormularzKierowcy/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TANKOWANIA tankowania = db.TANKOWANIA.Find(id);
            if (tankowania == null)
            {
                return HttpNotFound();
            }
            ViewBag.KIE_ID = new SelectList(db.KIEROWCY, "KIE_ID", "KIE_ID", tankowania.KIE_ID);
            ViewBag.POJ_ID = new SelectList(db.POJAZDY, "POJ_ID", "POJ_MARKA", tankowania.POJ_ID);
            return View(tankowania);
        }

        // POST: /FormularzKierowcy/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="TAN_ID,KIE_ID,POJ_ID,TAN_ILOSC,TAN_KWOTA,TAN_NASTEPNE,TAN_PRZEJECHANO")] TANKOWANIA tankowania)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tankowania).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.KIE_ID = new SelectList(db.KIEROWCY, "KIE_ID", "KIE_ID", tankowania.KIE_ID);
            ViewBag.POJ_ID = new SelectList(db.POJAZDY, "POJ_ID", "POJ_MARKA", tankowania.POJ_ID);
            return View(tankowania);
        }

        // GET: /FormularzKierowcy/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TANKOWANIA tankowania = db.TANKOWANIA.Find(id);
            if (tankowania == null)
            {
                return HttpNotFound();
            }
            return View(tankowania);
        }

        // POST: /FormularzKierowcy/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TANKOWANIA tankowania = db.TANKOWANIA.Find(id);
            db.TANKOWANIA.Remove(tankowania);
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
