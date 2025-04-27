using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoreManagement_Project.Migrations
{
    /// <inheritdoc />
    public partial class M25 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LocationComponents_LocationComponents_ParentId",
                table: "LocationComponents");

            migrationBuilder.DropForeignKey(
                name: "FK_LocationComponents_Warehouses_WarehouseId",
                table: "LocationComponents");

            migrationBuilder.DropColumn(
                name: "LocationComponentName",
                table: "LocationComponents");

            migrationBuilder.DropColumn(
                name: "LocationComponentType",
                table: "LocationComponents");

            migrationBuilder.RenameColumn(
                name: "ParentId",
                table: "LocationComponents",
                newName: "ZoneId");

            migrationBuilder.RenameIndex(
                name: "IX_LocationComponents_ParentId",
                table: "LocationComponents",
                newName: "IX_LocationComponents_ZoneId");

            migrationBuilder.AlterColumn<int>(
                name: "WarehouseId",
                table: "LocationComponents",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "AisleId",
                table: "LocationComponents",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BinId",
                table: "LocationComponents",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RackId",
                table: "LocationComponents",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ShelfId",
                table: "LocationComponents",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Aisles",
                columns: table => new
                {
                    AisleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AisleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WarehouseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aisles", x => x.AisleId);
                    table.ForeignKey(
                        name: "FK_Aisles_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "WarehouseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bins",
                columns: table => new
                {
                    BinId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BinName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WarehouseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bins", x => x.BinId);
                    table.ForeignKey(
                        name: "FK_Bins_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "WarehouseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Racks",
                columns: table => new
                {
                    RackId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RackName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WarehouseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Racks", x => x.RackId);
                    table.ForeignKey(
                        name: "FK_Racks_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "WarehouseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "shelves",
                columns: table => new
                {
                    ShelfId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShelfName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WarehouseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_shelves", x => x.ShelfId);
                    table.ForeignKey(
                        name: "FK_shelves_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "WarehouseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Zones",
                columns: table => new
                {
                    ZoneId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ZoneName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WarehouseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zones", x => x.ZoneId);
                    table.ForeignKey(
                        name: "FK_Zones_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "WarehouseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LocationComponents_AisleId",
                table: "LocationComponents",
                column: "AisleId");

            migrationBuilder.CreateIndex(
                name: "IX_LocationComponents_RackId",
                table: "LocationComponents",
                column: "RackId");

            migrationBuilder.CreateIndex(
                name: "IX_LocationComponents_ShelfId",
                table: "LocationComponents",
                column: "ShelfId");

            migrationBuilder.CreateIndex(
                name: "IX_GRNItems_ItemId",
                table: "GRNItems",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Aisles_WarehouseId",
                table: "Aisles",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Bins_WarehouseId",
                table: "Bins",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Racks_WarehouseId",
                table: "Racks",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_shelves_WarehouseId",
                table: "shelves",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Zones_WarehouseId",
                table: "Zones",
                column: "WarehouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_GRNItems_Items_ItemId",
                table: "GRNItems",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "ItemId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LocationComponents_Aisles_AisleId",
                table: "LocationComponents",
                column: "AisleId",
                principalTable: "Aisles",
                principalColumn: "AisleId");

            migrationBuilder.AddForeignKey(
                name: "FK_LocationComponents_Racks_RackId",
                table: "LocationComponents",
                column: "RackId",
                principalTable: "Racks",
                principalColumn: "RackId");

            migrationBuilder.AddForeignKey(
                name: "FK_LocationComponents_Warehouses_WarehouseId",
                table: "LocationComponents",
                column: "WarehouseId",
                principalTable: "Warehouses",
                principalColumn: "WarehouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_LocationComponents_Zones_ZoneId",
                table: "LocationComponents",
                column: "ZoneId",
                principalTable: "Zones",
                principalColumn: "ZoneId");

            migrationBuilder.AddForeignKey(
                name: "FK_LocationComponents_shelves_ShelfId",
                table: "LocationComponents",
                column: "ShelfId",
                principalTable: "shelves",
                principalColumn: "ShelfId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GRNItems_Items_ItemId",
                table: "GRNItems");

            migrationBuilder.DropForeignKey(
                name: "FK_LocationComponents_Aisles_AisleId",
                table: "LocationComponents");

            migrationBuilder.DropForeignKey(
                name: "FK_LocationComponents_Racks_RackId",
                table: "LocationComponents");

            migrationBuilder.DropForeignKey(
                name: "FK_LocationComponents_Warehouses_WarehouseId",
                table: "LocationComponents");

            migrationBuilder.DropForeignKey(
                name: "FK_LocationComponents_Zones_ZoneId",
                table: "LocationComponents");

            migrationBuilder.DropForeignKey(
                name: "FK_LocationComponents_shelves_ShelfId",
                table: "LocationComponents");

            migrationBuilder.DropTable(
                name: "Aisles");

            migrationBuilder.DropTable(
                name: "Bins");

            migrationBuilder.DropTable(
                name: "Racks");

            migrationBuilder.DropTable(
                name: "shelves");

            migrationBuilder.DropTable(
                name: "Zones");

            migrationBuilder.DropIndex(
                name: "IX_LocationComponents_AisleId",
                table: "LocationComponents");

            migrationBuilder.DropIndex(
                name: "IX_LocationComponents_RackId",
                table: "LocationComponents");

            migrationBuilder.DropIndex(
                name: "IX_LocationComponents_ShelfId",
                table: "LocationComponents");

            migrationBuilder.DropIndex(
                name: "IX_GRNItems_ItemId",
                table: "GRNItems");

            migrationBuilder.DropColumn(
                name: "AisleId",
                table: "LocationComponents");

            migrationBuilder.DropColumn(
                name: "BinId",
                table: "LocationComponents");

            migrationBuilder.DropColumn(
                name: "RackId",
                table: "LocationComponents");

            migrationBuilder.DropColumn(
                name: "ShelfId",
                table: "LocationComponents");

            migrationBuilder.RenameColumn(
                name: "ZoneId",
                table: "LocationComponents",
                newName: "ParentId");

            migrationBuilder.RenameIndex(
                name: "IX_LocationComponents_ZoneId",
                table: "LocationComponents",
                newName: "IX_LocationComponents_ParentId");

            migrationBuilder.AlterColumn<int>(
                name: "WarehouseId",
                table: "LocationComponents",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LocationComponentName",
                table: "LocationComponents",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LocationComponentType",
                table: "LocationComponents",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_LocationComponents_LocationComponents_ParentId",
                table: "LocationComponents",
                column: "ParentId",
                principalTable: "LocationComponents",
                principalColumn: "LocationComponentId");

            migrationBuilder.AddForeignKey(
                name: "FK_LocationComponents_Warehouses_WarehouseId",
                table: "LocationComponents",
                column: "WarehouseId",
                principalTable: "Warehouses",
                principalColumn: "WarehouseId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
