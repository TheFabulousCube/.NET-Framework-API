using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<SizeLookup> GetAllSizes()
        {
            return FindAll()
                .OrderBy(size => size.Size)
                .ToList();
        }

        public SizeLookup GetSizeById(string id)
        {
            return FindByCondition(size => size.SizeId.Equals(id))
                .FirstOrDefault();
        }
    }
}
