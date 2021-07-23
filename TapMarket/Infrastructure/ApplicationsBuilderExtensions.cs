namespace TapMarket.Infrastructure
{
    using Microsoft.AspNetCore.Builder;
    using TapMarket.Data;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.EntityFrameworkCore;
    using TapMarket.Data.Models;
    using System.Linq;

    public static class ApplicationsBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(this IApplicationBuilder app)
        {
            using var scopedService = app.ApplicationServices.CreateScope();

            var data = scopedService.ServiceProvider.GetService<TapMarketDbContext>();

            data.Database.Migrate();

            SeedCategories(data);

            return app;
        }

        private static void SeedCategories(TapMarketDbContext data)
        {
            if (data.Categories.Any())
            {
                return;
            }

            data.Categories.AddRange(new[]
            { 
                new Category {Name = "RealEstate"},
                new Category {Name = "Vehicles"},
                new Category {Name = "Electronics"},
                new Category {Name = "Fashion"},
                new Category {Name = "Home"},
                new Category {Name = "Tourism"},
                new Category {Name = "Sports"},
                new Category {Name = "Jobs"},
                new Category {Name = "Hobby"},
                new Category {Name = "Other"}
            });

            data.SaveChanges();
        }
    }
}
