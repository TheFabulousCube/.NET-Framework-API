using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("tadaatiedyetpc.users")]
    public partial class Users
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Users()
        {
            this.Carts = new HashSet<Carts>();
        }
    
        public int Id { get; set; }

        [Required]
        [StringLength(12)]
        public string Username { get; set; }

        [Required]
        [StringLength(40)]
        public string Password { get; set; }

        [StringLength(25)]
        public string Fname { get; set; }

        [StringLength(25)]
        public string Lname { get; set; }

        [StringLength(45)]
        public string Address { get; set; }

        [StringLength(20)]
        public string City { get; set; }

        [StringLength(2)]
        public string State { get; set; }

        [StringLength(10)]
        public string Zip { get; set; }

        [StringLength(45)]
        public string Email { get; set; }

        [Required]
        [StringLength(1)]
        public string Role { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Carts> Carts { get; set; }
        public virtual RoleLookup Role_lookup { get; set; }
    }
}
