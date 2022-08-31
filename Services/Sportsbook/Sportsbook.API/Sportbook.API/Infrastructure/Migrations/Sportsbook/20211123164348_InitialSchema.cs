using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BettingApp.Services.Sportbook.API.Infrastructure.Migrations.Sportsbook
{
    public partial class InitialSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "league",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_league", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "matchResult",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    AliasName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_matchResult", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "requirementType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_requirementType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "matches",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    HomeClubName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AwayClubName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LeagueId = table.Column<int>(type: "int", nullable: false),
                    LeagueName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KickoffDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CurrentMinute = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HomeClubScore = table.Column<int>(type: "int", nullable: false),
                    AwayClubScore = table.Column<int>(type: "int", nullable: false),
                    IsCanceled = table.Column<bool>(type: "bit", nullable: false),
                    IsBetable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_matches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_matches_league_LeagueId",
                        column: x => x.LeagueId,
                        principalTable: "league",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "possiblePicks",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MatchId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MatchResultId = table.Column<int>(type: "int", nullable: false),
                    MatchResultName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Odd = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RequirementTypeId = table.Column<int>(type: "int", nullable: false),
                    RequirementTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequiredValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsCanceled = table.Column<bool>(type: "bit", nullable: false),
                    IsDisabled = table.Column<bool>(type: "bit", nullable: false),
                    IsBetable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_possiblePicks", x => x.Id);
                    table.UniqueConstraint("AK_possiblePicks_MatchId_MatchResultId", x => new { x.MatchId, x.MatchResultId });
                    table.ForeignKey(
                        name: "FK_possiblePicks_matches_MatchId",
                        column: x => x.MatchId,
                        principalTable: "matches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_possiblePicks_matchResult_MatchResultId",
                        column: x => x.MatchResultId,
                        principalTable: "matchResult",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_possiblePicks_requirementType_RequirementTypeId",
                        column: x => x.RequirementTypeId,
                        principalTable: "requirementType",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "league",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "GreekSuperLeague" },
                    { 9, "EuropaConferenceLeague" },
                    { 8, "EuropaLeague" },
                    { 7, "ChampionsLeague" },
                    { 6, "NoDomesticLeague" },
                    { 10, "NoContinentalLeague" },
                    { 4, "ItalianSerieA" },
                    { 3, "SpanishLaLiga" },
                    { 2, "EnglishPremierLeague" },
                    { 5, "GermanBundesliga" }
                });

            migrationBuilder.InsertData(
                table: "matchResult",
                columns: new[] { "Id", "AliasName", "Name" },
                values: new object[,]
                {
                    { 1, "1", "winnerhomeclub" },
                    { 2, "X", "winnerdraw" },
                    { 3, "2", "winnerawayclub" },
                    { 4, "Over", "goalsunder" },
                    { 5, "Under", "goalsover" }
                });

            migrationBuilder.InsertData(
                table: "requirementType",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 4, "maximumselections" },
                    { 1, "norequirement" },
                    { 2, "minimumselections" },
                    { 3, "minimumwageredamount" },
                    { 5, "maximumwageredamount" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_matches_LeagueId",
                table: "matches",
                column: "LeagueId");

            migrationBuilder.CreateIndex(
                name: "IX_possiblePicks_MatchResultId",
                table: "possiblePicks",
                column: "MatchResultId");

            migrationBuilder.CreateIndex(
                name: "IX_possiblePicks_RequirementTypeId",
                table: "possiblePicks",
                column: "RequirementTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "possiblePicks");

            migrationBuilder.DropTable(
                name: "matches");

            migrationBuilder.DropTable(
                name: "matchResult");

            migrationBuilder.DropTable(
                name: "requirementType");

            migrationBuilder.DropTable(
                name: "league");
        }
    }
}
