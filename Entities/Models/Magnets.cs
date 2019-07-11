using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("tadaatiedyetpc.Magnets")]
    public partial class Magnets 
    {
        /// <summary>
        /// "SM" + the 2 letter state abbreviation eg: "SMAK", "SMTN"
        /// </summary>
        [Key]
        [Required]
        [StringLength(4)]
        public string ProdId { get; set; }

        [Required]
        [StringLength(25)]
        public string ProdPicture { get; set; }

        [Required]
        public decimal ProdPrice { get; set; }

        /// <summary>
        /// The number of this type on hand, it does *not* account for items in a cart
        /// </summary>
        [Required]
        public int ProdQty { get; set; }

        [Required]
        [StringLength(10)]
        public string Catagory { get; set; }

        [Required]
        [StringLength(15)]
        public string ProdName { get; set; }

        [Required]
        [StringLength(20)]
        public string Capital { get; set; }

        [Required]
        [StringLength(25)]
        public string Statehood { get; set; }
    }
}
