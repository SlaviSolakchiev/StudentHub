namespace StudentHub.Data.Models
{
    using System.Collections.Generic;

    using StudentHub.Data.Common.Models;

    public class Course : BaseModel<int>
    {
        public Course()
        {
            this.StudentsCourses = new HashSet<StudentsCourses>();
            this.Homeworks = new HashSet<Homework>();
        }

        public string Name { get; set; }

        public virtual ICollection<StudentsCourses> StudentsCourses { get; set; }

        public virtual ICollection<Homework> Homeworks { get; set; }

        public virtual Teacher Teacher { get; set; }
    }
}
