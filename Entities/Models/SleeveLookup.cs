using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("tadaatiedyetpc.sleeve_lookup")]
    public partial class SleeveLookup
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SleeveLookup()
        {
            this.Clothing = new HashSet<Clothing>();
        }

        [Key]
        [StringLength(2)]
        public string SleeveID { get; set; }

        [Column("Sleeve Length")]
        [Required]
        [StringLength(12)]
        public string Sleeve_Length { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Clothing> Clothing { get; set; }
    }
}
