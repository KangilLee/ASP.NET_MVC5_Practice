using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using AutoMapper;
using Assignment4.Models;

namespace Assignment4.Controllers
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

        public IEnumerable<InvoiceBase> InvoiceGetAll()
        {
            return Mapper.Map<IEnumerable<InvoiceBase>>(ds.Invoices.OrderByDescending(i => i.InvoiceDate));
        }

        public InvoiceBase InvoiceGetById(int id)
        {
            var o = ds.Invoices.Find(id);

            return o == null ? null : Mapper.Map<InvoiceBase>(o);
        }


        public InvoiceWithDetail InvoiceGetByIdWithDetail(int id)
        {

            var o = ds.Invoices.Include("Customer.Employee")
                               .Include("InvoiceLines.Track.MediaType")
                               .Include("InvoiceLines.Track.Album.Artist")
                               .SingleOrDefault(i => i.InvoiceId == id);

            return o == null ? null : Mapper.Map<InvoiceWithDetail>(o);
        }
    }
}