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
    using StudentHub.Web.ViewModels.Administration.Dashboard;
    using StudentHub.Web.ViewModels.Student;
    using StudentHub.Web.ViewModels.Teacher;

    public class StudentService : IStudentService
    {
        private readonly IDeletableEntityRepository<Student> studentsRepository;
        private readonly IRepository<Image> imagesRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly IRepository<StudentsCourses> studentsCoursesRepository;

        public StudentService(IDeletableEntityRepository<Student> studentsRepository, IRepository<Image> imagesRepository, IDeletableEntityRepository<ApplicationUser> usersRepository, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, IRepository<StudentsCourses> studentsCoursesRepository)
        {
            this.studentsRepository = studentsRepository;
            this.imagesRepository = imagesRepository;
            this.usersRepository = usersRepository;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.studentsCoursesRepository = studentsCoursesRepository;
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

        public async Task<T> GetByIdAsync<T>(int id)
        {
            var std = await this.studentsRepository.All()
                .Where(x => x.Id == id)
                .To<T>().FirstOrDefaultAsync();

            return std;
        }

        public async Task UpdateAsync(int id, EditStudentViewModel input)
        {
            var user = await this.userManager.FindByIdAsync(input.UserAccountId);
            List<string> roles = new List<string>();

            if (input.SelectedRoles != null && input.SelectedRoles.Count() > 0)
            {
                if (input.SelectedRoles.Any(x => x != null))
                {
                    roles = input.SelectedRoles.FirstOrDefault().Split(',', System.StringSplitOptions.RemoveEmptyEntries).ToList();
                }
            }

            var std = this.studentsRepository.All().FirstOrDefault(x => x.Id == id);
            if (input != null)
            {
                std.FirstName = input.FirstName;
                std.LastName = input.LastName;
                std.Age = input.Age;
                std.ImageId = input.ImageId;
                std.UserAccountId = user.Id;


                if (user != null || roles != null)
                {
                    await this.userManager.RemoveFromRolesAsync(user, this.userManager.GetRolesAsync(user).Result.ToList());
                    await this.userManager.AddToRolesAsync(user, roles);
                    await this.studentsRepository.SaveChangesAsync();
                }
            }

        }

        public async Task DeleteAsync(int id)
        {

            var std = this.studentsRepository.All().FirstOrDefault(x => x.Id == id);

            if (std != null)
            {
            var user = await this.userManager.FindByIdAsync(std.UserAccountId);

            this.studentsRepository.Delete(std);
            await this.studentsRepository.SaveChangesAsync();

            await this.userManager.DeleteAsync(user);
            }
        }

        public async Task<IEnumerable<CoursesViewModel>> GetCourses(ApplicationUser user)
        {
            var stdCourses = await this.studentsRepository.AllAsNoTracking().Where(x => x.UserAccountId == user.Id).To<StudentCoursesViewModel>().FirstOrDefaultAsync();

            return stdCourses.StudentsCourses;
        }

        public async Task<IEnumerable<StudentsInCourseViewModel>> GetStudentsInCourse(int courseId)
        {
            var studentsList = await this.studentsCoursesRepository.AllAsNoTracking().Where(x => x.CourseId == courseId).To<StudentsInCourseViewModel>().ToListAsync();
            return studentsList;
        }
    }
}
