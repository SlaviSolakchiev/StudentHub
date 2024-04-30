using Microsoft.EntityFrameworkCore;
using NPOI.SS.Formula.Functions;
using StudentHub.Data.Common.Repositories;
using StudentHub.Data.Models;
using StudentHub.Services.Mapping;
using StudentHub.Web.ViewModels.Administration.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentHub.Services.Data
{
    public class CoursesService : ICoursesService
    {
        private readonly IRepository<Course> coursesRepo;

        public CoursesService(IRepository<Course> coursesRepo)
        {
            this.coursesRepo = coursesRepo;
        }

        public async Task CreateCourse(string id)
        {
            var newCourse = new Course() { Name = id };
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
    }
}
