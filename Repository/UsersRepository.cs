using Contracts;
using Entities;
using Entities.Models;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    public class UsersRepository : RepositoryBase<Users>, IUsersRepository
    {
        public UsersRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public void CreateUser(Users user)
        {
            Create(user);
        }

        public IEnumerable<Users> GetAllUsers()
        {
            return FindAll()
                .OrderBy(u => u.Lname)
                .ToList();
        }

        public Users GetUserById(int id)
        {
            return FindByCondition(u => u.Id.Equals(id))
                .FirstOrDefault();
        }

        public Users GetUserByUserName(string userName)
        {
            return FindByCondition(u => u.Username.Equals(userName))
                .FirstOrDefault();
        }
    }
}
