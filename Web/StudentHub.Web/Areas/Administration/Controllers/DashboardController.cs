namespace StudentHub.Web.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using StudentHub.Services.Data;
    using StudentHub.Web.ViewModels.Administration.Dashboard;

    public class DashboardController : AdministrationController
    {
        private readonly ISettingsService settingsService;
        private readonly IDashboardService dashboardService;

        public DashboardController(ISettingsService settingsService, IDashboardService dashboardService)
        {
            this.settingsService = settingsService;
            this.dashboardService = dashboardService;
        }

        public IActionResult Index()
        {
            var viewModel = new IndexViewModel { SettingsCount = this.settingsService.GetCount(), };
            return this.View(viewModel);
        }


        [HttpGet]
        public IActionResult AllCourses()
        {
            var viewModel = new AllCoursesListViewModel()
            {
                AllCoursesList = this.dashboardService.GetAllCourses<AllCoursesInListViewModel>(),
            };

            return this.View(viewModel);
        }

        public IActionResult AllUsers()
        {
            return this.View();
        }
    }
}
