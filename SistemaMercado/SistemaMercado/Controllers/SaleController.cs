using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaMercado.Data;
using SistemaMercado.Migrations;
using SistemaMercado.Models;
using SistemaMercado.Service;

namespace SistemaMercado.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SaleController : Controller
    {
        private readonly SistemaMercadoDbContext _context;
        private readonly SalesService _salesService;

        public SaleController(SistemaMercadoDbContext context, SalesService salesService)
        {
            _context = context;
            _salesService = salesService;
        }

        [HttpGet]
        public async Task<IActionResult> GetSale()
        {
            try
            {
                var result = await _context.Sale.ToListAsync();
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest("Error in listing sale. Exception " + e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddSale([FromBody] Sale sale)
        {
            try
            {
                _salesService.AddSales(sale);
                return Ok("Sale concluded.");

            }
            catch (InvalidOperationException e)
            {
                return BadRequest("Sale not concluded. Exception: " + e.Message);
            }
            catch (Exception e)
            {
                return NotFound("Error in completing the sale. Exception: " + e.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSale([FromBody] Sale updatedSale)
        {
            try
            {
                var saleRecord = await _context.Sale.FindAsync(updatedSale.Id);

                if (saleRecord == null)
                {
                    return NotFound("Sale not found.");
                }

                var quantityDifference = updatedSale.Quantity - saleRecord.Quantity;

                saleRecord.Quantity = updatedSale.Quantity;

                var item = await _context.Inventory.SingleOrDefaultAsync(x => x.ProductId == saleRecord.ProductId);

                if (item != null)
                {
                    var productPrice = await _context.productPrice.SingleOrDefaultAsync(y => y.ProductId == saleRecord.ProductId);

                    if (productPrice != null)
                    {
                        saleRecord.SaleValueProduct = productPrice.SaleUnitValue;
                        saleRecord.TotalPricePerItem = saleRecord.SaleValueProduct * saleRecord.Quantity;

                        item.Amount -= updatedSale.Quantity;

                        _context.Inventory.Update(item);
                    }
                    else
                    {
                        return BadRequest("Product price not found.");
                    }
                }
                else
                {
                    return BadRequest("Product not found in inventory");
                }

                _context.Sale.Update(saleRecord);
                await _context.SaveChangesAsync();

                return Ok("Sale updated successfully.");

            }
            catch (InvalidOperationException e)
            {
                return BadRequest("Update failed. Exception: " + e.Message);
            }
            catch (Exception e)
            {
                return NotFound("Error in completing update. Exception: " + e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSale([FromRoute] Guid id)
        {
            try
            {
                Sale sale = await _context.Sale.FindAsync(id);

                if (sale != null)
                {
                    _salesService.CancelSale(sale);

                    _context.Sale.Remove(sale);

                    var result = await _context.SaveChangesAsync();

                    if (result == 1)
                    {
                        return Ok("Sale deleted.");
                    }
                    else
                    {
                        return BadRequest("Sale not deleted");
                    }
                }
                else
                {
                    return NotFound("Sale not found");
                }
            }
            catch (Exception e)
            {
                return BadRequest($"Error deleting Sale. Exception  {e.Message}");
            }
        }

    }
}
