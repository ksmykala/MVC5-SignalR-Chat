using System.Web.Mvc;
using SignalR_Mvc5.Repository;

namespace SignalR_Mvc5.Controllers
{
    public class HomeController : Controller
    {
        private IUserRepository _userRepository;

        public HomeController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ViewResult Chat()
        {
            var loggedUsers = _userRepository.GetAllUsers();
            return View(loggedUsers);
        }
	}
}