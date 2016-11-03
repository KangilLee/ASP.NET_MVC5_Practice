using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using AutoMapper;
using Assignment9.Models;
using System.Security.Claims;
using Microsoft.AspNet.Identity;

namespace Assignment9.Controllers
{
    public class Manager
    {
        // Reference to the data context
        private ApplicationDbContext ds = new ApplicationDbContext();

        // Declare a property to hold the user account for the current request
        // Can use this property here in the Manager class to control logic and flow
        // Can also use this property in a controller 
        // Can also use this property in a view; for best results, 
        // near the top of the view, add this statement:
        // var userAccount = new ConditionalMenu.Controllers.UserAccount(User as System.Security.Claims.ClaimsPrincipal);
        // Then, you can use "userAccount" anywhere in the view to render content
        public UserAccount UserAccount { get; private set; }

        public Manager()
        {
            // If necessary, add constructor code here

            // Initialize the UserAccount property
            UserAccount = new UserAccount(HttpContext.Current.User as ClaimsPrincipal);

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


        public ArtistWithMediaInfo ArtistGetByIdWithMediaItem(int id)
        {
            var artist = ds.Artists.Include("MediaItems").SingleOrDefault(a => a.Id == id);

            return artist == null ? null : Mapper.Map<ArtistWithMediaInfo>(artist);
        }

        public MediaItemContent ArtistMediaItemsGetById(string stringId)
        {
            var o = ds.MediaItems.SingleOrDefault(p => p.StringId == stringId);

            return (o == null) ? null : Mapper.Map<MediaItemContent>(o);
        }

        public ArtistWithDetail ArtisitAdd(ArtistAdd newItem)
        {

            var addedItem = ds.Artists.Add(Mapper.Map<Artist>(newItem));
            ds.SaveChanges();

            return addedItem == null ? null : Mapper.Map<ArtistWithDetail>(addedItem);

        }



        public ArtistWithDetail MediaItemAdd (MediaItemAdd newItem)
        {
            var artist = ds.Artists.Find(newItem.ArtistId);

            if (artist == null)
            {
                return null;
            }

            // Attempt to add the new item
            var o = Mapper.Map<MediaItem>(newItem);

            var addedItem = ds.MediaItems.Add(o);

            byte[] mediaBytes = new byte[newItem.MediaUpload.ContentLength];
            newItem.MediaUpload.InputStream.Read(mediaBytes, 0, newItem.MediaUpload.ContentLength);

            // Then, configure the new object's properties
            addedItem.Content = mediaBytes;
            addedItem.ContentType = newItem.MediaUpload.ContentType;

            addedItem.artist = artist;

            ds.SaveChanges();

            return Mapper.Map<ArtistWithDetail>(addedItem.artist);
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

            addedItem.Artists.Add(ds.Artists.Find(newItem.ArtistId));

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
            
            return Mapper.Map<TrackWithDetail>(track);
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

            byte[] audioBytes = new byte[newItem.AudioUpload.ContentLength];
            newItem.AudioUpload.InputStream.Read(audioBytes, 0, newItem.AudioUpload.ContentLength);

            // Then, configure the new object's properties
            addedItem.Audio = audioBytes;
            addedItem.AudioContentType = newItem.AudioUpload.ContentType;

            addedItem.Albums = new List<Album>() { album };

            ds.SaveChanges();

            return Mapper.Map<TrackWithDetail>(addedItem);

        }

        public TrackAudio TrackAudioGetById (int id)
        {
            var o = ds.Tracks.Find(id);

            return o == null ? null : Mapper.Map<TrackAudio>(o);

        }



        // Start of methods related to Genre
        public IEnumerable<GenreBase> GenreGetAll()
        {
            return Mapper.Map<IEnumerable<GenreBase>>(ds.Genres.OrderBy(g => g.Name));
        }

        public IEnumerable<String> RoleClaimGetAllStrings()
        { 
        
            return ds.RoleClaims.OrderBy(r => r.Name).Select(r => r.Name);

        }





        // Add some programmatically-generated objects to the data store
        // Can write one method, or many methods - your decision
        // The important idea is that you check for existing data first
        // Call this method from a controller action/method

        public bool LoadData()
        {
            // User name
            var user = HttpContext.Current.User.Identity.Name;

            // Monitor the progress
            bool done = false;

            // ############################################################
            // Genre

            if (ds.Genres.Count() == 0)
            {
                // Add genres

                ds.Genres.Add(new Genre { Name = "Alternative" });
                ds.Genres.Add(new Genre { Name = "Classical" });
                ds.Genres.Add(new Genre { Name = "Country" });
                ds.Genres.Add(new Genre { Name = "Easy Listening" });
                ds.Genres.Add(new Genre { Name = "Hip-Hop/Rap" });
                ds.Genres.Add(new Genre { Name = "Jazz" });
                ds.Genres.Add(new Genre { Name = "Pop" });
                ds.Genres.Add(new Genre { Name = "R&B" });
                ds.Genres.Add(new Genre { Name = "Rock" });
                ds.Genres.Add(new Genre { Name = "Soundtrack" });

                ds.SaveChanges();
                done = true;
            }

            // ############################################################
            // Artist

            if (ds.Artists.Count() == 0)
            {
                // Add artists

                ds.Artists.Add(new Artist
                {
                    Name = "The Beatles",
                    BirthOrStartDate = new DateTime(1962, 8, 15),
                    Executive = user,
                    Genre = "Pop",
                    UrlArtist = "https://upload.wikimedia.org/wikipedia/commons/9/9f/Beatles_ad_1965_just_the_beatles_crop.jpg"
                });

                ds.Artists.Add(new Artist
                {
                    Name = "Adele",
                    BirthName = "Adele Adkins",
                    BirthOrStartDate = new DateTime(1988, 5, 5),
                    Executive = user,
                    Genre = "Pop",
                    UrlArtist = "http://www.billboard.com/files/styles/article_main_image/public/media/Adele-2015-close-up-XL_Columbia-billboard-650.jpg"
                });

                ds.Artists.Add(new Artist
                {
                    Name = "Bryan Adams",
                    BirthOrStartDate = new DateTime(1959, 11, 5),
                    Executive = user,
                    Genre = "Rock",
                    UrlArtist = "https://upload.wikimedia.org/wikipedia/commons/7/7e/Bryan_Adams_Hamburg_MG_0631_flickr.jpg"
                });

                ds.SaveChanges();
                done = true;
            }

            // ############################################################
            // Album

            if (ds.Albums.Count() == 0)
            {
                // Add albums

                // For Bryan Adams
                var bryan = ds.Artists.SingleOrDefault(a => a.Name == "Bryan Adams");

                ds.Albums.Add(new Album
                {
                    Artists = new List<Artist> { bryan },
                    Name = "Reckless",
                    ReleaseDate = new DateTime(1984, 11, 5),
                    Coordinator = user,
                    Genre = "Rock",
                    UrlAlbum = "https://upload.wikimedia.org/wikipedia/en/5/56/Bryan_Adams_-_Reckless.jpg"
                });

                ds.Albums.Add(new Album
                {
                    Artists = new List<Artist> { bryan },
                    Name = "So Far So Good",
                    ReleaseDate = new DateTime(1993, 11, 2),
                    Coordinator = user,
                    Genre = "Rock",
                    UrlAlbum = "https://upload.wikimedia.org/wikipedia/pt/a/ab/So_Far_so_Good_capa.jpg"
                });

                ds.SaveChanges();
                done = true;
            }

            // ############################################################
            // Track

            if (ds.Tracks.Count() == 0)
            {
                // Add tracks

                // For Reckless
                var reck = ds.Albums.SingleOrDefault(a => a.Name == "Reckless");

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { reck },
                    Name = "Run To You",
                    Composers = "Bryan Adams, Jim Vallance",
                    Clerk = user,
                    Genre = "Rock"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { reck },
                    Name = "Heaven",
                    Composers = "Bryan Adams, Jim Vallance",
                    Clerk = user,
                    Genre = "Rock"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { reck },
                    Name = "Somebody",
                    Composers = "Bryan Adams, Jim Vallance",
                    Clerk = user,
                    Genre = "Rock"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { reck },
                    Name = "Summer of '69",
                    Composers = "Bryan Adams, Jim Vallance",
                    Clerk = user,
                    Genre = "Rock"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { reck },
                    Name = "Kids Wanna Rock",
                    Composers = "Bryan Adams, Jim Vallance",
                    Clerk = user,
                    Genre = "Rock"
                });

                // For Reckless
                var so = ds.Albums.SingleOrDefault(a => a.Name == "So Far So Good");

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { so },
                    Name = "Straight from the Heart",
                    Composers = "Bryan Adams, Eric Kagna",
                    Clerk = user,
                    Genre = "Rock"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { so },
                    Name = "It's Only Love",
                    Composers = "Bryan Adams, Jim Vallance",
                    Clerk = user,
                    Genre = "Rock"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { so },
                    Name = "This Time",
                    Composers = "Bryan Adams, Jim Vallance",
                    Clerk = user,
                    Genre = "Rock"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { so },
                    Name = "(Everything I Do) I Do It for You",
                    Composers = "Bryan Adams, Jim Vallance",
                    Clerk = user,
                    Genre = "Rock"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { so },
                    Name = "Heat of the Night",
                    Composers = "Bryan Adams, Jim Vallance",
                    Clerk = user,
                    Genre = "Rock"
                });

