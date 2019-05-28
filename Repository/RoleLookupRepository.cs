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
    }
}