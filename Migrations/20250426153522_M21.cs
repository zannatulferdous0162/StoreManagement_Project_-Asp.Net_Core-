using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoreManagement_Project.Migrations
{
    /// <inheritdoc />
    public partial class M21 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrders_Currencies_CurrencyId",
                table: "PurchaseOrders");

            migrationBuilder.AlterColumn<int>(
                name: "CurrencyId",
                table: "PurchaseOrders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                table: "PurchaseOrders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrders_Currencies_CurrencyId",
                table: "PurchaseOrders",
                column: "CurrencyId",
                principalTable: "Currencies",
                principalColumn: "CurrencyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrders_Currencies_CurrencyId",
                table: "PurchaseOrders");

            migrationBuilder.DropColumn(
                name: "Remarks",
                table: "PurchaseOrders");

            migrationBuilder.AlterColumn<int>(
                name: "CurrencyId",
                table: "PurchaseOrders",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrders_Currencies_CurrencyId",
                table: "PurchaseOrders",
                column: "CurrencyId",
                principalTable: "Currencies",
                principalColumn: "CurrencyId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
