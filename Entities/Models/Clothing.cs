using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{

    [Table("tadaatiedyetpc.Clothing")]
    public partial class Clothing
    {
        [Key]
        [StringLength(4)]
        public string ProdId { get; set; }

        [Required]
        [StringLength(25)]
        public string ProdPicture { get; set; }

        public decimal ProdPrice { get; set; }

        public int ProdQty { get; set; }

        [Required]
        [StringLength(10)]
        public string Catagory { get; set; }

        [Required]
        [StringLength(25)]
        public string BackPicture { get; set; }

        [Required]
        [StringLength(6)]
        public string Size { get; set; }

        [Required]
        [StringLength(2)]
        public string SleeveLength { get; set; }

        //public virtual CatagoryLookup Catagory_lookup { get; set; }
        //public virtual SizeLookup Size_lookup { get; set; }
        //public virtual SleeveLookup Sleeve_lookup { get; set; }
    }
}
