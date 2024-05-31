namespace StudentHub.Web.ViewModels.Administration.Dashboard
{
    using System;
    using System.Collections.Generic;

    using AutoMapper;
    using StudentHub.Data.Models;
    using StudentHub.Services.Mapping;

    public abstract class BaseStudentModel : IMapFrom<Student>, IMapFrom<ApplicationUser>, IMapFrom<Image>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public string UserAccountId { get; set; }

        public DateTime CreatedOn { get; set; }

        public ApplicationUser UserAccount { get; set; }

        public IEnumerable<string> Roles { get; set; }

        public Image Image { get; set; }

        public string ImageId { get; set; }

        public string ImageUrl { get; set; }

    }
}
