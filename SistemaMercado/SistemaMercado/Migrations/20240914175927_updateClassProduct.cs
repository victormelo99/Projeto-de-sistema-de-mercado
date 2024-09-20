using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaMercado.Migrations
{
    /// <inheritdoc />
    public partial class updateClassProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductSize",
                table: "BuyProduct");

            migrationBuilder.DropColumn(
                name: "UnitOfMeasure",
                table: "BuyProduct");

            migrationBuilder.AddColumn<decimal>(
                name: "ProductSize",
                table: "Product",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "UnitOfMeasure",
                table: "Product",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductSize",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "UnitOfMeasure",
                table: "Product");

            migrationBuilder.AddColumn<decimal>(
                name: "ProductSize",
                table: "BuyProduct",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "UnitOfMeasure",
                table: "BuyProduct",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
