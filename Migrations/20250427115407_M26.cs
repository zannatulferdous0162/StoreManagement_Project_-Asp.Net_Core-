using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoreManagement_Project.Migrations
{
    /// <inheritdoc />
    public partial class M26 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_LocationComponents_AisleId",
                table: "LocationComponents",
                column: "AisleId");

            migrationBuilder.AddForeignKey(
                name: "FK_LocationComponents_Aisles_AisleId",
                table: "LocationComponents",
                column: "AisleId",
                principalTable: "Aisles",
                principalColumn: "AisleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LocationComponents_Aisles_AisleId",
                table: "LocationComponents");

            migrationBuilder.DropIndex(
                name: "IX_LocationComponents_AisleId",
                table: "LocationComponents");
        }
    }
}
