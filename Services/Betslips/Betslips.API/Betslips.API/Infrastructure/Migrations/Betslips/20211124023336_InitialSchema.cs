using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BettingApp.Services.Betslips.API.Infrastructure.Migrations.Betslips
{
    public partial class InitialSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "betslips");

            migrationBuilder.CreateTable(
                name: "betslips",
                schema: "betslips",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GamblerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    WageredAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalOdd = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PotentialWinnings = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PotentialProfit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsBetable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_betslips", x => x.Id);
                    table.UniqueConstraint("AK_betslips_GamblerId", x => x.GamblerId);
                });

            migrationBuilder.CreateTable(
                name: "matchResult",
                schema: "betslips",
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
                schema: "betslips",
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
                name: "requirementType",
                schema: "betslips",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    AliasName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_requirementType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "wallets",
                schema: "betslips",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GamblerId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PreviousBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LastTimeUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wallets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "selections",
                schema: "betslips",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BetslipId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Odd = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GamblerMatchResultId = table.Column<int>(type: "int", nullable: false),
                    GamblerMatchResultName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SelectionTypeId = table.Column<int>(type: "int", nullable: false),
                    SelectionTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsCanceled = table.Column<bool>(type: "bit", nullable: false),
                    IsDisabled = table.Column<bool>(type: "bit", nullable: false),
                    IsBetable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_selections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_selections_betslips_BetslipId",
                        column: x => x.BetslipId,
                        principalSchema: "betslips",
                        principalTable: "betslips",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_selections_matchResult_GamblerMatchResultId",
                        column: x => x.GamblerMatchResultId,
                        principalSchema: "betslips",
                        principalTable: "matchResult",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "matches",
                schema: "betslips",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SelectionId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RelatedMatchId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HomeClubName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AwayClubName = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                        name: "FK_matches_selections_SelectionId",
                        column: x => x.SelectionId,
                        principalSchema: "betslips",
                        principalTable: "selections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "requirements",
                schema: "betslips",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SelectionId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RelatedMatchId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SelectionTypeId = table.Column<int>(type: "int", nullable: false),
                    RequirementTypeId = table.Column<int>(type: "int", nullable: false),
                    RequirementTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequiredValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsFulfilled = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_requirements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_requirements_requirementType_RequirementTypeId",
                        column: x => x.RequirementTypeId,
                        principalSchema: "betslips",
                        principalTable: "requirementType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_requirements_selections_SelectionId",
                        column: x => x.SelectionId,
                        principalSchema: "betslips",
                        principalTable: "selections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "betslips",
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
                schema: "betslips",
                table: "requirementType",
                columns: new[] { "Id", "AliasName", "Name" },
                values: new object[,]
                {
                    { 1, "No Req", "norequirement" },
                    { 2, "Min Sel", "minimumselections" },
                    { 3, "Min Wag", "minimumwageredamount" },
                    { 4, "Max Sel", "maximumselections" },
                    { 5, "Max Wag", "maximumwageredamount" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_matches_SelectionId",
                schema: "betslips",
                table: "matches",
                column: "SelectionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_requirements_RequirementTypeId",
                schema: "betslips",
                table: "requirements",
                column: "RequirementTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_requirements_SelectionId",
                schema: "betslips",
                table: "requirements",
                column: "SelectionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_selections_BetslipId",
                schema: "betslips",
                table: "selections",
                column: "BetslipId");

            migrationBuilder.CreateIndex(
                name: "IX_selections_GamblerMatchResultId",
                schema: "betslips",
                table: "selections",
                column: "GamblerMatchResultId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "matches",
                schema: "betslips");

            migrationBuilder.DropTable(
                name: "requests",
                schema: "betslips");

            migrationBuilder.DropTable(
                name: "requirements",
                schema: "betslips");

            migrationBuilder.DropTable(
                name: "wallets",
                schema: "betslips");

            migrationBuilder.DropTable(
                name: "requirementType",
                schema: "betslips");

            migrationBuilder.DropTable(
                name: "selections",
                schema: "betslips");

            migrationBuilder.DropTable(
                name: "betslips",
                schema: "betslips");

            migrationBuilder.DropTable(
                name: "matchResult",
                schema: "betslips");
        }
    }
}
