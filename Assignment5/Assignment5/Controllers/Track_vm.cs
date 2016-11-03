using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment5.Controllers
{
    public class TrackBase
    {
        [Key]
        public int TrackId { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name="Track name")]
        public string Name { get; set; }

        [StringLength(220)]
        public string Composer { get; set; }

        [Display(Name = "Length (ms)")]
        public int Milliseconds { get; set; }

        [Display(Name = "Unit Price")]
        public decimal UnitPrice { get; set; }

    }

    public class TrackWithDetail : TrackBase
    {

        [Display(Name = "Album title")]
        public string AlbumTitle { get; set; }

        [Display(Name = "Artist name")]
        public string AlbumArtistName { get; set; }

        public MediaTypeBase MediaType { get; set; }
    }

    public class TrackAdd
    {
        [Key]
        public int TrackId { get; set; }

        [Required, StringLength(200)]
        [Display(Name = "Track name")]
        public string Name { get; set; }

        [StringLength(220)]
        public string Composer { get; set; }

        [Display(Name = "Length (ms)")]
        public int Milliseconds { get; set; }

        [Display(Name = "Unit Price")]
        public decimal UnitPrice { get; set; }

        [Required]
        [Display(Name = "Album")]
        public int? AlbumId { get; set; }

        [Required]
        [Display(Name = "Media type")]
        public int? MediaTypeId { get; set; }

    }


    public class TrackAddForm : TrackAdd
    {

        [Required, Display(Name = "Album")]
        public SelectList AlbumList { get; set; }

        [Required, Display(Name = "Media Type")]
        public SelectList MediaTypeList { get; set; }


    }
}