using Entities.Models;
using System.Collections.Generic;

namespace Contracts
{
    public interface IClothingRepository : IRepositoryBase<Clothing>
    {
        IEnumerable<Clothing> GetAllClothing();
        Clothing GetClothingById(string clothingId);
        void UpdateClothing(Clothing dbClothing, Clothing item);
        void DeleteClothing(Clothing item);
        //Clothing GetClothingInACart(string clothingId);
    }
}
