namespace StudentHub.Web.ViewModels.Administration.Dashboard
{
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using StudentHub.Data.Models;

    public class CreateTeacherInputModel
    {
        [Required(ErrorMessage = "Name connot be empty.")]

        public string FullName { get; set; }

        [Required]
        [Range(20, 70)]
        public int Age { get; set; }

        [Required(ErrorMessage = "Course connot be empty.")]
        public int CourseId { get; set; }

    }
}
