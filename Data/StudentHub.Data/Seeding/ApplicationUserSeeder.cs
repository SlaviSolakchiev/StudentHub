namespace StudentHub.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using StudentHub.Common;
    using StudentHub.Data.Common.Repositories;
    using StudentHub.Data.Models;

    public class ApplicationUserSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            await SeedUser(serviceProvider, new ApplicationUser() { UserName = "slaviadmin@abv.bg", Email = "slaviadmin@abv.bg" }, "slavi123", GlobalConstants.AdministratorRoleName);
        }

        private static async Task SeedUser(IServiceProvider serviceProvider, ApplicationUser user, string password, string role)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            var result = await userManager.FindByEmailAsync(user.Email);

            if (result == null)
            {
                await userManager.CreateAsync(user, password);

                await userManager.AddToRoleAsync(user, role);
            }
        }

        private static async Task SeedTeacherUser(IServiceProvider serviceProvider, ApplicationUser user, string password, string role, Teacher teacherInfo)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var teachersRepository = serviceProvider.GetRequiredService<IDeletableEntityRepository<Teacher>>();

            var result = await userManager.FindByEmailAsync(user.Email);

            if (result == null)
            {
                await userManager.CreateAsync(user, password);

                await userManager.AddToRoleAsync(user, role);

                if (!teachersRepository.AllAsNoTracking().Any(x => x.FullName == teacherInfo.FullName))
                {
                    await teachersRepository.AddAsync(teacherInfo);
                }
            }
        }
    }
}
