using System.Linq;
using System.Web;
using Tamin.Models;

namespace Tamin
{
    public class StateClass
    {
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
                    SeeSum = db.Posts.Sum(s => s.PageCounter)
                };
            }
        }
    }
}