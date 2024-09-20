using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SistemaMercado.Models
{
    public class Employee
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The Name field is required.")]
        [StringLength(150, MinimumLength = 3, ErrorMessage = "The name must be between 3 and 50 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The CPF field is required.")]
        public string CPF { get; set; }

        [EmailAddress(ErrorMessage = "The Email field is not a valid email address.")]
        public string? Email { get; set; }

        [Phone(ErrorMessage = "The Celphone field is not a valid phone number.")]
        public string? Celphone { get; set; }

        [Required(ErrorMessage = "The Sex field is required.")]
        public char Sex { get; set; }

        [Required(ErrorMessage = "The JobTitle is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "The Job title must be between 3 and 50 characters.")]
        public string JobTitle { get; set; }

        [Required(ErrorMessage = " The HourlyPay field is required.")]
        [Column(TypeName = "decimal(6,2)")]
        [Range(0, double.MaxValue, ErrorMessage = "HourlyPay must be greater than 0")]
        public decimal HourlyPay { get; set; }

        [Required(ErrorMessage = "The HoursWorked field is required. ")]
        public TimeSpan HoursWorked { get; set; }

        [Required(ErrorMessage = "The WorkedDays field is required.")]
        public uint WorkedDays { get; set; }

        public decimal DailyPayment { get; set; }

        public decimal MonthlyPayment { get; set; }

        public Employee()
        {
        }

        public Employee(Guid id, decimal hourlyPay, TimeSpan hoursWorked, uint workedDays, string jobtitle)
        {
            Id = id;
            HourlyPay = hourlyPay;
            HoursWorked = hoursWorked;
            WorkedDays = workedDays;
        }
    }
}
