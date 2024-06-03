using StudentHub.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentHub.Web.ViewModels.Administration.Dashboard
{
    public class TeachersListViewModel
    {
        public IEnumerable<TeacherViewModel> StudentsTeachers { get; set; }
    }
}
