using System.Collections.Generic;
using System.Linq;
using Contracts;
using Entities;
using Entities.Models;

namespace Repository
{
    // Downloads have a co-key, there is an issue using the generic RepositoryBase
    public class DownloadsRepository : RepositoryBase<Downloads>, IDownloadsRepository
    {
        public DownloadsRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public IEnumerable<Downloads> GetAllDownloads()
        {
            return FindAll()
                .OrderBy(d => d.FileNameID)
                .ToList();
        }

        public int GetCountOfDownloads(string id)
        {
            return FindByCondition(d => d.FileNameID.Equals(id)).Count();
        }
    }
}

