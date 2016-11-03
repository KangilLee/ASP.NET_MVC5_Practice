using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment1.Controllers
{
    public class PhonesController : Controller
    {

        // Collection of phones
        private List<PhoneBase> Phones;

        public PhonesController()
        {
            //Initialize the collection
            Phones = new List<PhoneBase>();

            var priv = new PhoneBase();
            priv.Id = 1;
            priv.PhoneName = "Priv";
            priv.Manufacturer = "BlackBerry";
            priv.DateReleased = new DateTime(2015, 11, 6);
            priv.MSRP = 799;
            priv.ScreenSize = 5.43;
            Phones.Add(priv);

            var galaxy = new PhoneBase
            {
                Id = 2,
                PhoneName = "Galaxy S6",
                Manufacturer = "Samsung",
                DateReleased = new DateTime(2015,4,10),
                MSRP = 649,
                ScreenSize = 5.1
            };

            Phones.Add(galaxy);

            Phones.Add(new PhoneBase
            {
                Id = 3,
                PhoneName = "iPhone 6s",
                Manufacturer = "Apple",
                DateReleased = new DateTime(2015,9,25),
                MSRP = 649,
                ScreenSize = 4.7
            });


        }

        // GET: Phones
        public ActionResult Index()
        {
            return View(Phones);
        }

        // GET: Phones/Details/5
        public ActionResult Details(int id)
        {

            return View(Phones[ id - 1]);
        }

        // GET: Phones/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Phones/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                int msrp;
                double screenSize;
                bool isNumber;

                PhoneBase newItem = new PhoneBase();

                newItem.Id = Phones.Count + 1;

                //Configure the string properties
                newItem.PhoneName = collection["PhoneName"];
                newItem.Manufacturer = collection["Manufacturer"];

                //Configure the date; it comes into the method as a string
                newItem.DateReleased = Convert.ToDateTime(collection["DateReleased"]);

                isNumber = Int32.TryParse(collection["MSRP"], out msrp);

                if (isNumber)
                {
                    newItem.MSRP = msrp;
                }

                isNumber = Double.TryParse(collection["ScreenSize"], out screenSize);

                if (isNumber)
                {
                    newItem.ScreenSize = screenSize;
                }

                Phones.Add(newItem);

                //return RedirectToAction("Index");
                return View("Details",newItem);
            }
            catch
            {
                return View();
            }
        }

        
    }
}
