namespace StudentHub.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using StudentHub.Data.Common.Repositories;
    using StudentHub.Data.Models;

    public class RoleService : IRoleService
    {
        private readonly IDeletableEntityRepository<ApplicationRole> rolesRepository;

        public RoleService(IDeletableEntityRepository<ApplicationRole> rolesRepository)
        {
            this.rolesRepository = rolesRepository;
        }

        public IEnumerable<SelectListItem> GetAllRoles()
        {
            return this.rolesRepository.AllAsNoTracking().Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id,
            }).ToList();
        }
    }
}