                ds.SaveChanges();
                done = true;
            }


            if (ds.RoleClaims.Count() == 0)
            {
                ds.RoleClaims.Add(new RoleClaim() { Name = "Executive" });

                ds.RoleClaims.Add(new RoleClaim() { Name = "Coordinator" });

                ds.RoleClaims.Add(new RoleClaim() { Name = "Clerk" });

                ds.RoleClaims.Add(new RoleClaim() { Name = "Staff" });

                ds.RoleClaims.Add(new RoleClaim() { Name = "Financial" });

                ds.RoleClaims.Add(new RoleClaim() { Name = "Marketing" });

                ds.SaveChanges();
            }

            return done;
        }

        public bool RemoveData()
        {
            try
            {
                foreach (var e in ds.Tracks)
                {
                    ds.Entry(e).State = System.Data.Entity.EntityState.Deleted;
                }
                ds.SaveChanges();

                foreach (var e in ds.Albums)
                {
                    ds.Entry(e).State = System.Data.Entity.EntityState.Deleted;
                }
                ds.SaveChanges();

                foreach (var e in ds.Artists)
                {
                    ds.Entry(e).State = System.Data.Entity.EntityState.Deleted;
                }
                ds.SaveChanges();

                foreach (var e in ds.Genres)
                {
                    ds.Entry(e).State = System.Data.Entity.EntityState.Deleted;
                }
                ds.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool RemoveDatabase()
        {
            try
            {
                return ds.Database.Delete();
            }
            catch (Exception)
            {
                return false;
            }
        }

    }

    // New "UserAccount" class for the authenticated user
    // Includes many convenient members to make it easier to render user account info
    // Study the properties and methods, and think about how you could use it
    public class UserAccount
    {
        // Constructor, pass in the security principal
        public UserAccount(ClaimsPrincipal user)
        {
            if (HttpContext.Current.Request.IsAuthenticated)
            {
                Principal = user;

                // Extract the role claims
                RoleClaims = user.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);

                // User name
                Name = user.Identity.Name;

                // Extract the given name(s); if null or empty, then set an initial value
                string gn = user.Claims.SingleOrDefault(c => c.Type == ClaimTypes.GivenName).Value;
                if (string.IsNullOrEmpty(gn)) { gn = "(empty given name)"; }
                GivenName = gn;

                // Extract the surname; if null or empty, then set an initial value
                string sn = user.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Surname).Value;
                if (string.IsNullOrEmpty(sn)) { sn = "(empty surname)"; }
                Surname = sn;

                IsAuthenticated = true;
                IsAdmin = user.HasClaim(ClaimTypes.Role, "Admin") ? true : false;
                IsExecutive = user.HasClaim(ClaimTypes.Role, "Executive") ? true : false;
                IsCoordinator= user.HasClaim(ClaimTypes.Role, "Coordinator") ? true : false;
                IsClerk = user.HasClaim(ClaimTypes.Role, "Clerk") ? true : false;
                IsStaff = user.HasClaim(ClaimTypes.Role, "Staff") ? true : false;
                IsFinancial = user.HasClaim(ClaimTypes.Role, "Financial") ? true : false;
                IsMarketing = user.HasClaim(ClaimTypes.Role, "Marketing") ? true : false;
            }
            else
            {
                RoleClaims = new List<string>();
                Name = "anonymous";
                GivenName = "Unauthenticated";
                Surname = "Anonymous";
                IsAuthenticated = false;
                IsAdmin = false;
            }

            NamesFirstLast = $"{GivenName} {Surname}";
            NamesLastFirst = $"{Surname}, {GivenName}";
        }

        // Public properties
        public ClaimsPrincipal Principal { get; private set; }
        public IEnumerable<string> RoleClaims { get; private set; }

        public string Name { get; set; }

        public string GivenName { get; private set; }
        public string Surname { get; private set; }

        public string NamesFirstLast { get; private set; }
        public string NamesLastFirst { get; private set; }

        public bool IsAuthenticated { get; private set; }

        // Add other role-checking properties here as needed
        public bool IsAdmin { get; private set; }

        public bool IsExecutive { get; private set; }

        public bool IsCoordinator { get; private set; }

        public bool IsClerk { get; private set; }

        public bool IsStaff { get; private set; }

        public bool IsFinancial { get; private set; }

        public bool IsMarketing { get; private set; }


        public bool HasRoleClaim(string value)
        {
            if (!IsAuthenticated) { return false; }
            return Principal.HasClaim(ClaimTypes.Role, value) ? true : false;
        }

        public bool HasClaim(string type, string value)
        {
            if (!IsAuthenticated) { return false; }
            return Principal.HasClaim(type, value) ? true : false;
        }
    }

}