using SistemaMercado.Models;

namespace SistemaMercado.Service
{
    public class EmployeeService
    {
        public Decimal CalculateDailyPayment(Employee employee)
        {
            decimal hoursWorked = (decimal)employee.HoursWorked.TotalHours;
            decimal dailyPayment = hoursWorked * employee.HourlyPay;
            employee.DailyPayment = dailyPayment;
            return employee.DailyPayment;
        }

        public decimal CalculateMonthlyPayment(Employee employee)
        {
            Decimal monthlyPayment = employee.DailyPayment * employee.WorkedDays;
            return monthlyPayment;
        }
    }
}
