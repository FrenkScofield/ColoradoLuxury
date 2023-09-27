using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ColoradoLuxury.Migrations
{
    public partial class NullabeleTransferTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RideDetails_TransferTypes_TransferTypeId",
                table: "RideDetails");

            migrationBuilder.AlterColumn<int>(
                name: "TransferTypeId",
                table: "RideDetails",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_RideDetails_TransferTypes_TransferTypeId",
                table: "RideDetails",
                column: "TransferTypeId",
                principalTable: "TransferTypes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RideDetails_TransferTypes_TransferTypeId",
                table: "RideDetails");

            migrationBuilder.AlterColumn<int>(
                name: "TransferTypeId",
                table: "RideDetails",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RideDetails_TransferTypes_TransferTypeId",
                table: "RideDetails",
                column: "TransferTypeId",
                principalTable: "TransferTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
