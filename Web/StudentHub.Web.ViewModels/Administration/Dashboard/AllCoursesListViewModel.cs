namespace StudentHub.Web.ViewModels.Administration.Dashboard
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class AllCoursesListViewModel
    {
        [Required(ErrorMessage = "Name connot be empty.")]
        public string NameForCreate { get; set; }

        public IEnumerable<AllCoursesInListViewModel> AllCoursesList { get; set; }
    }
}
