using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("tadaatiedyetpc.Downloads")]
    public partial class Downloads
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(25)]
        public string FileNameID { get; set; }

        public int? Download { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(45)]
        public string Date { get; set; }
    }
}
