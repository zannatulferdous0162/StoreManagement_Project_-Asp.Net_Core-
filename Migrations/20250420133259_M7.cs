using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoreManagement_Project.Migrations
{
    /// <inheritdoc />
    public partial class M7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_UOMs_UOMId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrderItems_UOMs_UOMId",
                table: "PurchaseOrderItems");

            migrationBuilder.DropTable(
                name: "UOMs");

            migrationBuilder.RenameColumn(
                name: "UOMId",
                table: "PurchaseOrderItems",
                newName: "UnitId");

            migrationBuilder.RenameIndex(
                name: "IX_PurchaseOrderItems_UOMId",
                table: "PurchaseOrderItems",
                newName: "IX_PurchaseOrderItems_UnitId");

            migrationBuilder.RenameColumn(
                name: "UOMId",
                table: "Items",
                newName: "UnitId");

            migrationBuilder.RenameIndex(
                name: "IX_Items_UOMId",
                table: "Items",
                newName: "IX_Items_UnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Units_UnitId",
                table: "Items",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "UnitId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrderItems_Units_UnitId",
                table: "PurchaseOrderItems",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "UnitId",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Units_UnitId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrderItems_Units_UnitId",
                table: "PurchaseOrderItems");

            migrationBuilder.RenameColumn(
                name: "UnitId",
                table: "PurchaseOrderItems",
                newName: "UOMId");

            migrationBuilder.RenameIndex(
                name: "IX_PurchaseOrderItems_UnitId",
                table: "PurchaseOrderItems",
                newName: "IX_PurchaseOrderItems_UOMId");

            migrationBuilder.RenameColumn(
                name: "UnitId",
                table: "Items",
                newName: "UOMId");

            migrationBuilder.RenameIndex(
                name: "IX_Items_UnitId",
                table: "Items",
                newName: "IX_Items_UOMId");

            migrationBuilder.CreateTable(
                name: "UOMs",
                columns: table => new
                {
                    UOMId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UOMName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UOMs", x => x.UOMId);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Items_UOMs_UOMId",
                table: "Items",
                column: "UOMId",
                principalTable: "UOMs",
                principalColumn: "UOMId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrderItems_UOMs_UOMId",
                table: "PurchaseOrderItems",
                column: "UOMId",
                principalTable: "UOMs",
                principalColumn: "UOMId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
