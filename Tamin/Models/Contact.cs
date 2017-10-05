using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Tamin.Models
{
    public class Contact
    {
        [Key]
        public int ContactId { get; set; }
        [Display(Name = "نام :")]
        //[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Name { get; set; }
        [Display(Name = "نام خانوادگی :")]
        //[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Family { get; set; }
        [Display(Name = " تلفن تماس :")]
        //[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Tell { get; set; }
        [Display(Name = "ایمیل :")]
        //[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Email { get; set; }
        [Display(Name = "موضوع :")]
        //[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Subject { get; set; }
        [Display(Name = "توضیحات :")]
        //[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string TestContact { get; set; }

    }
}