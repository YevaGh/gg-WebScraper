namespace RateAmLib
{
    public class Rate
    {
        public int Id { get; set; }
        public string Date {  get; set; }
        public decimal BuyRate { get; set; }
        public decimal SellRate { get; set; }
        public int BankId { get; set; }
        public Bank Bank { get; set; }
        public int CurrencyId {  get; set; }
        public Currency Currency{ get; set;}
        
    }
}
