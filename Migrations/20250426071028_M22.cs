using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoreManagement_Project.Migrations
{
    /// <inheritdoc />
    public partial class M22 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrencyId",
                table: "PurchaseOrders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpectedDeliveryDate",
                table: "PurchaseOrders",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    CurrencyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameOfCurrency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExchangeRate = table.Column<double>(type: "float", nullable: false),
                    IsBaseCurrency = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.CurrencyId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_CurrencyId",
                table: "PurchaseOrders",
                column: "CurrencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrders_Currencies_CurrencyId",
                table: "PurchaseOrders",
                column: "CurrencyId",
                principalTable: "Currencies",
                principalColumn: "CurrencyId",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrders_Currencies_CurrencyId",
                table: "PurchaseOrders");

            migrationBuilder.DropTable(
                name: "Currencies");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseOrders_CurrencyId",
                table: "PurchaseOrders");

            migrationBuilder.DropColumn(
                name: "CurrencyId",
                table: "PurchaseOrders");

            migrationBuilder.DropColumn(
                name: "ExpectedDeliveryDate",
                table: "PurchaseOrders");
        }
    }
}
