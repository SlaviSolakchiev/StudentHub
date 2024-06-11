namespace StudentHub.Web.ViewModels.Administration.Dashboard
{
    using System.Collections;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc.Rendering;
    using StudentHub.Data.Models;

    public class CreateTeacherInputModel
    {
        public string FullName { get; set; }

        public int Age { get; set; }

        public int CourseId { get; set; }

        public IEnumerable<SelectListItem> Courses { get; set; }

    }
}
