using System.Web.Mvc;
using CodeLouisvilleCSharp.Models;
using CodeLouisvilleCSharp.Repository;
using Microsoft.AspNet.Identity;

namespace CodeLouisvilleCSharp.Controllers
{
    public class ItemViewController : Controller
    {
        [Authorize]
        public ActionResult AddItem()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult Submit(Home home)
        {
            if (!ModelState.IsValid)
            {
                return View("AddItem");
            }

            var piggyRepository = new PiggyRepository();
            home.UserManager = User.Identity.GetUserId();

            piggyRepository.Create(home);
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        [HttpGet]
        public ActionResult EditItem(int id)
        {
            var results = PiggyRepository.GetById(id);
            return View(results);
        }


        [Authorize]
        [HttpPost]
        public ActionResult EditItem(Home home)
        {
            PiggyRepository.Update(home);
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public ActionResult DeleteItem(Home home)
        {
            var results = PiggyRepository.GetById(home.Id);
            return View(results);
        }


        [Authorize]
        [HttpPost]
        public ActionResult DeleteItem(int id)
        {
            PiggyRepository.Delete(id);
            return RedirectToAction("Index", "Home");
        }
    }
}