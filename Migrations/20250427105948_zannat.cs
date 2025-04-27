using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoreManagement_Project.Migrations
{
    /// <inheritdoc />
    public partial class zannat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_LocationComponents_BinId",
                table: "LocationComponents",
                column: "BinId");

            migrationBuilder.AddForeignKey(
                name: "FK_LocationComponents_Bins_BinId",
                table: "LocationComponents",
                column: "BinId",
                principalTable: "Bins",
                principalColumn: "BinId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LocationComponents_Bins_BinId",
                table: "LocationComponents");

            migrationBuilder.DropIndex(
                name: "IX_LocationComponents_BinId",
                table: "LocationComponents");
        }
    }
}
