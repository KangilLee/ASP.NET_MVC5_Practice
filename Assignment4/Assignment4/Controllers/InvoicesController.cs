using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment4.Controllers
{
    public class InvoicesController : Controller
    {
        private Manager m = new Manager();

        // GET: Invoices
        public ActionResult Index()
        {
            return View(m.InvoiceGetAll());
        }

        // GET: Invoices/Details/5
        public ActionResult Details(int? id)
        {
            var o = m.InvoiceGetByIdWithDetail(id.GetValueOrDefault());

            if (o == null)
            {
                return HttpNotFound();
            } else
            {
                return View(o);
            }
        }
    }
}
