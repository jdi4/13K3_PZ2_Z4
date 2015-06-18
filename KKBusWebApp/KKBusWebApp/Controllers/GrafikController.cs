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
    [Authorize(Roles = "WORKER, DRIVER, OWNER")]
    public class GrafikController : Controller
    {
        private kkbusDBEntities db = new kkbusDBEntities();

        // GET: /Grafik/
        //public ActionResult Index()
        //{
        //    var czas_pracy = db.CZAS_PRACY.Include(c => c.PRACOWNICY);
        //    return View(czas_pracy.ToList());
        //}

        public ActionResult Index(int? employeeId)
        {
            if (employeeId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var czas_pracy = db.CZAS_PRACY
                .Where(cz => cz.PRA_ID == employeeId)
                .Include(cz => cz.PRACOWNICY)
                .Include(cz => cz.PRACOWNICY.OSOBY);
            return View(czas_pracy.ToList());
        }

        // GET: /Grafik/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CZAS_PRACY czas_pracy = db.CZAS_PRACY.Find(id);
            if (czas_pracy == null)
            {
                return HttpNotFound();
            }
            return View(czas_pracy);
        }

        // GET: /Grafik/Create
        public ActionResult Create(int employeeId)
        {
            ViewBag.PRA_ID = employeeId; //new SelectList(db.PRACOWNICY, "PRA_ID", "PRA_UPRAWNIENIA");
            CZAS_PRACY cp = new CZAS_PRACY()
            {
                PRA_ID = employeeId,
                PRACOWNICY = db.PRACOWNICY.Find(employeeId)
            };

            return View(cp);
        }

        // POST: /Grafik/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="PRA_ID,CZA_OD,CZA_DO")] CZAS_PRACY czas_pracy)
        {
            if (ModelState.IsValid)
            {
                db.CZAS_PRACY.Add(czas_pracy);
                db.SaveChanges();
                return RedirectToAction("Index", new { employeeId = czas_pracy.PRA_ID });
            }

            ViewBag.PRA_ID = czas_pracy.PRA_ID; //new SelectList(db.PRACOWNICY, "PRA_ID", "PRA_UPRAWNIENIA", czas_pracy.PRA_ID);
            return View(czas_pracy);
        }

        // GET: /Grafik/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CZAS_PRACY czas_pracy = db.CZAS_PRACY.Find(id);
            if (czas_pracy == null)
            {
                return HttpNotFound();
            }
            return View(czas_pracy);
        }

        // POST: /Grafik/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CZAS_PRACY czas_pracy = db.CZAS_PRACY.Find(id);
            db.CZAS_PRACY.Remove(czas_pracy);
            db.SaveChanges();
            return RedirectToAction("Index", new { employeeId = czas_pracy.PRA_ID });
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
