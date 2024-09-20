using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaMercado.Data;
using SistemaMercado.Models;
using SistemaMercado.Service;

namespace SistemaMercado.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly SistemaMercadoDbContext _context;

        public ProductController(SistemaMercadoDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetProduct()
        {
            try
            {
                var result = await _context.Product.ToListAsync();
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest($"Error in product listing. Exception  {e.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostProduct([FromBody] Product product)
        {
            try
            {
                await _context.Product.AddAsync(product);
                var result = await _context.SaveChangesAsync();

                if (result > 0)
                {
                    return Ok("Inserted product");
                }
                else
                {
                    return BadRequest("Product not included");
                }

            }
            catch (Exception e)
            {
                return BadRequest($"Error in product insert. Exception  {e.Message}");
            }
        }

        [HttpPut]
        public async Task<IActionResult> PutProduct([FromBody] Product product)
        {
            try
            {
                _context.Product.Update(product);
                var result = await _context.SaveChangesAsync();

                if (result > 0)
                {
                    return Ok("Updated product");
                }
                else
                {
                    return BadRequest("Product not updated.");
                }
            }
            catch (Exception e)
            {
                return BadRequest($"Error updating product. Exception  {e.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] Guid id)
        {
            try
            {
                Product product = await _context.Product.FindAsync(id);
                if (product != null)
                {
                    _context.Product.Remove(product);
                    var result = await _context.SaveChangesAsync();
                    if (result == 1)
                    {
                        return Ok("Product deleted.");
                    }
                    else
                    {
                        return BadRequest("Product not deleted");
                    }
                }
                else
                {
                    return NotFound("Product not found");
                }
            }
            catch (Exception e)
            {
                return BadRequest($"Error deleting product. Exception  {e.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ProductSearch([FromRoute] Guid id)
        {
            try
            {
                Product product = await _context.Product.FindAsync(id);
                if (product != null)
                {
                    return Ok(product);
                }
                else
                {
                    return NotFound("Product not found");
                }
            }
            catch (Exception e)
            {
                return BadRequest($"Error in search Product. Exception  {e.Message}");
            }
        }
    }
}
