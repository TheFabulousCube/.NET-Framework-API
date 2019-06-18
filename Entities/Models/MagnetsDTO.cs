using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models
{
    public partial class Magnets
    {
        //public IEnumerable<Carts> Carts { get; set; }

        public Magnets()
        { }

        public Magnets(Magnets magnet)
        {
            ProdId = magnet.ProdId;
            ProdPicture = magnet.ProdPicture;
            ProdPrice = magnet.ProdPrice;
            ProdQty = magnet.ProdQty;
            Catagory = magnet.Catagory;
            ProdName = magnet.ProdName;
            Capital = magnet.Capital;
            Statehood = magnet.Statehood;
        }
    }
}
