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

        public IEnumerable<KeyValuePair<string, string>> GetAllRolesAsKeyValuePair()
        {
            return this.rolesRepository.AllAsNoTracking().Select(x => new
            {
                x.Id,
                x.Name,
            }).ToList().Select(x => new KeyValuePair<string, string>(x.Id, x.Name));
        }
    }
}
