namespace CodeLouisvilleCSharp.Models
{
    public class Home
    {
        public int Id { get; set; }
        public double Ammount { get; set; }
        public Category ChosenCategory { get; set; }
        public Frequency ChosenFrequency { get; set;  }
        public string UserManager { get; set; }
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
        Weekly = 2,
        Monthly = 3,
        Annualy = 4
    }
}