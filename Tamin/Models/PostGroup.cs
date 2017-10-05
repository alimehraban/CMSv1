using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Tamin.Models
{

        public class PostGroup
        {
            public PostGroup()
            {
                this.Posts = new HashSet<Post>();
            }
            [Key]
            public int PostGroupID { get; set; }

            [Display(Name = "والد گروه خبر")]
            public int? ParentId { get; set; }

            [StringLength(100)]
            [Display(Name = "گروه خبر")]
            [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
            public string PostGroupTitle { get; set; }


            [StringLength(150)]
            [Display(Name = "تصویر گروه کالا")]
            [UIHint("Upload")]
            public string ImageUrl { get; set; }

            //Navigation Properties
            public virtual ICollection<Post> Posts { get; set; }
            public virtual PostGroup Parent { get; set; }



        }
}