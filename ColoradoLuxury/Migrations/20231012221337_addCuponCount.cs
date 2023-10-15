using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ColoradoLuxury.Migrations
{
    public partial class addCuponCount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "UseCount",
                table: "Cupons",
                type: "tinyint",
                nullable: true);

            migrationBuilder.AddColumn<byte>(
                name: "UsedCount",
                table: "Cupons",
                type: "tinyint",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UseCount",
                table: "Cupons");

            migrationBuilder.DropColumn(
                name: "UsedCount",
                table: "Cupons");
        }
    }
}
