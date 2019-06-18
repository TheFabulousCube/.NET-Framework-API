using System.Collections.Generic;
using System.Linq;
using Contracts;
using Entities;
using Entities.Models;

namespace Repository
{
    public class RoleLookupRepository : RepositoryBase<RoleLookup>, IRoleLookupRepository
    {
        public RoleLookupRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public IEnumerable<RoleLookup> GetAllRoles()
        {
            return FindAll()
                .OrderBy(role => role.Role)
                .ToList();
        }

        public RoleLookup GetRoleById(string id)
        {
            return FindByCondition(role => role.RoleId.Equals(id))
                .FirstOrDefault();
        }
    }
}