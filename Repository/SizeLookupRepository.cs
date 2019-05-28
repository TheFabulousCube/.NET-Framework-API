using Contracts;
using Entities;
using Entities.Models;

namespace Repository
{
    public class SizeLookupRepository : RepositoryBase<SizeLookup>, ISizeLookupRepository
    {
        public SizeLookupRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }
    }
}
