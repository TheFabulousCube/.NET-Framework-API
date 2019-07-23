﻿using Contracts;
using Entities;
using Entities.Models;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    public class ClothingRepository : RepositoryBase<Clothing>, IClothingRepository
    {
        public ClothingRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public void DeleteClothing(Clothing item)
        {
            Delete(item);
        }

        public IEnumerable<Clothing> GetAllClothing()
        {
            return FindAll()
                .OrderBy(c => c.ProdId)
                .ToList();
        }

        public Clothing GetClothingById(string clothingId)
        {
            return FindByCondition(c => c.ProdId.Equals(clothingId))
                .FirstOrDefault();
        }

        public void UpdateClothing(Clothing dbClothing, Clothing item)
        {
            Clothing.Map(dbClothing, item);
            Update(dbClothing);
        }

        //public Clothing GetClothingInACart(string clothingId)
        //{
        //    return new Clothing(GetClothingById(clothingId))
        //    {
        //        Carts = RepositoryContext.Cart
        //        .Where(c => c.ProdId == clothingId)
        //    };
        //}
    }
}
