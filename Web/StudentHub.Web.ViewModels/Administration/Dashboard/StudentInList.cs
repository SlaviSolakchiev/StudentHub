namespace StudentHub.Web.ViewModels.Administration.Dashboard
{
    using System;
    using System.Collections.Generic;
    using AutoMapper;
    using Microsoft.AspNetCore.Identity;
    using StudentHub.Data.Models;
    using StudentHub.Services.Mapping;

    public class StudentInList : IMapFrom<Student>, IHaveCustomMappings
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

        public string ImageUrl { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Student, StudentInList>()
                .ForMember(x => x.ImageUrl, opt => opt.MapFrom(x => "/images/students/" + x.ImageId + "." + x.Image.Extension));
        }
    }
}
