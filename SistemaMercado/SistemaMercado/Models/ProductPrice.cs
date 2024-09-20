using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SistemaMercado.Models
{
    public class ProductPrice
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage ="The unitvalue field is required.")]
        [Column(TypeName = "decimal(6,2)")]
        [Range(0, double.MaxValue, ErrorMessage = "UnitValue must be greater than 0")]
        public decimal SaleUnitValue { get; set; }

        [Required(ErrorMessage = "The ProfitPercentage field is required.")]
        [Column(TypeName = "decimal(4,2)")]
        public decimal ProfitPercentage { get; set; }

        [Required(ErrorMessage = "The productId field is required.")]
        public Guid ProductId { get; set; }

        [JsonIgnore]
        public Product? Product { get; set; }

        public ProductPrice()
        {
        }

        public ProductPrice(Guid id, decimal saleUnitValue, decimal profitPercentage, Guid productId)
        {
            Id = id;
            SaleUnitValue = saleUnitValue;
            ProfitPercentage = profitPercentage;
            ProductId = productId;
        }

    }
}
