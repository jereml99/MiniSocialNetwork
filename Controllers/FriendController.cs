using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniSocialNetwork.Models;

namespace MiniSocialNetwork.Controllers
{
    public class FriendController : Controller
    {
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
                Globals.CurrentlyLoggedUser?.Friends.Add(login);

                ViewBag.Success = true;
            }
            else
            {
                ViewBag.Success = false;
            }

            return View("FriendManageResult");
        }

        public ActionResult List()
        {
            if (!hasAccess()) return RedirectToAction("Login", "Login");

            //return Json(new { Globals.CurrentlyLoggedUser?.Friends });
            return View(Globals.CurrentlyLoggedUser);
        }

        public ActionResult Delete(string login)
        {
            if (!hasAccess()) return RedirectToAction("Login", "Login");

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
        {// return Json(new { success = true });
            if (!hasAccess()) return RedirectToAction("Login", "Login");

            try
            {
                Globals.CurrentlyLoggedUser?.Friends.RemoveAll(u => u == login);
                ViewBag.Success = true;
            }
            catch
            {
                ViewBag.Success = false;
            }

            return View("FriendManageResult");
        }

        public ActionResult Export()
        {
            if (!hasAccess()) return RedirectToAction("Login", "Login");
            var friends = Globals.CurrentlyLoggedUser?.Friends;
            if (friends is null || friends.Count == 0){

            }

            //System.IO.File.WriteAllLines("SavedLists.txt", );
            return View(Globals.CurrentlyLoggedUser);
        }

        private bool hasAccess() => Globals.CurrentlyLoggedUser != null;
    }
}
