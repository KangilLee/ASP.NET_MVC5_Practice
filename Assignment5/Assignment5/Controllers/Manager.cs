using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using AutoMapper;
using Assignment5.Models;

namespace Assignment5.Controllers
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

        public IEnumerable<TrackWithDetail> TrackGetAll()
        {
            return Mapper.Map<IEnumerable<TrackWithDetail>>(ds.Tracks.Include("Album.Artist").Include("MediaType").OrderBy(p=>p.Album.Title));
        }

        public TrackWithDetail TrackGetById(int id)
        {
            var t = ds.Tracks.Include("Album.Artist").Include("MediaType").FirstOrDefault(p => p.TrackId == id);

            return t == null ? null : Mapper.Map<TrackWithDetail>(t);
        }

        public TrackWithDetail TrackAdd(TrackAdd newItem)
        {
            
            // Attempt to find the associated object
            var album = ds.Albums.Find(newItem.AlbumId);
            var mediaType = ds.MediaTypes.Find(newItem.MediaTypeId);

            if (album == null && mediaType == null)
            {
                return null;
            }


            // Attempt to add the new item
            var addedItem = ds.Tracks.Add(Mapper.Map<Track>(newItem));
            // Set the associated item property
            addedItem.Album = album;
            addedItem.MediaType = mediaType;
            ds.SaveChanges();

            return addedItem == null ? null : Mapper.Map<TrackWithDetail>(addedItem);


        }

        public IEnumerable<AlbumBase> AlbumGetAll()
        {
            var a = ds.Albums.OrderBy(p => p.Title);
            
            return a == null ? null : Mapper.Map<IEnumerable<AlbumBase>>(a); ;
        }

        public AlbumBase AlbumGetById(int id)
        {
            var a = ds.Albums.Find(id);

            return a == null ? null : Mapper.Map<AlbumBase>(a);
        }

        public IEnumerable<MediaTypeBase> MediaTypeGetAll()
        {
            var a = ds.MediaTypes.OrderBy(p => p.Name);

            return a == null ? null : Mapper.Map<IEnumerable<MediaTypeBase>>(a); ;
        }

        public MediaTypeBase MediaTypeGetbyId(int id)
        {
            var m = ds.MediaTypes.Find(id);

            return m == null ? null : Mapper.Map<MediaTypeBase>(m); ;
        }
    }
}