using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Tamin.Models
{
    public class Comment
    {


        [Key]
        public int Comment_ID { get; set; }
        public int? User_ID { get; set; }
        public int? PostID { get; set; }
        public int? Parent_ID { get; set; }

        [Display(Name = "تاریخ ارسال")]
        [UIHint("DateTimePicker")]

        public DateTime Comment_Date { get; set; }

        [Display(Name = "وضعیت نمایش کامنت")]
        public bool CommentIsActive { get; set; }

        [StringLength(100)]
        [Display(Name = "نام فرستنده")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        public string Author { get; set; }

        [Display(Name = "عنوان نظر")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        public string Comment_title { get; set; }

        [Display(Name = "متن نظر")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        public string CommentContent { get; set; }

        [StringLength(200)]
        [Display(Name = "آدرس ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "شکل {0} وارد شده صحیح نیست")]
        public string Author_Email { get; set; }
        public string UserLogsData { get; set; }


        public virtual Post posts { get; set; }
        public virtual Comment Parent { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}