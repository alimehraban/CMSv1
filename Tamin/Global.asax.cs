using GSD.Globalization;
using System;
using System.Data.Entity;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Tamin.Models;

namespace Tamin
{
    public class MvcApplication : System.Web.HttpApplication
    {

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            var persianCulture = new PersianCulture();
            persianCulture.DateTimeFormat.ShortDatePattern = "yyyy/MM/dd";
            persianCulture.DateTimeFormat.LongDatePattern = "dddd d MMMM yyyy";
            persianCulture.DateTimeFormat.AMDesignator = "صبح";
            persianCulture.DateTimeFormat.PMDesignator = "عصر";
            Thread.CurrentThread.CurrentCulture = persianCulture;
            Thread.CurrentThread.CurrentUICulture = persianCulture;
        }
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            HttpContext.Current.Application["OnlineUser"] = 0;
            AreaRegistration.RegisterAllAreas();
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Migrations.Configuration>("DefaultConnection"));
        }



        protected void Session_Start()
        {
            HttpContext.Current.Application["OnlineUser"] = ((int)HttpContext.Current.Application["OnlineUser"] + 1);
           // StateClass.CounterState();
        }

        //protected void Session_End()
        //{
        //    HttpContext.Current.Application["OnlineUser"] = ((int)HttpContext.Current.Application["OnlineUser"] - 1);
        //}

    }
}
