using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InternshipTradingApp.CompanyInventory.Migrations
{
    /// <inheritdoc />
    public partial class initialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Symbol = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                    table.UniqueConstraint("AK_Companies_Symbol", x => x.Symbol);
                });

            migrationBuilder.CreateTable(
                name: "CompanyHistoryEntries",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanySymbol = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ReferencePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OpeningPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ClosingPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EPS = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PER = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DayVariation = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime", nullable: false),
                    Volume = table.Column<decimal>(type: "decimal(20,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyHistoryEntries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyHistoryEntries_Companies_CompanySymbol",
                        column: x => x.CompanySymbol,
                        principalTable: "Companies",
                        principalColumn: "Symbol",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompanyHistoryEntries_CompanySymbol",
                table: "CompanyHistoryEntries",
                column: "CompanySymbol");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyHistoryEntries");

            migrationBuilder.DropTable(
                name: "Companies");
        }
    }
}
