using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RateAmData.Entities
{
    [Table("rates")]
    public class RateEntity
    {
        [Key]
        [Column("rate_id")]
        public int RateId { get; set; }

        [Column("publish_date")]
        public DateTime PublishDate { get; set; }

        [Column("sell_rate")]
        public decimal SellRate { get; set; }

        [Column("buy_rate")]
        public decimal BuyRate { get; set; }

        
        [Column("bank_id")]
        public int BankId { get; set; }


        [Column("currency_id")]
        public int CurrencyId { get; set; }


        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            RateEntity other = (RateEntity)obj;

            
            return BankId == other.BankId &&
                   CurrencyId == other.CurrencyId &&
                   PublishDate == other.PublishDate &&
                   SellRate == other.SellRate &&
                   BuyRate == other.BuyRate;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(BankId, CurrencyId, PublishDate, SellRate, BuyRate);
        }
    }
}
