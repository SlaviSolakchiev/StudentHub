using NPOI.SS.Formula.Functions;
using StudentHub.Data.Common.Repositories;
using StudentHub.Data.Models;
using StudentHub.Services.Mapping;
using StudentHub.Web.ViewModels.Administration.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentHub.Services.Data
{
    public class DashboardService : IDashboardService
    {
        private readonly IRepository<Course> coursesRepo;

        public DashboardService(IRepository<Course> coursesRepo)
        {
            this.coursesRepo = coursesRepo;
        }

        public IEnumerable<T> GetAllCourses<T>()
        {
            var list = this.coursesRepo.AllAsNoTracking()
     .OrderBy(x => x.Id)
     .To<T>()
     .ToList();

            return list;
        }
    }
}
