using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ColoradoLuxury.Migrations
{
    public partial class addRideAndVehicleDetailsForeignKeyId2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserInfos_VehicleInfoDetails_VehicleInfoDetailsId",
                table: "UserInfos");

            migrationBuilder.DropColumn(
                name: "VehicleIndoDetailId",
                table: "UserInfos");

            migrationBuilder.AlterColumn<int>(
                name: "VehicleInfoDetailsId",
                table: "UserInfos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_UserInfos_VehicleInfoDetails_VehicleInfoDetailsId",
                table: "UserInfos",
                column: "VehicleInfoDetailsId",
                principalTable: "VehicleInfoDetails",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserInfos_VehicleInfoDetails_VehicleInfoDetailsId",
                table: "UserInfos");

            migrationBuilder.AlterColumn<int>(
                name: "VehicleInfoDetailsId",
                table: "UserInfos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VehicleIndoDetailId",
                table: "UserInfos",
                type: "int",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserInfos_VehicleInfoDetails_VehicleInfoDetailsId",
                table: "UserInfos",
                column: "VehicleInfoDetailsId",
                principalTable: "VehicleInfoDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
