using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SistemaMercado.Models
{
    public class Account
    {
        [Key]

        public Guid ID { get; set; }

        [Required(ErrorMessage = "The Description Field is Required")]
        [StringLength(150, MinimumLength = 3, ErrorMessage = "The Description must be between 3 and 150 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "The Value Field is required.")]
        [Column(TypeName = "decimal(18,2)")]
        [Range(0, double.MaxValue, ErrorMessage = "value must be greater than 0")]
        public Decimal Value { get; set; }

        [DataType(DataType.Date)]
        public DateTime? Paymentdate { get; set; }

        [Required(ErrorMessage = "The Duedate Field is required")]
        [DataType(DataType.Date)]
        public DateTime DueDate { get; set; }

        public Situation Situation { get; set; }

        public Account() { }
    }
}
