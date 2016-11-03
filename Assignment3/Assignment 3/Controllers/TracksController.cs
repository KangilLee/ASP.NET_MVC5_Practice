using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment_3.Controllers
{
    public class TracksController : Controller
    {
        private Manager m = new Manager();

        // GET : All tracks
        public ActionResult Index()
        {
            return View(m.TrackGetAll());
        }

        // GET: DeepPurple
        public ActionResult DeepPurple()
        {
            return View("Index", m.TrackGetAllDeepPurple());
        }

        // GET: Pop
        public ActionResult Pop()
        {
            return View("Index", m.TrackGetAllPop());
        }

        // GET: Top100Longest
        public ActionResult Top100Longest()
        {
            return View("Index", m.TrackGetAllTop100Longest());
        }

    }
}
