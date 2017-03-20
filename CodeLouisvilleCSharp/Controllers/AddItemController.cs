using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CodeLouisvilleCSharp.Models;
using CodeLouisvilleCSharp.Repository;

namespace CodeLouisvilleCSharp.Controllers
{
    public class AddItemController : Controller
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

        public void Enter(Home home)
        {
            using (var piggyModel = new PiggyModel())
            {
                piggyModel.Home.Add(home);

                piggyModel.SaveChanges();
            }
        }
    }
}