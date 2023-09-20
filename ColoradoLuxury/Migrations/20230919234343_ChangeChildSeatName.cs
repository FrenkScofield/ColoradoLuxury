using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ColoradoLuxury.Migrations
{
    public partial class ChangeChildSeatName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChildSeats");

            migrationBuilder.CreateTable(
                name: "VehicleInfoDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PassengersCount = table.Column<short>(type: "smallint", nullable: false),
                    SuitCasesCount = table.Column<short>(type: "smallint", nullable: false),
                    VehicleTypeId = table.Column<int>(type: "int", nullable: false),
                    ChildSeatCount = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChildSeatDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoofTopCargoBoxCount = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoofTopCargoBoxDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EditDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ClientIpAddress = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleInfoDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VehicleInfoDetails_VehicleTypes_VehicleTypeId",
                        column: x => x.VehicleTypeId,
                        principalTable: "VehicleTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VehicleInfoDetails_VehicleTypeId",
                table: "VehicleInfoDetails",
                column: "VehicleTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VehicleInfoDetails");

            migrationBuilder.CreateTable(
                name: "ChildSeats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientIpAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EditDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChildSeats", x => x.Id);
                });
        }
    }
}
