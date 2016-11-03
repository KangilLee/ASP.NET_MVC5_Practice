using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment5.Controllers
{
    public class TracksController : Controller
    {
        private Manager m = new Manager();

        // GET: Tracks
        public ActionResult Index()
        {
            return View(m.TrackGetAll());
        }

        // GET: Tracks/Details/5
        public ActionResult Details(int? id)
        {
            var item = m.TrackGetById(id.GetValueOrDefault());

            if (item == null)
            {
                return HttpNotFound();
            }

            return View(item);
        }

        // GET: Tracks/Create
        public ActionResult Create()
        {

            var albumList = m.AlbumGetAll();
            var mediaTypeList = m.MediaTypeGetAll();

            var trackAddForm = new TrackAddForm();

            trackAddForm.AlbumList = new SelectList(albumList, "AlbumId","Title");
            trackAddForm.MediaTypeList = new SelectList(mediaTypeList, "MediaTypeId", "Name" );


            return View(trackAddForm);
        }

        // POST: Tracks/Create
        [HttpPost]
        public ActionResult Create(TrackAdd newItem)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(newItem);

                }

                //In this time, the data of Employee is added
                var addedItem = m.TrackAdd(newItem);

                if (addedItem == null)
                {
                    return View(newItem);
                }
                else
                {
                    return RedirectToAction("Details", new { id = addedItem.TrackId});
                }


            }
            catch
            {
                return View(newItem);
            }
        }
    }
}
