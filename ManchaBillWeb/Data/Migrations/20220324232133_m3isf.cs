using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManchaBillWeb.Data.Migrations
{
    public partial class m3isf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UnitsReturned",
                table: "BillLines",
                newName: "ReturnedUnits");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReturnedUnits",
                table: "BillLines",
                newName: "UnitsReturned");
        }
    }
}
