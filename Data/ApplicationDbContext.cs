using CoreArchitecture.Models;
using Microsoft.EntityFrameworkCore;

namespace CoreArchitecture.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductPrice> ProductPrices { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
};