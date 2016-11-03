using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Assignment8.Controllers
{
    [Authorize]
    public class ArtistsController : Controller
    {
        private Manager m = new Manager();

        // GET: Artists
        public ActionResult Index()
        {
            return View(m.ArtistGetAll());
        }

        // GET: Artists/Details/5
        public ActionResult Details(int? id)
        {
            var o = m.ArtistGetById(id.GetValueOrDefault());

            if (o == null)
            {
                return HttpNotFound();
            }

            return View(o);
        }

        // GET: Artists/Create
        [Authorize(Roles = "Executive")]
        public ActionResult Create()
        {

            ArtistAddForm addform = new ArtistAddForm();
            SelectList genres = new SelectList(m.GenreGetAll(), "Name", "Name");

            addform.GenreList = genres;
            
            return View(addform);
        }

        // POST: Artists/Create
        [HttpPost]
        [Authorize(Roles = "Executive")]
        public ActionResult Create(ArtistAdd newItem)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(newItem);
                }

                newItem.Executive = User.Identity.GetUserName();

                var addedNewItem = m.ArtisitAdd(newItem);

                if (addedNewItem == null)
                {
                    return RedirectToAction("Index");
                } else
                {
                    return RedirectToAction("Details", new { id = addedNewItem.Id });
                }
            }
            catch
            {
                return View(newItem);
            }
        }


        [Route("artists/{id}/addalbum")]
        [Authorize(Roles = "Coordinator")]
        public ActionResult AddAlbum(int? id)
        {
            var o = m.ArtistGetById(id.GetValueOrDefault());

            if (o == null)
            {
                return HttpNotFound();
            }

            AlbumAddForm addForm = new AlbumAddForm();
            
            addForm.ArtistId = o.Id;
            addForm.ArtistName = o.Name;
            
            SelectList genre = new SelectList(m.GenreGetAll(), "Name", "Name");
            MultiSelectList artists = new MultiSelectList(m.ArtistGetAll(), "Id", "Name", new List<int> { o.Id });
            MultiSelectList tracks = new MultiSelectList(m.TrackGetAll(), "Id", "Name");

            addForm.GenreList = genre;
            addForm.ArtistsList = artists;
            addForm.TracksList = tracks;
            
            return View(addForm);
        }

        [Route("artists/{id}/addalbum")]
        [Authorize(Roles = "Coordinator")]
        [HttpPost]
        public ActionResult AddAlbum(AlbumAdd newItem)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(newItem);
                }

                newItem.Cooordinator = User.Identity.GetUserName();

                var addedNewItem = m.AlbumAdd(newItem);

                if (addedNewItem == null)
                {
                    return View(newItem);
                }
                else
                {
                    return RedirectToAction("Details", "Albums", new { id = addedNewItem.Id });
                }
            }
            catch
            {
                return View(newItem);
            }
        }
    }
}
