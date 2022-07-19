using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace first_api.Models
{
    public class Customer
    {
        [Key]
        public int UUID { get; set; }
        [Required]
        [Column(TypeName="varchar(50)")]
        public string Name { get; set; } = string.Empty;
        [Required]
        [Column(TypeName="varchar(11)")]
        public string CPF { get; set; } 
        [Required]
        public string Address { get; set; } = string.Empty;
        [Column]
        public string PhoneNumber { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }

    }
}
