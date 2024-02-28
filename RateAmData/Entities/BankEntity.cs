using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RateAmData.Entities
{
    [Table("banks")]
    public class BankEntity
    {
        [Key]
        [Column("bank_id")]
        public int BankId { get; set; }

        [Column("name")]
        public string ?Name { get; set; }

        [Column("icon_url")]
        public string ?IconURL { get; set; }
    }
}
