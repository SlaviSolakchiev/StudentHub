namespace StudentHub.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.IdentityModel.Tokens;
    using StudentHub.Data.Models;

    public class CoursesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            // if (dbContext.Courses.IsNullOrEmpty())
            // {
            //    var courses = new List<Course>()
            //    {
            //    new Course() { Name = "Математика" },
            //    new Course() { Name = "Български език и литература" },
            //    };

            // await dbContext.Courses.AddRangeAsync(courses);
            // }
        }
    }
}
