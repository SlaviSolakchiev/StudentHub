namespace StudentHub.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using StudentHub.Common;
    using StudentHub.Data.Models;
    using StudentHub.Services.Data;
    using StudentHub.Web.ViewModels.Student;
    using System.Threading.Tasks;

    public class StudentController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IStudentService studentService;

        public StudentController(UserManager<ApplicationUser> userManager, IStudentService studentService)
        {
            this.userManager = userManager;
            this.studentService = studentService;
        }

        [HttpGet]
        [Authorize(Roles = GlobalConstants.StudentRoleName)]
        public async Task<IActionResult> AllCourses()
        {
            var viewModel = new StudentCoursesViewModel();
            var user = await this.userManager.GetUserAsync(this.User);
            viewModel.StudentsCourses = await this.studentService.GetCourses(user);
            return this.View(viewModel);
        }
    }
}
