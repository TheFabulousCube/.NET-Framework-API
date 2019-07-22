using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public partial class Carts
    {
        public Carts()
        { }

        public Carts(Carts cart)
        {
            UserId = cart.UserId;
            ProdID = cart.ProdID;
            Qty = cart.Qty;
        }
        public void Map(Carts cart)
        {
            this.UserId = cart.UserId;
            this.ProdID = cart.ProdID;
            this.Qty = cart.Qty;
        }

        public override string ToString()
        {
            return ($"UserId = {UserId}" +
                    $"\nProdId = {ProdID}" +
                    $"\nQty = {Qty}");
        }
    }
}
