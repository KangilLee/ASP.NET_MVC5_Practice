using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment9.Controllers
{
    [Authorize]
    public class AlbumsController : Controller
    {
        private Manager m = new Manager();


        // GET: Albums
        public ActionResult Index()
        {
            return View(m.AlbumGetAll());
        }

        // GET: Albums/Details/5
        public ActionResult Details(int? id)
        {
            var album = m.AlbumGetById(id.GetValueOrDefault());

            if (album == null)
            {
                return HttpNotFound();
            }

            return View(album);
        }

        [Route("albums/{id}/addtrack")]
        [Authorize(Roles = "Executive, Coordinator, Clerk, Staff")]
        public ActionResult AddTrack(int? id)
        {
            var o = m.AlbumGetById(id.GetValueOrDefault());

            if (o == null)
            {
                return HttpNotFound();
            }


            TrackAddForm addForm = new TrackAddForm();
            SelectList genre = new SelectList(m.GenreGetAll(), "Name", "Name");

            addForm.AlbumId = o.Id;
            addForm.AlbumName = o.Name;
            addForm.GenreList = genre;

            return View(addForm);
        }

        [Route("albums/{id}/addtrack")]
        [HttpPost]
        [Authorize(Roles = "Executive, Coordinator, Clerk, Staff")]
        public ActionResult AddTrack(TrackAdd newItem)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(newItem);
                }

                newItem.Clerk = User.Identity.GetUserName();
                

                var addedNewItem = m.TrackAdd(newItem);

                if (addedNewItem == null)
                {
                    return View(newItem);
                }
                else
                {
                    return RedirectToAction("Details", "Tracks", new { id = addedNewItem.Id });
                }
            }
            catch
            {
                return View(newItem);
            }
        }
    }
}
