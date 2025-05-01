using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoreManagement_Project.Migrations
{
    /// <inheritdoc />
    public partial class M6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UnitSets",
                columns: table => new
                {
                    UnitSetId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameOfUnitSet = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitSets", x => x.UnitSetId);
                });

            migrationBuilder.CreateTable(
                name: "Units",
                columns: table => new
                {
                    UnitId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameOfUnit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnitSetId = table.Column<int>(type: "int", nullable: false),
                    UnitFactor = table.Column<double>(type: "float", nullable: false),
                    IsBaseUnit = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Units", x => x.UnitId);
                    table.ForeignKey(
                        name: "FK_Units_UnitSets_UnitSetId",
                        column: x => x.UnitSetId,
                        principalTable: "UnitSets",
                        principalColumn: "UnitSetId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Units_UnitSetId",
                table: "Units",
                column: "UnitSetId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Units");

            migrationBuilder.DropTable(
                name: "UnitSets");
        }
    }
}
