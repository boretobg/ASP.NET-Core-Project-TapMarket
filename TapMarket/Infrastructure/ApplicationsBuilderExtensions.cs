namespace TapMarket.Infrastructure
{
    using Microsoft.AspNetCore.Builder;
    using TapMarket.Data;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.EntityFrameworkCore;
    using TapMarket.Data.Models;
    using System.Linq;
    using System;
    using Microsoft.AspNetCore.Identity;
    using System.Threading.Tasks;

    using static WebConstants;

    public static class ApplicationsBuilderExtensions
    {
        private const string moderatorEmail = "moderator@tm.com";
        private const string moderatorPassword = "moderator123";

        public static IApplicationBuilder PrepareDatabase(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var services = serviceScope.ServiceProvider;

            MigrateDatabase(services);

            SeedCategories(services);
            SeedConditions(services);
            SeedModerator(services);

            return app;
        }
        private static void MigrateDatabase(IServiceProvider services)
        {
            var data = services.GetRequiredService<TapMarketDbContext>();

            data.Database.Migrate();
        }

        private static void SeedModerator(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<User>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            var data = services.GetRequiredService<TapMarketDbContext>();

            Task
                .Run(async () =>
                {
                    if (await roleManager.RoleExistsAsync(ModeratorRoleName))
                    {
                        return;
                    }

                    var role = new IdentityRole { Name = ModeratorRoleName };

                    await roleManager.CreateAsync(role);
                    
                    var user = new User
                    {
                        Email = moderatorEmail,
                        UserName = moderatorEmail
                    };

                    await userManager.CreateAsync(user, moderatorPassword);

                    await userManager.AddToRoleAsync(user, role.Name);
                })
                .GetAwaiter()
                .GetResult();
        }

        private static void SeedCategories(IServiceProvider services)
        {
            var data = services.GetRequiredService<TapMarketDbContext>();

            if (data.Categories.Any())
            {
                return;
            }

            data.Categories.AddRange(new[]
            {
                new Category {Name = "RealEstate"},
                new Category {Name = "Vehicles"},
                new Category {Name = "Hobby"},
                new Category {Name = "Electronics"},
                new Category {Name = "Home"},
                new Category {Name = "Tourism"},
                new Category {Name = "Sports"},
                new Category {Name = "Jobs"},
                new Category {Name = "Fashion"},
                new Category {Name = "Other"}
            });

            data.SaveChanges();
        }

        private static void SeedConditions(IServiceProvider services)
        {
            var data = services.GetRequiredService<TapMarketDbContext>();

            if (data.Conditions.Any())
            {
                return;
            }

            data.Conditions.AddRange(new[]
            {
                new Condition {Name = "New" },
                new Condition {Name = "Used" },
                new Condition {Name = "Good" },
                new Condition {Name = "Bad" },
                new Condition {Name = "None" }
            });

            data.SaveChanges();
        }
    }
}
