using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assignment9.Controllers
{
    public class MediaItemBase
    {
        [Key]
        public int Id { get; set; }

        public string StringId { get; set; }

        public string Caption { get; set; }


        public string ContentType { get; set; }

        public DateTime Timestamp { get; set; }


        public ArtistBase artist { get; set; }
    }

    public class MediaItemContent
    {
        public int Id { get; set; }
        public byte[] Content { get; set; }

        public string ContentType { get; set; }
    }

    public class MediaItemAddForm
    {
        public int ArtistId { get; set; }

        public string ArtistName { get; set; }

        [Display(Name ="Descriptive caption")]
        public string Caption { get; set; }


        [Required]
        [Display(Name = "Media Item")]
        [DataType(DataType.Upload)]
        public string MediaUpload { get; set; }

    }

    public class MediaItemAdd
    {
        
        public string Caption { get; set; }

        public HttpPostedFileBase MediaUpload { get; set; }

        public int ArtistId { get; set; }
    }
}