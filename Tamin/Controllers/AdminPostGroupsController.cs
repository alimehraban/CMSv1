using System;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Tamin.Models;
using System.IO;

namespace Tamin.Controllers
{
    [Authorize]

    public class AdminPostGroupsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AdminPostGroups
        public async Task<ActionResult> Index()
        {
            var postGroups = db.PostGroups.Include(p => p.Parent);
            return View(await postGroups.ToListAsync());
        }

        // GET: AdminPostGroups/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PostGroup postGroup = await db.PostGroups.FindAsync(id);
            if (postGroup == null)
            {
                return HttpNotFound();
            }
            return View(postGroup);
        }

        // GET: AdminPostGroups/Create
        public ActionResult Create()
        {
            ViewBag.ParentId = new SelectList(db.PostGroups, "PostGroupID", "PostGroupTitle");
            return View();
        }

        // POST: AdminPostGroups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "PostGroupID,ParentId,PostGroupTitle,ImageUrl")] PostGroup postGroup, HttpPostedFileBase ImageUrl)
        {
            if (ModelState.IsValid)
            {
                if (ImageUrl != null)
                {
                    var fileName = Path.GetFileName(ImageUrl.FileName);
                    string newFilename = Guid.NewGuid().ToString()
                                             .Replace("-", string.Empty) +
                                         Path.GetExtension(fileName);
                    newFilename = "/Uploads/PostGroups/" + newFilename;
                    var physicalPath = Server.MapPath(newFilename);
                    ImageUrl.SaveAs(physicalPath);
                    postGroup.ImageUrl = newFilename;
                }
                db.PostGroups.Add(postGroup);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ParentId = new SelectList(db.PostGroups, "PostGroupID", "PostGroupTitle", postGroup.ParentId);
            return View(postGroup);
        }

        // GET: AdminPostGroups/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PostGroup postGroup = await db.PostGroups.FindAsync(id);
            if (postGroup == null)
            {
                return HttpNotFound();
            }
            ViewBag.ParentId = new SelectList(db.PostGroups, "PostGroupID", "PostGroupTitle", postGroup.ParentId);
            return View(postGroup);
        }

        // POST: AdminPostGroups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "PostGroupID,ParentId,PostGroupTitle,ImageUrl")] PostGroup postGroup, HttpPostedFileBase ImageUrl)
        {
            if (ModelState.IsValid)
            {
                if (ImageUrl != null)
                {
                    var fileName = Path.GetFileName(ImageUrl.FileName);
                    string newFilename = Guid.NewGuid().ToString()
                                             .Replace("-", string.Empty) +
                                         Path.GetExtension(fileName);
                    newFilename = "/Uploads/PostGroups/" + newFilename;
                    var physicalPath = Server.MapPath(newFilename);
                    ImageUrl.SaveAs(physicalPath);
                    if (System.IO.File.Exists(Server.MapPath("~/" + postGroup.ImageUrl)))
                    {
                        System.IO.File.Delete(Server.MapPath("~/" + postGroup.ImageUrl));
                    }
                    postGroup.ImageUrl = newFilename;
                }
                db.Entry(postGroup).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ParentId = new SelectList(db.PostGroups, "PostGroupID", "PostGroupTitle", postGroup.ParentId);
            return View(postGroup);
        }

        // GET: AdminPostGroups/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PostGroup postGroup = await db.PostGroups.FindAsync(id);
            if (postGroup == null)
            {
                return HttpNotFound();
            }
            return View(postGroup);
        }

        // POST: AdminPostGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            PostGroup postGroup = await db.PostGroups.FindAsync(id);
            db.PostGroups.Remove(postGroup);
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
