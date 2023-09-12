using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ColoradoLuxury.Migrations
{
    public partial class AddCuponAndStatuses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "VehicleTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "TransferTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Cupons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NewCupon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Percentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CuponCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EditDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClientIpAddress = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cupons", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cupons");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "VehicleTypes");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "TransferTypes");
        }
    }
}
