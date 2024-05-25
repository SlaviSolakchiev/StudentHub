namespace StudentHub.Web.ViewModels.Administration.Dashboard
{
    using System.Collections.Generic;
    using System;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using StudentHub.Data.Models;
    using StudentHub.Services.Mapping;

    public class EditStudentViewModel : IMapFrom<Student>, IMapFrom<Image>, IMapFrom<ApplicationRole>, IMapFrom<IdentityUserRole<string>>, IMapFrom<ApplicationUser>
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public string UserAccountId { get; set; }

        public DateTime CreatedOn { get; set; }

        public virtual ApplicationUser UserAccount { get; set; }

        public virtual IEnumerable<string> Roles { get; set; }


        public virtual Image Image { get; set; }

        public string ImageId { get; set; }

    }
}
