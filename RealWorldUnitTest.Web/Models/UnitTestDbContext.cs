using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace RealWorldUnitTest.Web.Models
{
    public partial class UnitTestDbContext : DbContext
    {
        public UnitTestDbContext()
        {
        }

        public UnitTestDbContext(DbContextOptions<UnitTestDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<Category> Category { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.Color).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");
            });

            //modelBuilder.Entity<Category>().HasData(new Category { Id = 1, Name = "Kalemler" }, new Category() { Id = 2, Name = "Defterler" });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
