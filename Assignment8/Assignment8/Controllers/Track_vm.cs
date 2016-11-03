using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment8.Controllers
{
    public class TrackBase
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(100)]
        [Display(Name = "Track name")]
        public string Name { get; set; }

        

        [Required, StringLength(100)]
        [Display(Name = "Composer name(s)")]
        public string Composers { get; set; }

        [Required, StringLength(100)]
        [Display(Name = "Track genre")]
        public string Genre { get; set; }

        [Required, StringLength(100)]
        [Display(Name = "Clerk who helps with")]
        public string Clerk { get; set; }

        public IEnumerable<AlbumBase> Albums { get; set; }
    }

    public class TrackWithDetail : TrackBase
    {
        [Display(Name = "Numer of albumns with this track")]
        public int AlbumsCount { get; set; }

        [Display(Name = "Album with this track")]
        public IEnumerable<string> AlbumNames { get; set; }
    }

    public class TrackAdd
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(100)]
        [Display(Name = "Track name")]
        public string Name { get; set; }

        [Required, StringLength(100)]
        [Display(Name = "Composer names (comma-separated)")]
        public string Composers { get; set; }

        [Required, StringLength(100)]
        public string Genre { get; set; }

        [StringLength(100)]
        public string Clerk { get; set; }

        [Required]
        public int AlbumId { get; set; }

        public IEnumerable<AlbumBase> Albums { get; set; }
    }

    public class TrackAddForm : TrackAdd
    {
        public string AlbumName { get; set; }

        [Display(Name = "Track genre")]
        public SelectList GenreList { get; set; }

    }
}