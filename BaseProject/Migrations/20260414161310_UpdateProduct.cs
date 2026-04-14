using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BaseProject.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Product");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "SaleProduct",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "SaleProduct");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Product",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
