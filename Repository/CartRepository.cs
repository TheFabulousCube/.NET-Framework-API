﻿using Contracts;
using Entities;
using Entities.Models;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    public class CartRepository : RepositoryBase<Carts>, ICartRepository
    {
        public CartRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public IEnumerable<Carts> GetAllCarts()
        {
            return FindAll()
                .OrderBy(cart => cart.UserId)
                .ToList();
        }

        public Carts GetCartById(int userId, string prodId)
        {
            return FindByCondition(cart => cart.UserId.Equals(userId) && cart.ProdID.Equals(prodId))
                .FirstOrDefault();
        }

        public IEnumerable<Carts> GetCartsByUser(int userId)
        {
            return FindByCondition(cart => cart.UserId.Equals(userId));
        }
    }
}
