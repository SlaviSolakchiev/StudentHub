namespace StudentHub.Web.ViewModels.Administration.Dashboard
{
    using AutoMapper;
    using StudentHub.Data.Models;
    using StudentHub.Services.Mapping;
    using System.Collections.Generic;

    public class StudentInListViewModel : BaseStudentModel, IHaveCustomMappings
    {
        public int Id { get; set; }

        public IEnumerable<string> Roles { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Student, StudentInListViewModel>()
                .ForMember(x => x.ImageUrl, opt => opt.MapFrom(x => "/images/students/" + x.ImageId + "." + x.Image.Extension));
        }
    }
}
