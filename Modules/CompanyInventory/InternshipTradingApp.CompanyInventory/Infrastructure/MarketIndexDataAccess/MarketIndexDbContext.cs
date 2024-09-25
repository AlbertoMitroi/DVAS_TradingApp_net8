using InternshipTradingApp.CompanyInventory.Domain.MarketIndex;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipTradingApp.CompanyInventory.Infrastructure.MarketIndexDataAccess
{
    public class MarketIndexDbContext : DbContext
    {
        public MarketIndexDbContext(DbContextOptions<MarketIndexDbContext> options)
            : base(options)
        {
        }

        public DbSet<MarketIndexHistory> MarketIndexHistories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MarketIndexHistory>(entity =>
            {
                entity.ToTable("MarketIndexHistories");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Date)
               .HasConversion(
                   v => v.ToDateTime(TimeOnly.MinValue), 
                   v => DateOnly.FromDateTime(v)          
               )
               .IsRequired();

                entity.Property(e => e.Value)
                    .HasColumnType("decimal(18, 2)")
                    .IsRequired();
            });
        }
    }
}
