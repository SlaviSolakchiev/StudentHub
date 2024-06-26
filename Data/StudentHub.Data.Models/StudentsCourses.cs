﻿namespace StudentHub.Data.Models
{
    using System.Collections;
    using System.Collections.Generic;

    public class StudentsCourses
    {
        public StudentsCourses()
        {
            this.Grades = new HashSet<Grade>(); // Използване на HashSet за уникални оценки
        }

        public int Id { get; set; }

        public int StudentId { get; set; }

        public virtual Student Student { get; set; }

        public int CourseId { get; set; }

        public virtual Course Course { get; set; }

        public virtual ICollection<Grade> Grades { get; set; }

    }
}
