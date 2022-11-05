using Microsoft.EntityFrameworkCore.Migrations;

namespace Dulce.Heladeria.DataAccess.Migrations
{
    public partial class correccionsaleDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SaleDetail_Item_ProductId",
                table: "SaleDetail");

            migrationBuilder.AddForeignKey(
                name: "FK_SaleDetail_Product_ProductId",
                table: "SaleDetail",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SaleDetail_Product_ProductId",
                table: "SaleDetail");

            migrationBuilder.AddForeignKey(
                name: "FK_SaleDetail_Item_ProductId",
                table: "SaleDetail",
                column: "ProductId",
                principalTable: "Item",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
