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
    [Authorize(Roles = "OWNER")]
    public class RozkladController : Controller
    {
        private kkbusDBEntities db = new kkbusDBEntities();
        // GET: /Rozklad/
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View(db.KURSY.ToList());
        }

        // GET: /Rozklad/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KURSY kursy = db.KURSY.Find(id);
            if (kursy == null)
            {
                return HttpNotFound();
            }
            return View(kursy);
        }

        // GET: /Rozklad/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Rozklad/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="KUR_RELACJA,KUR_NAZWA")] KURSY kursy)
        {
            if (ModelState.IsValid)
            {
                db.KURSY.Add(kursy);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(kursy);
        }

        // GET: /Rozklad/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KURSY kursy = db.KURSY.Find(id);
            if (kursy == null)
            {
                return HttpNotFound();
            }
            return View(kursy);
        }

        // POST: /Rozklad/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="KUR_ID,KUR_RELACJA,KUR_NAZWA")] KURSY kursy)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kursy).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(kursy);
        }

        // GET: /Rozklad/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KURSY kursy = db.KURSY.Find(id);
            if (kursy == null)
            {
                return HttpNotFound();
            }
            return View(kursy);
        }

        // POST: /Rozklad/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            KURSY kursy = db.KURSY.Find(id);
            db.KURSY.Remove(kursy);
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
