using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using CodeLouisvilleCSharp.Models;
using CodeLouisvilleCSharp.Repository;
using Microsoft.AspNet.Identity;

namespace CodeLouisvilleCSharp.Controllers
{
    public class HomeController : Controller
    {
        private List<Home> categories;
        private double PaycheckAmmount;
        private double BillAmmount;
        private double SavingsAmmount;
        private double DiningAmmount;
        private double EntertainmentAmmount;

        [Authorize]
        public ActionResult Index()
        {
            var repository = new PiggyRepository();
            
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
            List<Home> home = new List<Home>();
            ArrayList xValue = new ArrayList();
            List<double> yValue = new List<double>();
            using (var context = new PiggyModel())
            {

                var userId = User.Identity.GetUserId();
                
                categories = context.Home.Where(h => h.UserManager == userId).ToList();

                foreach (var category in categories)
                {
                    if (category.ChosenFrequency == Frequency.Annualy)
                    {
                        catCheck(category.Ammount, category);
                    }
                    else if (category.ChosenFrequency == Frequency.Daily)
                    {
                        catCheck(category.Ammount * 365, category);
                    }
                    else if (category.ChosenFrequency == Frequency.Monthly)
                    {
                        catCheck(category.Ammount * 12, category);
                    }
                    else if (category.ChosenFrequency == Frequency.Weekly)
                    {
                        catCheck(category.Ammount * 52, category);
                    }
                }
                
                xValue.Add("Pay Check");
                xValue.Add("Bill");
                xValue.Add("Savings");
                xValue.Add("Dining");
                xValue.Add("Entertainment");
                
                yValue.Add(PaycheckAmmount);
                yValue.Add(BillAmmount);
                yValue.Add(SavingsAmmount);
                yValue.Add(DiningAmmount);
                yValue.Add(EntertainmentAmmount);
            }

            var chart = new Chart(800, 600, ChartTheme.Vanilla3D)
           .AddSeries(chartType: "pie",
                           xValue: xValue,
                           yValues: yValue)
                           .GetBytes("png");
            return File(chart, "image/bytes");
        }

        public void catCheck(double amount, Home home)
        {
            if (home.ChosenCategory == Category.Bill)
            {
                BillAmmount += amount;
            }
            else if (home.ChosenCategory == Category.Dining)
            {
                DiningAmmount += amount;
            }
            else if (home.ChosenCategory == Category.Entertainment)
            {
                EntertainmentAmmount += amount;
            }
            else if (home.ChosenCategory == Category.PayCheck)
            {
                PaycheckAmmount += amount;
            }
            else if (home.ChosenCategory == Category.Savings)
            {
                SavingsAmmount += amount;
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