using System.ComponentModel.DataAnnotations;


namespace RateAmLib
{
    public class Bank
    {
        public int BankId { get; set; }
        public string Name { get; set; }
        public string IconURL { get; set; }

        //public List<Rate> Rates { get; set; } = new List<Rate>();
        
    }
}
