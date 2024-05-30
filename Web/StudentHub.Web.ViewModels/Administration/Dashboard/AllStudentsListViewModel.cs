namespace StudentHub.Web.ViewModels.Administration.Dashboard
{
    using NPOI.SS.Formula.Functions;
    using System.Collections.Generic;

    public class AllStudentsListViewModel
    {
        public IEnumerable<StudentInListViewModel> StudentInList { get; set; }
    }
}
