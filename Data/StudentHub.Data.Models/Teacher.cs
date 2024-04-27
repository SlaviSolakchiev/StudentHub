namespace StudentHub.Data.Models
{
    using System.Collections.Generic;

    using StudentHub.Data.Common.Models;

    public class Teacher : BaseDeletableModel<int>
    {
        public Teacher()
        {
            this.Students = new HashSet<Student>();
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Age { get; set; }

        public int CourseId { get; set; }

        public Course Course { get; set; }

        public virtual ICollection<Student> Students { get; set; }
    }
}