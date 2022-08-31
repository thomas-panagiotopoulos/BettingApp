using Microsoft.EntityFrameworkCore.Migrations;

namespace BettingApp.Services.Sportbook.API.Infrastructure.Migrations.Sportsbook
{
    public partial class AddedInitialOdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "InitialOdd",
                table: "possiblePicks",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InitialOdd",
                table: "possiblePicks");
        }
    }
}
