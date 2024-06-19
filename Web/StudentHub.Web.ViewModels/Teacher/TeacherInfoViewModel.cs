namespace StudentHub.Web.ViewModels.Teacher
{
    using System.Collections.Generic;

    using StudentHub.Data.Models;
    using StudentHub.Services.Mapping;

    public class TeacherInfoViewModel : IMapFrom<Teacher>
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public int Age { get; set; }

        public int CourseId { get; set; }

        public virtual Course Course { get; set; }

        public string UserAccountId { get; set; }

        public virtual ApplicationUser UserAccount { get; set; }

        public virtual ICollection<StudentsTeachers> StudentsTeachers { get; set; }
    }
}
