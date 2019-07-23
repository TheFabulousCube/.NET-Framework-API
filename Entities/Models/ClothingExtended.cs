using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models
{
    public partial class Clothing
    {
        //public IEnumerable<Cart> Carts { get; set; }

        public Clothing()
        { }

        public Clothing(Clothing clothing)
        {
            ProdId = clothing.ProdId;
            ProdPicture = clothing.ProdPicture;
            ProdPrice = clothing.ProdPrice;
            ProdQty = clothing.ProdQty;
            Catagory = clothing.Catagory;
            BackPicture = clothing.BackPicture;
            Size = clothing.Size;
            SleeveLength = clothing.SleeveLength;
            //Catagory_lookup = clothing.Catagory_lookup;
            //Size_lookup = clothing.Size_lookup;
            //Sleeve_lookup = clothing.Sleeve_lookup;
        }

        public static void Map(Clothing dbClothing, Clothing clothing)
        {
            dbClothing.ProdId = clothing.ProdId;
            dbClothing.ProdPicture = clothing.ProdPicture;
            dbClothing.ProdPrice = clothing.ProdPrice;
            dbClothing.ProdQty = clothing.ProdQty;
            dbClothing.Catagory = clothing.Catagory;
            dbClothing.BackPicture = clothing.BackPicture;
            dbClothing.Size = clothing.Size;
            dbClothing.SleeveLength = clothing.SleeveLength;
        }
    }
}
