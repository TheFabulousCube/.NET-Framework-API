using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<SleeveLookup> GetAllSleeves()
        {
            return FindAll()
                .OrderBy(s => s.Sleeve_Length)
                .ToList();
        }

        public SleeveLookup GetSleeveById(string id)
        {
            return FindByCondition(s => s.SleeveID.Equals(id))
                .FirstOrDefault();
        }
    }
}
