using Contracts;
using Entities;
using Entities.Models;

namespace Repository
{
    public class DownloadsRepository : RepositoryBase<Downloads>, IDownloadsRepository
    {
        public DownloadsRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }
    }
}

