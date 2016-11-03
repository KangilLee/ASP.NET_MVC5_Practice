using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment9.Controllers
{
    public class AlbumBase
    {

        [Key]
        public int Id { get; set; }

        [Required, StringLength(100)]
        [Display(Name = "Album name")]
        public string Name { get; set; }

        [Display(Name = "Release date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ReleaseDate { get; set; }

        [Required, StringLength(200)]
        [Display(Name = "Album images (cover art)")]
        public string UrlAlbum { get; set; }

        [Required, StringLength(100)]
        [Display(Name = "Album's primary genre")]
        public string Genre { get; set; }


        [Required, StringLength(100)]
        [Display(Name = "Coordinator who looks after the album")]
        public string Coordinator { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Artist protrayal")]
        public string Depiction { get; set; }

        public IEnumerable<TrackBase> Tracks { get; set; }

        public IEnumerable<ArtistBase> Artists { get; set; }
    }

    public class AlbumWithDetail : AlbumBase
    {
        [Display(Name="Number of tracks on this album")]
        public int TracksCount { get; set; }

        [Display(Name = "Number of artists on this album")]
        public int ArtistsCount { get; set; }

        [Display(Name ="Tracks with this album")]
        public IEnumerable<string> TrackNames { get; set; }

        [Display(Name = "Artists with this album")]
        public IEnumerable<string> ArtistNames { get; set; }


    }

    

    public class AlbumAdd 
    {

        [Key]
        public int Id { get; set; }

        [Required, StringLength(100)]
        [Display(Name = "Album name")]
        public string Name { get; set; }

        [Display(Name = "Release date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ReleaseDate { get; set; }

        [Required, StringLength(200)]
        [Display(Name = "Album images  (cover art)")]
        public string UrlAlbum { get; set; }

        [Required, StringLength(100)]
        [Display(Name = "Album's primary genre")]
        public string Genre { get; set; }


        [StringLength(100)]
        [Display(Name = "Coordinator who looks after the album")]
        public string Coordinator { get; set; }

        public int ArtistId { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Artist protrayal")]
        public string Depiction { get; set; }
    }

    public class AlbumAddForm : AlbumAdd
    {
        public AlbumAddForm()
        {
            ReleaseDate = DateTime.Now;
        }
        public string ArtistName { get; set; }

        public SelectList GenreList { get; set; }
    }
}