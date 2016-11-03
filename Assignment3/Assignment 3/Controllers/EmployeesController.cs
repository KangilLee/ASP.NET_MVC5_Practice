using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment_3.Controllers
{
    public class EmployeesController : Controller
    {
        private Manager m = new Manager();

        // GET: Employees
        public ActionResult Index()
        {
            return View(m.EmployeeGetAll());
        }
        

        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            var o = m.EmployeeGetById(id.GetValueOrDefault());

            if (o == null)
            {
                return HttpNotFound();
            }
            else
            {
                var editForm = AutoMapper.Mapper.Map<EmployeeEditContactInfoForm>(o);
                return View(editForm);
            }
        }

        // POST: Employees/Edit/5
        [HttpPost]
        public ActionResult Edit(int? id, EmployeeEditContactInfo newItem)
        {
            if(!ModelState.IsValid)
            {
                return RedirectToAction("Edit", new { id = newItem.EmployeeId });
            }

            if(id.GetValueOrDefault() != newItem.EmployeeId)
            {
                return RedirectToAction("Index");
            }

            var editedItem = m.EmployeeEditContactInfo(newItem);

            if (editedItem == null)
            {
                return RedirectToAction("Edit", new { id = newItem.EmployeeId });
            } else
            {
                return RedirectToAction("Index");
            }
        }
    }
}
