using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("tadaatiedyetpc.size_lookup")]
    public partial class SizeLookup
    {
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        //public SizeLookup()
        //{
        //    this.Clothings = new HashSet<Clothing>();
        //}

        [Key]
        [StringLength(6)]
        public string SizeId { get; set; }

        [Required]
        [StringLength(15)]
        public string Size { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Clothing> Clothings { get; set; }
    }
}
