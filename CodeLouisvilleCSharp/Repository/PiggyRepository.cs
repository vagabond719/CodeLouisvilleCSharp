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

        public static void Update(Home home)
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

        public static void Delete(int id)
        {
            using (var context = new PiggyModel())
            {
                var homeToRemove = context.Home.Find(id);
                context.Home.Remove(homeToRemove);

                context.SaveChanges();
            }
        }

        public static Home GetById(int id)
        {
            Home home;
            using (var context = new PiggyModel())
            {
                home = context.Home.Find(id);
            }

            return home;
        }
    }
}