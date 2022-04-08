using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManchaBillWeb.Data.Migrations
{
    public partial class ashudfhsduhf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CashRegisters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OpeningDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClosingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Close = table.Column<bool>(type: "bit", nullable: false),
                    LastCloseRemander = table.Column<double>(type: "float", nullable: false),
                    CashSales = table.Column<double>(type: "float", nullable: false),
                    OutFlows = table.Column<double>(type: "float", nullable: false),
                    RealCash = table.Column<double>(type: "float", nullable: false),
                    CashOut = table.Column<double>(type: "float", nullable: false),
                    Remander = table.Column<double>(type: "float", nullable: false),
                    Unbalance = table.Column<double>(type: "float", nullable: false),
                    UnbalancePercentage = table.Column<double>(type: "float", nullable: false),
                    Observations = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CashRegisters", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CashRegisters");
        }
    }
}
