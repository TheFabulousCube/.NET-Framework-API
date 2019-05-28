using Contracts;
using Entities;
using Entities.Models;

namespace Repository
{
    public class CatagoryLookupRepository : RepositoryBase<CatagoryLookup>, ICatagoryLookupRepository
    {
        public CatagoryLookupRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }
    }
}
