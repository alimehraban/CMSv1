using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Tamin.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        [Display(Name = "وضعیت کاربر")]
        public bool Is_archive { get; set; }

        [StringLength(50)]
        [Display(Name = "نام , نام خانوادگی")]
        public string Fullname { get; set; }

        [Display(Name = "تاریخ ثبت نام")]
        public DateTime? DateOfRegistration { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<User_meta> User_meta { get; set; }
    }

    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole() : base() { }
        public ApplicationRole(string name) : base(name) { }
    }




    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Migrations.Configuration>("DefaultConnection"));

        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }


        public DbSet<Post> Posts { get; set; }
        public DbSet<PostGroup> PostGroups { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<language> Languages { get; set; }
        public DbSet<Post_meta> PostMetas { get; set; }
        public DbSet<Site_Option> SiteOptions { get; set; }
        public DbSet<User_meta> UserMetas { get; set; }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>().
              HasOptional(e => e.Parent).
              WithMany().
              HasForeignKey(m => m.Parent_ID);

            modelBuilder.Entity<PostGroup>().
             HasOptional(e => e.Parent).
             WithMany().
             HasForeignKey(m => m.ParentId);

            base.OnModelCreating(modelBuilder);
        }

        public System.Data.Entity.DbSet<Tamin.Models.Contact> Contacts { get; set; }

        //public System.Data.Entity.DbSet<Tamin.Models.ApplicationUser> ApplicationUsers { get; set; }
        
    }
}