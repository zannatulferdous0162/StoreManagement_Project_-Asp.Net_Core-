using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoreManagement_Project.Migrations
{
    /// <inheritdoc />
    public partial class M16 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_GRNs_GRNNumber",
                table: "GRNs");

            migrationBuilder.AlterColumn<string>(
                name: "GRNNumber",
                table: "GRNs",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_GRNs_GRNNumber",
                table: "GRNs",
                column: "GRNNumber",
                unique: true,
                filter: "[GRNNumber] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_GRNs_GRNNumber",
                table: "GRNs");

            migrationBuilder.AlterColumn<string>(
                name: "GRNNumber",
                table: "GRNs",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GRNs_GRNNumber",
                table: "GRNs",
                column: "GRNNumber",
                unique: true);
        }
    }
}
