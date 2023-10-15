using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ColoradoLuxury.Migrations
{
    public partial class removeUsrid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserUsedCupons_UserInfos_UserInfoId",
                table: "UserUsedCupons");

            migrationBuilder.DropIndex(
                name: "IX_UserUsedCupons_UserInfoId",
                table: "UserUsedCupons");

            migrationBuilder.DropColumn(
                name: "UserInfoId",
                table: "UserUsedCupons");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserInfoId",
                table: "UserUsedCupons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UserUsedCupons_UserInfoId",
                table: "UserUsedCupons",
                column: "UserInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserUsedCupons_UserInfos_UserInfoId",
                table: "UserUsedCupons",
                column: "UserInfoId",
                principalTable: "UserInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
