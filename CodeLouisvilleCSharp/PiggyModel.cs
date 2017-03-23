using CodeLouisvilleCSharp.Models;

namespace CodeLouisvilleCSharp
{
    using System.Data.Entity;

    public class PiggyModel : DbContext
    {
        // Your context has been configured to use a 'PiggyModel' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'CodeLouisvilleCSharp.PiggyModel' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'PiggyModel' 
        // connection string in the application configuration file.
        public PiggyModel()
            : base("name=PiggyModel")
        {
            //Database.SetInitializer(new MyCustomInitializer());
            Database.SetInitializer<PiggyModel>(new CreateDatabaseIfNotExists<PiggyModel>());
        }
        public DbSet<Home> Home { get; set; }
    }

    //public class MyCustomInitializer :
    //        DropCreateDatabaseIfModelChanges<PiggyModel>
    //{
    //    public override void InitializeDatabase(PiggyModel context)
    //    {
    //        context.Home.Add(new Home() { Ammount = 500.00, ChosenCategory = (Category) 1, ChosenFrequency = (Frequency) 3 });
    //        context.Home.Add(new Home() { Ammount = 50.00, ChosenCategory = (Category) 3, ChosenFrequency = (Frequency) 4 });
    //        context.Home.Add(new Home() { Ammount = 100.00, ChosenCategory = (Category) 3, ChosenFrequency = (Frequency) 4 });
    //        context.Home.Add(new Home() { Ammount = 25.00, ChosenCategory = (Category) 5, ChosenFrequency = (Frequency) 1 });
    //        context.Home.Add(new Home() { Ammount = 50.00, ChosenCategory = (Category) 4, ChosenFrequency = (Frequency) 2 });
    //        base.InitializeDatabase(context);
    //    }
    //}
}