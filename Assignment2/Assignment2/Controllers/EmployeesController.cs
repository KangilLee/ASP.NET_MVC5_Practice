using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment2.Controllers
{
    public class EmployeesController : Controller
    {
        private Manager m = new Manager();

        // GET: Employees
        public ActionResult Index()
        {
            
            return View(m.EmployeeGetAll());
        }

        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            var empDtls = m.EmployeeGetById(id.GetValueOrDefault());
            if(empDtls == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(m.EmployeeGetById(id.GetValueOrDefault()));
            }
            
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            
            return View(new EmployeeAdd());
        }

        // POST: Employees/Create
        [HttpPost]
        public ActionResult Create(EmployeeAdd newEmp)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return View(newEmp);

                }

                //In this time, the data of Employee is added
                var addedItem = m.EmployeeAdd(newEmp);

                if(addedItem == null)
                {
                    return View(newEmp);
                }
                else
                {
                    return RedirectToAction("Details", new { id = addedItem.EmployeeId });
                }

                
            }
            catch
            {
                return View(newEmp);
            }
        }

    }
}
