namespace StudentHub.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using StudentHub.Web.ViewModels.Administration.Dashboard;

    public interface ITeacherService
    {
        Task<IEnumerable<TeacherViewModel>> GetAllTeachersAsync();

        Task CreateTeacherAsync(CreateTeacherInputModel model);

    }
}
