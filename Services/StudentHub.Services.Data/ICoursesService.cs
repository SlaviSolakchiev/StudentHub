namespace StudentHub.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using NPOI.SS.Formula.Functions;
    using StudentHub.Web.ViewModels.Administration.Dashboard;

    public interface ICoursesService
    {
        IEnumerable<T> GetAllCourses<T>();

        Task CreateCourse(string name);

        Task DeleteAsync(int id);
    }
}
