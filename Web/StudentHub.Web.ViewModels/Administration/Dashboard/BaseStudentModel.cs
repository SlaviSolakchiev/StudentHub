namespace StudentHub.Web.ViewModels.Administration.Dashboard
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;
    using StudentHub.Data.Models;
    using StudentHub.Services.Mapping;

    public abstract class BaseStudentModel : IMapFrom<Student>, IMapFrom<ApplicationUser>, IMapFrom<Image>
    {
        [Required]
        [MaxLength(20)]
        [MinLength(2)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(20)]
        [MinLength(2)]
        public string LastName { get; set; }

        [Required]
        [Range(0, 50)]
        public int Age { get; set; }

        [Required]
        public string UserAccountId { get; set; }

        public DateTime CreatedOn { get; set; }

        [Required]
        public string ImageId { get; set; }

        [Required]
        public string ImageUrl { get; set; }

    }
}
