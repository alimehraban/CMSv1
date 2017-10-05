using System;
using System.Linq;
using System.Web.Mvc;
using Tamin.Models;
using CaptchaMvc.HtmlHelpers;

namespace Tamin.Controllers
{
    public class CommentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [HttpGet]
        public ActionResult InsertComment(int? id)
        {
            ViewBag.postId = id;
            return PartialView("_InsertComment");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InsertComment(Comment comment)
        {
            if (this.IsCaptchaValid("کد وارد شده صحیح نمی باشد"))
            {
                if (ModelState.IsValid)
                {
                    comment.Comment_Date = DateTime.Now;
                    db.Comments.Add(comment);
                    db.SaveChanges();
                    TempData["info"] = "پیام شما با موفقیت ارسال شد و پس از تایید منتشر خواهد شد";
                    TempData["color"] = "alert-success";
                }
            }
            else
            {
                ModelState.AddModelError("", "کد وارد شده صحیح نمی باشد");
            }


            return RedirectToAction("PostDetails", "Home", new { id = comment.PostID });
        }
        

        public ActionResult ShowComment(int? id)
        {
            var comments = db.Comments.Where(c => c.PostID == id && c.CommentIsActive == true);
            ViewBag.count = comments.Count();
            return PartialView("_ShowComment", comments.ToList());
        }

    }
}