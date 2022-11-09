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
                Globals.Users.Add(new User($"user_{_id++}", DateTime.Now, new List<string>() { $"friend_{1}", $"friend_{2}" }));
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

        public ActionResult Delete(string login)
        {
            if (!hasAccess()) return RedirectToAction("Login", "Login");

            if (login == null)
            {
                return RedirectToAction("List", "User");
            }
            ViewBag.Login = login;
            return View();
        }

        // POST: HomeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string login, IFormCollection collection)
        {
            try
            {
                Globals.Users.RemoveAll(u => u.Login == login);
                return RedirectToAction("List", "User");
            }
            catch
            {
                return View();
            }
        }

        private bool hasAccess() => Globals.CurrentlyLoggedUser != null && Globals.CurrentlyLoggedUser.Login == "a";
    }
}