using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniSocialNetwork.Models;
using System.Xml.Linq;

namespace MiniSocialNetwork.Controllers
{
    public class LoginController : Controller
    {

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Logout()
        {
            Globals.CurrentlyLoggedUser = null;
            return RedirectToAction("Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string login)
        {
            var currentlyLoggedUser = Globals.Users.FirstOrDefault(x => x.Login == login);

            if (currentlyLoggedUser == null)
            {
                return View();
            }
            else
            {
                Globals.CurrentlyLoggedUser = currentlyLoggedUser;
                return RedirectToAction("List", "Friend", new { login = currentlyLoggedUser.Login });
            }

        }
        
    }
}
