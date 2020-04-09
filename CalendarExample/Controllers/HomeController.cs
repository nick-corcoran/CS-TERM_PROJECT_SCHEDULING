using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CalendarExample.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {

            
            return View();
        }


        public ActionResult Scheduler(Guid cid)
        {
            ViewBag.cid = cid;
            return View();
        }
    }
}