using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaMercado.Migrations
{
    /// <inheritdoc />
    public partial class update6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalValue",
                table: "Sale",
                newName: "TotalPricePerItem");

            migrationBuilder.RenameColumn(
                name: "SaleValue",
                table: "Sale",
                newName: "SaleValueProduct");

            migrationBuilder.RenameColumn(
                name: "UnitValue",
                table: "productPrice",
                newName: "SaleUnitValue");

            migrationBuilder.AddColumn<string>(
                name: "Barcode",
                table: "Sale",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<decimal>(
                name: "ProfitPercentage",
                table: "productPrice",
                type: "numeric(6,3)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(6,2)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Barcode",
                table: "Sale");

            migrationBuilder.RenameColumn(
                name: "TotalPricePerItem",
                table: "Sale",
                newName: "TotalValue");

            migrationBuilder.RenameColumn(
                name: "SaleValueProduct",
                table: "Sale",
                newName: "SaleValue");

            migrationBuilder.RenameColumn(
                name: "SaleUnitValue",
                table: "productPrice",
                newName: "UnitValue");

            migrationBuilder.AlterColumn<decimal>(
                name: "ProfitPercentage",
                table: "productPrice",
                type: "numeric(6,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(6,3)");
        }
    }
}
