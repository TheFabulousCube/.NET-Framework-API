using Entities.Models;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Entities
{
    public class RepositoryContext : DbContext
    {

        public RepositoryContext()
            : base("name=TaDaaTieDyeTPCEntities")
        { }

        public virtual DbSet<Carts> Carts { get; set; }
        public virtual LookUps.Catagory Catagory_lookup { get; set; }
        //public virtual DbSet<Catagory> Catagory_lookup { get; set; }
        public virtual DbSet<Clothing> Clothings { get; set; }
        public virtual DbSet<Downloads> Downloads { get; set; }
        public virtual DbSet<Magnets> Magnets { get; set; }
        public virtual DbSet<RoleLookup> Role_lookup { get; set; }
        public virtual DbSet<SizeLookup> Size_lookup { get; set; }
        public virtual DbSet<SleeveLookup> Sleeve_lookup { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        
    }
}
