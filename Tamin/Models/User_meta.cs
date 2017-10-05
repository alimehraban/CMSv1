using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Tamin.Models
{
    public class User_meta
    {
        [Key]
        public int meta_id { get; set; }
        public Nullable<int> user_ID { get; set; }

        [Display(Name = "کلمات کلیدی")]
        public string meta_key { get; set; }

        [Display(Name = "توضیحات")]
        public string meta_value { get; set; }

        //public virtual ApplicationUser ApplicationUser { get; set; }

    }
}