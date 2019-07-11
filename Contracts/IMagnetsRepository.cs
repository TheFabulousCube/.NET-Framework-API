using Entities.Models;
using System.Collections.Generic;

namespace Contracts
{
    public interface IMagnetsRepository : IRepositoryBase<Magnets>
    {
        IEnumerable<Magnets> GetAllMagnets();
        Magnets GetMagnetById(string magnetId);
        //Magnets GetMagnetsInACart(string magnetId);
        void CreateMagnet(Magnets magnet);
        void Updatemagnet(Magnets dbMagnet, Magnets magnet);
    }
}
