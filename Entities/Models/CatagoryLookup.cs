using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{

    [Table("tadaatiedyetpc.catagory_lookup")]
    public partial class CatagoryLookup
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CatagoryLookup() => Clothing = new HashSet<Clothing>();

        [Key]
        [StringLength(10)]
        public string CatId { get; set; }

        [Required]
        [StringLength(25)]
        public string Type { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Clothing> Clothing { get; set; }
    }
}
