namespace StudentHub.Web.ViewModels.Administration.Dashboard
{
    using System.Collections.Generic;

    public class TeachersListViewModel
    {
        public TeachersListViewModel()
        {
            this.CreateTeacherInputModel = new CreateTeacherInputModel();
        }

        public CreateTeacherInputModel CreateTeacherInputModel { get; set; }

        public IEnumerable<TeacherViewModel> StudentsTeachers { get; set; }
    }
}
