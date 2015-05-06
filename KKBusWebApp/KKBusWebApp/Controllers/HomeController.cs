using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KKBusWebApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Rozklad()
        {
            ViewBag.Message = "Strona z rozkladem jazdy";

            return View();
        }

        public ActionResult StrefaKlienta()
        {
            ViewBag.Message = "Strona z informacjami dotyczącymi naszych usług";

            return View();
        }
    }
}