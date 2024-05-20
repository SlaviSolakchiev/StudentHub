namespace StudentHub.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using StudentHub.Data.Models;

    public interface IStudentService
    {
        Task CreateStudentAsync(string firstName, string lastName, string age, string accountId, Image image);

        IEnumerable<T> GetAllStudents<T>();
    }
}
