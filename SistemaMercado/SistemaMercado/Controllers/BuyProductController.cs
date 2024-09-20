using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaMercado.Data;
using SistemaMercado.Models;
using SistemaMercado.Service;

namespace SistemaMercado.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class BuyProductController : Controller
    {
        private readonly SistemaMercadoDbContext _context;
        private readonly BuyProductService _buyProduct;

        public BuyProductController(SistemaMercadoDbContext context, BuyProductService buyProduct)
        {
            _context = context;
            _buyProduct = buyProduct;
        }

        [HttpGet]
        public async Task<IActionResult> GetPurchases()
        {
            try
            {
                var result = await _context.BuyProduct.Include(x => x.Product).ToListAsync();
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest($"Error in purchases listing. Exception  {e.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostBuyProduct([FromBody] BuyProduct buyProduct)
        {
            try
            {
                var product = await _context.Product.FindAsync(buyProduct.ProductId);

                if (product == null)
                {
                    return NotFound("Product not found.");
                }

                await _context.BuyProduct.AddAsync(buyProduct);
                var result = await _context.SaveChangesAsync();

                _buyProduct.BuyProducts(product, buyProduct);

                if (result > 0)
                {
                    return Ok("Purchase finished:");
                }
                else
                {
                    return BadRequest("Purchase not finished");
                }

            }
            catch (Exception e)
            {
                return BadRequest($"Error when purchasing. Exception  {e.Message}");
            }
        }

        [HttpPut]
        public async Task<IActionResult> PutBuyProduct([FromBody] BuyProduct buyProduct, BuyProductService buyProductService)
        {
            try
            {
                var existingBuyProduct = await _context.BuyProduct.FindAsync(buyProduct.Id);

                if (existingBuyProduct == null)
                {
                    return NotFound("Purchase not found.");
                }

                var product = await _context.Product.FindAsync(buyProduct.ProductId);

                if (product == null)
                {
                    return NotFound("Product not found.");
                }

                existingBuyProduct.Batch = buyProduct.Batch;
                existingBuyProduct.ManufacturingDate = buyProduct.ManufacturingDate;
                existingBuyProduct.ExpirationDate = buyProduct.ExpirationDate;
                existingBuyProduct.Amount = buyProduct.Amount;
                existingBuyProduct.Barcode = buyProduct.Barcode;
                existingBuyProduct.CostValue = buyProduct.CostValue;
                existingBuyProduct.ProductId = buyProduct.ProductId;

                _buyProduct.UpdateBuyProducts(product, existingBuyProduct);

                _context.BuyProduct.Update(existingBuyProduct);

                var result = await _context.SaveChangesAsync();

                if (result > 0)
                {
                    return Ok("Update Purchase:");
                }
                else
                {
                    return BadRequest("Purchase not update.");
                }
            }
            catch (Exception e)
            {
                return BadRequest($"Error updating purchase. Exception  {e.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBuyProduct([FromRoute] Guid id)
        {
            try
            {
                BuyProduct buyProduct = await _context.BuyProduct.FindAsync(id);

                if (buyProduct != null)
                {

                    var product = await _context.Product.FindAsync(buyProduct.ProductId);

                    _buyProduct.DeleteBuyProducts(product, buyProduct);
                    _context.BuyProduct.Remove(buyProduct);
                    await _context.SaveChangesAsync();

                    return Ok("Buy cancelled and removed from inventory.");
                }
                else
                {
                    return NotFound("buy not found");
                }
            }
            catch (Exception e)
            {
                return BadRequest($"Error deleting buy. Exception  {e.Message}");
            }
        }
    }
}
