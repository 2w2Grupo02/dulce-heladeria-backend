using Microsoft.EntityFrameworkCore.Migrations;

namespace Dulce.Heladeria.DataAccess.Migrations
{
    public partial class maxitemamountproductentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaxItemAmount",
                table: "Product",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxItemAmount",
                table: "Product");
        }
    }
}
