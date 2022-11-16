using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniSocialNetwork.Models;
using System.Data;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Net.Http.Headers;
using System.Text;

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

            if (!ModelState.IsValid || !Globals.Users.Select(user => user.Login).Contains(login) ||
                (Globals.CurrentlyLoggedUser?.Friends.Contains(login) ?? true)) return Json(false);
            Globals.CurrentlyLoggedUser?.Friends.Add(login);
            return Json(true);

        }

        public ActionResult List()
        {
            if (!hasAccess()) return RedirectToAction("Login", "Login");

            //return Json(new { Globals.CurrentlyLoggedUser?.Friends });
            return View(Globals.CurrentlyLoggedUser);
        }
        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string login, IFormCollection collection)
        {// return Json(new { success = true });
            if (!hasAccess()) return RedirectToAction("Login", "Login");

            try
            {
                Globals.CurrentlyLoggedUser?.Friends.RemoveAll(u => u == login);
                return Json(true);
            }
            catch
            {
                return Json(false);
            }
            
        }

        public ActionResult Export()
        {
            if (!hasAccess()) return RedirectToAction("Login", "Login");

            var friends = Globals.CurrentlyLoggedUser?.Friends;

            if (friends is null || friends.Count == 0){
                return View("List", Globals.CurrentlyLoggedUser);
            }

            string filePath = $"{Globals.CurrentlyLoggedUser!.Login}_" + DateTime.Today.ToString("yyyy-MM-dd") + ".txt";

            StringBuilder sb = new();
            Globals.CurrentlyLoggedUser!.Friends.ForEach(f => sb.AppendLine(f));
            byte[] fileContent = Encoding.ASCII.GetBytes(sb.ToString());

            return new FileContentResult(fileContent, "text/txt")
            {
                FileDownloadName = filePath
            };
        }

        public ActionResult Import()
        {
            if (!hasAccess()) return RedirectToAction("Login", "Login");

            return View();
        }

        [HttpPost]
        public ActionResult Import(IFormFile postedFile)
        {
            if(postedFile is null) return Json(false);
            Globals.CurrentlyLoggedUser?.Friends.Clear();
            Globals.CurrentlyLoggedUser?.Friends.AddRange(RetriveFriendsFromFile(postedFile));

            return View("List", Globals.CurrentlyLoggedUser);
        }

        private List<string> RetriveFriendsFromFile(IFormFile file)
        {// if null
            var result = new List<string>();
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                while (reader.Peek() >= 0)
                {
                    result.Add(reader.ReadLine() ?? "");
                }
            }

            return result;
        }

        private bool hasAccess() => Globals.CurrentlyLoggedUser != null;
    }
}
