using Microsoft.EntityFrameworkCore;
using SistemaMercado.Models;
using System.Collections.Generic;
using System.Security.Principal;

namespace SistemaMercado.Data
{
    public class SistemaMercadoDbContext : DbContext
    {
        public SistemaMercadoDbContext(DbContextOptions<SistemaMercadoDbContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Account> Account { get; set; }
        public DbSet<Inventory> Inventory { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Sale> Sale { get; set; }
        public DbSet<Supplier> Supplier { get; set; }
        public DbSet<BuyProduct> BuyProduct { get; set; }
        public DbSet <ProductPrice> productPrice { get; set; }

    }
}
