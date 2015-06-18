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
    [Authorize(Roles = "WORKER, OWNER")]
    public class TrasyController : Controller
    {
        private kkbusDBEntities db;

        public TrasyController()
            :this(new kkbusDBEntities())
        {

        }

        public TrasyController(kkbusDBEntities dbcontext)
        {
            this.db = dbcontext;
        }

        // GET: /Trasy/
        public ActionResult Index()
        {
            var trasa = db.TRASA.Include(t => t.KURSY).Include(t => t.PRZYSTANKI);
            return View(trasa.ToList());
        }

        // GET: /Trasy/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TRASA trasa = db.TRASA.Find(id);
            if (trasa == null)
            {
                return HttpNotFound();
            }
            return View(trasa);
        }

        // GET: /Trasy/Create
        public ActionResult Create()
        {
            ViewBag.KUR_ID = new SelectList(db.KURSY, "KUR_ID", "KUR_RELACJA");
            ViewBag.PRZY_ID = new SelectList(db.PRZYSTANKI, "PRZY_ID", "PRZY_NAZWA");
            return View();
        }

        // POST: /Trasy/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="TRA_ID,KUR_ID,PRZY_ID,TRA_KOLEJNOSC,TRA_CZAS_PRZEJAZDU,TRA_CENA")] TRASA trasa)
        {
            if (ModelState.IsValid)
            {
                db.TRASA.Add(trasa);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.KUR_ID = new SelectList(db.KURSY, "KUR_ID", "KUR_RELACJA", trasa.KUR_ID);
            ViewBag.PRZY_ID = new SelectList(db.PRZYSTANKI, "PRZY_ID", "PRZY_NAZWA", trasa.PRZY_ID);
            return View(trasa);
        }

        // GET: /Trasy/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TRASA trasa = db.TRASA.Find(id);
            if (trasa == null)
            {
                return HttpNotFound();
            }
            ViewBag.KUR_ID = new SelectList(db.KURSY, "KUR_ID", "KUR_RELACJA", trasa.KUR_ID);
            ViewBag.PRZY_ID = new SelectList(db.PRZYSTANKI, "PRZY_ID", "PRZY_NAZWA", trasa.PRZY_ID);
            return View(trasa);
        }

        // POST: /Trasy/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="TRA_ID,KUR_ID,PRZY_ID,TRA_KOLEJNOSC,TRA_CZAS_PRZEJAZDU,TRA_CENA")] TRASA trasa)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trasa).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.KUR_ID = new SelectList(db.KURSY, "KUR_ID", "KUR_RELACJA", trasa.KUR_ID);
            ViewBag.PRZY_ID = new SelectList(db.PRZYSTANKI, "PRZY_ID", "PRZY_NAZWA", trasa.PRZY_ID);
            return View(trasa);
        }

        // GET: /Trasy/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TRASA trasa = db.TRASA.Find(id);
            if (trasa == null)
            {
                return HttpNotFound();
            }
            return View(trasa);
        }

        // POST: /Trasy/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TRASA trasa = db.TRASA.Find(id);
            db.TRASA.Remove(trasa);
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
