using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Tamin.Models;
using System.IO;

namespace Tamin.Controllers
{
    //[Authorize]

    public class AdminPostGroupsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AdminPostGroups
        public ActionResult Index()

        {
            var postGroups = db.PostGroups.Include(p => p.Parent);

            return View(db.PostGroups.ToList());

        }



        public ActionResult Add()

        {
            ViewBag.ParentId = new SelectList(db.PostGroups, "PostGroupID", "PostGroupTitle");
            return PartialView();

        }



        [HttpPost]

        public ActionResult Add([Bind(Include = "PostGroupID,ParentId,PostGroupTitle,ImageUrl")] PostGroup postGroup, HttpPostedFileBase ImageUrl)

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

                db.SaveChanges();

            }
            ViewBag.ParentId = new SelectList(db.PostGroups, "PostGroupID", "PostGroupTitle", postGroup.ParentId);
            return PartialView("_Detail", db.PostGroups.ToList());

        }



        public ActionResult Edit(int id)

        {

            return PartialView(db.PostGroups.Find(id));

        }



        [HttpPost]

        public ActionResult Edit(PostGroup model, int id)

        {

            if (ModelState.IsValid)

            {

                db.Entry(model).State = System.Data.Entity.EntityState.Modified;

                db.SaveChanges();

            }

            return PartialView("_Detail", db.PostGroups.ToList());

        }



        public ActionResult Delete(int id)

        {

            db.PostGroups.Remove(db.PostGroups.Find(id));

            db.SaveChanges();

            return PartialView("_Detail", db.PostGroups.ToList());

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
