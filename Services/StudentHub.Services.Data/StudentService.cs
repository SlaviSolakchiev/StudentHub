namespace StudentHub.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using StudentHub.Data.Common.Repositories;
    using StudentHub.Data.Models;
    using StudentHub.Services.Mapping;

    public class StudentService : IStudentService
    {
        private readonly IDeletableEntityRepository<Student> studentsRepository;
        private readonly IRepository<Image> imagesRepository;

        public StudentService(IDeletableEntityRepository<Student> studentsRepository, IRepository<Image> imagesRepository)
        {
            this.studentsRepository = studentsRepository;
            this.imagesRepository = imagesRepository;
        }

        public async Task CreateStudentAsync(string firstName, string lastName, string age, string accountId, Image image)
        {
            var student = new Student()
            {
                FirstName = firstName,
                LastName = lastName,
                Age = age,
                UserAccountId = accountId,
                Image = image,
            };

            await this.studentsRepository.AddAsync(student);
            await this.studentsRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAllStudents<T>()
        {
            var list = this.studentsRepository.AllAsNoTracking()
     .OrderBy(x => x.Id)
     .To<T>()
     .ToList();

            return list;
        }
    }
}
