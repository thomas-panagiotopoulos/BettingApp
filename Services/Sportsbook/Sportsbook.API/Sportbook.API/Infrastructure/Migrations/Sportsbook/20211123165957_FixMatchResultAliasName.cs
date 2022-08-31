using Microsoft.EntityFrameworkCore.Migrations;

namespace BettingApp.Services.Sportbook.API.Infrastructure.Migrations.Sportsbook
{
    public partial class FixMatchResultAliasName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "matchResult",
                keyColumn: "Id",
                keyValue: 4,
                column: "AliasName",
                value: "Under");

            migrationBuilder.UpdateData(
                table: "matchResult",
                keyColumn: "Id",
                keyValue: 5,
                column: "AliasName",
                value: "Over");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "matchResult",
                keyColumn: "Id",
                keyValue: 4,
                column: "AliasName",
                value: "Over");

            migrationBuilder.UpdateData(
                table: "matchResult",
                keyColumn: "Id",
                keyValue: 5,
                column: "AliasName",
                value: "Under");
        }
    }
}
