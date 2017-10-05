namespace Tamin.Migrations
{
    using Models;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Data.Entity.Migrations;
    using Microsoft.AspNet.Identity;
    using Tamin;

    internal sealed class Configuration : DbMigrationsConfiguration<Tamin.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "Tamin.Models.ApplicationDbContext";
        }

        protected override void Seed(Tamin.Models.ApplicationDbContext context)
        {
            ApplicationUserManager userMgr = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            ApplicationRoleManager roleMgr = new ApplicationRoleManager(new RoleStore<ApplicationRole>(context));
            string roleName = "administrator";
            string password = "123456789@a";
            string email = "admin@gmail.com";
            if (!roleMgr.RoleExists(roleName))
            {
                roleMgr.Create(new ApplicationRole(roleName));
            }
            ApplicationUser user = userMgr.FindByName("supercoach");
            if (user == null)
            {
                userMgr.Create(new ApplicationUser { UserName = "supercoach", Email = email },
                    password);
                user = userMgr.FindByName("supercoach");
                if (!userMgr.IsInRole(user.Id, roleName))
                {
                    userMgr.AddToRole(user.Id, roleName);
                }
                context.SaveChanges();
            }
        }
        }
    }

