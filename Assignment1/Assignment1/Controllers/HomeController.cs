using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "About this application :";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact me";

            return View();
        }
    }
}