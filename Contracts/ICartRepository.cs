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
        void EmptyCart(int user); // User user?
        /**************************************
         * Combine AddToCart() & UpdateQty()
         * into an Upsert()?
         * ***********************************/

        // From MVC:
        Carts AddToCart(Carts cart);
        void RemoveFromCart(Carts cart);
        List<Carts> GetCart(Users user);
    }
}
