using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TPGamePlus.Domain;
using TPGamePlus.Domain.Entities;
using Console = TPGamePlus.Domain.Entities.Console;

namespace TPGamePlus.Data
{
    public class GamePlusDbContext: IdentityDbContext<ApplicationUser>
    {
        public DbSet<ProductInfo> ProductsInfos { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Compagny> Compagnies { get; set; }
        public DbSet<Console> Consoles { get; set; }
        public DbSet<Plateforme> Plateformes { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Product> Products { get; set; }

        public DbSet<CartItem> ShoppingCartItems { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }


        public DbSet<Order> Orders { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Received> Receiveds { get; set; }


        public GamePlusDbContext(DbContextOptions<GamePlusDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Seed();
            base.OnModelCreating(builder);
        }
    }
}
