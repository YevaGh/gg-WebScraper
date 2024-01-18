namespace RateAmLib
{
    public class Rate
    {
        public int Id { get; set; }
        public string BuyRate { get; set; }
        public string SellRate { get; set; }
        public int BankId { get; set; }
        public Bank Bank { get; set; }
        public int CurrencyId {  get; set; }
        public Currency Currency{ get; set;}
        
    }
}
