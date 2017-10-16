using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Tamin.Models;
using System.IO;
using Microsoft.AspNet.Identity;

namespace Tamin.Controllers
{
    [Authorize]

    public class AdminPostsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AdminPosts
        public async Task<ActionResult> Index()
        {
            var posts = db.Posts.Include(p => p.ApplicationUser).Include(p => p.PostGroups).OrderBy(p=>p.PostDate).ThenBy(p=>p.Modifiedat);
            return View(await posts.ToListAsync());
        }

        public ActionResult ShowByGroup(int? id)
        {
            var posts = (from p in db.Posts
                where p.PostGroupID == id
                select p);
            return View(posts.ToList());
        }


        public ActionResult Shownews()
        {
            var posts = (from p in db.Posts
                    where (p.PostIsActive && !p.Is_delete && (p.Startdate!=null  && p.Startdate <= DateTime.Now)) ||
                          (p.PostIsActive && !p.Is_delete && (p.Enddate != null && p.Enddate >= DateTime.Now)) ||
                          (p.PostIsActive && !p.Is_delete && (p.Startdate != null && p.Startdate <= DateTime.Now) && (p.Enddate != null && p.Enddate >= DateTime.Now)) ||
                          (p.PostIsActive && !p.Is_delete)
                         select p)
                .OrderByDescending(x => x.Priority)
                .ThenByDescending(y => y.PostID);

            return View(posts.ToList());
        }
        
        // GET: AdminPosts/Details/5
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
            return View(post);
        }

        // GET: AdminPosts/Create
        public ActionResult Create()
        {
            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "Fullname");
            ViewBag.PostGroupID = new SelectList(db.PostGroups, "PostGroupID", "PostGroupTitle");
            return View();
        }

        // POST: AdminPosts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "PostID,PostGroupID,ApplicationUserId,PostTitle,Summary,PostText,PostIsActive,PostDate,Modifiedat,Modifiedby,Is_delete,Startdate,Enddate,Comment_conut,Comment_status,ImageUrl,PostThumbnailImageUrl,Priority")] Post post, HttpPostedFileBase ImageUrl)
        {
            if (ModelState.IsValid)
            {
                if (ImageUrl != null)
                {
                    string filename = Path.GetFileName(ImageUrl.FileName);
                    string newFilename = Guid.NewGuid().ToString()
                                             .Replace("-", string.Empty) +
                                         Path.GetExtension(filename);
                    var newFilenameUrl = "/Uploads/Post/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);
                    ImageUrl.SaveAs(physicalFilename);
                    var thumbnailUrl = Utils.CreateThumbnail(physicalFilename);
                    post.ImageUrl = newFilenameUrl;
                    post.PostThumbnailImageUrl = thumbnailUrl;
                }
                post.ApplicationUserId = User.Identity.GetUserId();
                post.PostText = Server.HtmlDecode(post.PostText);
                post.PostDate=DateTime.Now.AddDays(1);
                db.Posts.Add(post);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "Fullname", post.ApplicationUserId);
            ViewBag.PostGroupID = new SelectList(db.PostGroups, "PostGroupID", "PostGroupTitle", post.PostGroupID);
            return View(post);
        }

        // GET: AdminPosts/Edit/5
        public async Task<ActionResult> Edit(int? id)
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
            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "Fullname", post.ApplicationUserId);
            ViewBag.PostGroupID = new SelectList(db.PostGroups, "PostGroupID", "PostGroupTitle", post.PostGroupID);
            return View(post);
        }

        // POST: AdminPosts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "PostID,PostGroupID,ApplicationUserId,PostTitle,Summary,PostText,PostIsActive,Modifiedat,Modifiedby,Is_delete,Startdate,Enddate,Comment_conut,Comment_status,ImageUrl,PostThumbnailImageUrl,Priority")] Post post, HttpPostedFileBase ImageUrl)
        {
            if (ModelState.IsValid)
            {
                if (ImageUrl != null)
                {
                    string filename = Path.GetFileName(ImageUrl.FileName);
                    string newFilename = Guid.NewGuid().ToString().Replace("-", string.Empty)
                                         + Path.GetExtension(filename);

                    var newFilenameUrl = "/Uploads/Post/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);

                    ImageUrl.SaveAs(physicalFilename);

                    var thumbnailUrl = Utils.CreateThumbnail(physicalFilename);

                    if (System.IO.File.Exists(Server.MapPath("~/" + post.ImageUrl)))
                    {
                        System.IO.File.Delete(Server.MapPath("~/" + post.ImageUrl));
                    }
                    if (System.IO.File.Exists(Server.MapPath("~/" + post.PostThumbnailImageUrl)))
                    {
                        System.IO.File.Delete(Server.MapPath("~/" + post.PostThumbnailImageUrl));
                    }
                    post.ImageUrl = newFilenameUrl;
                    post.PostThumbnailImageUrl = thumbnailUrl;
                }
                post.PostDate=DateTime.Now;
                post.Modifiedby = User.Identity.GetUserId();
                post.Modifiedat=DateTime.Now;
                post.PostText = Server.HtmlDecode(post.PostText);
                db.Entry(post).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "Fullname", post.ApplicationUserId);
            ViewBag.PostGroupID = new SelectList(db.PostGroups, "PostGroupID", "PostGroupTitle", post.PostGroupID);
            return View(post);
        }

        [HttpPost]
        public ActionResult Changestatus(int? id)
        {
            var original = db.Posts.Find(id);
            if (original != null)
            {
                original.PostIsActive = !original.PostIsActive;
                db.SaveChanges();
                return Content("true");
            }
            return Content("false");
        }

        // GET: AdminPosts/Delete/5
        public async Task<ActionResult> Delete(int? id)
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
            return View(post);
        }

        // POST: AdminPosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Post post = await db.Posts.FindAsync(id);
            if (post != null) db.Posts.Remove(post);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
