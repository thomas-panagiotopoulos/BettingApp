using Microsoft.EntityFrameworkCore.Migrations;

namespace BettingApp.Services.Sportbook.API.Infrastructure.Migrations.Sportsbook
{
    public partial class AddAliasNameToReqType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AliasName",
                table: "requirementType",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "requirementType",
                keyColumn: "Id",
                keyValue: 1,
                column: "AliasName",
                value: "No Req");

            migrationBuilder.UpdateData(
                table: "requirementType",
                keyColumn: "Id",
                keyValue: 2,
                column: "AliasName",
                value: "Min Sel");

            migrationBuilder.UpdateData(
                table: "requirementType",
                keyColumn: "Id",
                keyValue: 3,
                column: "AliasName",
                value: "Min Wag");

            migrationBuilder.UpdateData(
                table: "requirementType",
                keyColumn: "Id",
                keyValue: 4,
                column: "AliasName",
                value: "Max Sel");

            migrationBuilder.UpdateData(
                table: "requirementType",
                keyColumn: "Id",
                keyValue: 5,
                column: "AliasName",
                value: "Max Wag");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AliasName",
                table: "requirementType");
        }
    }
}
