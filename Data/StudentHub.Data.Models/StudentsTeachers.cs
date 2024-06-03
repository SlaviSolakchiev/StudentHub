using System.Data.Common;

namespace StudentHub.Data.Models
{
    public class StudentsTeachers
    {
        public int Id { get; set; }

        public int StudentId { get; set; }

        public Student Student { get; set; }

        public int TeacherId { get; set; }

        public Teacher Teacher { get; set; }
    }
}
