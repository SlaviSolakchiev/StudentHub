namespace StudentHub.Web.ViewModels.Teacher
{
    using StudentHub.Data.Models;
    using StudentHub.Services.Mapping;
    using System.Collections.Generic;

    public class StudentsInCourseViewModel : IMapFrom<StudentsCourses>
    {
        public int Id { get; set; }

        public int StudentId { get; set; }

        public virtual Student Student { get; set; }

        public int CourseId { get; set; }

        public virtual Course Course { get; set; }

        public virtual ICollection<Grade> Grades { get; set; }
    }
}
