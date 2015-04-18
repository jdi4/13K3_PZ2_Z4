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
    public class VehiclesController : Controller
    {
        private sql372873Entities db = new sql372873Entities();

        // GET: /Vehicles/
        public ActionResult Index()
        {
            return View(db.POJAZDY.ToList());
        }

        // GET: /Vehicles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            POJAZDY pojazdy = db.POJAZDY.Find(id);
            if (pojazdy == null)
            {
                return HttpNotFound();
            }
            return View(pojazdy);
        }

        // GET: /Vehicles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Vehicles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="POJ_ID,POJ_MARKA,POJ_SILNIK,POJ_DATA_PRODUKCJI,POJ_UBEZPIECZONY_DO,POJ_SPRAWNY,POJ_ADNOTACJE,POJ_MIEJSCA")] POJAZDY pojazdy)
        {
            if (ModelState.IsValid)
            {
                db.POJAZDY.Add(pojazdy);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pojazdy);
        }

        // GET: /Vehicles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            POJAZDY pojazdy = db.POJAZDY.Find(id);
            if (pojazdy == null)
            {
                return HttpNotFound();
            }
            return View(pojazdy);
        }

        // POST: /Vehicles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="POJ_ID,POJ_MARKA,POJ_SILNIK,POJ_DATA_PRODUKCJI,POJ_UBEZPIECZONY_DO,POJ_SPRAWNY,POJ_ADNOTACJE,POJ_MIEJSCA")] POJAZDY pojazdy)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pojazdy).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pojazdy);
        }

        // GET: /Vehicles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            POJAZDY pojazdy = db.POJAZDY.Find(id);
            if (pojazdy == null)
            {
                return HttpNotFound();
            }
            return View(pojazdy);
        }

        // POST: /Vehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            POJAZDY pojazdy = db.POJAZDY.Find(id);
            db.POJAZDY.Remove(pojazdy);
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
