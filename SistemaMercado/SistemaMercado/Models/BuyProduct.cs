using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SistemaMercado.Models
{
    public class BuyProduct
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The Batch field is required.")]
        public string Batch { get; set; }

        [Required(ErrorMessage = "The ManufacturingDate field is required.")]
        public DateTime ManufacturingDate { get; set; }

        [Required(ErrorMessage = "The ExpirationDate field is required.")]
        public DateTime ExpirationDate { get; set; }

        [Required(ErrorMessage = "The Amount field is required.")]
        public int Amount { get; set; }

        [Required(ErrorMessage = "The Barcode field is required.")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "The Barcode must be between 3 and 200 characters.")]
        public string Barcode { get; set; }

        [Required(ErrorMessage = "The CostValue field is required.")]
        [Column(TypeName = "decimal(18,2)")]
        [Range(0, double.MaxValue, ErrorMessage = "Cost value must be greater than 0.")]
        public decimal CostValue { get; set; }

        public Guid ProductId { get; set; }

        [JsonIgnore]
        public Product? Product { get; set; }

        public BuyProduct()
        {
        }

        public BuyProduct(Guid id, string batch, DateTime manufacturingDate, DateTime expirationDate, int amount, 
            string barcode, decimal costValue, Guid productId)
        {
            Id = id;
            Batch = batch;
            ManufacturingDate = manufacturingDate;
            ExpirationDate = expirationDate;
            Amount = amount;
            Barcode = barcode;
            CostValue = costValue;
            ProductId = productId;
        }
    }
}