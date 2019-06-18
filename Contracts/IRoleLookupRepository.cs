using Entities.Models;
using System.Collections.Generic;

namespace Contracts
{
    public interface IRoleLookupRepository : IRepositoryBase<RoleLookup>
    {
        IEnumerable<RoleLookup> GetAllRoles();
        RoleLookup GetRoleById(string id);
    }
}
