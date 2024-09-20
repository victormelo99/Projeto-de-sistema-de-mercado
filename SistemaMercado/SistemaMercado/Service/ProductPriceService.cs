using SistemaMercado.Data;
using SistemaMercado.Models;

namespace SistemaMercado.Service
{
    public class ProductPriceService
    {
        private readonly SistemaMercadoDbContext _context;

        public ProductPriceService(SistemaMercadoDbContext context)
        {
            _context = context;
        }

        public void CalculateSaleValue(ProductPrice price, BuyProduct buyProduct)
        {
            
            if (price == null || buyProduct == null)
            {
                throw new ArgumentNullException("Um ou mais parâmetros estão nulos.");
            }

            price.SaleUnitValue = buyProduct.CostValue + (buyProduct.CostValue * (price.ProfitPercentage/100));
            
            _context.SaveChanges();
        }
 
    }
}
