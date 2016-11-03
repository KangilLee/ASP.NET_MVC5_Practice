using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment8.Controllers
{
    [Authorize(Roles = "Admin")]
    public class LoadDataController : Controller
    {
        // Reference to the manager object
        private Manager m = new Manager();

        // GET: LoadData
        public ActionResult Index()
        {
            return View();
        }

        // GET: LoadData/AllData
        public ActionResult AllData()
        {
            if (m.LoadData())

            {
                ViewBag.Result = "All data was loaded";
            }
            else
            {
                ViewBag.Result = "(done)";
            }
            return View("result");
        }

        public ActionResult RemoveDatabase()
        {
            if (m.RemoveDatabase())
            {
                ViewBag.Result = "The database was deleted";
            }
            else
            {
                ViewBag.Result = "(done)";
            }
            return View("result");
        }
    }
}
