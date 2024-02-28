using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RateAmData.Entities
{
    [Table("last_updated")]
    public class LastDateEntity
    {
        [Key]
        [Column("id")]
        public int ColId { get; set; }

        [Column("last_date")]
        public DateTime LastDate { get; set; }
    }
}
