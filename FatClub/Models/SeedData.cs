using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FatClub.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new FatClubContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<FatClubContext>>()))
            {
                // Look for any movies.
                if (context.Food.Any())
                {
                    return;   // DB has been seeded
                }

                context.Food.AddRange(
                    new Food
                    {
                        Name = "When Harry Met Sally",
                        Price = 7.99M
                    },

                    new Food
                    {
                        Name = "Ghostbusters ",
                        Price = 8.99M
                    },

                    new Food
                    {
                        Name = "Ghostbusters 2",
                        Price = 9.99M
                    },

                    new Food
                    {
                        Name = "Rio Bravo",
                        Price = 3.99M
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
