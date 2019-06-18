using Entities.Models;
using System.Collections.Generic;

namespace Contracts
{
    public interface IDownloadsRepository : IRepositoryBase<Downloads>
    {
        IEnumerable<Downloads> GetAllDownloads();
        int GetCountOfDownloads(string id);
    }
}
