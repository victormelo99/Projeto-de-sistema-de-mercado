using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaMercado.Data;
using SistemaMercado.Models;
using SistemaMercado.Service;

namespace SistemaMercado.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryController : Controller
    {
        private readonly SistemaMercadoDbContext _context;

        public InventoryController(SistemaMercadoDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetInventory()
        {
            try
            {
                var result = await _context.Inventory.ToListAsync();
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest($"Error in Inventory listing. Exception  {e.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostInventory([FromBody] Inventory inventory)
        {
            try
            {

                await _context.Inventory.AddAsync(inventory);       
                var result = await _context.SaveChangesAsync();
                if (result == 1)
                {
                    return Ok("Inserted inventory");
                }
                else
                {
                    return BadRequest("Inventory not included");
                }
            }
            catch (Exception e)
            {
                return BadRequest($"Error in inventory insert. Exception  {e.Message}");
            }
        }

        [HttpPut]
        public async Task<IActionResult> PutInventory([FromBody] Inventory inventory)
        {
            try
            {

                _context.Inventory.Update(inventory);
                var result = await _context.SaveChangesAsync();
                if (result == 1)
                {
                    return Ok("Updated inventory");
                }
                else
                {
                    return BadRequest("inventory not updated");
                }
            }
            catch (Exception e)
            {
                return BadRequest($"Error updating inventory. Exception  {e.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInventory([FromRoute] Guid id)
        {
            try
            {
                Inventory inventory = await _context.Inventory.FindAsync(id);
                if (inventory != null)
                {
                    _context.Inventory.Remove(inventory);
                    var result = await _context.SaveChangesAsync();

                    if (result == 1)
                    {
                        return Ok("Inventory deleted.");
                    }
                    else
                    {
                        return BadRequest("Inventory not deleted");
                    }
                }
                else
                {
                    return NotFound("Inventory not found");
                }
            }
            catch (Exception e)
            {
                return BadRequest($"Error deleting inventory. Exception  {e.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> InventorySearch([FromRoute] Guid id)
        {
            try
            {
                Inventory inventory = await _context.Inventory.FindAsync(id);
                if (inventory != null)
                {
                    return Ok(inventory);
                }
                else
                {
                    return NotFound("Inventory not found");
                }
            }
            catch (Exception e)
            {
                return BadRequest($"Error in search inventory. Exception  {e.Message}");
            }
        }
    }
}
