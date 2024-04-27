namespace StudentHub.Services.Data
{
    using System.Collections.Generic;
    using NPOI.SS.Formula.Functions;
    using StudentHub.Web.ViewModels.Administration.Dashboard;

    public interface IDashboardService
    {
        IEnumerable<T> GetAllCourses<T>();
    }
}
