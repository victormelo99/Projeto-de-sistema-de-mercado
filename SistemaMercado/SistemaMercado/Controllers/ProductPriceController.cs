using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaMercado.Data;
using SistemaMercado.Models;
using SistemaMercado.Service;

namespace SistemaMercado.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductPriceController : Controller
    {
        private readonly SistemaMercadoDbContext _context;

        public ProductPriceController(SistemaMercadoDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> GetPrice()
        {
            try
            {
                var result = await _context.productPrice.ToListAsync();
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest($"Error when listing product price. Exception  {e.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostPrice([FromBody] ProductPrice productPrice)
        {
            try
            {
                var productPriceService = new ProductPriceService(_context);
                var buyProduct = _context.BuyProduct.FirstOrDefault(bp => bp.ProductId == productPrice.ProductId);
                productPriceService.CalculateSaleValue(productPrice, buyProduct);

                await _context.productPrice.AddAsync(productPrice);
                var result = await _context.SaveChangesAsync();
                
                if (result == 1)
                {
                    return Ok("Inserted price");
                }
                else
                {
                    return BadRequest("Price not included");
                }
            }
            catch (Exception e)
            {
                return BadRequest($"Error in price insert. Exception  {e.Message}");
            }
        }

        [HttpPut]
        public async Task<IActionResult> PutPrice([FromBody] ProductPrice productPrice)
        {
            try
            {
                var productPriceService = new ProductPriceService(_context);
                var buyProduct = _context.BuyProduct.FirstOrDefault(bp => bp.ProductId == productPrice.ProductId);
                productPriceService.CalculateSaleValue(productPrice, buyProduct);
                 _context.productPrice.Update(productPrice);
                var result = await _context.SaveChangesAsync();

                if (result == 1)
                {
                    return Ok("Updated price");
                }
                else
                {
                    return BadRequest("Price not updated");
                }
            }
            catch (Exception e)
            {
                return BadRequest($"Error when updating price. Exception  {e.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrice([FromRoute] Guid id)
        {
            try
            {
                ProductPrice productPrice = await _context.productPrice.FindAsync(id);

                if (productPrice != null)
                {
                    _context.productPrice.Remove(productPrice);
                    await _context.SaveChangesAsync();
                    return Ok("Price deleted");
                }
                else
                {
                    return NotFound("Price not deleted");
                }
            }catch (Exception e)
            {
                return BadRequest("Error when excluded price. "+ e.Message);
            }
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> SearchProduct([FromRoute] Guid productId)
        {
            try
            {
                var productPrice = await (from x in _context.productPrice
                                          join y in _context.Product on x.ProductId equals y.Id
                                          where y.Id == productId
                                          select new
                                          {
                                              ProductName = y.Name,
                                              Price = x.SaleUnitValue
                                          }).FirstOrDefaultAsync();

                if (productPrice != null)
                {
                    return Ok(productPrice);
                }
                else
                {
                    return NotFound("Price not found for this product");
                }
            }
            catch (Exception e)
            {
                return BadRequest("Error when finding price. "+ e.Message);
            }
        }
    }
}
