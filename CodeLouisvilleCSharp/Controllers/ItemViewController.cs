using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CodeLouisvilleCSharp.Models;
using CodeLouisvilleCSharp.Repository;

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
            piggyRepository.Create(home);
            return RedirectToAction("Index", "Home");
        }

        //public void Enter(Home home)
        //{
        //    using (var piggyModel = new PiggyModel())
        //    {
        //        piggyModel.Home.Add(home);

        //        piggyModel.SaveChanges();
        //    }
        //}

        [HttpPost]
        public ActionResult EditItem(Home home)
        {
            if (!ModelState.IsValid)
            {
                return View("EditItem");
            }

            var piggyRepository = new PiggyRepository();
            piggyRepository.Create(home);
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public ActionResult DeleteItem(int id)
        {
            var repository = new PiggyRepository();
            var results = repository.GetById(id);
            //repository.Delete(id);
            //return View("DeleteItem");
            return View(results);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var repository = new PiggyRepository();
            repository.Delete(id);
            return View("DeleteItem");
            return RedirectToAction("Index", "Home");
        }
    }
}