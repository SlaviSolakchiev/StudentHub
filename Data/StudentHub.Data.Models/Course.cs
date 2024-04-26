namespace StudentHub.Data.Models
{
    using System.Collections.Generic;

    using StudentHub.Data.Common.Models;

    public class Course : BaseModel<int>
    {
        public Course()
        {
            this.Students = new HashSet<Student>();
        }

        public string Name { get; set; }

        public virtual ICollection<Student> Students { get; set; }

        public int TeacherId { get; set; }

        public virtual Teacher Teacher { get; set; }
    }
}