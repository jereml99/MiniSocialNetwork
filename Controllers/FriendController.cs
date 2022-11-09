using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniSocialNetwork.Models;

namespace MiniSocialNetwork.Controllers
{
    public class FriendController : Controller
    {
        private static User CurrentlyLoggedUser = UserController.CurrentlyLoggedUser;
        private static List<string> friends = CurrentlyLoggedUser.Friends;
        [HttpPost]
        public ActionResult Add(string login)
        {

            if (ModelState.IsValid)
            {
                CurrentlyLoggedUser.Friends.Add(login);

                return RedirectToAction("List", "Friend");
            }
            else
            {
                return View(login);
            }
        }

        public ActionResult List()
        {
            return View(CurrentlyLoggedUser.Friends);
        }

        public ActionResult Delete(string login)
        {
            if (login == null)
            {
                return RedirectToAction("List", "Friend");
            }

            ViewBag.Login = login;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string login, IFormCollection collection)
        {
            try
            {
                CurrentlyLoggedUser.Friends.RemoveAll(u => u == login);
                return RedirectToAction("List", "User");
            }
            catch
            {
                return View();
            }
        }
    }
}
