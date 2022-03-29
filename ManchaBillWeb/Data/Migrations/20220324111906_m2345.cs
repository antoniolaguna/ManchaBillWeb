using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManchaBillWeb.Data.Migrations
{
    public partial class m2345 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Nombre",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nombre",
                table: "AspNetUsers");
        }
    }
}
