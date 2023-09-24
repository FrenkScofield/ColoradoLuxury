using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ColoradoLuxury.Migrations
{
    public partial class addIsActiveToVehicleTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "VehicleTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "VehicleTypes");
        }
    }
}
