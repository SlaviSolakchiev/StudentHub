using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using StudentHub.Common;
using StudentHub.Data.Models;
using System;
using System.Threading.Tasks;

namespace StudentHub.Data.Seeding
{
    public class ApplicationUserSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {

            await SeedUser(serviceProvider, new ApplicationUser() { UserName = "slaviadmin@abv.bg", Email = "slaviadmin@abv.bg" }, "slavi123", GlobalConstants.AdministratorRoleName);
        }

        private static async Task SeedUser(IServiceProvider serviceProvider, ApplicationUser user, string password,  string role)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            var result = await userManager.FindByEmailAsync(user.Email);

            if (result == null)
            {
                await userManager.CreateAsync(user, password);

                await userManager.AddToRoleAsync(user, role);
            }
        }
    }
}
