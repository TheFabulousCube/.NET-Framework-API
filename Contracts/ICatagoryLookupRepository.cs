using Entities.Models;
using System.Collections.Generic;

namespace Contracts
{
    public interface ICatagoryLookupRepository : IRepositoryBase<Catagory>
    {
        IEnumerable<Catagory> GetAllCatagories();
        Catagory GetCatagoryById(string catId);

    }
}
