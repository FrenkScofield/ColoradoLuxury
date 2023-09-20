using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ColoradoLuxury.Migrations
{
    public partial class ChangeTypeuserInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserInfos_ArrivalAirlineInfos_ArrivalAirlineInfoId",
                table: "UserInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_UserInfos_BillingAddress_BillingAddressId",
                table: "UserInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_UserInfos_ForDriverBettings_ForDriverbettingId",
                table: "UserInfos");

            migrationBuilder.AlterColumn<int>(
                name: "ForDriverbettingId",
                table: "UserInfos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "BillingAddressId",
                table: "UserInfos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ArrivalAirlineInfoId",
                table: "UserInfos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_UserInfos_ArrivalAirlineInfos_ArrivalAirlineInfoId",
                table: "UserInfos",
                column: "ArrivalAirlineInfoId",
                principalTable: "ArrivalAirlineInfos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserInfos_BillingAddress_BillingAddressId",
                table: "UserInfos",
                column: "BillingAddressId",
                principalTable: "BillingAddress",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserInfos_ForDriverBettings_ForDriverbettingId",
                table: "UserInfos",
                column: "ForDriverbettingId",
                principalTable: "ForDriverBettings",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserInfos_ArrivalAirlineInfos_ArrivalAirlineInfoId",
                table: "UserInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_UserInfos_BillingAddress_BillingAddressId",
                table: "UserInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_UserInfos_ForDriverBettings_ForDriverbettingId",
                table: "UserInfos");

            migrationBuilder.AlterColumn<int>(
                name: "ForDriverbettingId",
                table: "UserInfos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BillingAddressId",
                table: "UserInfos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ArrivalAirlineInfoId",
                table: "UserInfos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserInfos_ArrivalAirlineInfos_ArrivalAirlineInfoId",
                table: "UserInfos",
                column: "ArrivalAirlineInfoId",
                principalTable: "ArrivalAirlineInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserInfos_BillingAddress_BillingAddressId",
                table: "UserInfos",
                column: "BillingAddressId",
                principalTable: "BillingAddress",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserInfos_ForDriverBettings_ForDriverbettingId",
                table: "UserInfos",
                column: "ForDriverbettingId",
                principalTable: "ForDriverBettings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
