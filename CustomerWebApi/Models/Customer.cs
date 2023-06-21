using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime;

namespace CustomerWebApi.Models
{

    [Table("customer", Schema = "dbo")]
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Id")]
        public int Id { get; set; }

        [Column("Name")]
        public string? Name { get; set; }

        [Column("Mobile")]
        public int Mobile { get; set; }

        [Column("Email")]
        public string Email { get; set; }
    }
}
