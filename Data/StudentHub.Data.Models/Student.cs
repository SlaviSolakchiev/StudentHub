﻿namespace StudentHub.Data.Models
{
    using System.Collections.Generic;

    using StudentHub.Data.Common.Models;

    public class Student : BaseDeletableModel<int>
    {
        public Student()
        {
            this.StudentsCourses = new HashSet<StudentsCourses>();
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Age { get; set; }

        public string UserAccountId { get; set; }

        public virtual ApplicationUser UserAccount { get; set; }

        public virtual ICollection<StudentsCourses> StudentsCourses { get; set; }

        public virtual Image Image { get; set; }

        public string ImageId { get; set; }
    }
}
