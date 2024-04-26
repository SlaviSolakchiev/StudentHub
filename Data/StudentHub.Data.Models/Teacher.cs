using StudentHub.Data.Common.Models;
using System.Collections;
using System.Collections.Generic;

namespace StudentHub.Data.Models
{
    public class Teacher : BaseDeletableModel<int>
    {
        public Teacher()
        {
            this.Students = new HashSet<Student>();
            this.Courses = new HashSet<Course>();
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Age { get; set; }

        public virtual ICollection<Course> Courses { get; set; }

        public virtual ICollection<Student> Students { get; set; }
    }
}