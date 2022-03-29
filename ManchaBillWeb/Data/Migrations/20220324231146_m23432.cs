using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManchaBillWeb.Data.Migrations
{
    public partial class m23432 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UnitsReturned",
                table: "BillLines",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UnitsReturned",
                table: "BillLines");
        }
    }
}
