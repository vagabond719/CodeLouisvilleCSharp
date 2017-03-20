using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CodeLouisvilleCSharp.Models;

namespace CodeLouisvilleCSharp.Repository
{
    public class PiggyRepository
    {
        public void Create(Home home)
        {
            using (var context = new PiggyModel())
            {
                context.Home.Add(home);

                context.SaveChanges();
            }
        }

        public void Update(Home home)
        {
            using (var context = new PiggyModel())
            {
                var homeToUpdate = context.Home.Find(home.Id);
                homeToUpdate.Ammount = home.Ammount;
                homeToUpdate.ChosenCategory = home.ChosenCategory;
                homeToUpdate.ChosenFrequency = home.ChosenFrequency;

                context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            using (var context = new PiggyModel())
            {
                var homeToRemove = context.Home.Find(id);
                context.Home.Remove(homeToRemove);

                context.SaveChanges();
            }
            return; 
        }

        public List<Home> GetAll()
        {
            List<Home> homes = new List<Home>();
            using (var context = new PiggyModel())
            {
                homes = context.Home.ToList();
            }

            return homes;
        }

        public Home GetById(int id)
        {
            Home home = null;
            using (var context = new PiggyModel())
            {
                home = context.Home.Find(id);
            }

            return home;
        }

        public List<Home> GetByUser(string userName)
        {
            List<Home> home = new List<Home>();
            using (var context = new PiggyModel())
            {
                home = context.Home.Where(u => u.UserManager == userName).ToList();
            }

            return home;
        }
    }
}