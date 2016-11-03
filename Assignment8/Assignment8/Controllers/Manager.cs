using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using AutoMapper;
using Assignment8.Models;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace Assignment8.Controllers
{
    public class Manager
    {
        // Reference to the data context
        private ApplicationDbContext ds = new ApplicationDbContext();

        public Manager()
        {
            // If necessary, add constructor code here

            // Turn off the Entity Framework (EF) proxy creation features
            // We do NOT want the EF to track changes - we'll do that ourselves
            ds.Configuration.ProxyCreationEnabled = false;

            // Also, turn off lazy loading...
            // We want to retain control over fetching related objects
            ds.Configuration.LazyLoadingEnabled = false;
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

        

        // Start of methods related to Aritist
        public IEnumerable<ArtistBase> ArtistGetAll()
        {
            return Mapper.Map<IEnumerable<ArtistBase>>(ds.Artists.OrderBy(g => g.Name));
        }

        public ArtistWithDetail ArtistGetById(int id)
        {
            var artist = ds.Artists.Include("Albums").SingleOrDefault(a => a.Id == id);

            return artist == null ? null : Mapper.Map<ArtistWithDetail>(artist);
        }


        public ArtistWithDetail ArtisitAdd(ArtistAdd newItem)
        {
            
            var addedItem = ds.Artists.Add(Mapper.Map<Artist>(newItem));
            ds.SaveChanges();

            return addedItem == null ? null : Mapper.Map<ArtistWithDetail>(addedItem);
            
        }

        // Start of methods related to Album
        public IEnumerable<AlbumBase> AlbumGetAll()
        {
            return Mapper.Map<IEnumerable<AlbumBase>>(ds.Albums.OrderBy(a => a.Name));
        }

        public AlbumWithDetail AlbumGetById(int id)
        {
            var album = ds.Albums.Include("Tracks").Include("Artists").SingleOrDefault(a => a.Id == id);

            if (album == null)
            {
                return null;
            }

            var result = Mapper.Map<AlbumWithDetail>(album);

            result.TrackNames = album.Tracks.Select(a => a.Name);
            result.ArtistNames = album.Artists.Select(a => a.Name);

            return result;

        }

        public AlbumWithDetail AlbumAdd(AlbumAdd newItem)
        { 
            
            // Attempt to add the new item
            var addedItem = ds.Albums.Add(Mapper.Map<Album>(newItem));

            
            var selectedArtists = new List<Artist>();
            var selectedTracks = new List<Track>();

            //How to implement "In" keyword in "Where" clause using LINQ ????

            selectedArtists.Add(ds.Artists.Find(newItem.ArtistId));

            if (newItem.ArtistIds != null)
            {
                //Selected Artist
                foreach (var item in newItem.ArtistIds)
                {
                    var a = ds.Artists.Find(item);
                    selectedArtists.Add(a);
                }
            }
            
            if (newItem.TrackIds != null)
            {
                //Selected tracks
                foreach (var item in newItem.TrackIds)
                {
                    var t = ds.Tracks.Find(item);
                    selectedTracks.Add(t);
                }
            }

            addedItem.Artists = selectedArtists;
            addedItem.Tracks = selectedTracks;

            ds.SaveChanges();

            return Mapper.Map<AlbumWithDetail>(addedItem);


           
        }

        // Start of methods related to Track
        public IEnumerable<TrackBase> TrackGetAll()
        {
            return Mapper.Map<IEnumerable<TrackBase>>(ds.Tracks.OrderBy(t => t.Name));
        }

        public TrackWithDetail TrackGetById(int id)
        {
            var track = ds.Tracks.Include("Albums").SingleOrDefault(a => a.Id == id);

            if (track == null)
            {
                return null;
            }
           
            var result = Mapper.Map<TrackWithDetail>(track);

            result.AlbumNames = track.Albums.Select(a => a.Name);

            return result;
        }


        public TrackWithDetail TrackAdd(TrackAdd newItem)
        {
            var album = ds.Albums.Find(newItem.AlbumId);

            if (album == null)
            {
                return null;
            }

            // Attempt to add the new item
            var addedItem = ds.Tracks.Add(Mapper.Map<Track>(newItem));
            
            addedItem.Albums = new List<Album>() { album };

            ds.SaveChanges();

            return Mapper.Map<TrackWithDetail>(addedItem);

        }

        // Start of methods related to Genre
        public IEnumerable<GenreBase> GenreGetAll()
        {
            return Mapper.Map<IEnumerable<GenreBase>>(ds.Genres.OrderBy(g => g.Name));
        }

        // Attention - 13 - Add some programmatically-generated objects to the data store
        // Can write one method, or many methods - your decision
        // The important idea is that you check for existing data first
        // Call this method from a controller action/method

        public bool LoadData()
        {
            // Return if there's existing data

            //if (ds.Your_Entity_Set.Count() > 0) { return false; }

            // Otherwise...
            // Create and add objects
            // Save changes

            LoadDataGenre();

            LoadDataArtist();

            LoadDataAlbum();

           LoadDataTrack();

            return true;
        }


        public bool LoadDataGenre()
        {

            if (ds.Genres.Count() > 0) return false;

            ds.Genres.Add(new Genre { Name = "Blues" });
            ds.Genres.Add(new Genre { Name = "Classics" });
            ds.Genres.Add(new Genre { Name = "Country" });
            ds.Genres.Add(new Genre { Name = "Indie pop" });
            ds.Genres.Add(new Genre { Name = "Alternative" });

            ds.Genres.Add(new Genre { Name = "Rock" });
            ds.Genres.Add(new Genre { Name = "Anime" });
            ds.Genres.Add(new Genre { Name = "R&B/Soul" });
            ds.Genres.Add(new Genre { Name = "Pop" });
            ds.Genres.Add(new Genre { Name = "Jazz" });


            ds.SaveChanges();
            return true;

        }

        public bool LoadDataArtist()
        {

            try
            {
                if (ds.Artists.Count() > 0) return false;

                ds.Artists.Add(new Artist
                {
                    Name = "Maroon 5",
                    BirthName = "Maroon 5",
                    BirthOrStartDate = new DateTime(1994, 01, 01),
                    Executive = "kangil@example.com",
                    Genre = "Rock",
                    UrlArtist = "https://upload.wikimedia.org/wikipedia/commons/1/19/Maroon_5%2C_2011.jpg"
                });

                ds.Artists.Add(new Artist
                {
                    Name = "Taylor Swift",
                    BirthName = "Taylor Swift",
                    BirthOrStartDate = new DateTime(1989, 12, 13),
                    Executive = "kangil@example.com",
                    Genre = "Pop",
                    UrlArtist = "https://upload.wikimedia.org/wikipedia/commons/b/bd/Taylor_Swift_May_2015_cropped_and_retouched.jpg"
                });

                ds.Artists.Add(new Artist
                {
                    Name = "Norah Jones",
                    BirthName = "Norah Jones",
                    BirthOrStartDate = new DateTime(1979, 3, 30),
                    Executive = "kangil@example.com",
                    Genre = "Jazz",
                    UrlArtist = "https://upload.wikimedia.org/wikipedia/commons/9/97/Norah.jpg"
                });


                ds.SaveChanges();
                return true;
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                return false;
            }
        }

        public bool LoadDataAlbum()
        {

            if (ds.Albums.Count() > 0) return false;

            //Maroon V - V
            var maroonV = ds.Artists.SingleOrDefault(m => m.Name == "Maroon 5");
            if (maroonV == null) { return false; }
            ds.Albums.Add(new Album() {
                Name = "V",
                UrlAlbum = "https://upload.wikimedia.org/wikipedia/en/5/53/Maroon_5_-_V_%28Official_Album_Cover%29.png",
                Genre = "Pop",
                ReleaseDate = new DateTime(2014, 8, 29),
                Cooordinator = "kangil@example.com",
                Artists = new List<Artist>() { maroonV },
                Tracks = new List<Track>()
            });


            //Maroon V - Songs About Janem
            ds.Albums.Add(new Album() {
                Name = "Songs About Jane",
                UrlAlbum = "https://upload.wikimedia.org/wikipedia/en/b/be/Maroon_5_-_Songs_About_Jane.png",
                Genre = "Rock",
                ReleaseDate = new DateTime(2002, 6, 25),
                Cooordinator = "kangil@example.com",
                Artists = new List<Artist>() { maroonV },
                Tracks = new List<Track>()
            });


            //Taylor Swift
            var taylorSwift = ds.Artists.SingleOrDefault(m => m.Name == "Taylor Swift");
            if (taylorSwift == null) { return false; }

            ds.Albums.Add(new Album() {
                Name = "Red",
                UrlAlbum = "https://upload.wikimedia.org/wikipedia/en/e/e8/Taylor_Swift_-_Red.png",
                Genre = "Pop",
                ReleaseDate = new DateTime(2012, 10, 22),
                Cooordinator = "kangil@example.com",
                Artists = new List<Artist>() { taylorSwift },
                Tracks = new List<Track>()
            });

            ds.Albums.Add(new Album() {
                Name = "1989",
                UrlAlbum = "https://upload.wikimedia.org/wikipedia/en/f/f6/Taylor_Swift_-_1989.png",
                Genre = "Pop",
                ReleaseDate = new DateTime(2014, 10, 27),
                Cooordinator = "kangil@example.com",
                Artists = new List<Artist>() { taylorSwift },
                Tracks = new List<Track>()
            });

            //Norah Jones
            var norahJones = ds.Artists.SingleOrDefault(m => m.Name == "Norah Jones");
            if (norahJones == null) { return false; }

            ds.Albums.Add(new Album() {
                Name = "Come Away with Me",
                UrlAlbum = "https://upload.wikimedia.org/wikipedia/en/0/00/Norah_Jones_-_Come_Away_With_Me.jpg",
                Genre = "Jazz",
                ReleaseDate = new DateTime(2002, 2, 1),
                Cooordinator = "kangil@example.com",
                Artists = new List<Artist>() { norahJones },
                Tracks = new List<Track>()
            });
            
            ds.Albums.Add(new Album() {
                Name = "Little Broken Hearts",
                UrlAlbum = "https://upload.wikimedia.org/wikipedia/en/2/21/Norah_Jones_-_Little_Broken_Hearts.png",
                Genre = "Indie pop",
                ReleaseDate = new DateTime(2012, 5, 1),
                Cooordinator = "kangil@example.com",
                Artists = new List<Artist>() { norahJones },
                Tracks = new List<Track>()
            });

            ds.SaveChanges();
            return true;
        }

        public bool LoadDataTrack()
        {
            if (ds.Tracks.Count() > 0) return false;

            //Maroon V - V
            var v = ds.Albums.SingleOrDefault(m => m.Name == "V");
            if (v == null) { return false; }

            ds.Tracks.Add(new Track() {
                Name = "Maps",
                Composers = "Benny Blanco,Teddor",
                Genre = "Pop",
                Clerk = "kangil@example.com",
                Albums = { v }
            });

            ds.Tracks.Add(new Track()
            {
                Name = "Animals",
                Composers = "Shellback",
                Genre = "Pop",
                Clerk = "kangil@example.com",
                Albums = { v }
                
            });

            ds.Tracks.Add(new Track()
            {
                Name = "It Was Alaways you",
                Composers = "The Monsters and Strangers",
                Genre = "Pop",
                Clerk = "kangil@example.com",
                Albums = { v }
            });

            ds.Tracks.Add(new Track()
            {
                Name = "Feelings",
                Composers = "Shellback,OzGo",
                Genre = "Pop",
                Clerk = "kangil@example.com",
                Albums = { v }
            });

            ds.Tracks.Add(new Track()
            {
                Name = "Sugar",
                Composers = "Ammo, Cirkut",
                Genre = "Pop",
                Clerk = "kangil@example.com",
                Albums = { v }
            });


            //Taylor Swift - rd
            var red = ds.Albums.SingleOrDefault(m => m.Name == "Red");
            if (red == null) { return false; }

            ds.Tracks.Add(new Track()
            {
                Name = "State of Grace",
                Composers = "Swift",
                Genre = "Pop",
                Clerk = "kangil@example.com",
                Albums = { red }
            });

            ds.Tracks.Add(new Track()
            {
                Name = "Red",
                Composers = "Swift",
                Genre = "Pop",
                Clerk = "kangil@example.com",
                Albums = { red }
            });

            ds.Tracks.Add(new Track()
            {
                Name = "I knew you were trobule",
                Composers = "Shellback",
                Genre = "Pop",
                Clerk = "kangil@example.com",
                Albums = { red }
            });

            ds.Tracks.Add(new Track()
            {
                Name = "22",
                Composers = "Martin,Shellback",
                Genre = "Pop",
                Clerk = "kangil@example.com",
                Albums = { red }
            });

            ds.Tracks.Add(new Track()
            {
                Name = "Sugar",
                Composers = "Ammo, Cirkut",
                Genre = "Pop",
                Clerk = "kangil@example.com",
                Albums = { red }
            });

            //Norah Jones - Little Broken Hearts
            var littleBroken = ds.Albums.SingleOrDefault(m => m.Name == "Little Broken Hearts");
            if (littleBroken == null) { return false; }

            ds.Tracks.Add(new Track()
            {
                Name = "Good Morning",
                Composers = "Norah Jones,Brian Burton",
                Genre = "Pop",
                Clerk = "kangil@example.com",
                Albums = { littleBroken }
            });

            ds.Tracks.Add(new Track()
            {
                Name = "Say Goodbye",
                Composers = "Norah Jones,Brian Burton",
                Genre = "Pop",
                Clerk = "kangil@example.com",
                Albums = { littleBroken }
            });

            ds.Tracks.Add(new Track()
            {
                Name = "Little Broken Hearts",
                Composers = "Norah Jones,Brian Burton",
                Genre = "Pop",
                Clerk = "kangil@example.com",
                Albums = { littleBroken }
            });

            ds.Tracks.Add(new Track()
            {
                Name = "She's 22",
                Composers = "Norah Jones,Brian Burton",
                Genre = "Pop",
                Clerk = "kangil@example.com",
                Albums = { littleBroken }
            });

            ds.Tracks.Add(new Track()
            {
                Name = "Take It Back",
                Composers = "Norah Jones,Brian Burton",
                Genre = "Pop",
                Clerk = "kangil@example.com",
                Albums = { littleBroken }
            });

            ds.SaveChanges();
            return true;

        }

        public bool RemoveDatabase()
        {
            try
            {
                // Delete database
                return ds.Database.Delete();
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}