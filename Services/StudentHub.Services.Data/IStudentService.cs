namespace StudentHub.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using StudentHub.Data.Models;

    public interface IStudentService
    {
        Task CreateStudentAsync(string firstName, string lastName, string age, string accountId);

        List<Student> GetAllStudents();
    }
}
