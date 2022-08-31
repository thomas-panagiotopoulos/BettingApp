using Microsoft.EntityFrameworkCore.Migrations;

namespace BettingApp.Services.Betslips.API.Infrastructure.Migrations.Betslips
{
    public partial class AddedLatestAdditionIdToBetslip : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LatestAdditionId",
                schema: "betslips",
                table: "betslips",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LatestAdditionId",
                schema: "betslips",
                table: "betslips");
        }
    }
}
