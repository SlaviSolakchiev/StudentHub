﻿namespace StudentHub.Web.ViewModels.Administration.Dashboard
{
    using System.Collections.Generic;

    public class AllStudentsListViewModel
    {
        public IEnumerable<AllStudentsInListViewModel> AllUsersList { get; set; }
    }
}
