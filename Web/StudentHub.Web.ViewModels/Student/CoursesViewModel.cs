namespace StudentHub.Web.ViewModels.Student
{
    using System.Collections.Generic;

    using StudentHub.Data.Models;
    using StudentHub.Services.Mapping;

    public class CoursesViewModel : IMapFrom<StudentsCourses>
    {
        public string CourseName { get; set; }

        public int CourseId { get; set; }

        public ICollection<Homework> CourseHomeworks { get; set; }
    }
}
