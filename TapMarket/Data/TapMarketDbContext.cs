using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TapMarket.Data.Models;

namespace TapMarket.Data
{
    public class TapMarketDbContext : IdentityDbContext
    {
        public TapMarketDbContext(DbContextOptions<TapMarketDbContext> options)
            : base(options)
        {
        }

        public DbSet<Listing> Listings { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Listing>()
                .HasOne(c => c.Category)
                .WithMany(c => c.Listings)
                .HasForeignKey(c => c.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Listing>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,0)");

            base.OnModelCreating(builder);
        }
    }
}
