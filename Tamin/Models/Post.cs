using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Tamin.Models
{
    public class Post
    {
        public Post()
        {
            this.Comments = new HashSet<Comment>();

        }
        [Key]
        public int PostID { get; set; }


        [Display(Name = "گروه خبر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]

        public int PostGroupID { get; set; }
        public string ApplicationUserId { get; set; }
        [StringLength(150)]
        [Display(Name = "عنوان خبر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string PostTitle { get; set; }

        [Display(Name = "خلاصه خبر")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [UIHint("Html")]
        [AllowHtml]
        public string Summary { get; set; }

        [Display(Name = "متن خبر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [UIHint("Html")]
        [AllowHtml]
        public string PostText { get; set; }

        [Display(Name = "وضعیت نمایش خبر")]
        public bool PostIsActive { get; set; }

        public DateTime PostDate { get; set; }


        [Display(Name = "تاریخ ویرایش خبر")]
        public DateTime? Modifiedat { get; set; }

        [Display(Name = "ویرایش توسط")]
        public string Modifiedby { get; set; }
        [Display(Name = "وضعیت خبر")]
        public bool Is_delete { get; set; }


        [Display(Name = "دارای اولویت")]
        public bool Priority { get; set; }


        [Display(Name = "زمان شروع نمایش خبر")]
        [UIHint("DateTimePicker2")]
        public DateTime? Startdate { get; set; }


        [Display(Name = "پایان زمان نمایش خبر")]
        [UIHint("DateTimePicker3")]
        public DateTime? Enddate { get; set; }



        [Display(Name = "تعداد نظرات")]
        public int CommentCounter { get; set; }
        [Display(Name = "وضعیت نمایش نظرات")]
        public bool Comment_status { get; set; }
        
        [Display(Name = "تعداد بازدید صفحه")]
        public int PageCounter { get; set; }



        [StringLength(150)]
        [Display(Name = "تصویر خبر")]
        [UIHint("Upload")]
        public string ImageUrl { get; set; }

        [StringLength(150)]
        [Display(Name = "تصویر کوچک خبر")]
        public string PostThumbnailImageUrl { get; set; }

        //Navigation Properties
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual PostGroup PostGroups { get; set; }
    }
}