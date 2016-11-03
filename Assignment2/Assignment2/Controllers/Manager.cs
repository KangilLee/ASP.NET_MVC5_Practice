using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using AutoMapper;
using Assignment2.Models;

namespace Assignment2.Controllers
{
    public class Manager
    {
        // Reference to the data context
        private DataContext ds = new DataContext();

        public Manager()
        {
            // If necessary, add constructor code here
        }

        // Add methods below
        // Controllers will call these methods
        // Ensure that the methods accept and deliver ONLY view model objects and collections
        // The collection return type is almost always IEnumerable<T>

        // Suggested naming convention: Entity + task/action
        // For example:
        // ProductGetAll()
        // ProductGetById()
        // ProductAdd()
        // ProductEdit()
        // ProductDelete()




        public IEnumerable<EmployeeBase> EmployeeGetAll()
        {
            return Mapper.Map<IEnumerable<EmployeeBase>>(ds.Employees);
        }

        public EmployeeBase EmployeeGetById(int id)
        {
            var employeeOne = ds.Employees.Find(id);
            return employeeOne == null ? null : Mapper.Map<EmployeeBase>(employeeOne);
        }

        public EmployeeBase EmployeeAdd(EmployeeAdd newItem)
        {
            var addedItem = ds.Employees.Add(Mapper.Map<Employee>(newItem));
            ds.SaveChanges();

            return addedItem == null ? null : Mapper.Map<EmployeeBase>(addedItem);
        }
    }
}