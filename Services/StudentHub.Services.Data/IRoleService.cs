namespace StudentHub.Services.Data
{
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc.Rendering;

    public interface IRoleService
    {
        IEnumerable<SelectListItem> GetAllRoles();
    }
}
