﻿namespace StudentHub.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using NPOI.SS.Formula.Functions;
    using StudentHub.Web.ViewModels.Administration.Dashboard;

    public interface ICoursesService
    {
        IEnumerable<T> GetAllCourses<T>();

        Task CreateCourseAsync(string name);

        Task DeleteAsync(int id);

        Task<IEnumerable<SelectListItem>> GetAllCoursesAsSelectedListItems();
    }
}
