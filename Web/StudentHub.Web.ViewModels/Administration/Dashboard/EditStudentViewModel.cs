namespace StudentHub.Web.ViewModels.Administration.Dashboard
{
    using AutoMapper;
    using StudentHub.Data.Models;
    using StudentHub.Services.Mapping;
    using System.Collections.Generic;

    public class EditStudentViewModel : BaseStudentModel, IHaveCustomMappings
    {
        public int Id { get; set; }

        public IEnumerable<KeyValuePair<string, string>> RolesKeyValue { get; set; }

        public IEnumerable<string> SelectedRoles { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Student, EditStudentViewModel>()
                .ForMember(x => x.ImageUrl, opt => opt.MapFrom(x => "/images/students/" + x.ImageId + "." + x.Image.Extension));
        }
    }
}
