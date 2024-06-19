namespace StudentHub.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Security.Cryptography.Xml;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
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
        private readonly IRoleService roleService;
        private readonly ITeacherService teacherService;

        public DashboardController(ISettingsService settingsService, ICoursesService coursesService, IStudentService studentService, UserManager<ApplicationUser> userManager, IRoleService roleService, ITeacherService teacherService)
        {
            this.settingsService = settingsService;
            this.coursesService = coursesService;
            this.studentService = studentService;
            this.userManager = userManager;
            this.roleService = roleService;
            this.teacherService = teacherService;
        }

        public IActionResult Index()
        {
            return this.View();
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
            var stds = await this.studentService.GetAllStudents<StudentInListViewModel>();

            foreach (var student in stds)
            {
                student.Roles = this.userManager.GetRolesAsync(await this.userManager.FindByIdAsync(student.UserAccountId)).Result.ToList();
            }

            var viewModel = new AllStudentsListViewModel() { StudentInList = stds };

            return this.View(viewModel);
        }
            
        [HttpGet]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> EditStudent(int id)
        {
            var viewModel = await this.studentService.GetByIdAsync<EditStudentViewModel>(id);

            viewModel.RolesKeyValue = this.roleService.GetAllRolesAsKeyValuePair();

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> EditStudent(EditStudentViewModel viewModel, int id)
        {
            if (this.ModelState.IsValid)
            {
                await this.studentService.UpdateAsync(id, viewModel);
                return this.RedirectToAction(nameof(this.AllStudents));
            }
            else
            {
                return this.RedirectToAction(nameof(this.EditStudent));
            }
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            await this.studentService.DeleteAsync(id);

            return this.RedirectToAction(nameof(this.AllStudents));
        }

        [HttpGet]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> AllTeachers()
        {
            var viewModel = new TeachersListViewModel();

            viewModel.StudentsTeachers = await this.teacherService.GetAllTeachersAsync();
            var courses = await this.coursesService.GetAllCoursesAsSelectedListItems();
            viewModel.Courses = courses;

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> CreateTeacherAsync(TeachersListViewModel inputModel)
        {
            if (this.ModelState.IsValid)
            {
                await this.teacherService.CreateTeacherAsync(inputModel.CreateTeacherInputModel);

                return this.RedirectToAction(nameof(this.AllTeachers));
            }
            else
            {
                return this.View(inputModel);
            }
        }
    }
}
