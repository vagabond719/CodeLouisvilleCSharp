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
            List<decimal> yValue = new List<decimal>();
            using (var context = new PiggyModel())
            {
                var results = context.Home.Select(rs => new {rs.ChosenCategory, rs.Ammount}).ToList();

                results.ToList().ForEach(rs => xValue.Add(rs.ChosenCategory.ToString()));
                results.ToList().ForEach(rs => yValue.Add((decimal)rs.Ammount));
            }

            var chart = new Chart(800, 600, ChartTheme.Vanilla3D)
           .AddSeries(chartType: "pie",
                           xValue: xValue,
                           yValues: yValue)
                           .GetBytes("png");
            return File(chart, "image/bytes");
        }

        public string GetTable()
        {
            using (var context = new PiggyModel())
            {
                var results = context.Home.ToList();

                var table = @"<table id=""ChartData"" class=""table - striped""><tr><th> Ammount </th><th> Category </th><th> Frequency </t ><th> </th><th> </th></tr>";

                foreach (var result in results)
                {
                    table += @"<tr><td>" + result.Ammount + "</td><td>" +
                             result.ChosenCategory.ToString() + "</td><td>" + result.ChosenFrequency + "</td>" +
                             @"<td><span class=""glyphicon glyphicon-edit""></span></td><td><a href=""/Home/Delete/" + result.Id + @"""><span class=""glyphicon glyphicon-trash""></span></a></td></tr>";
                }
                table += "</table>";
                return table;

            }
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var repository = new PiggyRepository();
            repository.Delete(id);
            return View("Index");
        }
    }
}