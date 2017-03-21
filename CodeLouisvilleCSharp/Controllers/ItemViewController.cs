using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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

        [HttpGet]
        public ActionResult EditItem(int id)
        {
            var piggyRepository = new PiggyRepository();
            var results = piggyRepository.GetById(id);
            return View(results);
        }

        [HttpPost]
        public ActionResult EditItem(Home home)
        {
            var piggyRepository = new PiggyRepository();
            piggyRepository.Update(home);
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public ActionResult DeleteItem(int id)
        {
            var repository = new PiggyRepository();
            var results = repository.GetById(id);
            return View(results);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var repository = new PiggyRepository();
            repository.Delete(id);
            return RedirectToAction("Index", "Home");
        }
    }
}