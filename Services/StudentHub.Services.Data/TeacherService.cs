namespace StudentHub.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using StudentHub.Data.Common.Repositories;
    using StudentHub.Data.Models;
    using StudentHub.Services.Mapping;
    using StudentHub.Web.ViewModels.Administration.Dashboard;

    public class TeacherService : ITeacherService
    {
        private readonly IDeletableEntityRepository<Teacher> teacherRepository;
        private readonly IRepository<Course> courseRepository;

        public TeacherService(IDeletableEntityRepository<Teacher> teacherRepository, IRepository<Course> courseRepository)
        {
            this.teacherRepository = teacherRepository;
            this.courseRepository = courseRepository;
        }

        public async Task CreateTeacherAsync(CreateTeacherInputModel model)
        {
            if (model != null)
            {
                await this.teacherRepository.AddAsync(new Teacher
                {
                    FullName = model.FullName,
                    Age = model.Age,
                    CourseId = model.CourseId,
                });
            }

            await this.teacherRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<TeacherViewModel>> GetAllTeachersAsync()
        {
            var teachers = await this.teacherRepository.AllAsNoTracking().To<TeacherViewModel>().ToListAsync();
            return teachers;
        }
    }
}
