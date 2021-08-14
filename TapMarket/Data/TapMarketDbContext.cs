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
        public DbSet<User> User { get; set; }
        public DbSet<Condition> Conditions { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Favorite> Favorites { get; set; }

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
                .HasOne(c => c.User)
                .WithMany(c => c.Listings)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Listing>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,0)");

            builder
                .Entity<Message>()
                .HasOne(m => m.Sender)
                .WithMany(s => s.Messages)
                .HasForeignKey(m => m.SenderId);

            base.OnModelCreating(builder);
        }
    }
}
