using System.Collections.Generic;
using System.Linq;
using Contracts;
using Entities;
using Entities.Models;

namespace Repository
{
    public class CatagoryLookupRepository : RepositoryBase<Catagory>, ICatagoryLookupRepository
    {
        public CatagoryLookupRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public IEnumerable<Catagory> GetAllCatagories()
        {
            return FindAll()
                .OrderBy(cat => cat.Type)
                .ToList();
        }

        public Catagory GetCatagoryById(string catId)
        {
            return FindByCondition(cat => cat.CatId.Equals(catId))
                .FirstOrDefault();
        }
    }
}
