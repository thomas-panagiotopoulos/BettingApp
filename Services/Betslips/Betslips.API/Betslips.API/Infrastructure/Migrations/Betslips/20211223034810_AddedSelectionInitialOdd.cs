using Microsoft.EntityFrameworkCore.Migrations;

namespace BettingApp.Services.Betslips.API.Infrastructure.Migrations.Betslips
{
    public partial class AddedSelectionInitialOdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "InitialOdd",
                schema: "betslips",
                table: "selections",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InitialOdd",
                schema: "betslips",
                table: "selections");
        }
    }
}
