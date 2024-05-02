namespace StudentHub.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using StudentHub.Data.Common.Repositories;
    using StudentHub.Data.Models;

    public class StudentService : IStudentService
    {
        private readonly IDeletableEntityRepository<Student> studentsRepository;
        private readonly IRepository<Image> imagesRepository;

        public StudentService(IDeletableEntityRepository<Student> studentsRepository, IRepository<Image> imagesRepository)
        {
            this.studentsRepository = studentsRepository;
            this.imagesRepository = imagesRepository;
        }

        public async Task CreateStudentAsync(string firstName, string lastName, string age, string accountId)
        {
            var student = new Student()
            {
                FirstName = firstName,
                LastName = lastName,
                Age = age,
                UserAccountId = accountId,
            };

             var img = new Image() { };


            await this.studentsRepository.AddAsync(student);
            await this.studentsRepository.SaveChangesAsync();
        }

        public List<Student> GetAllStudents()
        {
            return this.studentsRepository.AllAsNoTracking().ToList();
        }
    }
}
