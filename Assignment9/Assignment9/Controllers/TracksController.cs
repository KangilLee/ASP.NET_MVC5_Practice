﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment9.Controllers
{
    [Authorize]
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
            var o = m.TrackGetById(id.GetValueOrDefault());

            if (o == null)
            {
                return HttpNotFound();
            }

            return View(o);
        }


        //Get: clip/5
        [Route("clip/{id}")]
        public ActionResult Clip(int? id)
        { 
        
            // Attempt to get the matching object
            var o = m.TrackAudioGetById(id.GetValueOrDefault());

            if (o == null)
            {
                return HttpNotFound();
            }
            else
            {
                // Return a file content result
                // Set the Content-Type header, and return the photo bytes
                return File(o.Audio, o.AudioContentType);
            }
        }
        
    }
}
