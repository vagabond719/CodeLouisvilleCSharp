using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Helpers;
using System.Web.Mvc;
using CodeLouisvilleCSharp.Models;
using Microsoft.AspNet.Identity;

namespace CodeLouisvilleCSharp.Controllers
{
    public class HomeController : Controller
    {
        private List<Home> _categories;
        private double _paycheckAmmount;
        private double _billAmmount;
        private double _savingsAmmount;
        private double _diningAmmount;
        private double _entertainmentAmmount;

        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [Authorize]
        public ActionResult GetChartImage()
        {
            //new List<Home>();
            ArrayList xValue = new ArrayList();
            List<double> yValue = new List<double>();
            using (var context = new PiggyModel())
            {

                var userId = User.Identity.GetUserId();
                
                _categories = context.Home.Where(h => h.UserManager == userId).ToList();

                foreach (var category in _categories)
                {
                    if (category.ChosenFrequency == Frequency.Annualy)
                    {
                        CatCheck(category.Ammount, category);
                    }
                    else if (category.ChosenFrequency == Frequency.Daily)
                    {
                        CatCheck(category.Ammount * 365, category);
                    }
                    else if (category.ChosenFrequency == Frequency.Monthly)
                    {
                        CatCheck(category.Ammount * 12, category);
                    }
                    else if (category.ChosenFrequency == Frequency.Weekly)
                    {
                        CatCheck(category.Ammount * 52, category);
                    }
                }
                if (_paycheckAmmount > 0)
                {
                    yValue.Add(_paycheckAmmount);
                    xValue.Add("Pay Check");
                }

                if (_billAmmount > 0)
                {
                    yValue.Add(_billAmmount);
                    xValue.Add("Bill");
                }

                if (_savingsAmmount > 0)
                {
                    yValue.Add(_savingsAmmount);
                    xValue.Add("Savings");
                }

                if (_diningAmmount > 0)
                {
                    yValue.Add(_diningAmmount);
                    xValue.Add("Dining");
                }

                if (_entertainmentAmmount > 0)
                {
                    yValue.Add(_entertainmentAmmount);
                    xValue.Add("Entertainment");
                }
            }

            var chart = new Chart(800, 600, ChartTheme.Vanilla3D)
           .AddSeries(chartType: "pie",
                           xValue: xValue,
                           yValues: yValue)
                           .GetBytes("png");
            return File(chart, "image/bytes");
        }

        public void CatCheck(double amount, Home home)
        {
            if (home.ChosenCategory == Category.Bill)
            {
                _billAmmount += amount;
            }
            else if (home.ChosenCategory == Category.Dining)
            {
                _diningAmmount += amount;
            }
            else if (home.ChosenCategory == Category.Entertainment)
            {
                _entertainmentAmmount += amount;
            }
            else if (home.ChosenCategory == Category.PayCheck)
            {
                _paycheckAmmount += amount;
            }
            else if (home.ChosenCategory == Category.Savings)
            {
                _savingsAmmount += amount;
            }
        }

        public string GetTable()
        {
            using (var context = new PiggyModel())
            {
                var userId = User.Identity.GetUserId();

                var results = context.Home.Where(h => h.UserManager == userId);

                var table = @"<table id=""ChartData"" class=""table - striped""><tr><th> Ammount </th><th> Category </th><th> Frequency </t ><th> </th><th> </th></tr>";

                foreach (var result in results)
                {
                    table += @"<tr><td>" + result.Ammount + "</td><td>" +
                             result.ChosenCategory.ToString() + "</td><td>" + result.ChosenFrequency + "</td>" +
                             @"<td><a href=""/ItemView/EditItem/" + result.Id + @"""><span class=""glyphicon glyphicon-edit""></span></a></td>" + "" +
                             @"<td><a href=""/ItemView/DeleteItem/" + result.Id + @"""><span class=""glyphicon glyphicon-trash""></span></a></td></tr>";
                }
                table += "</table>";
                return table;

            }
        }
    }
}