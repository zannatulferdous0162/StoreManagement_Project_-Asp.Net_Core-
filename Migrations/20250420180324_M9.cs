using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoreManagement_Project.Migrations
{
    /// <inheritdoc />
    public partial class M9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GRN_PurchaseOrders_PurchaseOrderId",
                table: "GRN");

            migrationBuilder.DropForeignKey(
                name: "FK_GRNItem_GRN_GRNId",
                table: "GRNItem");

            migrationBuilder.DropForeignKey(
                name: "FK_GRNItem_Items_ItemId",
                table: "GRNItem");

            migrationBuilder.DropForeignKey(
                name: "FK_GRNItem_Units_UnitId",
                table: "GRNItem");

            migrationBuilder.DropForeignKey(
                name: "FK_Stock_Items_ItemId",
                table: "Stock");

            migrationBuilder.DropForeignKey(
                name: "FK_Stock_Units_UnitId",
                table: "Stock");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Stock",
                table: "Stock");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GRNItem",
                table: "GRNItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GRN",
                table: "GRN");

            migrationBuilder.RenameTable(
                name: "Stock",
                newName: "Stocks");

            migrationBuilder.RenameTable(
                name: "GRNItem",
                newName: "GRNItems");

            migrationBuilder.RenameTable(
                name: "GRN",
                newName: "GRNs");

            migrationBuilder.RenameIndex(
                name: "IX_Stock_UnitId",
                table: "Stocks",
                newName: "IX_Stocks_UnitId");

            migrationBuilder.RenameIndex(
                name: "IX_Stock_ItemId",
                table: "Stocks",
                newName: "IX_Stocks_ItemId");

            migrationBuilder.RenameIndex(
                name: "IX_GRNItem_UnitId",
                table: "GRNItems",
                newName: "IX_GRNItems_UnitId");

            migrationBuilder.RenameIndex(
                name: "IX_GRNItem_ItemId",
                table: "GRNItems",
                newName: "IX_GRNItems_ItemId");

            migrationBuilder.RenameIndex(
                name: "IX_GRNItem_GRNId",
                table: "GRNItems",
                newName: "IX_GRNItems_GRNId");

            migrationBuilder.RenameIndex(
                name: "IX_GRN_PurchaseOrderId",
                table: "GRNs",
                newName: "IX_GRNs_PurchaseOrderId");

            migrationBuilder.AlterColumn<string>(
                name: "GRNNumber",
                table: "GRNs",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Stocks",
                table: "Stocks",
                column: "StockId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GRNItems",
                table: "GRNItems",
                column: "GRNItemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GRNs",
                table: "GRNs",
                column: "GRNId");

            migrationBuilder.CreateIndex(
                name: "IX_GRNs_GRNNumber",
                table: "GRNs",
                column: "GRNNumber",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_GRNItems_GRNs_GRNId",
                table: "GRNItems",
                column: "GRNId",
                principalTable: "GRNs",
                principalColumn: "GRNId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GRNItems_Items_ItemId",
                table: "GRNItems",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "ItemId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GRNItems_Units_UnitId",
                table: "GRNItems",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "UnitId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GRNs_PurchaseOrders_PurchaseOrderId",
                table: "GRNs",
                column: "PurchaseOrderId",
                principalTable: "PurchaseOrders",
                principalColumn: "PurchaseOrderId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_Items_ItemId",
                table: "Stocks",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "ItemId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_Units_UnitId",
                table: "Stocks",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "UnitId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GRNItems_GRNs_GRNId",
                table: "GRNItems");

            migrationBuilder.DropForeignKey(
                name: "FK_GRNItems_Items_ItemId",
                table: "GRNItems");

            migrationBuilder.DropForeignKey(
                name: "FK_GRNItems_Units_UnitId",
                table: "GRNItems");

            migrationBuilder.DropForeignKey(
                name: "FK_GRNs_PurchaseOrders_PurchaseOrderId",
                table: "GRNs");

            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_Items_ItemId",
                table: "Stocks");

            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_Units_UnitId",
                table: "Stocks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Stocks",
                table: "Stocks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GRNs",
                table: "GRNs");

            migrationBuilder.DropIndex(
                name: "IX_GRNs_GRNNumber",
                table: "GRNs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GRNItems",
                table: "GRNItems");

            migrationBuilder.RenameTable(
                name: "Stocks",
                newName: "Stock");

            migrationBuilder.RenameTable(
                name: "GRNs",
                newName: "GRN");

            migrationBuilder.RenameTable(
                name: "GRNItems",
                newName: "GRNItem");

            migrationBuilder.RenameIndex(
                name: "IX_Stocks_UnitId",
                table: "Stock",
                newName: "IX_Stock_UnitId");

            migrationBuilder.RenameIndex(
                name: "IX_Stocks_ItemId",
                table: "Stock",
                newName: "IX_Stock_ItemId");

            migrationBuilder.RenameIndex(
                name: "IX_GRNs_PurchaseOrderId",
                table: "GRN",
                newName: "IX_GRN_PurchaseOrderId");

            migrationBuilder.RenameIndex(
                name: "IX_GRNItems_UnitId",
                table: "GRNItem",
                newName: "IX_GRNItem_UnitId");

            migrationBuilder.RenameIndex(
                name: "IX_GRNItems_ItemId",
                table: "GRNItem",
                newName: "IX_GRNItem_ItemId");

            migrationBuilder.RenameIndex(
                name: "IX_GRNItems_GRNId",
                table: "GRNItem",
                newName: "IX_GRNItem_GRNId");

            migrationBuilder.AlterColumn<string>(
                name: "GRNNumber",
                table: "GRN",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Stock",
                table: "Stock",
                column: "StockId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GRN",
                table: "GRN",
                column: "GRNId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GRNItem",
                table: "GRNItem",
                column: "GRNItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_GRN_PurchaseOrders_PurchaseOrderId",
                table: "GRN",
                column: "PurchaseOrderId",
                principalTable: "PurchaseOrders",
                principalColumn: "PurchaseOrderId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GRNItem_GRN_GRNId",
                table: "GRNItem",
                column: "GRNId",
                principalTable: "GRN",
                principalColumn: "GRNId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GRNItem_Items_ItemId",
                table: "GRNItem",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "ItemId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GRNItem_Units_UnitId",
                table: "GRNItem",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "UnitId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stock_Items_ItemId",
                table: "Stock",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "ItemId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stock_Units_UnitId",
                table: "Stock",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "UnitId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
