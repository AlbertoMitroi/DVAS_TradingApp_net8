﻿// <auto-generated />
using System;
using InternshipTradingApp.CompanyInventory.Infrastructure.CompanyDataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace InternshipTradingApp.CompanyInventory.Migrations
{
    [DbContext(typeof(CompanyDbContext))]
    partial class CompanyDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("InternshipTradingApp.CompanyInventory.Domain.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Symbol")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("Id");

                    b.ToTable("Companies", (string)null);
                });

            modelBuilder.Entity("InternshipTradingApp.CompanyInventory.Domain.CompanyHistory.CompanyHistoryEntry", b =>
                {
                    b.Property<decimal>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("decimal(20,0)");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<decimal>("Id"));

                    b.Property<decimal>("ClosingPrice")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<string>("CompanySymbol")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime");

                    b.Property<decimal>("DayVariation")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<decimal>("EPS")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<decimal>("OpeningPrice")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<decimal>("PER")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<decimal>("ReferencePrice")
                        .HasColumnType("decimal(18, 2)");

                    b.HasKey("Id");

                    b.HasIndex("CompanySymbol");

                    b.ToTable("CompanyHistoryEntries", (string)null);
                });

            modelBuilder.Entity("InternshipTradingApp.CompanyInventory.Domain.CompanyHistory.CompanyHistoryEntry", b =>
                {
                    b.HasOne("InternshipTradingApp.CompanyInventory.Domain.Company", "Company")
                        .WithMany("CompanyHistoryEntries")
                        .HasForeignKey("CompanySymbol")
                        .HasPrincipalKey("Symbol")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");
                });

            modelBuilder.Entity("InternshipTradingApp.CompanyInventory.Domain.Company", b =>
                {
                    b.Navigation("CompanyHistoryEntries");
                });
#pragma warning restore 612, 618
        }
    }
}
