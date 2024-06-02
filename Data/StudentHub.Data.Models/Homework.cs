namespace StudentHub.Data.Models
{
    using StudentHub.Data.Common.Models;

    public class Homework : BaseModel<int>
    {
        public int CourseId { get; set; }

        public Course Course { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
