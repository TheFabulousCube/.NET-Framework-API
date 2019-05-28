using Contracts;
using Entities;
using Entities.Models;

namespace Repository
{
    public class UsersRepository : RepositoryBase<Users>, IUsersRepository
    {
        public UsersRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }
    }
}
