using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KKBusWebApp.Areas.Admin.Controllers
{
    [Authorize(Roles="OWNER")]
    public class AdminPanelController : Controller
    {
        //
        // GET: /Admin/AdminPanel/
        public ActionResult Index()
        {
            return View();
        }
	}
}