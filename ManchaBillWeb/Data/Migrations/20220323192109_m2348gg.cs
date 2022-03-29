using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManchaBillWeb.Data.Migrations
{
    public partial class m2348gg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Parameters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parameters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentTypeId = table.Column<int>(type: "int", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentTypes_PaymentTypes_PaymentTypeId",
                        column: x => x.PaymentTypeId,
                        principalTable: "PaymentTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Seasons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seasons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SizeTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SizeTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EMail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fax = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bills",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MoneyDelivered = table.Column<double>(type: "float", nullable: false),
                    MoneyReturned = table.Column<double>(type: "float", nullable: false),
                    SeriesBill = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    YearBill = table.Column<int>(type: "int", nullable: false),
                    NumberBill = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Discount = table.Column<double>(type: "float", nullable: false),
                    Prize = table.Column<double>(type: "float", nullable: false),
                    Tax = table.Column<double>(type: "float", nullable: false),
                    Value = table.Column<double>(type: "float", nullable: false),
                    PaymentTypeId = table.Column<int>(type: "int", nullable: false),
                    Client = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bills_PaymentTypes_PaymentTypeId",
                        column: x => x.PaymentTypeId,
                        principalTable: "PaymentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SizeTypeId = table.Column<int>(type: "int", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemTypes_SizeTypes_SizeTypeId",
                        column: x => x.SizeTypeId,
                        principalTable: "SizeTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sizes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SizeTypeId = table.Column<int>(type: "int", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sizes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sizes_SizeTypes_SizeTypeId",
                        column: x => x.SizeTypeId,
                        principalTable: "SizeTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Returns",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BillId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SeriesReturn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    YearReturn = table.Column<int>(type: "int", nullable: false),
                    NumberReturn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prize = table.Column<double>(type: "float", nullable: false),
                    Tax = table.Column<double>(type: "float", nullable: false),
                    Value = table.Column<double>(type: "float", nullable: false),
                    Client = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Returns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Returns_Bills_BillId",
                        column: x => x.BillId,
                        principalTable: "Bills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShortDescription = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    LongDescription = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    CostPrize = table.Column<double>(type: "float", nullable: false),
                    Value = table.Column<double>(type: "float", nullable: false),
                    Tax = table.Column<double>(type: "float", nullable: false),
                    Discount = table.Column<double>(type: "float", nullable: false),
                    Prize = table.Column<double>(type: "float", nullable: false),
                    ItemTypeId = table.Column<int>(type: "int", nullable: false),
                    SupplierId = table.Column<int>(type: "int", nullable: true),
                    SeasonId = table.Column<int>(type: "int", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Items_ItemTypes_ItemTypeId",
                        column: x => x.ItemTypeId,
                        principalTable: "ItemTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Items_Seasons_SeasonId",
                        column: x => x.SeasonId,
                        principalTable: "Seasons",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Items_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Models",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Colour = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Barcode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountStore = table.Column<int>(type: "int", nullable: false),
                    CountSold = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    SizeId = table.Column<int>(type: "int", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Models", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Models_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Models_Sizes_SizeId",
                        column: x => x.SizeId,
                        principalTable: "Sizes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BillLines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Units = table.Column<int>(type: "int", nullable: false),
                    UnitPrize = table.Column<double>(type: "float", nullable: false),
                    Tax = table.Column<double>(type: "float", nullable: false),
                    Discount = table.Column<double>(type: "float", nullable: false),
                    Sum = table.Column<double>(type: "float", nullable: false),
                    ModelId = table.Column<int>(type: "int", nullable: true),
                    BillId = table.Column<int>(type: "int", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillLines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BillLines_Bills_BillId",
                        column: x => x.BillId,
                        principalTable: "Bills",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BillLines_Models_ModelId",
                        column: x => x.ModelId,
                        principalTable: "Models",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ReturnLines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Units = table.Column<int>(type: "int", nullable: false),
                    UnitPrize = table.Column<double>(type: "float", nullable: false),
                    Tax = table.Column<double>(type: "float", nullable: false),
                    Discount = table.Column<double>(type: "float", nullable: false),
                    Sum = table.Column<double>(type: "float", nullable: false),
                    ModelId = table.Column<int>(type: "int", nullable: true),
                    ReturnId = table.Column<int>(type: "int", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReturnLines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReturnLines_Models_ModelId",
                        column: x => x.ModelId,
                        principalTable: "Models",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReturnLines_Returns_ReturnId",
                        column: x => x.ReturnId,
                        principalTable: "Returns",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BillLines_BillId",
                table: "BillLines",
                column: "BillId");

            migrationBuilder.CreateIndex(
                name: "IX_BillLines_ModelId",
                table: "BillLines",
                column: "ModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_PaymentTypeId",
                table: "Bills",
                column: "PaymentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_ItemTypeId",
                table: "Items",
                column: "ItemTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_SeasonId",
                table: "Items",
                column: "SeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_SupplierId",
                table: "Items",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemTypes_SizeTypeId",
                table: "ItemTypes",
                column: "SizeTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Models_ItemId",
                table: "Models",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Models_SizeId",
                table: "Models",
                column: "SizeId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentTypes_PaymentTypeId",
                table: "PaymentTypes",
                column: "PaymentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ReturnLines_ModelId",
                table: "ReturnLines",
                column: "ModelId");

            migrationBuilder.CreateIndex(
                name: "IX_ReturnLines_ReturnId",
                table: "ReturnLines",
                column: "ReturnId");

            migrationBuilder.CreateIndex(
                name: "IX_Returns_BillId",
                table: "Returns",
                column: "BillId");

            migrationBuilder.CreateIndex(
                name: "IX_Sizes_SizeTypeId",
                table: "Sizes",
                column: "SizeTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BillLines");

            migrationBuilder.DropTable(
                name: "Parameters");

            migrationBuilder.DropTable(
                name: "ReturnLines");

            migrationBuilder.DropTable(
                name: "Models");

            migrationBuilder.DropTable(
                name: "Returns");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Sizes");

            migrationBuilder.DropTable(
                name: "Bills");

            migrationBuilder.DropTable(
                name: "ItemTypes");

            migrationBuilder.DropTable(
                name: "Seasons");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropTable(
                name: "PaymentTypes");

            migrationBuilder.DropTable(
                name: "SizeTypes");
        }
    }
}
