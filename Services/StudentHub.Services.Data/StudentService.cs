namespace StudentHub.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using NPOI.SS.Formula.Functions;
    using StudentHub.Data.Common.Repositories;
    using StudentHub.Data.Models;
    using StudentHub.Services.Mapping;

    public class StudentService : IStudentService
    {
        private readonly IDeletableEntityRepository<Student> studentsRepository;
        private readonly IRepository<Image> imagesRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationRole> roleManager;

        public StudentService(IDeletableEntityRepository<Student> studentsRepository, IRepository<Image> imagesRepository, IDeletableEntityRepository<ApplicationUser> usersRepository, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            this.studentsRepository = studentsRepository;
            this.imagesRepository = imagesRepository;
            this.usersRepository = usersRepository;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task CreateStudentAsync(string firstName, string lastName, int age, string accountId, Image image)
        {
            var student = new Student()
            {
                FirstName = firstName,
                LastName = lastName,
                Age = age,
                UserAccountId = accountId,
                Image = image,
            };

            await this.studentsRepository.AddAsync(student);
            await this.studentsRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllStudents<T>()
        {
            var list = this.studentsRepository.AllAsNoTracking()
     .OrderBy(x => x.Id)
     .To<T>()
     .ToListAsync();

            return await list;
        }
    }
}
