using Entities.Models;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Entities
{
    public class RepositoryContext : DbContext
    {

        public RepositoryContext()
            : base("name=TaDaaTieDyeTPCEntities")
        {
        }

        //public RepositoryContext(I<RepositoryContext> options)
        //    : base(options)
        //{ }

        public virtual DbSet<Carts> Carts { get; set; }
        public virtual DbSet<Catagory> Catagory_lookup { get; set; }
        public virtual DbSet<Clothing> Clothings { get; set; }
        public virtual DbSet<Downloads> Downloads { get; set; }
        public virtual DbSet<Magnets> Magnets { get; set; }
        public virtual DbSet<RoleLookup> Role_lookup { get; set; }
        public virtual DbSet<SizeLookup> Size_lookup { get; set; }
        public virtual DbSet<SleeveLookup> Sleeve_lookup { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Carts>()
        //        .Property(e => e.ProdID)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<CatagoryLookup>()
        //        .Property(e => e.CatId)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<CatagoryLookup>()
        //        .Property(e => e.Type)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<CatagoryLookup>()
        //        .HasMany(e => e.Clothing)
        //        .WithRequired(e => e.Catagory_lookup)
        //        .HasForeignKey(e => e.Catagory)
        //        .WillCascadeOnDelete(false);

        //    modelBuilder.Entity<Clothing>()
        //        .Property(e => e.ProdId)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<Clothing>()
        //        .Property(e => e.ProdPicture)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<Clothing>()
        //        .Property(e => e.Catagory)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<Clothing>()
        //        .Property(e => e.BackPicture)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<Clothing>()
        //        .Property(e => e.Size)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<Clothing>()
        //        .Property(e => e.SleeveLength)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<Downloads>()
        //        .Property(e => e.FileNameID)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<Downloads>()
        //        .Property(e => e.Date)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<Magnets>()
        //        .Property(e => e.ProdId)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<Magnets>()
        //        .Property(e => e.ProdPicture)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<Magnets>()
        //        .Property(e => e.Catagory)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<Magnets>()
        //        .Property(e => e.ProdName)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<Magnets>()
        //        .Property(e => e.Capital)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<Magnets>()
        //        .Property(e => e.Statehood)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<RoleLookup>()
        //        .Property(e => e.RoleId)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<RoleLookup>()
        //        .Property(e => e.Role)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<RoleLookup>()
        //        .HasMany(e => e.Users)
        //        .WithRequired(e => e.Role_lookup)
        //        .HasForeignKey(e => e.Role)
        //        .WillCascadeOnDelete(false);

        //    modelBuilder.Entity<SizeLookup>()
        //        .Property(e => e.SizeId)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<SizeLookup>()
        //        .Property(e => e.Size)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<SizeLookup>()
        //        .HasMany(e => e.Clothings)
        //        .WithRequired(e => e.Size_lookup)
        //        .HasForeignKey(e => e.Size)
        //        .WillCascadeOnDelete(false);

        //    modelBuilder.Entity<SleeveLookup>()
        //        .Property(e => e.SleeveID)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<SleeveLookup>()
        //        .Property(e => e.Sleeve_Length)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<SleeveLookup>()
        //        .HasMany(e => e.Clothing)
        //        .WithRequired(e => e.Sleeve_lookup)
        //        .HasForeignKey(e => e.SleeveLength)
        //        .WillCascadeOnDelete(false);

        //    modelBuilder.Entity<Users>()
        //        .Property(e => e.Username)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<Users>()
        //        .Property(e => e.Password)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<Users>()
        //        .Property(e => e.Fname)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<Users>()
        //        .Property(e => e.Lname)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<Users>()
        //        .Property(e => e.Address)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<Users>()
        //        .Property(e => e.City)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<Users>()
        //        .Property(e => e.State)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<Users>()
        //        .Property(e => e.Zip)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<Users>()
        //        .Property(e => e.Email)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<Users>()
        //        .Property(e => e.Role)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<Users>()
        //        .HasMany(e => e.Carts)
        //        .WithRequired(e => e.User)
        //        .WillCascadeOnDelete(false);
        //}

    }
}
