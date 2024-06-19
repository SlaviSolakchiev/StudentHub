namespace StudentHub.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using StudentHub.Data.Models;
    using StudentHub.Web.ViewModels.Administration.Dashboard;
    using StudentHub.Web.ViewModels.Teacher;

    public interface ITeacherService
    {
        Task<IEnumerable<TeacherViewModel>> GetAllTeachersAsync();

        Task CreateTeacherAsync(CreateTeacherInputModel model);

        Task<TeacherInfoViewModel> GetTeacherByUserAccountId(ApplicationUser user);
    }
}
