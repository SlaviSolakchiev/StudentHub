namespace StudentHub.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using NPOI.SS.Formula.Functions;
    using StudentHub.Data.Models;
    using StudentHub.Web.ViewModels.Administration.Dashboard;
    using StudentHub.Web.ViewModels.Student;
    using StudentHub.Web.ViewModels.Teacher;

    public interface IStudentService
    {
        Task CreateStudentAsync(string firstName, string lastName, int age, string accountId, Image image);

        Task<IEnumerable<T>> GetAllStudents<T>();

        Task<T> GetByIdAsync<T>(int id);

        Task UpdateAsync(int id, EditStudentViewModel input);

        Task DeleteAsync(int id);

        Task<IEnumerable<CoursesViewModel>> GetCourses(ApplicationUser user);

        Task<IEnumerable<StudentsInCourseViewModel>> GetStudentsInCourse(int courseId);
    }
}
