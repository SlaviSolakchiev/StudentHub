namespace StudentHub.Data.Models
{
    using System.Collections;
    using System.Collections.Generic;

    using StudentHub.Data.Common.Models;

    public class Teacher : BaseDeletableModel<int>
    {
        public Teacher()
        {
            this.StudentsTeachers = new HashSet<StudentsTeachers>();
        }

        public string FullName { get; set; }

        public int Age { get; set; }

        public int CourseId { get; set; }

        public virtual Course Course { get; set; }

        public string UserAccountId { get; set; }

        public virtual ApplicationUser UserAccount { get; set; }

        public virtual ICollection<StudentsTeachers> StudentsTeachers { get; set; }
    }
}
