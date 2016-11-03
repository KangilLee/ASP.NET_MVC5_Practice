using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using AutoMapper;
using Assignment_3.Models;

namespace Assignment_3.Controllers
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
            var emp = ds.Employees.Find(id);
            return emp == null ? null : Mapper.Map<EmployeeBase>(emp);
            
        }

        public EmployeeBase EmployeeEditContactInfo(EmployeeEditContactInfo newEmp)
        {
            var o = ds.Employees.Find(newEmp.EmployeeId);

            if (o == null)
            {
                return null;
            }
            else
            {
                ds.Entry(o).CurrentValues.SetValues(newEmp);
                ds.SaveChanges();
            }

            return Mapper.Map<EmployeeBase>(o);
        }


        //Start of Track
        public IEnumerable<TrackBase> TrackGetAll()
        {
            return Mapper.Map<IEnumerable<TrackBase>>(ds.Tracks.OrderBy(t => t.TrackId)
                                                                .ThenBy(t => t.Name)
                                                               );
        }

        public IEnumerable<TrackBase> TrackGetAllPop()
        {
            
            return Mapper.Map<IEnumerable<TrackBase>>(ds.Tracks.Where(t => t.GenreId == 9)
                                                               .OrderBy(t => t.Name)
                                                               );
        }

        public IEnumerable<TrackBase> TrackGetAllDeepPurple()
        {
            //To-Do 
            return Mapper.Map<IEnumerable<TrackBase>>(ds.Tracks.Where(t => t.Composer.Contains("Jon Lord"))
                                                               .OrderBy(t => t.TrackId)
                                                               );
        }

        public IEnumerable<TrackBase> TrackGetAllTop100Longest()
        {
            //To-Do 
            return Mapper.Map<IEnumerable<TrackBase>>(ds.Tracks.OrderByDescending(t => t.Milliseconds)
                                                               .Take(100)
                                                               );
        }
        //End of Track
    }
}