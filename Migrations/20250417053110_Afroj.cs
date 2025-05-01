using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoreManagement_Project.Migrations
{
    /// <inheritdoc />
    public partial class Afroj : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ItemCode",
                table: "Items",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ItemCode",
                table: "Items");
        }
    }
}
