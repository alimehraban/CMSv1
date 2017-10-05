using System.Linq;
using System.Web;
using Tamin.Models;

namespace Tamin
{
    public class StateClass
    {

        //public static void CounterState()
        //{

        //   // DateTime dt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
        //    if (HttpContext.Current.Request.Cookies["Posts"] != null)
        //    {
        //        if (Convert.ToDateTime(HttpContext.Current.Request.Cookies["Posts"].Value.ToString()) != dt)
        //        {
        //           // HttpCookie cookieCode = new HttpCookie("Posts", dt.ToString());
        //           // HttpContext.Current.Response.Cookies.Add(cookieCode);
        //            CountUpState();
        //        }
        //    }
        //    else
        //    {
        //        CountUpState();
        //        HttpCookie cookie = new HttpCookie("Posts");
        //        cookie.Value = dt.ToString();
        //        HttpContext.Current.Response.Cookies.Add(cookie);
        //    }


        //}

        static void CountUpState()
        {
            ApplicationDbContext db = new ApplicationDbContext(); 
            {
                //DateTime dt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                var state = db.Posts.FirstOrDefault();//(f => f.PostDate == dt);
                if (state != null)
                {  
                    state.PageCounter += 1;
                }
                else
                {
                    db.Posts.Add(new Post()
                    {
                        //PostDate = dt,
                        PageCounter = 1
                    });
                }
                db.SaveChanges();
            }


        }

        public static ShowStateViewModel ShowState()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            {
              //  DateTime dt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
               // DateTime dt2 = dt.AddDays(-1);
                return new ShowStateViewModel()
                {
                    OnlineUser = (int)HttpContext.Current.Application["OnlineUser"],
                    SeeSum = db.Posts.Sum(s => s.PageCounter),
                  //  SeeToday = db.Posts.First(s => s.PostDate == dt).PageCounter,
                  //  SeeYesterday = db.Posts.Where(s => s.PostDate == dt2).Select(s => s.PageCounter).FirstOrDefault()
                };
            }
        }
    }
}