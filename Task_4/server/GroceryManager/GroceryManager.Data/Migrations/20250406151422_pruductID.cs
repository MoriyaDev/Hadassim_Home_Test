using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GroceryManager.Data.Migrations
{
    public partial class pruductID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProductId",
                table: "Inventorys",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Inventorys");
        }
    }
}
