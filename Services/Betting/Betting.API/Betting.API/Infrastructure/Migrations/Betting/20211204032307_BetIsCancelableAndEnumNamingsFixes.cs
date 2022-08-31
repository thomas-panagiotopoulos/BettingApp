using Microsoft.EntityFrameworkCore.Migrations;

namespace BettingApp.Services.Betting.API.Infrastructure.Migrations.Betting
{
    public partial class BetIsCancelableAndEnumNamingsFixes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCancelable",
                schema: "betting",
                table: "bets",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                schema: "betting",
                table: "bettingResult",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Won");

            migrationBuilder.UpdateData(
                schema: "betting",
                table: "bettingResult",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Lost");

            migrationBuilder.UpdateData(
                schema: "betting",
                table: "matchResult",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Name", "TypeName" },
                values: new object[] { "WinnerHomeClub", "Winner" });

            migrationBuilder.UpdateData(
                schema: "betting",
                table: "matchResult",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Name", "TypeName" },
                values: new object[] { "WinnerDraw", "Winner" });

            migrationBuilder.UpdateData(
                schema: "betting",
                table: "matchResult",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Name", "TypeName" },
                values: new object[] { "WinnerAwayClub", "Winner" });

            migrationBuilder.UpdateData(
                schema: "betting",
                table: "matchResult",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Name", "TypeName" },
                values: new object[] { "GoalsUnder", "Goals" });

            migrationBuilder.UpdateData(
                schema: "betting",
                table: "matchResult",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Name", "TypeName" },
                values: new object[] { "GoalsOver", "Goals" });

            migrationBuilder.UpdateData(
                schema: "betting",
                table: "status",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Pending");

            migrationBuilder.UpdateData(
                schema: "betting",
                table: "status",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Ongoing");

            migrationBuilder.UpdateData(
                schema: "betting",
                table: "status",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Completed");

            migrationBuilder.UpdateData(
                schema: "betting",
                table: "status",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Canceled");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCancelable",
                schema: "betting",
                table: "bets");

            migrationBuilder.UpdateData(
                schema: "betting",
                table: "bettingResult",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "won");

            migrationBuilder.UpdateData(
                schema: "betting",
                table: "bettingResult",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "lost");

            migrationBuilder.UpdateData(
                schema: "betting",
                table: "matchResult",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Name", "TypeName" },
                values: new object[] { "winnerhomeclub", "winner" });

            migrationBuilder.UpdateData(
                schema: "betting",
                table: "matchResult",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Name", "TypeName" },
                values: new object[] { "winnerdraw", "winner" });

            migrationBuilder.UpdateData(
                schema: "betting",
                table: "matchResult",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Name", "TypeName" },
                values: new object[] { "winnerawayclub", "winner" });

            migrationBuilder.UpdateData(
                schema: "betting",
                table: "matchResult",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Name", "TypeName" },
                values: new object[] { "goalsunder", "goals" });

            migrationBuilder.UpdateData(
                schema: "betting",
                table: "matchResult",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Name", "TypeName" },
                values: new object[] { "goalsover", "goals" });

            migrationBuilder.UpdateData(
                schema: "betting",
                table: "status",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "pending");

            migrationBuilder.UpdateData(
                schema: "betting",
                table: "status",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "ongoing");

            migrationBuilder.UpdateData(
                schema: "betting",
                table: "status",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "completed");

            migrationBuilder.UpdateData(
                schema: "betting",
                table: "status",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "canceled");
        }
    }
}
