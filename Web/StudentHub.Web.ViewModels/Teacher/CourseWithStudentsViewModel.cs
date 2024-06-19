namespace StudentHub.Web.ViewModels.Teacher
{
    using System.Collections.Generic;

    using StudentHub.Data.Models;

    using StudentHub.Services.Mapping;

    public class CourseWithStudentsViewModel
    {
        public IEnumerable<StudentsInCourseViewModel> StudentsCourses { get; set; }

        public TeacherInfoViewModel Teacher { get; set; }

        public int NewGradeValue { get; set; }
    }
}
