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

        public Carts AddToCart(Carts cart)
        {
            if (cart.Qty <= 0)
            {
                Delete(cart);
                return null;
            }

            Carts dbCart = FindByCondition(c => c.UserId == cart.UserId && c.ProdID == cart.ProdID).FirstOrDefault();
            if (dbCart == null)
            {
                Create(cart);
            }
            else
            {
                dbCart.Qty = cart.Qty;
            }
            return FindByCondition(c => c.UserId == cart.UserId && c.ProdID == cart.ProdID).FirstOrDefault();
        }

        public void EmptyCart(int user)
        {
            RemoveByCondition(cart => cart.UserId == user);
        }

        public IEnumerable<Carts> GetAllCarts()
        {
            return FindAll()
                .OrderBy(cart => cart.UserId)
                .ToList();
        }

        public List<Carts> GetCart(Users user)
        {
            throw new System.NotImplementedException();
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

        public void RemoveFromCart(Carts cart)
        {
            Delete(cart);
        }
    }
}
