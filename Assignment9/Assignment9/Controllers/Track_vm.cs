using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment9.Controllers
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

        [Display(Name = "Sample clip")]
        public string AudioUpload { get; set; }

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

        public HttpPostedFileBase AudioUpload { get; set; }



        public IEnumerable<AlbumBase> Albums { get; set; }
    }

    public class TrackAddForm
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(100)]
        [Display(Name = "Track name")]
        public string Name { get; set; }

        [Required, StringLength(100)]
        [Display(Name = "Composer names (comma-separated)")]
        public string Composers { get; set; }

        [Required]
        public int AlbumId { get; set; }

        public string AlbumName { get; set; }

        [Required]
        [Display(Name = "Sample clip")]
        [DataType(DataType.Upload)]
        public string AudioUpload { get; set; }

        [Display(Name = "Track genre")]
        public SelectList GenreList { get; set; }



    }
}