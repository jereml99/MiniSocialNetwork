using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using MiniSocialNetwork.Models;
using NuGet.Protocol.Plugins;
using System.Diagnostics;

namespace MiniSocialNetwork.Controllers
{
    public class UserController : Controller
    {
        private static int _id = default;

        public ActionResult Init()
        {
            if (!hasAccess()) return RedirectToAction("Login", "Login");

            for (int i = 0; i < 5; i++)
            {
                var friends = Globals.Users.TakeLast(2).Select(user => user.Login).ToList();
                Globals.Users.Add(new User($"user{_id++}", DateTime.Now, friends));
            }

            return RedirectToAction("List", "User");
        }

        public ActionResult Add()
        {
            if (!hasAccess()) return RedirectToAction("Login", "Login");

            return View();
        }

        [HttpPost]
        public ActionResult Add(string login)
        {
            if (!hasAccess()) return RedirectToAction("Login", "Login");

            if (ModelState.IsValid)
            {
                Globals.Users.Add(new User(login, DateTime.Now, new List<string>()));

                return RedirectToAction("List", "User");
            }
            else
            {
                return View(login);
            }
        }

        public ActionResult List()
        {
            if (!hasAccess()) return RedirectToAction("Login", "Login");

            return View(Globals.Users);
        }

        // POST: HomeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string login, IFormCollection collection)
        {
            try
            {
                Globals.Users.RemoveAll(u => u.Login == login);
                foreach (var user in Globals.Users)
                {
                    user.Friends.RemoveAll(f => f == login);
                }
                return RedirectToAction("List", "User");
            }
            catch
            {
                return View();
            }
        }

        private bool hasAccess() => Globals.CurrentlyLoggedUser != null && Globals.CurrentlyLoggedUser.Login == Globals.AdminLogin;
    }
}