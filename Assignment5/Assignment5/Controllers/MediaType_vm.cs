﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assignment5.Controllers
{
    public class MediaTypeBase
    {
        [Key]
        public int MediaTypeId { get; set; }

        [StringLength(120)]
        [Display(Name= "Media type" )]
        public string Name { get; set; }
    }
}