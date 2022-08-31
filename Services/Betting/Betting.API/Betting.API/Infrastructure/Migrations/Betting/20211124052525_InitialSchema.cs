using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BettingApp.Services.Betting.API.Infrastructure.Migrations.Betting
{
    public partial class InitialSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "betting");

            migrationBuilder.CreateTable(
                name: "bettingResult",
                schema: "betting",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bettingResult", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "matchResult",
                schema: "betting",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    AliasName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    TypeId = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    TypeName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_matchResult", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "requests",
                schema: "betting",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_requests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "status",
                schema: "betting",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_status", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "bets",
                schema: "betting",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GamblerId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateTimeCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    StatusName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BetResultId = table.Column<int>(type: "int", nullable: false),
                    BetResultName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WageredAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalOdd = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PotentialWinnings = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PotentialProfit = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_bets_bettingResult_BetResultId",
                        column: x => x.BetResultId,
                        principalSchema: "betting",
                        principalTable: "bettingResult",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_bets_status_StatusId",
                        column: x => x.StatusId,
                        principalSchema: "betting",
                        principalTable: "status",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "selections",
                schema: "betting",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BetId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    StatusName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ResultId = table.Column<int>(type: "int", nullable: false),
                    ResultName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GamblerMatchResultId = table.Column<int>(type: "int", nullable: false),
                    GamblerMatchResultName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SelectionTypeId = table.Column<int>(type: "int", nullable: false),
                    SelectionTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Odd = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_selections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_selections_bets_BetId",
                        column: x => x.BetId,
                        principalSchema: "betting",
                        principalTable: "bets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_selections_bettingResult_ResultId",
                        column: x => x.ResultId,
                        principalSchema: "betting",
                        principalTable: "bettingResult",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_selections_matchResult_GamblerMatchResultId",
                        column: x => x.GamblerMatchResultId,
                        principalSchema: "betting",
                        principalTable: "matchResult",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_selections_status_StatusId",
                        column: x => x.StatusId,
                        principalSchema: "betting",
                        principalTable: "status",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "matches",
                schema: "betting",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SelectionId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RelatedMatchId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HomeClubName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AwayClubName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KincoffDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CurrentMinute = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HomeClubScore = table.Column<int>(type: "int", nullable: false),
                    AwayClubScore = table.Column<int>(type: "int", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    StatusName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WinnerResultId = table.Column<int>(type: "int", nullable: false),
                    WinnerResultName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GoalsResultId = table.Column<int>(type: "int", nullable: false),
                    GoalsResultName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_matches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_matches_matchResult_GoalsResultId",
                        column: x => x.GoalsResultId,
                        principalSchema: "betting",
                        principalTable: "matchResult",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_matches_selections_SelectionId",
                        column: x => x.SelectionId,
                        principalSchema: "betting",
                        principalTable: "selections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_matches_status_StatusId",
                        column: x => x.StatusId,
                        principalSchema: "betting",
                        principalTable: "status",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                schema: "betting",
                table: "bettingResult",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "won" },
                    { 2, "lost" }
                });

            migrationBuilder.InsertData(
                schema: "betting",
                table: "matchResult",
                columns: new[] { "Id", "AliasName", "Name", "TypeId", "TypeName" },
                values: new object[,]
                {
                    { 1, "1", "winnerhomeclub", 1, "winner" },
                    { 2, "X", "winnerdraw", 1, "winner" },
                    { 3, "2", "winnerawayclub", 1, "winner" },
                    { 4, "Under", "goalsunder", 2, "goals" },
                    { 5, "Over", "goalsover", 2, "goals" }
                });

            migrationBuilder.InsertData(
                schema: "betting",
                table: "status",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "pending" },
                    { 2, "ongoing" },
                    { 3, "completed" },
                    { 4, "canceled" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_bets_BetResultId",
                schema: "betting",
                table: "bets",
                column: "BetResultId");

            migrationBuilder.CreateIndex(
                name: "IX_bets_StatusId",
                schema: "betting",
                table: "bets",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_matches_GoalsResultId",
                schema: "betting",
                table: "matches",
                column: "GoalsResultId");

            migrationBuilder.CreateIndex(
                name: "IX_matches_SelectionId",
                schema: "betting",
                table: "matches",
                column: "SelectionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_matches_StatusId",
                schema: "betting",
                table: "matches",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_selections_BetId",
                schema: "betting",
                table: "selections",
                column: "BetId");

            migrationBuilder.CreateIndex(
                name: "IX_selections_GamblerMatchResultId",
                schema: "betting",
                table: "selections",
                column: "GamblerMatchResultId");

            migrationBuilder.CreateIndex(
                name: "IX_selections_ResultId",
                schema: "betting",
                table: "selections",
                column: "ResultId");

            migrationBuilder.CreateIndex(
                name: "IX_selections_StatusId",
                schema: "betting",
                table: "selections",
                column: "StatusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "matches",
                schema: "betting");

            migrationBuilder.DropTable(
                name: "requests",
                schema: "betting");

            migrationBuilder.DropTable(
                name: "selections",
                schema: "betting");

            migrationBuilder.DropTable(
                name: "bets",
                schema: "betting");

            migrationBuilder.DropTable(
                name: "matchResult",
                schema: "betting");

            migrationBuilder.DropTable(
                name: "bettingResult",
                schema: "betting");

            migrationBuilder.DropTable(
                name: "status",
                schema: "betting");
        }
    }
}
