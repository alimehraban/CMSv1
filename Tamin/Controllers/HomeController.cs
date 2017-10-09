using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Net;
using Tamin.Models;
using System.Web.UI;
using Tamin.Security;

namespace Tamin.Controllers
{



    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            //var posts = (from p in db.Posts
            //    orderby p.PostID descending
            //    select p);
            return View(Getlatestpost(0));
        }

        private List<Post> Getlatestpost(int pageNumber, int recordsPerPage = 5)
        {
            var skipRecords = pageNumber * recordsPerPage;
            var d = (from p in db.Posts
                    where (p.PostIsActive && !p.Is_delete && (p.Startdate != null && p.Startdate <= DateTime.Now)) ||
                          (p.PostIsActive && !p.Is_delete && (p.Enddate != null && p.Enddate >= DateTime.Now)) ||
                          (p.PostIsActive && !p.Is_delete && (p.Startdate != null && p.Startdate <= DateTime.Now) && (p.Enddate != null && p.Enddate >= DateTime.Now)) ||
                          (p.PostIsActive && !p.Is_delete)
                    select p)
                .OrderByDescending(x => x.Priority)
                .ThenByDescending(z => z.PostDate)
                .ThenByDescending(y => y.PostID)
                .Skip(skipRecords)
                .Take(recordsPerPage)
                .ToList();
            return d;
        }
        [HttpPost]
        [AjaxOnly]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public virtual ActionResult PagedIndex(int? page)
        {
            var pageNumber = page ?? 0;
            var list = Getlatestpost(pageNumber);
            if (list == null || !list.Any())
                return Content("no-more-info"); //این شرط ما است برای نمایش عدم یافتن رکوردها

            return PartialView("_ItemList", list);
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult FAQ()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Shownews()
        {
            var posts = (from p in db.Posts
                         orderby p.PostID descending
                         select p);
            return View(posts.ToList());
        }
        public async Task<ActionResult> PostDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = await db.Posts.FindAsync(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            ViewBag.info = TempData["info"];
            ViewBag.color = TempData["color"];
            Increadcount(post.PostID);
            return View(post);
        }

        private void Increadcount(int postid)
        {
            using (db)
            {
                var result = db.Posts.SingleOrDefault(b => b.PostID == postid);
                if (result != null)
                {
                    result.PageCounter++;
                    db.SaveChanges();
                }
            }
        }


        public ActionResult ShowState()
        {
            return PartialView(StateClass.ShowState());
        }
    }
}