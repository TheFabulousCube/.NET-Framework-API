using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("tadaatiedyetpc.Cart")]
    public partial class Carts
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserId { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(4)]
        public string ProdID { get; set; }

        public int Qty { get; set; }

        //public virtual Users User { get; set; }
    }
}
