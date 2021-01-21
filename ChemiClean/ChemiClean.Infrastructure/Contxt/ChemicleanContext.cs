using ChemiClean.Core;
using Microsoft.EntityFrameworkCore;

namespace ChemiClean.Infrastructure
{
    public partial class ChemicleanContext : DbContext
    {

        public ChemicleanContext(DbContextOptions<ChemicleanContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.SeedInitializer();
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.SupplierName).HasMaxLength(250);

                entity.Property(e => e.Url)
                    .IsRequired()
                    .HasMaxLength(300);

                entity.Property(e => e.FileContent).HasColumnType("varbinary(MAX)");
                entity.Property(e => e.UserName).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}