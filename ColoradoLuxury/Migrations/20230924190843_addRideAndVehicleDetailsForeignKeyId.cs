using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ColoradoLuxury.Migrations
{
    public partial class addRideAndVehicleDetailsForeignKeyId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RideDetailId",
                table: "UserInfos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VehicleIndoDetailId",
                table: "UserInfos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VehicleInfoDetailsId",
                table: "UserInfos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UserInfos_RideDetailId",
                table: "UserInfos",
                column: "RideDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_UserInfos_VehicleInfoDetailsId",
                table: "UserInfos",
                column: "VehicleInfoDetailsId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserInfos_RideDetails_RideDetailId",
                table: "UserInfos",
                column: "RideDetailId",
                principalTable: "RideDetails",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserInfos_VehicleInfoDetails_VehicleInfoDetailsId",
                table: "UserInfos",
                column: "VehicleInfoDetailsId",
                principalTable: "VehicleInfoDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserInfos_RideDetails_RideDetailId",
                table: "UserInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_UserInfos_VehicleInfoDetails_VehicleInfoDetailsId",
                table: "UserInfos");

            migrationBuilder.DropIndex(
                name: "IX_UserInfos_RideDetailId",
                table: "UserInfos");

            migrationBuilder.DropIndex(
                name: "IX_UserInfos_VehicleInfoDetailsId",
                table: "UserInfos");

            migrationBuilder.DropColumn(
                name: "RideDetailId",
                table: "UserInfos");

            migrationBuilder.DropColumn(
                name: "VehicleIndoDetailId",
                table: "UserInfos");

            migrationBuilder.DropColumn(
                name: "VehicleInfoDetailsId",
                table: "UserInfos");
        }
    }
}
