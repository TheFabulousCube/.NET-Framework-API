﻿using Contracts;
using Entities;
using Entities.Models;

namespace Repository
{
    public class CartRepository : RepositoryBase<Carts>, ICartRepository
    {
        public CartRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }
    }
}
