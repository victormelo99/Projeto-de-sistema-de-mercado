using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace SistemaMercado.Models
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The Name field is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "The name must be between 3 and 50 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The Category field is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "The Category must be between 3 and 50 characters.")]
        public string Category { get; set; }

        [Required(ErrorMessage = "The Barcode field is required.")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "The Barcode must be between 3 and 200 characters.")]
        public string Barcode { get; set; }

        [Required(ErrorMessage = "The ProductSize field is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Product size must be greater than 0.")]
        public decimal ProductSize { get; set; }

        [Required(ErrorMessage = "The UnitOfMeasure field is required.")]
        public UnitType UnitOfMeasure { get; set; }

        [ForeignKey("Supplier")]
        public Guid SupplierId { get; set; }

        [JsonIgnore]
        public Supplier? Supplier { get; set; }

        public Product()
        {
        }

        public Product(Guid id, string name, string category, string barcode, decimal productSize, UnitType unitOfMeasure, Guid supplierId)
        {
            Id = id;
            Name = name;
            Category = category;
            Barcode = barcode;
            ProductSize = productSize;
            UnitOfMeasure = unitOfMeasure;
            SupplierId = supplierId;
        }
    }
}
