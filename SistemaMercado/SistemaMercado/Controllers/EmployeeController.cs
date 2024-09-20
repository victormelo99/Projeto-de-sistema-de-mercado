using SistemaMercado.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaMercado.Models;
using SistemaMercado.Service;

namespace SistemaMercado.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : Controller
    {
        private readonly SistemaMercadoDbContext _context;

        public EmployeeController(SistemaMercadoDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployee()
        {
            try
            {
                var result = await _context.Employees.ToListAsync();
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest($"Error in employee listing. Exception  {e.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostEmployee([FromBody] Employee employee)
        {
            try
            {
                var employeeService = new EmployeeService();
                employeeService.CalculateDailyPayment(employee);
                employee.MonthlyPayment = employeeService.CalculateMonthlyPayment(employee);

                await _context.Employees.AddAsync(employee);
                var result = await _context.SaveChangesAsync();
                if (result == 1)
                {
                    return Ok("Inserted employee");
                }
                else
                {
                    return BadRequest("Employee not included");
                }
            }
            catch (Exception e)
            {
                return BadRequest($"Error in employee insert. Exception  {e.Message}");
            }
        }

        [HttpPut]
        public async Task<IActionResult> PutEmployee([FromBody] Employee employee)
        {
            try
            {
                var employeeService = new EmployeeService();
                employeeService.CalculateDailyPayment(employee);
                employee.MonthlyPayment = employeeService.CalculateMonthlyPayment(employee);

                _context.Employees.Update(employee);
                var result = await _context.SaveChangesAsync();
                if (result == 1)
                {
                    return Ok("Updated employee");
                }
                else
                {
                    return BadRequest("Employee not updated");
                }
            }
            catch (Exception e)
            {
                return BadRequest($"Error updating employee. Exception  {e.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] Guid id)
        {
            try
            {
                Employee employee = await _context.Employees.FindAsync(id);
                if (employee != null)
                {
                    _context.Employees.Remove(employee);
                    var result = await _context.SaveChangesAsync();
                    if (result == 1)
                    {
                        return Ok("Employee deleted.");
                    }
                    else
                    {
                        return BadRequest("Employee not deleted");
                    }
                }
                else
                {
                    return NotFound("Employee not found");
                }
            }
            catch (Exception e)
            {
                return BadRequest($"Error deleting employee. Exception  {e.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> EmployeeSearch([FromRoute] Guid id)
        {
            try
            {
                Employee employee = await _context.Employees.FindAsync(id);
                if (employee != null)
                {
                    return Ok(employee);
                }
                else
                {
                    return NotFound("Employee not found");
                }
            }
            catch (Exception e)
            {
                return BadRequest($"Error in search employee. Exception  {e.Message}");
            }
        }
    }
}
