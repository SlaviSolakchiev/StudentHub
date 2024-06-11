namespace StudentHub.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using NPOI.OpenXmlFormats.Dml.Diagram;
    using NPOI.XWPF.UserModel;
    using StudentHub.Data.Common.Repositories;
    using StudentHub.Data.Models;
    using StudentHub.Services.Mapping;

    public class CoursesService : ICoursesService
    {
        private readonly IRepository<Course> coursesRepo;

        public CoursesService(IRepository<Course> coursesRepo)
        {
            this.coursesRepo = coursesRepo;
        }

        public async Task CreateCourseAsync(string name)
        {
            var newCourse = new Course() { Name = name };
            await this.coursesRepo.AddAsync(newCourse);
            await this.coursesRepo.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var courseForDelete = this.coursesRepo.All().FirstOrDefaultAsync(x => x.Id == id).Result;

            this.coursesRepo.Delete(courseForDelete);
            await this.coursesRepo.SaveChangesAsync();
        }

        public IEnumerable<T> GetAllCourses<T>()
        {
            var list = this.coursesRepo.AllAsNoTracking()
     .OrderBy(x => x.Id)
     .To<T>()
     .ToList();

            return list;
        }

        public async Task<IEnumerable<SelectListItem>> GetAllCoursesAsSelectedListItems()
        {
            if (this.coursesRepo.AllAsNoTracking().Any())
            {
                var courses = await this.coursesRepo.AllAsNoTracking().Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString(),
                }).ToListAsync();

                return courses;
            }
            else
            {
                return null;
            }

        }
    }
}
