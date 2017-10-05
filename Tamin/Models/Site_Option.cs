using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Tamin.Models
{
    public class Site_Option
    {
        [Key]
        public int id { get; set; }

        [Display(Name = "کلمات کلیدی")]
        public string key { get; set; }

        [Display(Name = "توضیحات")]
        public string value { get; set; }

    }
}