using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using SistemaMercado.Data;
using SistemaMercado.Models;

namespace SistemaMercado.Service
{
    public class SalesService
    {
        private readonly SistemaMercadoDbContext _context;
        private readonly ProductPriceService _productPriceService;

        public SalesService(SistemaMercadoDbContext context, ProductPriceService productPrice)
        {
            _context = context;
            _productPriceService = productPrice;
        }

        public void AddSales(Sale sale)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var item = _context.Inventory.SingleOrDefault(x => x.Barcode == sale.Barcode);

                    if (item != null)
                    {
                        if (item.Amount >= sale.Quantity)
                        {
                            _context.Entry(item).State = EntityState.Modified;
                            item.Amount -= sale.Quantity;
                            _context.Inventory.Update(item);


                            var productPrice = _context.productPrice.SingleOrDefault(y => y.ProductId == item.ProductId);
                            if (productPrice == null)
                            {
                                throw new InvalidOperationException("Price not found for product.");
                            }

                            sale.ProductId = item.ProductId;
                            sale.SaleValueProduct = productPrice.SaleUnitValue;
                            sale.TotalPricePerItem = sale.SaleValueProduct * sale.Quantity;

                            _context.Sale.Add(sale);
                            _context.SaveChanges();
                            
                            transaction.Commit();
                        }
                        else
                        {
                            throw new InvalidOperationException("Insufficient inventory to make the sale.");
                        }
                    }
                    else
                    {
                        throw new InvalidOperationException("Product not found.");
                    }
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw new InvalidOperationException("Failed sale, erro: " + e.Message);
                }
            }
        }

        public void CancelSale(Sale sale)
        {
            try
            {
                var item= _context.Sale.FirstOrDefault(x=> x.Id == sale.Id);

                if (item != null)
                {
                    var inventory = _context.Inventory.FirstOrDefault(y => y.ProductId == item.ProductId);

                    if (inventory != null)
                    {
                        inventory.Amount += item.Quantity;
                        _context.Inventory.Update(inventory);
                        _context.SaveChanges();
                    }
                    else
                    {
                        throw new InvalidOperationException("Inventory not found for the product.");
                    }
                }
            }
            catch(Exception e)
            {
                throw new InvalidOperationException("Error in the sale process. Exception: " + e.Message);
            }
        }
    }
}
