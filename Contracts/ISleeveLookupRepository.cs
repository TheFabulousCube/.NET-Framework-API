using Entities.Models;
using System.Collections.Generic;

namespace Contracts
{
    public interface ISleeveLookupRepository : IRepositoryBase<SleeveLookup>
    {
        IEnumerable<SleeveLookup> GetAllSleeves();
        SleeveLookup GetSleeveById(string id);
    }
}
