using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;

namespace CodeLouisvilleCSharp.Models
{
    public class Home
    {
        public int Id { get; set; }
        public double Ammount { get; set; }
        public Category ChosenCategory { get; set; }
        public Frequency ChosenFrequency { get; set;  }
        public string UserManager { get; set; }
        
        public List<Home> GetList()
        {
            using(var context = new PiggyModel())
            {
                return context.Home.ToList();
            }
        }
    }

    public enum Category
    {
        PayCheck = 1,
        Savings = 2,
        Bill = 3,
        Dining = 4,
        Entertainment = 5
    }

    public enum Frequency
    {
        Daily = 1,
        BusinessWeek = 2,
        Weekly = 3,
        Monthly = 4,
        Annualy = 5
    }
}