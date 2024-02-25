using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace RateAmData.Entities
{
    [Table("currencies")]
    public class CurrencyEntity
    {
        [Key]
        [Column("currency_id")]
        public int Id {  get; set; }

        [Column("name")]
        public string Name {  get; set; }

        [Column("symbol")]
        public string Symbol { get; set; }

        [Column("icon_url")]
        public string IconUrl { get; set; }

    }
}
