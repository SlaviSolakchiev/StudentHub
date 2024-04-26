using StudentHub.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentHub.Data.Models
{
    public class Student : BaseDeletableModel<int>
    {
        public Student()
        {
            this.Courses = new HashSet<Course>();
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Age { get; set; }

        public int ClassTeacherId { get; set; }

        public virtual Teacher Teacher { get; set; }

        public string UserAccountId { get; set; }

        public virtual ApplicationUser UserAccount { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
    }
}
