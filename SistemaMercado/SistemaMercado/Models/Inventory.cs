using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SistemaMercado.Models
{
    public class Inventory
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The Description Field is Required")]
        [StringLength(150, MinimumLength = 3, ErrorMessage = "The Description must be between 3 and 200 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "The Batch field is required.")]
        public string Batch { get; set; }

        [Required(ErrorMessage = "The Barcode field is required.")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "The Barcode must be between 3 and 200 characters.")]
        public string Barcode { get; set; }

        [Required(ErrorMessage = "The Amount Field is Required")]
        public int Amount { get; set; }

        [Required(ErrorMessage = "The ExpirationDate field is required.")]
        public DateTime ExpirationDate { get; set; }

        [Required(ErrorMessage = "The ProductSize field is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Product size must be greater than 0.")]
        public decimal ProductSize { get; set; }

        [JsonIgnore]
        public BuyProduct? buyProduct { get; set; }

        public Guid BuyProductId { get; set; }

        [Required]
        public Guid ProductId { get; set; }

        [JsonIgnore]
        public Product? Product { get; set; }

        public Inventory()
        {
        }
        public Inventory(Guid id, string description, int amount, DateTime expirationDate, Guid buyProductId,string batch,
            decimal productSize, Guid productId, string barcode)
        {
            Id = id;
            Description = description;
            Batch = batch;
            Amount = amount;
            ExpirationDate = expirationDate;
            BuyProductId = buyProductId;
            ProductSize = productSize;
            ProductId = productId;
            Barcode = barcode;
        }
    }
}
