using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assignment9.Controllers
{
    public class TrackAudio
    {
        [Key]
        public int Id { get; set; }

        public byte[] Audio { get; set; }

        public string AudioContentType { get; set; }


    }
}