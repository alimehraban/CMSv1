using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Tamin.Models
{
    public class language
    {
        [Key]
        public int laug_id { get; set; }
        public int laug_code { get; set; }

        [Display(Name = "عنوان زبان")]
        public string title { get; set; }
        public string image_flag { get; set; }

        [Display(Name = "تاریخ ایجاد ")]
        public DateTime createdat { get; set; }

        [Display(Name = "ایجاد شده توسط")]
        public string createdby { get; set; }

        [Display(Name = "کلمات کلیدی")]
        public string meta_key { get; set; }

        [Display(Name = "توضیحات")]
        public string meta_value { get; set; }


    }
}