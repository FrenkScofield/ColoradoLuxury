using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ColoradoLuxury.Migrations
{
    public partial class constraint2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Status",
                table: "Cupons",
                type: "bit",
                nullable: false,
                computedColumnSql: "CASE WHEN CouponDeatline > GETDATE() THEN True ELSE False END",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldComputedColumnSql: "CASE WHEN CouponDeatline > GETDATE() THEN 1 ELSE 0 END");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Status",
                table: "Cupons",
                type: "bit",
                nullable: false,
                computedColumnSql: "CASE WHEN CouponDeatline > GETDATE() THEN 1 ELSE 0 END",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldComputedColumnSql: "CASE WHEN CouponDeatline > GETDATE() THEN True ELSE False END");
        }
    }
}
