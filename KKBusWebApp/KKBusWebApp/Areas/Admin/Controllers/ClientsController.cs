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
    public class ClientsController : Controller
    {
        private kkbusDBEntities db = new kkbusDBEntities();

        // GET: /Admin/Clients/
        public ActionResult Index()
        {
            var klienci = db.KLIENCI.Include(k => k.OSOBY);
            return View(klienci.ToList());
        }

        // GET: /Admin/Clients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KLIENCI klienci = db.KLIENCI.Find(id);
            if (klienci == null)
            {
                return HttpNotFound();
            }
            return View(klienci);
        }

        // GET: /Admin/Clients/Create
        public ActionResult Create()
        {
            ViewBag.OSO_ID = new SelectList(db.OSOBY, "OSO_ID", "OSO_IMIE");
            return View();
        }

        // POST: /Admin/Clients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="KLI_ID,OSO_ID,KLI_EMAIL,KLI_PUNKTY,KLI_ZABLOKOWANY")] KLIENCI klienci)
        {
            if (ModelState.IsValid)
            {
                db.KLIENCI.Add(klienci);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OSO_ID = new SelectList(db.OSOBY, "OSO_ID", "OSO_IMIE", klienci.OSO_ID);
            return View(klienci);
        }

        // GET: /Admin/Clients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KLIENCI klienci = db.KLIENCI.Find(id);
            if (klienci == null)
            {
                return HttpNotFound();
            }
            ViewBag.OSO_ID = new SelectList(db.OSOBY, "OSO_ID", "OSO_IMIE", klienci.OSO_ID);
            return View(klienci);
        }

        // POST: /Admin/Clients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="KLI_ID,OSO_ID,KLI_EMAIL,KLI_PUNKTY,KLI_ZABLOKOWANY")] KLIENCI klienci)
        {
            if (ModelState.IsValid)
            {
                db.Entry(klienci).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OSO_ID = new SelectList(db.OSOBY, "OSO_ID", "OSO_IMIE", klienci.OSO_ID);
            return View(klienci);
        }

        // GET: /Admin/Clients/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KLIENCI klienci = db.KLIENCI.Find(id);
            if (klienci == null)
            {
                return HttpNotFound();
            }
            return View(klienci);
        }

        // POST: /Admin/Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            KLIENCI klienci = db.KLIENCI.Find(id);
            db.KLIENCI.Remove(klienci);
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
