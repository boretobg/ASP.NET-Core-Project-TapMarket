using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TapMarket.Data.Models;

namespace TapMarket.Data
{
    public class TapMarketDbContext : IdentityDbContext
    {
        public TapMarketDbContext(DbContextOptions<TapMarketDbContext> options)
            : base(options)
        {
        }

        public DbSet<Item> Items { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Item>()
                .HasOne(c => c.Category)
                .WithMany(c => c.Items)
                .HasForeignKey(c => c.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
           
            base.OnModelCreating(builder);
            builder.Entity<Item>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,4)");
        }
    }
}
