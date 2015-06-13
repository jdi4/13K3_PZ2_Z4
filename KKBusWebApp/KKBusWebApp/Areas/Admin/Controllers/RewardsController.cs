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
    public class RewardsController : Controller
    {
        private kkbusDBEntities db;

        public RewardsController()
            :this(new kkbusDBEntities())
        {

        }

        public RewardsController(kkbusDBEntities dbcontext)
        {
            this.db = dbcontext;
        }

        // GET: /Admin/Rewards/
        public ActionResult Index()
        {
            return View(db.DOSTEPNE_NAGRODY.ToList());
        }

        // GET: /Admin/Rewards/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DOSTEPNE_NAGRODY dostepne_nagrody = db.DOSTEPNE_NAGRODY.Find(id);
            if (dostepne_nagrody == null)
            {
                return HttpNotFound();
            }
            return View(dostepne_nagrody);
        }

        // GET: /Admin/Rewards/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Admin/Rewards/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="NAG_ID,NAG_NAZWA,NAG_AKTYWNA,NAG_PUNKTY,NAG_SCIEZKA_OBRAZKA")] DOSTEPNE_NAGRODY dostepne_nagrody)
        {
            if (ModelState.IsValid)
            {
                db.DOSTEPNE_NAGRODY.Add(dostepne_nagrody);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dostepne_nagrody);
        }

        // GET: /Admin/Rewards/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DOSTEPNE_NAGRODY dostepne_nagrody = db.DOSTEPNE_NAGRODY.Find(id);
            if (dostepne_nagrody == null)
            {
                return HttpNotFound();
            }
            return View(dostepne_nagrody);
        }

        // POST: /Admin/Rewards/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="NAG_ID,NAG_NAZWA,NAG_AKTYWNA,NAG_PUNKTY,NAG_SCIEZKA_OBRAZKA")] DOSTEPNE_NAGRODY dostepne_nagrody)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dostepne_nagrody).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dostepne_nagrody);
        }

        // GET: /Admin/Rewards/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DOSTEPNE_NAGRODY dostepne_nagrody = db.DOSTEPNE_NAGRODY.Find(id);
            if (dostepne_nagrody == null)
            {
                return HttpNotFound();
            }
            return View(dostepne_nagrody);
        }

        // POST: /Admin/Rewards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DOSTEPNE_NAGRODY dostepne_nagrody = db.DOSTEPNE_NAGRODY.Find(id);
            db.DOSTEPNE_NAGRODY.Remove(dostepne_nagrody);
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
