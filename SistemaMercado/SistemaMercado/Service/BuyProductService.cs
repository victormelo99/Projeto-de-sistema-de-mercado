using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using SistemaMercado.Data;
using SistemaMercado.Models;
namespace SistemaMercado.Service
{
    public class BuyProductService
    {
        private readonly SistemaMercadoDbContext _context;

        public BuyProductService(SistemaMercadoDbContext context)
        {
            _context = context;
        }

        public void BuyProducts(Product product, BuyProduct buyProduct)
        {
            var inventory = _context.Inventory
                            .SingleOrDefault(x => x.ProductId == product.Id && x.ProductSize == product.ProductSize &&
                            x.Batch == buyProduct.Batch);

            if (inventory != null)
            {

                inventory.Amount += buyProduct.Amount;

                if (inventory.Amount < 0)
                {
                    inventory.Amount = 0;

                }

                inventory.BuyProductId = buyProduct.Id;

                _context.Inventory.Update(inventory);

            }
            else
            {
                var newInventory = new Inventory(
                    Guid.NewGuid(),
                    product.Name,
                    buyProduct.Amount,
                    buyProduct.ExpirationDate,
                    buyProduct.Id,
                    buyProduct.Batch,
                    product.ProductSize,
                    product.Id,
                    product.Barcode
                    );
                _context.Inventory.Add(newInventory);
            }
            _context.SaveChanges();
        }

        public void UpdateBuyProducts(Product product, BuyProduct buyProduct)
        {

            var inventory = _context.Inventory
                            .SingleOrDefault(x => x.ProductId == product.Id && x.ProductSize == product.ProductSize &&
                            x.Batch == buyProduct.Batch);

            var previousBuy = _context.BuyProduct.AsNoTracking().SingleOrDefault(x => x.Id == buyProduct.Id);

            var quantityDifference = buyProduct.Amount - previousBuy.Amount;

            inventory.Amount += quantityDifference;

            _context.Inventory.Update(inventory);
            _context.SaveChanges();

        }

        public void DeleteBuyProducts(Product product, BuyProduct buyProduct)
        {

            var inventory = _context.Inventory
                            .Where(x => x.ProductId == product.Id && x.ProductSize == product.ProductSize && x.Batch == buyProduct.Batch)
                            .FirstOrDefault();

            var deleteBuy = _context.BuyProduct.AsNoTracking().SingleOrDefault(x => x.Id == buyProduct.Id);

            inventory.Amount -= deleteBuy.Amount;

            if (inventory.Amount < 0)
            {
                inventory.Amount = 0;
            }

            _context.Inventory.Update(inventory);
            _context.SaveChanges();

        }
    }
}
