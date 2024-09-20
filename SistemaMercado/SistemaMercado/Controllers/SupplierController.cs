using SistemaMercado.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaMercado.Models;

namespace SistemaMercado.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SupplierController : Controller
    {
        private readonly SistemaMercadoDbContext _context;

        public SupplierController(SistemaMercadoDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetSupplier()
        {
            try
            {
                var result = await _context.Supplier.ToListAsync();
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest($"Error in Supplier listing. Exception  {e.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostSupplier([FromBody] Supplier supplier)
        {
            try
            {

                await _context.Supplier.AddAsync(supplier);
                var result = await _context.SaveChangesAsync();
                if (result == 1)
                {
                    return Ok("Inserted supplier");
                }
                else
                {
                    return BadRequest("Supplier not included");
                }
            }
            catch (Exception e)
            {
                return BadRequest($"Error in supplier insert. Exception {e.Message}");
            }
        }

        [HttpPut]
        public async Task<IActionResult> PutSupplier([FromBody] Supplier supplier)
        {
            try
            {
                _context.Supplier.Update(supplier);
                var result = await _context.SaveChangesAsync();
                if (result == 1)
                {
                    return Ok("Updated supplier");
                }
                else
                {
                    return BadRequest("Supplier not updated");
                }
            }
            catch (Exception e)
            {
                return BadRequest($"Error updating supplier. Exception {e.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSupplier([FromRoute] Guid id)
        {
            try
            {
                Supplier supplier = await _context.Supplier.FindAsync(id);
                if (supplier != null)
                {
                    _context.Supplier.Remove(supplier);
                    var result = await _context.SaveChangesAsync();
                    if (result == 1)
                    {
                        return Ok("Supplier deleted.");
                    }
                    else
                    {
                        return BadRequest("Supplier not deleted");
                    }
                }
                else
                {
                    return NotFound("Supplier not found");
                }
            }
            catch (Exception e)
            {
                return BadRequest($"Error deleting supplier. Exception  {e.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> SupplierSearch([FromRoute] Guid id)
        {
            try
            {
                Supplier supplier = await _context.Supplier.FindAsync(id);
                if (supplier != null)
                {
                    return Ok(supplier);
                }
                else
                {
                    return NotFound("Supplier not found");
                }
            }
            catch (Exception e)
            {
                return BadRequest($"Error in search supplier. Exception  {e.Message}");
            }
        }
    }
}
