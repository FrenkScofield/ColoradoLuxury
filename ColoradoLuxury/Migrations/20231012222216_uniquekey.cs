using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ColoradoLuxury.Migrations
{
    public partial class uniquekey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UniqueKey",
                table: "UserUsedCupons",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UniqueKey",
                table: "UserUsedCupons");
        }
    }
}
