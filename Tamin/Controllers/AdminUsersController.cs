using System;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Tamin.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace Tamin.Controllers
{
    [Authorize]
    public class AdminUsersController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }
        // GET: AdminUsers
     //   [Authorize]
        public async Task<ActionResult> Index()
        {
            return View(await db.Users.ToListAsync());
        }


        // GET: AdminUsers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Is_archive,Fullname,DateOfRegistration,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] ApplicationUser applicationUser)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = applicationUser.Email,
                    Email = applicationUser.Email,
                    Fullname = applicationUser.Fullname,
                    Is_archive = applicationUser.Is_archive,
                    DateOfRegistration = DateTime.Now,
                    PhoneNumber = applicationUser.PhoneNumber,
                    EmailConfirmed = true
                };
                var result = await UserManager.CreateAsync(user, applicationUser.PasswordHash);
                if (result.Succeeded)
                {
                    UserManager.AddToRole(user.Id, "user");
                    return RedirectToAction("Index", "AdminUsers");
                }
                AddErrors(result);
            }

            return View(applicationUser);
        }

        private void AddErrors(IdentityResult result)
        {
            throw new NotImplementedException();
        }

        // GET: AdminUsers/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = await UserManager.FindByIdAsync(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }

        // POST: AdminUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Is_archive,Fullname,DateOfRegistration,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] ApplicationUser applicationUser)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await UserManager.FindByIdAsync(applicationUser.Id);
                if (user != null)
                {
                    user.Email = applicationUser.Email;
                    user.UserName = applicationUser.Email;
                    user.Fullname = applicationUser.Fullname;
                    user.Is_archive = applicationUser.Is_archive;
                    user.DateOfRegistration = applicationUser.DateOfRegistration ?? DateTime.Now;
                    user.PhoneNumber = applicationUser.PhoneNumber;

                    IdentityResult validEmail = await UserManager.UserValidator.ValidateAsync(user);
                    if (!validEmail.Succeeded)
                    {
                        AddErrors(validEmail);
                    }
                    IdentityResult validPass = null;
                    if (applicationUser.PasswordHash != null)
                    {
                        validPass = await UserManager.PasswordValidator.ValidateAsync(applicationUser.PasswordHash);
                        if (validPass.Succeeded)
                        {
                            user.PasswordHash = UserManager.PasswordHasher.HashPassword(applicationUser.PasswordHash);
                        }
                        else
                        {
                            AddErrors(validPass);
                        }
                    }
                    if ((validEmail.Succeeded && validPass == null) || (validEmail.Succeeded
                                                                        && applicationUser.PasswordHash != null && validPass.Succeeded))
                    {
                        IdentityResult result = await UserManager.UpdateAsync(user);
                        if (result.Succeeded)
                        {
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            AddErrors(result);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", "کاربر مورد نظر یافت نشد");
                }
            }
            return View(applicationUser);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(string id)
        {
            ApplicationUser user = await UserManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await UserManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View("Error", result.Errors);
                }
            }
            else
            {
                return View("Error", new string[] { "کاربر مورد نظر یافت نشد" });
            }
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
