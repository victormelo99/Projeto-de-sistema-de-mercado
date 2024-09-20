﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaMercado.Migrations
{
    /// <inheritdoc />
    public partial class createdProductPrice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "productPrice",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UnitValue = table.Column<decimal>(type: "numeric(6,2)", nullable: false),
                    ProfitPercentage = table.Column<decimal>(type: "numeric(6,2)", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_productPrice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_productPrice_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_productPrice_ProductId",
                table: "productPrice",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "productPrice");
        }
    }
}
