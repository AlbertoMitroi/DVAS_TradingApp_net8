using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InternshipTradingApp.CompanyInventory.Migrations
{
    /// <inheritdoc />
    public partial class AddHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CompanyPriceHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OpeningPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ClosingPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PriceDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyPriceHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyPriceHistories_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompanyPriceHistories_CompanyId",
                table: "CompanyPriceHistories",
                column: "CompanyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyPriceHistories");
        }
    }
}
