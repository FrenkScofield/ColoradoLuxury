using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ColoradoLuxury.Migrations
{
    public partial class ChangeNullableContactDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArrivalAirlineInfos_AirLines_AirlineId",
                table: "ArrivalAirlineInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_BillingAddress_Countries_CountryId",
                table: "BillingAddress");

            migrationBuilder.AlterColumn<int>(
                name: "CountryId",
                table: "BillingAddress",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "AirlineId",
                table: "ArrivalAirlineInfos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_ArrivalAirlineInfos_AirLines_AirlineId",
                table: "ArrivalAirlineInfos",
                column: "AirlineId",
                principalTable: "AirLines",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BillingAddress_Countries_CountryId",
                table: "BillingAddress",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArrivalAirlineInfos_AirLines_AirlineId",
                table: "ArrivalAirlineInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_BillingAddress_Countries_CountryId",
                table: "BillingAddress");

            migrationBuilder.AlterColumn<int>(
                name: "CountryId",
                table: "BillingAddress",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AirlineId",
                table: "ArrivalAirlineInfos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ArrivalAirlineInfos_AirLines_AirlineId",
                table: "ArrivalAirlineInfos",
                column: "AirlineId",
                principalTable: "AirLines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BillingAddress_Countries_CountryId",
                table: "BillingAddress",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
