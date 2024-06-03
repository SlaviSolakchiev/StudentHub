namespace StudentHub.Web.ViewModels.Administration.Dashboard
{
    using System.Collections.Generic;

    using StudentHub.Data.Models;
    using StudentHub.Services.Mapping;

    public class TeacherViewModel : IMapFrom<Teacher>
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public int Age { get; set; }

        public int CourseId { get; set; }

        public virtual Course Course { get; set; }

    }
}
