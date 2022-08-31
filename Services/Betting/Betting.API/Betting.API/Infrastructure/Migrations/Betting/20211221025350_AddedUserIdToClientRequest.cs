using Microsoft.EntityFrameworkCore.Migrations;

namespace BettingApp.Services.Betting.API.Infrastructure.Migrations.Betting
{
    public partial class AddedUserIdToClientRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                schema: "betting",
                table: "requests",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "betting",
                table: "requests");
        }
    }
}
