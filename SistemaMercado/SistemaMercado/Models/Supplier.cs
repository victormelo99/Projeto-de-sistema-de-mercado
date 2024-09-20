using System.ComponentModel.DataAnnotations;

namespace SistemaMercado.Models
{
    public class Supplier
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The Name Field is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "The name must be between 3 and 50 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The CNPJ field is required.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "The CNPJ must be between 3 and 100 characters.")]
        public string CNPJ { get; set; }

        [Required(ErrorMessage = "The Description field is required.")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "The description must be between 3 and 200 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "The Email field is required")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The Phone field is required")]
        [Phone(ErrorMessage = "Invalid Phone")]
        public string Phone { get; set; }

        [StringLength(200, MinimumLength = 3, ErrorMessage = "The Adress must be between 3 and 200 characters.")]
        public string? Adress { get; set; }

        public Supplier(Guid id, string name, string cNPJ, string description, string email, string phone, string? adress)
        {
            Id = id;
            Name = name;
            CNPJ = cNPJ;
            Description = description;
            Email = email;
            Phone = phone;
            Adress = adress;
        }
    }
}
