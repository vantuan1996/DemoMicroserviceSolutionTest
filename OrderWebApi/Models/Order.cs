using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OrderWebApi.Models
{
    [Table("order", Schema = "dbo")]
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Id")]
        public int Id { get; set; }

        [Column("customer_id")]
        public int CustomerId { get; set; }

        [Column("OrderedOn")]
        public DateTime OrderedOn { get; set; }

        [Column("OrderDetailsId")]
        public int OrderDetailsId { get; set; }


    }
}
