using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using TestProject.WebAPI.Models;

namespace TestProject.WebAPI.SeedData
{
    public static class SeedData
    {
        public static void Populate(Microsoft.AspNetCore.Builder.IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                Seed(serviceScope.ServiceProvider.GetService<DataContext>());
            }
        }

        public static void Seed(DataContext context)
        {
            context.Database.Migrate();
            if (!context.Users.Any())
            {
                var user = new User()
                {
                    Email = "peter@rizk.co",
                    Expenses = 4000,
                    Salary = 8000
                };
                context.AddRange(
                    user,
                    new User()
                    {
                        Email = "a@b.co",
                        Expenses = 3000,
                        Salary = 7000
                    }
                    );

                context.Add(
                 new Account()
                 {
                     Address = "12 Henderson st",
                     Phone = "0400283777",
                     Points = 11,
                     User = user
                 });

                context.SaveChanges();
            }
        }
    }
}
