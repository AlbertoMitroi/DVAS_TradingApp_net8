using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using InternshipTradingApp.CompanyInventory.Domain;
using InternshipTradingApp.CompanyInventory.Domain.CompanyHistory;
using System;

namespace InternshipTradingApp.CompanyInventory.Infrastructure.CompanyDataAccess
{
    public class CompanyDbContext : DbContext
    {
        public CompanyDbContext(DbContextOptions<CompanyDbContext> options)
            : base(options)
        {
        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<CompanyHistoryEntry> CompanyHistoryEntries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Define value converter for DateOnly
            var dateOnlyConverter = new ValueConverter<DateOnly, DateTime>(
                v => v.ToDateTime(new TimeOnly()),
                v => DateOnly.FromDateTime(v));

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

                entity.Property(c => c.Status)
                    .HasConversion<int>();

                entity.HasMany(c => c.CompanyHistoryEntries)
                      .WithOne(e => e.Company)
                      .HasForeignKey(e => e.CompanySymbol)
                      .HasPrincipalKey(c => c.Symbol);
            });

            modelBuilder.Entity<CompanyHistoryEntry>(entity =>
            {
                entity.ToTable("CompanyHistoryEntries");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.CompanySymbol)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ReferencePrice)
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.OpeningPrice)
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ClosingPrice)
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.EPS)
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.DayVariation)
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.PER)
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Date)
                    .HasConversion(dateOnlyConverter)
                    .HasColumnType("datetime");
            });
        }
    }
}
