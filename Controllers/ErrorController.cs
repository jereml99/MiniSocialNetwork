using Microsoft.AspNetCore.Mvc;

namespace MiniSocialNetwork.Controllers
{
    public class ErrorController : Controller
    {
        public ViewResult Index()
        {
            return View("Error");
        }
    }
}
