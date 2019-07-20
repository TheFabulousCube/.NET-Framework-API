using Contracts;
using Entities;
using Entities.Models;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    public class MagnetsRepository : RepositoryBase<Magnets>, IMagnetsRepository
    {

        public MagnetsRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public IEnumerable<Magnets> GetAllMagnets()
        {
            return FindAll()
                .OrderBy(mag => mag.ProdName)
                .ToList();
        }

        public Magnets GetMagnetById(string magnetId)
        {
            return FindByCondition(mag => mag.ProdId.Equals(magnetId))
                .FirstOrDefault();
        }

        //public Magnets GetMagnetsInACart(string magnetId)
        //{
        //    return new Magnets(GetMagnetById(magnetId))
        //    {
        //        Carts = RepositoryContext.Carts
        //        .Where(c => c.ProdID == magnetId)
        //    };
        //}

        public void CreateMagnet(Magnets magnet)
        {
            Create(magnet);
        }

        public void UpdateMagnet(Magnets dbMagnet, Magnets magnet)
        {
            Magnets.Map(dbMagnet, magnet);
            Update(dbMagnet);
        }

        public void DeleteMagnet(Magnets magnet)
        {
            Delete(magnet);
        }
    }
}