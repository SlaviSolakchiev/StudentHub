using StudentHub.Data.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentHub.Web.ViewModels.Administration.Dashboard
{
    public class AllCoursesListViewModel
    {
        [Required(ErrorMessage = "Name connot be empty.")]
        public string NameForCreate { get; set; }

        public IEnumerable<AllCoursesInListViewModel> AllCoursesList { get; set; }
    }
}
