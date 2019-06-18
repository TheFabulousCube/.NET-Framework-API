using Entities.Models;
using System.Collections.Generic;

namespace Contracts
{
    public interface ISizeLookupRepository : IRepositoryBase<SizeLookup>
    {
        IEnumerable<SizeLookup> GetAllSizes();
        SizeLookup GetSizeById(string id);
    }
}
