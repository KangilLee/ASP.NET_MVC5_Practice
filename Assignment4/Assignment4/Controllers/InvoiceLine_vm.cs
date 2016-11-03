using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assignment4.Controllers
{
    public class InvoiceLineBase
    {
        [Key]
        [Display(Name = "Invoice line ID")]
        public int InvoiceLineId { get; set; }

        [Display(Name = "Invoice ID")]
        public int InvoiceId { get; set; }

        [Display(Name = "Track ID")]
        public int TrackId { get; set; }

        [Display(Name = "Unit price")]
        public decimal UnitPrice { get; set; }

        public int Quantity { get; set; }

        [Display(Name = "Item total")]
        public decimal ItemTotal
        {
            get { return UnitPrice * Quantity; }
        }
    }

    public class InvoiceLineWithDetail : InvoiceLineBase
    {
        [Display(Name = "Track name")]
        public string TrackName { get; set; }

        [Display(Name = "Composer(s)")]
        public string TrackComposer { get; set; }

        [Display(Name = "Album")]
        public string TrackAlbumTitle { get; set; }

        [Display(Name = "artist")]
        public string TrackAlbumArtistName { get; set; }

        [Display(Name = "Format")]
        public string TrackMediaTypeName { get; set; }
    }
}