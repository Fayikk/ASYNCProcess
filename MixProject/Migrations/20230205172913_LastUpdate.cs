using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MixProject.Migrations
{
    public partial class LastUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Products",
                newName: "productImage");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "productImage",
                table: "Products",
                newName: "Image");
        }
    }
}
