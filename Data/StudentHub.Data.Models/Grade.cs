namespace StudentHub.Data.Models
{
    using System;

    using StudentHub.Data.Common.Models;

    public class Grade : BaseModel<int>
    {
        public int Value { get; set; }

        public int StudentId { get; set; } // StudentId

        public int CourseId { get; set; } // CourseId

        public virtual StudentsCourses StudentsCourses { get; set; } // Навигационно свойство
    }
}
