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


        static List<User> Users = new List<User>();

        public ActionResult Add()
        {
            return View();
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
            //return Ok(User);
            return View(Users);
        }

        public ActionResult Delete(string login)
        {
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