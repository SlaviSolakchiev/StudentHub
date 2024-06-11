namespace StudentHub.Web.ViewModels.Administration.Dashboard
{
    using StudentHub.Data.Models;
    using StudentHub.Services.Mapping;

    public class AllCoursesInListViewModel : IMapFrom<Course>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
