namespace StudentHub.Web.ViewModels.Administration.Dashboard
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using AutoMapper;
    using Microsoft.AspNetCore.Identity;
    using StudentHub.Data.Models;
    using StudentHub.Services.Mapping;

    public class AllStudentsInListViewModel : IMapFrom<Student>, IMapFrom<Image>, IMapFrom<ApplicationRole>, IMapFrom<IdentityUserRole<string>>, IMapFrom<ApplicationUser>
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
