using Contracts;
using Entities;
using Entities.Models;

namespace Repository
{
    public class SleeveLookupRepository : RepositoryBase<SleeveLookup>, ISleeveLookupRepository
    {
        public SleeveLookupRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }
    }
}
