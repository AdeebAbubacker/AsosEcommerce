using AsosEcommerceApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AsosEcommerceApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<ProductVariation> ProductVariations { get; set; }

        public DbSet<Cart> Carts { get; set; }

        public DbSet<CartItem> CartItems { get; set; }
    }
}
