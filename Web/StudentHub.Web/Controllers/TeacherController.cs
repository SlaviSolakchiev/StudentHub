namespace StudentHub.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.CodeAnalysis.Operations;
    using Microsoft.EntityFrameworkCore;
    using StudentHub.Common;
    using StudentHub.Data.Common.Repositories;
    using StudentHub.Data.Models;
    using StudentHub.Services.Data;
    using StudentHub.Web.ViewModels.Student;
    using StudentHub.Web.ViewModels.Teacher;

    [Authorize(Roles = GlobalConstants.TeacherRoleName)]
    public class TeacherController : BaseController
    {
        private readonly ITeacherService teacherService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IStudentService studentService;
        private readonly IRepository<StudentsCourses> stdCoursesRepository;

        public TeacherController(ITeacherService teacherService, UserManager<ApplicationUser> userManager, IStudentService studentService, IRepository<StudentsCourses> stdCoursesRepository)
        {
            this.teacherService = teacherService;
            this.userManager = userManager;
            this.studentService = studentService;
            this.stdCoursesRepository = stdCoursesRepository;
        }

        [HttpGet]
        [Authorize(Roles = GlobalConstants.TeacherRoleName)]
        public async Task<IActionResult> MyCourses()
        {
            var viewModel = new CourseWithStudentsViewModel();

            viewModel.Teacher = await this.teacherService.GetTeacherByUserAccountId(await this.userManager.GetUserAsync(this.User));
            viewModel.StudentsCourses = await this.studentService.GetStudentsInCourse(viewModel.Teacher.CourseId);

            return this.View(viewModel);
        }


        [HttpPost]
        [Authorize(Roles = GlobalConstants.TeacherRoleName)]
        public async Task<IActionResult> AddGradeToStudent(string id)
        {
            var b = id;
            return this.RedirectToAction(nameof(this.MyCourses));
        }
    }
}
