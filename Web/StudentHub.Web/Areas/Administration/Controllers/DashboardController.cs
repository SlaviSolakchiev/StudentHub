namespace StudentHub.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using StudentHub.Common;
    using StudentHub.Services.Data;
    using StudentHub.Web.ViewModels.Administration.Dashboard;

    public class DashboardController : AdministrationController
    {
        private readonly ISettingsService settingsService;
        private readonly ICoursesService coursesService;
        private readonly IStudentService studentService;

        public DashboardController(ISettingsService settingsService, ICoursesService coursesService, IStudentService studentService)
        {
            this.settingsService = settingsService;
            this.coursesService = coursesService;
            this.studentService = studentService;
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

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> CreateCourse(AllCoursesListViewModel inputModel)
        {
            await this.coursesService.CreateCourseAsync(inputModel.NameForCreate);
            return this.RedirectToAction(nameof(this.AllCourses));
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            await this.coursesService.DeleteAsync(id);
            return this.RedirectToAction(nameof(this.AllCourses));
        }

        [HttpGet]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult AllUsers()
        {
            var stds = this.studentService.GetAllStudents<AllStudentsInListViewModel>();
            var viewModel = new AllStudentsListViewModel()
            {
                AllUsersList = stds,
            };

            return this.View(viewModel);
        }

        public IActionResult AllTeachers()
        {
            return this.View();
        }
    }
}
