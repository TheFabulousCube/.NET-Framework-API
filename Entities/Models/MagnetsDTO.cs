﻿using System;
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

        public static void Map(Magnets dbMagnet, Magnets magnet)
        {
            dbMagnet.ProdId = magnet.ProdId;
            dbMagnet.ProdPicture = magnet.ProdPicture;
            dbMagnet.ProdPrice = magnet.ProdPrice;
            dbMagnet.ProdQty = magnet.ProdQty;
            dbMagnet.Catagory = magnet.Catagory;
            dbMagnet.ProdName = magnet.ProdName;
            dbMagnet.Capital = magnet.Capital;
            dbMagnet.Statehood = magnet.Statehood;
        }
    }
}
