using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface ICartRepository : IRepositoryBase<Carts>
    {
        //Still need a way to get a single cart.
        //Nice to have: get counts magnets,clothing in carts
        IEnumerable<Carts> GetAllCarts();
        Carts GetCartById(int userId, string prodId);
        IEnumerable<Carts> GetCartsByUser(int userId);
    }
}
