namespace StudentHub.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using StudentHub.Common;
    using StudentHub.Data.Common.Repositories;
    using StudentHub.Data.Models;
    using StudentHub.Services.Mapping;
    using StudentHub.Web.ViewModels.Administration.Dashboard;
    using StudentHub.Web.ViewModels.Teacher;

    public class TeacherService : ITeacherService
    {
        private readonly IDeletableEntityRepository<Teacher> teacherRepository;
        private readonly IRepository<Course> courseRepository;
        private readonly UserManager<ApplicationUser> userManager;

        public TeacherService(IDeletableEntityRepository<Teacher> teacherRepository, IRepository<Course> courseRepository, UserManager<ApplicationUser> userManager)
        {
            this.teacherRepository = teacherRepository;
            this.courseRepository = courseRepository;
            this.userManager = userManager;
        }

        public async Task CreateTeacherAsync(CreateTeacherInputModel model)
        {
            if (model != null)
            {
                var user = new ApplicationUser()
                {
                    UserName = $"{new string(model.FullName.Trim().Where(c => !char.IsWhiteSpace(c)).ToArray())}@abv.bg",
                    Email = $"{new string(model.FullName.Trim().Where(c => !char.IsWhiteSpace(c)).ToArray())}@abv.bg",
                };
                var password = $"{new string(model.FullName.Trim().Where(c => !char.IsWhiteSpace(c)).ToArray())}123";
                var result = await this.userManager.FindByEmailAsync(user.Email);

                if (result == null)
                {
                    await this.userManager.CreateAsync(user, password);

                    await this.userManager.AddToRoleAsync(user, GlobalConstants.TeacherRoleName);

                    if (!this.teacherRepository.AllAsNoTracking().Any(x => x.FullName == model.FullName))
                    {
                        var teacher = new Teacher()
                        {
                            FullName = model.FullName,
                            Age = model.Age,
                            CourseId = model.CourseId,
                            UserAccountId = user.Id,
                            UserAccount = user,
                        };
                        await this.teacherRepository.AddAsync(teacher);
                        await this.teacherRepository.SaveChangesAsync();
                    }
                }
            }
        }

        public async Task<IEnumerable<TeacherViewModel>> GetAllTeachersAsync()
        {
            var teachers = await this.teacherRepository.AllAsNoTracking().To<TeacherViewModel>().ToListAsync();
            return teachers;
        }

        public async Task<TeacherInfoViewModel> GetTeacherByUserAccountId(ApplicationUser user)
        {
            var currentTeacher = await this.teacherRepository.AllAsNoTracking().Where(x => x.UserAccountId == user.Id).To<TeacherInfoViewModel>().FirstOrDefaultAsync();
            return currentTeacher;
        }
    }
}
