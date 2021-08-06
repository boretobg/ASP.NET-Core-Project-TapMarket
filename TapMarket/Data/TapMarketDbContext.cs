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
        public DbSet<Condition> Conditions { get; set; }
        public DbSet<Favorites> Favorites { get; set; }
        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Listing>()
                .HasOne(c => c.Category)
                .WithMany(c => c.Listings)
                .HasForeignKey(c => c.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Listing>()
                .HasOne(c => c.Condition)
                .WithMany(c => c.Listings)
                .HasForeignKey(c => c.ConditionId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Listing>()
                .HasOne(c => c.Customer)
                .WithMany(c => c.Listings)
                .HasForeignKey(c => c.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Listing>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,0)");

            builder
                .Entity<Customer>()
                .HasOne<IdentityUser>()
                .WithOne()
                .HasForeignKey<Customer>(c => c.Id)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Message>()
                .HasOne(c => c.Sender)
                .WithMany(m => m.Messages)
                .HasForeignKey(c => c.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Favorites>()
                .HasNoKey();

            base.OnModelCreating(builder);
        }
    }
}
