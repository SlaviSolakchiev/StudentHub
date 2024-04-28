namespace StudentHub.Web.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using StudentHub.Common;
    using StudentHub.Services.Data;
    using StudentHub.Web.ViewModels.Administration.Dashboard;

    public class DashboardController : AdministrationController
    {
        private readonly ISettingsService settingsService;
        private readonly ICoursesService coursesService;

        public DashboardController(ISettingsService settingsService, ICoursesService coursesService)
        {
            this.settingsService = settingsService;
            this.coursesService = coursesService;
        }

        public IActionResult Index()
        {
            var viewModel = new IndexViewModel { SettingsCount = this.settingsService.GetCount(), };
            return this.View(viewModel);
        }

        [HttpGet]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult AllCourses()
        {
            var viewModel = new AllCoursesListViewModel()
            {
                AllCoursesList = this.coursesService.GetAllCourses<AllCoursesInListViewModel>(),
            };

            return this.View(viewModel);
        }


        [HttpGet]
        public IActionResult CreateCourse()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult CreateCourse(int Id)
        {
            return this.Redirect("/");
        }

        [HttpGet]

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult DeleteCourse(int id)
        {
            this.coursesService.DeleteAsync(id);
            return this.RedirectToAction(nameof(this.AllCourses));
        }

        public IActionResult AllUsers()
        {
            return this.View();
        }
    }
}
