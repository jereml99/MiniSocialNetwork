using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using MiniSocialNetwork.Models;
using NuGet.Protocol.Plugins;
using System.Diagnostics;

namespace MiniSocialNetwork.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private static int _id = default(int);
        public static List<User> Users = new List<User>()
        {
            new User("admin", DateTime.Now, new List<string>() { "ziom1", "ziom2" }),
            new User("greg", DateTime.Now, new List<string>() { "ziom1", "ziom2" })
        };

        public static User? CurrentlyLoggedUser = null;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public ActionResult Add()
        {
            return View();
        }

        public ActionResult Init()
        {
            for (int i = 0; i < 5; i++)
            {
                Users.Add(new User($"user_{_id++}", DateTime.Now, new List<string>() { $"friend_{1}", $"friend_{2}" }));
            }

            return RedirectToAction("List", "User");
        }

        [HttpPost]
        public ActionResult Add(string login)
        {

            if (ModelState.IsValid)
            {
                Users.Add(new User(login, DateTime.Now, new List<string>()));

                return RedirectToAction("List", "User");
            }
            else
            {
                return View(login);
            }
        }

        public ActionResult List()
        {
            return View(Users);
        }

        public ActionResult Delete(string login)
        {
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
                Users.RemoveAll(u => u.Login == login);
                return RedirectToAction("List", "User");
            }
            catch
            {
                return View();
            }
        }
    }
}