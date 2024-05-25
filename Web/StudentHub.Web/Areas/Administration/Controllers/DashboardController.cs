namespace StudentHub.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using StudentHub.Common;
    using StudentHub.Data.Models;
    using StudentHub.Services.Data;
    using StudentHub.Web.ViewModels.Administration.Dashboard;

    public class DashboardController : AdministrationController
    {
        private readonly ISettingsService settingsService;
        private readonly ICoursesService coursesService;
        private readonly IStudentService studentService;
        private readonly UserManager<ApplicationUser> userManager;

        public DashboardController(ISettingsService settingsService, ICoursesService coursesService, IStudentService studentService, UserManager<ApplicationUser> userManager)
        {
            this.settingsService = settingsService;
            this.coursesService = coursesService;
            this.studentService = studentService;
            this.userManager = userManager;
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
        public async Task<IActionResult> AllStudents()
        {
            var stds = await this.studentService.GetAllStudents<StudentInList>();

            foreach (var student in stds)
            {
                student.Roles = this.userManager.GetRolesAsync(student.UserAccount).Result.ToList();
            }

            var viewModel = new AllStudentsListViewModel() { StudentInList = stds };

            return this.View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> EditStudent(int id)
        {
            var viewModel = await this.studentService.GetByIdAsync<EditStudentViewModel>(id);
            viewModel.Roles = this.userManager.GetRolesAsync(viewModel.UserAccount).Result.ToList();
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditStudent(EditStudentViewModel model, int id)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }
            await this.studentService.UpdateAsync(id, model);
            return this.RedirectToAction(nameof(this.AllStudents));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            await this.studentService.DeleteAsync(id);

            return this.RedirectToAction(nameof(this.AllStudents));
        }
    }
}
