using System.Web.Mvc;

namespace Tamin.Controllers
{
    public class AdminHomeController : Controller
    {
        [Authorize]

        // GET: AdminHome
        public ActionResult Index()
        {
            return View();
        }
    }
}