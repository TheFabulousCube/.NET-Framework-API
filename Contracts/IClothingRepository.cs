using Entities.Models;
using System.Collections.Generic;

namespace Contracts
{
    public interface IClothingRepository : IRepositoryBase<Clothing>
    {
        IEnumerable<Clothing> GetAllClothing();
        Clothing GetClothingById(string magnetId);
        //Clothing GetClothingInACart(string magnetId);
    }
}
