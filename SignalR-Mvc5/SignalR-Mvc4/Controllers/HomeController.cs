using System.Linq;
using System.Web.Mvc;
using SignalR_Mvc4.Infrastructure;

namespace SignalR_Mvc4.Controllers
{
    public class HomeController : Controller
    {
        public ViewResult Index()
        {
            return View();
        }

        [Authorize]
        public ViewResult Chat()
        {
            return View();
        }

        [Authorize]
        public ViewResult PrivChat(string id)
        {
            var user = UserHandler.ConnectedUsers.FirstOrDefault(x => x.UserName == id);
            return View(user);
        }
    }
}
