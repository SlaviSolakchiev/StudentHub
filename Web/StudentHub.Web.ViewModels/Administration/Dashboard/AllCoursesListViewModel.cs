using StudentHub.Data.Models;
using System.Collections.Generic;

namespace StudentHub.Web.ViewModels.Administration.Dashboard
{
    public class AllCoursesListViewModel
    {
        public IEnumerable<AllCoursesInListViewModel> AllCoursesList { get; set; }
    }
}
