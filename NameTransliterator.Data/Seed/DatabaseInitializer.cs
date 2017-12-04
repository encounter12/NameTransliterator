using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;

using NameTransliterator.Data.Context;
using NameTransliterator.Models.IdentityModels;

namespace NameTransliterator.Data.Seed
{
    public static class DatabaseInitializer
    {
        public static async Task SeedData(IServiceProvider serviceProvider)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceProvider.GetService<ApplicationDbContext>();

                var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();

                if (context.AllMigrationsApplied())
                {
                    await SeedDatabase(context, userManager);
                }
            }
        }

        public static async Task SeedDatabase(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            if (!context.Users.Any())
            {
                var applicationUser = new ApplicationUser
                {
                    UserName = "john.doe",
                    Email = "john.doe@gmail.com"
                };

                string password = "abc";

                IdentityResult result =
                        await userManager.CreateAsync(applicationUser, password);

                if (!result.Succeeded)
                {
                    throw new Exception("The user seed is not successful...");
                }

                context.SaveChanges();
            }
        }
    }
}
