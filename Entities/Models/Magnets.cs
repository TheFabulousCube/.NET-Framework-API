using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("tadaatiedyetpc.Magnets")]
    public partial class Magnets 
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
