namespace StudentHub.Web.ViewModels.Administration.Dashboard
{
    using System;
    using System.Collections.Generic;
    using AutoMapper;
    using StudentHub.Data.Models;
    using StudentHub.Services.Mapping;

    public class AllStudentsInListViewModel : IMapFrom<ApplicationUser>, IMapFrom<Student>, IMapFrom<Image>
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Age { get; set; }

        public virtual ApplicationUser UserAccount { get; set; }

        public Image Image { get; set; }

        public virtual ICollection<Course> Courses { get; set; }

        public string ImageUrl { get; set; }

        //public void CreateMappings(IProfileExpression configuration)
        //{
        //    configuration.CreateMap<Student, AllStudentsInListViewModel>()
        //        .ForMember(x => this.ImageUrl, opt => opt.MapFrom(x => "C:\\Users\\slavi\\Desktop\\StudentHub\\Web\\StudentHub.Web\\images\\recipes\\" + x.Image.Id + "." + x.Image.Extension));
        //}
    }
}
