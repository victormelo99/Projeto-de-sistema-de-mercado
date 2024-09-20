using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SistemaMercado.Models
{
    public class Sale
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The Quantity field is required.")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "The Barcode field is required.")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "The Barcode must be between 3 and 200 characters.")]
        public string Barcode { get; set; }

        [Required(ErrorMessage = "The SaleValue field is required.")]
        [Column(TypeName = "decimal(18,2)")]
        [Range(0, double.MaxValue, ErrorMessage = "value must be greater than 0")]
        public decimal SaleValueProduct { get; set; }

        [Column(TypeName = "decimal(6,2)")]
        public decimal TotalPricePerItem { get; set; }

        [Required]
        public Guid ProductId { get; set; }

        [JsonIgnore]
        public Product? Product { get; set; }

        public Sale()
        {
        }

        public Sale(Guid id, int quantity, string barcode, decimal saleValueProduct, decimal totalPricePerItem, Guid productId)
        {
            Id = id;
            Quantity = quantity;
            Barcode = barcode;
            SaleValueProduct = saleValueProduct;
            TotalPricePerItem = totalPricePerItem;
            ProductId = productId;
        }
    }
}
