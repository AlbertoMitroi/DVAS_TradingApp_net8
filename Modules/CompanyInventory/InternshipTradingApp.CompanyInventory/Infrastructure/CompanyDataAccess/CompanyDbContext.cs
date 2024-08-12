using Microsoft.EntityFrameworkCore;
using InternshipTradingApp.CompanyInventory.Domain;

namespace InternshipTradingApp.CompanyInventory.Infrastructure.CompanyDataAccess
{
    public class CompanyDbContext : DbContext
    {
        public CompanyDbContext(DbContextOptions<CompanyDbContext> options)
            : base(options)
        {
        }

        public DbSet<Company> Companies { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Company>(entity =>
            {
                entity.ToTable("Companies");
                entity.HasKey(c => c.Id);

                entity.Property(c => c.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(c => c.Symbol)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(c => c.Price)
                    .HasColumnType("decimal(18, 2)");

                entity.Property(c => c.ReferencePrice)
                    .HasColumnType("decimal(18, 2)");

                entity.Property(c => c.OpeningPrice)
                    .HasColumnType("decimal(18, 2)");

                entity.Property(c => c.ClosingPrice)
                    .HasColumnType("decimal(18, 2)");

                entity.Property(c => c.EPS)
                    .HasColumnType("decimal(18, 2)");

                entity.Property(c => c.DayVariation)
                    .HasColumnType("decimal(18, 2)");

                entity.Property(c => c.PER)
                    .HasColumnType("decimal(18, 2)");

                entity.Property(c => c.Status)
                    .HasConversion<int>();
            });
        }


    }
}
