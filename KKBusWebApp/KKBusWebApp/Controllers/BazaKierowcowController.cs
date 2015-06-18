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
    public class BazaKierowcowController : Controller
    {
        private kkbusDBEntities db = new kkbusDBEntities();

        // GET: /BazaKierowcow/
        public ActionResult Index()
        {
            var kierowcy = db.KIEROWCY
                .Include(k => k.OSOBY)
                .Include(k => k.PRZEJAZDY);
            return View(kierowcy.ToList());
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
