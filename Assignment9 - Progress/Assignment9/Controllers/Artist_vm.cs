using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment9.Controllers
{
    public class ArtistBase
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(100)]
        [Display(Name = "Artist name or stage name")]
        public string Name { get; set; }

        [StringLength(100)]
        [Display(Name = "If applicable, artist's birth name")]
        public string BirthName { get; set; }

        [Display(Name = "Birth date, or start date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime BirthOrStartDate { get; set; }

        [Required, StringLength(200)]
        [Display(Name = "Artist photo")]
        public string UrlArtist { get; set; }

        [Required, StringLength(100)]
        [Display(Name = "Artist's primary genre")]
        public string Genre { get; set; }

        [Required, StringLength(100)]
        [Display(Name = "Executive who looks after this artist")]
        public string Executive { get; set; }

        [Display(Name = "Artist protrayal")]
        public string Portrayal { get; set; }

        public IEnumerable<AlbumBase> Albums { get; set; }
    }

    public class ArtistWithDetail : ArtistBase
    {

        [Display(Name = "Number of albums")]
        public int AlbumsCount { get; set; }


    }


    public class ArtistAdd
    {
        public ArtistAdd()
        {
            BirthOrStartDate = new DateTime(1996, 3, 10);
        }

        [Key]
        public int Id { get; set; }

        [Required, StringLength(100)]
        [Display(Name = "Artist name or stage name")]
        public string Name { get; set; }

        [StringLength(100)]
        [Display(Name = "If applicable, artist's birth name")]
        public string BirthName { get; set; }

        [Display(Name = "Birth date, or start date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BirthOrStartDate { get; set; }

        [Required, StringLength(100)]
        public string Genre { get; set; }

        [Required, StringLength(200)]
        [Display(Name = "Url to artist photo")]
        public string UrlArtist { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Artist protrayal")]
        public string Portrayal { get; set; }

        [StringLength(100)]
        public string Executive { get; set; }

    }

    public class ArtistAddForm : ArtistAdd
    {
        [Display(Name = "Artist's primary genre")]
        public SelectList GenreList { get; set; }

        
    }
}