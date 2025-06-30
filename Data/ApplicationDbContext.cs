using AuthenticationApi.Models;
using CoreArchitecture.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationApi.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductPrice> ProductPrices { get; set; }
        public DbSet<TestModel> TestModels { get; set; }
        public DbSet<TestTestModel> TestTestModels { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
};