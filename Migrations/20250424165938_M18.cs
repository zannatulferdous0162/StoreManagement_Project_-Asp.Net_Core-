using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoreManagement_Project.Migrations
{
    /// <inheritdoc />
    public partial class M18 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LocationComponentId",
                table: "Stocks",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WarehouseId",
                table: "PurchaseOrders",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LocationComponentId",
                table: "GRNs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WarehouseId",
                table: "GRNs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LocationComponentId",
                table: "GRNItems",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Warehouses",
                columns: table => new
                {
                    WarehouseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouses", x => x.WarehouseId);
                });

            migrationBuilder.CreateTable(
                name: "LocationComponents",
                columns: table => new
                {
                    LocationComponentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocationComponentName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LocationComponentType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    WarehouseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationComponents", x => x.LocationComponentId);
                    table.ForeignKey(
                        name: "FK_LocationComponents_LocationComponents_ParentId",
                        column: x => x.ParentId,
                        principalTable: "LocationComponents",
                        principalColumn: "LocationComponentId");
                    table.ForeignKey(
                        name: "FK_LocationComponents_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "WarehouseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_LocationComponentId",
                table: "Stocks",
                column: "LocationComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_WarehouseId",
                table: "PurchaseOrders",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_GRNs_LocationComponentId",
                table: "GRNs",
                column: "LocationComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_GRNs_WarehouseId",
                table: "GRNs",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_GRNItems_LocationComponentId",
                table: "GRNItems",
                column: "LocationComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_LocationComponents_ParentId",
                table: "LocationComponents",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_LocationComponents_WarehouseId",
                table: "LocationComponents",
                column: "WarehouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_GRNItems_LocationComponents_LocationComponentId",
                table: "GRNItems",
                column: "LocationComponentId",
                principalTable: "LocationComponents",
                principalColumn: "LocationComponentId");

            migrationBuilder.AddForeignKey(
                name: "FK_GRNs_LocationComponents_LocationComponentId",
                table: "GRNs",
                column: "LocationComponentId",
                principalTable: "LocationComponents",
                principalColumn: "LocationComponentId");

            migrationBuilder.AddForeignKey(
                name: "FK_GRNs_Warehouses_WarehouseId",
                table: "GRNs",
                column: "WarehouseId",
                principalTable: "Warehouses",
                principalColumn: "WarehouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrders_Warehouses_WarehouseId",
                table: "PurchaseOrders",
                column: "WarehouseId",
                principalTable: "Warehouses",
                principalColumn: "WarehouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_LocationComponents_LocationComponentId",
                table: "Stocks",
                column: "LocationComponentId",
                principalTable: "LocationComponents",
                principalColumn: "LocationComponentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GRNItems_LocationComponents_LocationComponentId",
                table: "GRNItems");

            migrationBuilder.DropForeignKey(
                name: "FK_GRNs_LocationComponents_LocationComponentId",
                table: "GRNs");

            migrationBuilder.DropForeignKey(
                name: "FK_GRNs_Warehouses_WarehouseId",
                table: "GRNs");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrders_Warehouses_WarehouseId",
                table: "PurchaseOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_LocationComponents_LocationComponentId",
                table: "Stocks");

            migrationBuilder.DropTable(
                name: "LocationComponents");

            migrationBuilder.DropTable(
                name: "Warehouses");

            migrationBuilder.DropIndex(
                name: "IX_Stocks_LocationComponentId",
                table: "Stocks");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseOrders_WarehouseId",
                table: "PurchaseOrders");

            migrationBuilder.DropIndex(
                name: "IX_GRNs_LocationComponentId",
                table: "GRNs");

            migrationBuilder.DropIndex(
                name: "IX_GRNs_WarehouseId",
                table: "GRNs");

            migrationBuilder.DropIndex(
                name: "IX_GRNItems_LocationComponentId",
                table: "GRNItems");

            migrationBuilder.DropColumn(
                name: "LocationComponentId",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "WarehouseId",
                table: "PurchaseOrders");

            migrationBuilder.DropColumn(
                name: "LocationComponentId",
                table: "GRNs");

            migrationBuilder.DropColumn(
                name: "WarehouseId",
                table: "GRNs");

            migrationBuilder.DropColumn(
                name: "LocationComponentId",
                table: "GRNItems");
        }
    }
}
