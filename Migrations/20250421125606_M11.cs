using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoreManagement_Project.Migrations
{
    /// <inheritdoc />
    public partial class M11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GRNItems_Items_ItemId",
                table: "GRNItems");

            migrationBuilder.DropForeignKey(
                name: "FK_GRNItems_Units_UnitId",
                table: "GRNItems");

            migrationBuilder.DropIndex(
                name: "IX_GRNItems_ItemId",
                table: "GRNItems");

            migrationBuilder.DropIndex(
                name: "IX_GRNItems_UnitId",
                table: "GRNItems");

            migrationBuilder.RenameColumn(
                name: "UnitId",
                table: "GRNItems",
                newName: "Quantity");

            migrationBuilder.AddColumn<DateTime>(
                name: "InvoiceDate",
                table: "GRNs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "InvoiceNo",
                table: "GRNs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SupplierId",
                table: "GRNs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ItemName",
                table: "GRNItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UnitName",
                table: "GRNItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_GRNs_SupplierId",
                table: "GRNs",
                column: "SupplierId");

            migrationBuilder.AddForeignKey(
                name: "FK_GRNs_Suppliers_SupplierId",
                table: "GRNs",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "SupplierId",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GRNs_Suppliers_SupplierId",
                table: "GRNs");

            migrationBuilder.DropIndex(
                name: "IX_GRNs_SupplierId",
                table: "GRNs");

            migrationBuilder.DropColumn(
                name: "InvoiceDate",
                table: "GRNs");

            migrationBuilder.DropColumn(
                name: "InvoiceNo",
                table: "GRNs");

            migrationBuilder.DropColumn(
                name: "SupplierId",
                table: "GRNs");

            migrationBuilder.DropColumn(
                name: "ItemName",
                table: "GRNItems");

            migrationBuilder.DropColumn(
                name: "UnitName",
                table: "GRNItems");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "GRNItems",
                newName: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_GRNItems_ItemId",
                table: "GRNItems",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_GRNItems_UnitId",
                table: "GRNItems",
                column: "UnitId");

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
        }
    }
}
