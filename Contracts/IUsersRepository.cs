using Entities.Models;
using System.Collections.Generic;

namespace Contracts
{
    public interface IUsersRepository : IRepositoryBase<Users>
    {
        IEnumerable<Users> GetAllUsers();
        Users GetUserById(int id);
    }
}
