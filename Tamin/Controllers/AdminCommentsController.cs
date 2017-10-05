using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using Tamin.Models;

namespace Tamin.Controllers
{
    [Authorize]
    public class AdminCommentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AdminComments
        public async Task<ActionResult> Index()
        {
            //var comments = db.Comments.Include(c => c.Parent);

            var comments = db.Comments.Include(c => c.posts).Include(c => c.Parent);

            return View(await comments.ToListAsync());
        }

        // GET: AdminComments/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = await db.Comments.FindAsync(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // GET: AdminComments/Create
        public ActionResult Create(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            ViewBag.PageId = comment.PostID;
            ViewBag.ParentId = comment.Comment_ID;
            ViewBag.ParentContent = comment.CommentContent;
            ViewBag.Author = "مدیر سایت";
            ViewBag.AuthorEmail = "Admin@Test.com";
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View();
        }
        // POST: AdminComments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Comment_ID,User_ID,PostID,Parent_ID,Comment_Date,CommentIsActive,Author,Comment_title,CommentContent,Author_Email,UserLogsData")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                db.Comments.Add(comment);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Parent_ID = new SelectList(db.Comments, "Comment_ID", "Author", comment.Parent_ID);
            return View(comment);
        }

        // GET: AdminComments/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = await db.Comments.FindAsync(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            ViewBag.PostID = new SelectList(db.Posts, "PostID", "PostTitle", comment.PostID);
            ViewBag.Parent_ID = new SelectList(db.Comments, "Comment_ID", "Author", comment.Parent_ID);
            return View(comment);
        }

        // POST: AdminComments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Comment_ID,User_ID,PostID,Parent_ID,Comment_Date,CommentIsActive,Author,Comment_title,CommentContent,Author_Email,UserLogsData")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comment).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.PageID = new SelectList(db.Posts, "PostID", "PageTitle", comment.PostID);

            ViewBag.Parent_ID = new SelectList(db.Comments, "Comment_ID", "Author", comment.Parent_ID);
            return View(comment);
        }

        // GET: AdminComments/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = await db.Comments.FindAsync(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // POST: AdminComments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Comment comment = await db.Comments.FindAsync(id);
            db.Comments.Remove(comment);
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
