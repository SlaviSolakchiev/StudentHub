namespace StudentHub.Web.ViewModels.Student
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using StudentHub.Data.Models;
    using StudentHub.Services.Mapping;

    public class StudentCoursesViewModel : IMapFrom<Student>
    {
        public IEnumerable<CoursesViewModel> StudentsCourses { get; set; }

    }
}
