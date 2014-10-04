using System.Web.Mvc;

namespace SignalR_Mvc4.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult Chat()
        {
            return View();
        }
    }
}
