using Microsoft.EntityFrameworkCore.Migrations;

namespace BettingApp.Services.Betting.API.Infrastructure.Migrations.Betting
{
    public partial class AddedInitialTotalOddWinningsAndProfit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "InitialPotentialProfit",
                schema: "betting",
                table: "bets",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "InitialPotentialWinnings",
                schema: "betting",
                table: "bets",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "InitialTotalOdd",
                schema: "betting",
                table: "bets",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InitialPotentialProfit",
                schema: "betting",
                table: "bets");

            migrationBuilder.DropColumn(
                name: "InitialPotentialWinnings",
                schema: "betting",
                table: "bets");

            migrationBuilder.DropColumn(
                name: "InitialTotalOdd",
                schema: "betting",
                table: "bets");
        }
    }
}
