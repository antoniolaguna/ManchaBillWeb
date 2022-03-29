using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManchaBillWeb.Data.Migrations
{
    public partial class a234f : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentTypes_PaymentTypes_PaymentTypeId",
                table: "PaymentTypes");

            migrationBuilder.DropIndex(
                name: "IX_PaymentTypes_PaymentTypeId",
                table: "PaymentTypes");

            migrationBuilder.DropColumn(
                name: "PaymentTypeId",
                table: "PaymentTypes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PaymentTypeId",
                table: "PaymentTypes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PaymentTypes_PaymentTypeId",
                table: "PaymentTypes",
                column: "PaymentTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentTypes_PaymentTypes_PaymentTypeId",
                table: "PaymentTypes",
                column: "PaymentTypeId",
                principalTable: "PaymentTypes",
                principalColumn: "Id");
        }
    }
}
