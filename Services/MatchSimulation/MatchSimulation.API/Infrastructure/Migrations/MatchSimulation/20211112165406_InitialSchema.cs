using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BettingApp.Services.MatchSimulation.API.Infrastructure.Migrations.MatchSimulation
{
    public partial class InitialSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "matchsimulation");

            migrationBuilder.CreateTable(
                name: "league",
                schema: "matchsimulation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    TypeId = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    TypeName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_league", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "matchResult",
                schema: "matchsimulation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    TypeId = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    TypeName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_matchResult", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "requirementType",
                schema: "matchsimulation",
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
                name: "status",
                schema: "matchsimulation",
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
                name: "clubs",
                schema: "matchsimulation",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DomesticLeagueId = table.Column<int>(type: "int", nullable: false),
                    DomesticLeagueName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContinentalLeagueId = table.Column<int>(type: "int", nullable: false),
                    ContinentalLeagueName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AttackStat = table.Column<int>(type: "int", nullable: false),
                    DefenceStat = table.Column<int>(type: "int", nullable: false),
                    FormStat = table.Column<int>(type: "int", nullable: false),
                    HasActiveSimulation = table.Column<bool>(type: "bit", nullable: false),
                    ActiveSimulationId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActiveMatchId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clubs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_clubs_league_ContinentalLeagueId",
                        column: x => x.ContinentalLeagueId,
                        principalSchema: "matchsimulation",
                        principalTable: "league",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_clubs_league_DomesticLeagueId",
                        column: x => x.DomesticLeagueId,
                        principalSchema: "matchsimulation",
                        principalTable: "league",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "matches",
                schema: "matchsimulation",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SimulationId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HomeClubId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HomeClubName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AwayClubId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AwayClubName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LeagueId = table.Column<int>(type: "int", nullable: false),
                    LeagueName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KickoffDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CurrentMinute = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HomeClubScore = table.Column<int>(type: "int", nullable: false),
                    AwayClubScore = table.Column<int>(type: "int", nullable: false),
                    IsCanceled = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_matches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_matches_league_LeagueId",
                        column: x => x.LeagueId,
                        principalSchema: "matchsimulation",
                        principalTable: "league",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "simulations",
                schema: "matchsimulation",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MatchId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrentMinute = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrentMinuteInt = table.Column<int>(type: "int", nullable: false),
                    HomeClubScore = table.Column<int>(type: "int", nullable: false),
                    AwayClubScore = table.Column<int>(type: "int", nullable: false),
                    GoalsScored = table.Column<int>(type: "int", nullable: false),
                    RemainingExtraTimeMinutes = table.Column<int>(type: "int", nullable: false),
                    CurrentExtraTimeMinute = table.Column<int>(type: "int", nullable: false),
                    MinutesPassedInHalfTime = table.Column<int>(type: "int", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    StatusName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_simulations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_simulations_status_StatusId",
                        column: x => x.StatusId,
                        principalSchema: "matchsimulation",
                        principalTable: "status",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "possiblePicks",
                schema: "matchsimulation",
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
                    IsDisabled = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_possiblePicks", x => x.Id);
                    table.UniqueConstraint("AK_possiblePicks_MatchId_MatchResultId", x => new { x.MatchId, x.MatchResultId });
                    table.ForeignKey(
                        name: "FK_possiblePicks_matches_MatchId",
                        column: x => x.MatchId,
                        principalSchema: "matchsimulation",
                        principalTable: "matches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_possiblePicks_matchResult_MatchResultId",
                        column: x => x.MatchResultId,
                        principalSchema: "matchsimulation",
                        principalTable: "matchResult",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_possiblePicks_requirementType_RequirementTypeId",
                        column: x => x.RequirementTypeId,
                        principalSchema: "matchsimulation",
                        principalTable: "requirementType",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                schema: "matchsimulation",
                table: "league",
                columns: new[] { "Id", "Name", "TypeId", "TypeName" },
                values: new object[,]
                {
                    { 2, "EnglishPremierLeague", 1, "domestic" },
                    { 10, "NoContinentalLeague", 2, "continental" },
                    { 9, "EuropaConferenceLeague", 2, "continental" },
                    { 8, "EuropaLeague", 2, "continental" },
                    { 7, "ChampionsLeague", 2, "continental" },
                    { 6, "NoDomesticLeague", 1, "domestic" },
                    { 5, "GermanBundesliga", 1, "domestic" },
                    { 4, "ItalianSerieA", 1, "domestic" },
                    { 3, "SpanishLaLiga", 1, "domestic" },
                    { 1, "GreekSuperLeague", 1, "domestic" }
                });

            migrationBuilder.InsertData(
                schema: "matchsimulation",
                table: "matchResult",
                columns: new[] { "Id", "Name", "TypeId", "TypeName" },
                values: new object[,]
                {
                    { 1, "winnerhomeclub", 1, "winner" },
                    { 5, "goalsover", 2, "goals" },
                    { 4, "goalsunder", 2, "goals" },
                    { 3, "winnerawayclub", 1, "winner" },
                    { 2, "winnerdraw", 1, "winner" }
                });

            migrationBuilder.InsertData(
                schema: "matchsimulation",
                table: "requirementType",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 4, "maximumselections" },
                    { 3, "minimumwageredamount" },
                    { 2, "minimumselections" },
                    { 1, "norequirement" },
                    { 5, "maximumwageredamount" }
                });

            migrationBuilder.InsertData(
                schema: "matchsimulation",
                table: "status",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 3, "completed" },
                    { 1, "pending" },
                    { 2, "ongoing" },
                    { 4, "canceled" }
                });

            migrationBuilder.InsertData(
                schema: "matchsimulation",
                table: "clubs",
                columns: new[] { "Id", "ActiveMatchId", "ActiveSimulationId", "AttackStat", "ContinentalLeagueId", "ContinentalLeagueName", "DefenceStat", "DomesticLeagueId", "DomesticLeagueName", "FormStat", "HasActiveSimulation", "Name" },
                values: new object[,]
                {
                    { "0201", "", "", 16, 7, "ChampionsLeague", 14, 2, "EnglishPremierLeague", 10, false, "Manchester United" },
                    { "0108", "", "", 8, 10, "NoContinentalLeague", 7, 1, "GreekSuperLeague", 10, false, "OFI" },
                    { "0109", "", "", 8, 10, "NoContinentalLeague", 8, 1, "GreekSuperLeague", 10, false, "Asteras Tripolis" },
                    { "0110", "", "", 7, 10, "NoContinentalLeague", 7, 1, "GreekSuperLeague", 10, false, "Panaitolikos" },
                    { "0111", "", "", 7, 10, "NoContinentalLeague", 7, 1, "GreekSuperLeague", 10, false, "PAS Lamia" },
                    { "0112", "", "", 7, 10, "NoContinentalLeague", 7, 1, "GreekSuperLeague", 10, false, "Atromitos" },
                    { "0113", "", "", 6, 10, "NoContinentalLeague", 8, 1, "GreekSuperLeague", 10, false, "Apollon Smyrnis" },
                    { "0114", "", "", 7, 10, "NoContinentalLeague", 6, 1, "GreekSuperLeague", 10, false, "Ionikos" },
                    { "0204", "", "", 14, 10, "NoContinentalLeague", 13, 2, "EnglishPremierLeague", 10, false, "Arsenal" },
                    { "0208", "", "", 13, 10, "NoContinentalLeague", 12, 2, "EnglishPremierLeague", 10, false, "Wolverhampton" },
                    { "0209", "", "", 12, 10, "NoContinentalLeague", 11, 2, "EnglishPremierLeague", 10, false, "Everton" },
                    { "0210", "", "", 12, 10, "NoContinentalLeague", 11, 2, "EnglishPremierLeague", 10, false, "Crystal Palace" },
                    { "0212", "", "", 11, 10, "NoContinentalLeague", 11, 2, "EnglishPremierLeague", 10, false, "Brighton" },
                    { "0213", "", "", 10, 10, "NoContinentalLeague", 10, 2, "EnglishPremierLeague", 10, false, "Aston Villa" },
                    { "0214", "", "", 10, 10, "NoContinentalLeague", 10, 2, "EnglishPremierLeague", 10, false, "Southampton" },
                    { "0215", "", "", 11, 10, "NoContinentalLeague", 10, 2, "EnglishPremierLeague", 10, false, "Brentford" },
                    { "0107", "", "", 8, 10, "NoContinentalLeague", 8, 1, "GreekSuperLeague", 10, false, "PAS Giannina" },
                    { "0106", "", "", 9, 10, "NoContinentalLeague", 7, 1, "GreekSuperLeague", 10, false, "Volos" },
                    { "0105", "", "", 10, 10, "NoContinentalLeague", 8, 1, "GreekSuperLeague", 10, false, "Aris" },
                    { "0103", "", "", 11, 10, "NoContinentalLeague", 10, 1, "GreekSuperLeague", 10, false, "AEK" },
                    { "0914", "", "", 11, 9, "EuropaConferenceLeague", 10, 6, "NoDomesticLeague", 10, false, "Randers" },
                    { "0915", "", "", 9, 9, "EuropaConferenceLeague", 10, 6, "NoDomesticLeague", 10, false, "Cluj" },
                    { "0916", "", "", 13, 9, "EuropaConferenceLeague", 13, 6, "NoDomesticLeague", 10, false, "Feyenord" },
                    { "0917", "", "", 12, 9, "EuropaConferenceLeague", 10, 6, "NoDomesticLeague", 10, false, "Slavia Praha" },
                    { "0918", "", "", 9, 9, "EuropaConferenceLeague", 11, 6, "NoDomesticLeague", 10, false, "Maccabi Haifa" },
                    { "0919", "", "", 12, 9, "EuropaConferenceLeague", 11, 6, "NoDomesticLeague", 10, false, "Copenhagen" },
                    { "0920", "", "", 10, 9, "EuropaConferenceLeague", 10, 6, "NoDomesticLeague", 10, false, "Slovan Bratislava" },
                    { "0216", "", "", 11, 10, "NoContinentalLeague", 10, 2, "EnglishPremierLeague", 10, false, "Leeds United" },
                    { "0921", "", "", 7, 9, "EuropaConferenceLeague", 7, 6, "NoDomesticLeague", 10, false, "Lincoln Red Imps" },
                    { "0923", "", "", 11, 9, "EuropaConferenceLeague", 10, 6, "NoDomesticLeague", 10, false, "Vitesse" },
                    { "0924", "", "", 8, 9, "EuropaConferenceLeague", 8, 6, "NoDomesticLeague", 10, false, "Mura" },
                    { "0925", "", "", 11, 9, "EuropaConferenceLeague", 10, 6, "NoDomesticLeague", 10, false, "Qarabag" },
                    { "0926", "", "", 12, 9, "EuropaConferenceLeague", 11, 6, "NoDomesticLeague", 10, false, "Basel" },
                    { "0927", "", "", 10, 9, "EuropaConferenceLeague", 9, 6, "NoDomesticLeague", 10, false, "Omonoia" },
                    { "0928", "", "", 9, 9, "EuropaConferenceLeague", 8, 6, "NoDomesticLeague", 10, false, "Kairat Almaty" },
                    { "0102", "", "", 10, 10, "NoContinentalLeague", 8, 1, "GreekSuperLeague", 10, false, "Panathinaikos" },
                    { "0922", "", "", 13, 9, "EuropaConferenceLeague", 12, 6, "NoDomesticLeague", 10, false, "Stade Rennais" },
                    { "0217", "", "", 10, 10, "NoContinentalLeague", 10, 2, "EnglishPremierLeague", 10, false, "Watford" },
                    { "0218", "", "", 10, 10, "NoContinentalLeague", 9, 2, "EnglishPremierLeague", 10, false, "Burnley" },
                    { "0219", "", "", 10, 10, "NoContinentalLeague", 9, 2, "EnglishPremierLeague", 10, false, "Newcastle United" },
                    { "0414", "", "", 12, 10, "NoContinentalLeague", 12, 4, "ItalianSerieA", 10, false, "Udinese" },
                    { "0415", "", "", 10, 10, "NoContinentalLeague", 12, 4, "ItalianSerieA", 10, false, "Venezia" }
                });

            migrationBuilder.InsertData(
                schema: "matchsimulation",
                table: "clubs",
                columns: new[] { "Id", "ActiveMatchId", "ActiveSimulationId", "AttackStat", "ContinentalLeagueId", "ContinentalLeagueName", "DefenceStat", "DomesticLeagueId", "DomesticLeagueName", "FormStat", "HasActiveSimulation", "Name" },
                values: new object[,]
                {
                    { "0416", "", "", 11, 10, "NoContinentalLeague", 9, 4, "ItalianSerieA", 10, false, "Spezia" },
                    { "0417", "", "", 12, 10, "NoContinentalLeague", 10, 4, "ItalianSerieA", 10, false, "Genoa" },
                    { "0418", "", "", 11, 10, "NoContinentalLeague", 9, 4, "ItalianSerieA", 10, false, "Sampdoria" },
                    { "0419", "", "", 8, 10, "NoContinentalLeague", 9, 4, "ItalianSerieA", 10, false, "Salernitana" },
                    { "0420", "", "", 10, 10, "NoContinentalLeague", 9, 4, "ItalianSerieA", 10, false, "Cagliari" },
                    { "0413", "", "", 12, 10, "NoContinentalLeague", 12, 4, "ItalianSerieA", 10, false, "Sassuolo" },
                    { "0506", "", "", 13, 10, "NoContinentalLeague", 13, 5, "GermanBundesliga", 10, false, "Borussia Monchengladbach" },
                    { "0508", "", "", 13, 10, "NoContinentalLeague", 13, 5, "GermanBundesliga", 10, false, "Mainz" },
                    { "0510", "", "", 13, 10, "NoContinentalLeague", 12, 5, "GermanBundesliga", 10, false, "Hoffenheim" },
                    { "0511", "", "", 13, 10, "NoContinentalLeague", 11, 5, "GermanBundesliga", 10, false, "Koln" },
                    { "0512", "", "", 11, 10, "NoContinentalLeague", 12, 5, "GermanBundesliga", 10, false, "Bochum" },
                    { "0513", "", "", 12, 10, "NoContinentalLeague", 9, 5, "GermanBundesliga", 10, false, "Hertha Berlin" },
                    { "0515", "", "", 12, 10, "NoContinentalLeague", 11, 5, "GermanBundesliga", 10, false, "Stuttgart" },
                    { "0516", "", "", 10, 10, "NoContinentalLeague", 11, 5, "GermanBundesliga", 10, false, "Augsburg" },
                    { "0507", "", "", 14, 10, "NoContinentalLeague", 14, 5, "GermanBundesliga", 10, false, "Freiburg" },
                    { "0913", "", "", 10, 9, "EuropaConferenceLeague", 11, 6, "NoDomesticLeague", 10, false, "Jablonec" },
                    { "0412", "", "", 12, 10, "NoContinentalLeague", 14, 4, "ItalianSerieA", 10, false, "Torino" },
                    { "0410", "", "", 14, 10, "NoContinentalLeague", 11, 4, "ItalianSerieA", 10, false, "Verona" },
                    { "0220", "", "", 10, 10, "NoContinentalLeague", 8, 2, "EnglishPremierLeague", 10, false, "Norwich City" },
                    { "0306", "", "", 15, 10, "NoContinentalLeague", 11, 3, "SpanishLaLiga", 10, false, "Valencia" },
                    { "0309", "", "", 14, 10, "NoContinentalLeague", 14, 3, "SpanishLaLiga", 10, false, "Athletic Bilbao" },
                    { "0310", "", "", 13, 10, "NoContinentalLeague", 12, 3, "SpanishLaLiga", 10, false, "Rayo Vallecano" },
                    { "0311", "", "", 12, 10, "NoContinentalLeague", 12, 3, "SpanishLaLiga", 10, false, "Osasuna" },
                    { "0312", "", "", 12, 10, "NoContinentalLeague", 13, 3, "SpanishLaLiga", 10, false, "Espanyol" },
                    { "0313", "", "", 11, 10, "NoContinentalLeague", 10, 3, "SpanishLaLiga", 10, false, "Mallorca" },
                    { "0411", "", "", 13, 10, "NoContinentalLeague", 11, 4, "ItalianSerieA", 10, false, "Empoli" },
                    { "0314", "", "", 9, 10, "NoContinentalLeague", 12, 3, "SpanishLaLiga", 10, false, "Deportivo Alaves" },
                    { "0316", "", "", 11, 10, "NoContinentalLeague", 11, 3, "SpanishLaLiga", 10, false, "Cadiz" },
                    { "0317", "", "", 11, 10, "NoContinentalLeague", 12, 3, "SpanishLaLiga", 10, false, "Granada" },
                    { "0318", "", "", 10, 10, "NoContinentalLeague", 11, 3, "SpanishLaLiga", 10, false, "Elche" },
                    { "0319", "", "", 11, 10, "NoContinentalLeague", 8, 3, "SpanishLaLiga", 10, false, "Levante" },
                    { "0320", "", "", 9, 10, "NoContinentalLeague", 10, 3, "SpanishLaLiga", 10, false, "Getafe" },
                    { "0408", "", "", 12, 10, "NoContinentalLeague", 14, 4, "ItalianSerieA", 10, false, "Fiorentina" },
                    { "0409", "", "", 13, 10, "NoContinentalLeague", 11, 4, "ItalianSerieA", 10, false, "Bologna" },
                    { "0315", "", "", 12, 10, "NoContinentalLeague", 12, 3, "SpanishLaLiga", 10, false, "Celta de Vigo" },
                    { "0912", "", "", 12, 9, "EuropaConferenceLeague", 12, 6, "NoDomesticLeague", 10, false, "AZ Alkmaar" },
                    { "0911", "", "", 8, 9, "EuropaConferenceLeague", 9, 6, "NoDomesticLeague", 10, false, "CSKA Sofia" },
                    { "0910", "", "", 11, 9, "EuropaConferenceLeague", 11, 6, "NoDomesticLeague", 10, false, "Zorya Luhansk" },
                    { "0705", "", "", 14, 7, "ChampionsLeague", 13, 6, "NoDomesticLeague", 10, false, "Sporting Lisbon" },
                    { "0706", "", "", 12, 7, "ChampionsLeague", 10, 6, "NoDomesticLeague", 10, false, "Besiktas" },
                    { "0707", "", "", 8, 7, "ChampionsLeague", 10, 6, "NoDomesticLeague", 10, false, "Sheriff Tiraspol" },
                    { "0708", "", "", 13, 7, "ChampionsLeague", 13, 6, "NoDomesticLeague", 10, false, "Shakhtar Donetsk" }
                });

            migrationBuilder.InsertData(
                schema: "matchsimulation",
                table: "clubs",
                columns: new[] { "Id", "ActiveMatchId", "ActiveSimulationId", "AttackStat", "ContinentalLeagueId", "ContinentalLeagueName", "DefenceStat", "DomesticLeagueId", "DomesticLeagueName", "FormStat", "HasActiveSimulation", "Name" },
                values: new object[,]
                {
                    { "0709", "", "", 14, 7, "ChampionsLeague", 13, 6, "NoDomesticLeague", 10, false, "Benfica" },
                    { "0710", "", "", 12, 7, "ChampionsLeague", 11, 6, "NoDomesticLeague", 10, false, "Dynamo Kyiv" },
                    { "0711", "", "", 11, 7, "ChampionsLeague", 11, 6, "NoDomesticLeague", 10, false, "Young Boys" },
                    { "0704", "", "", 15, 7, "ChampionsLeague", 14, 6, "NoDomesticLeague", 10, false, "Ajax" },
                    { "0712", "", "", 13, 7, "ChampionsLeague", 13, 6, "NoDomesticLeague", 10, false, "Salzburg" },
                    { "0714", "", "", 13, 7, "ChampionsLeague", 13, 6, "NoDomesticLeague", 10, false, "Zenit Saint Petersburg" },
                    { "0715", "", "", 10, 7, "ChampionsLeague", 10, 6, "NoDomesticLeague", 10, false, "Malmo" },
                    { "0101", "", "", 12, 8, "EuropaLeague", 11, 1, "GreekSuperLeague", 10, false, "Olympiakos" },
                    { "0207", "", "", 14, 8, "EuropaLeague", 13, 2, "EnglishPremierLeague", 10, false, "West Ham" },
                    { "0211", "", "", 13, 8, "EuropaLeague", 11, 2, "EnglishPremierLeague", 10, false, "Leicester City" },
                    { "0307", "", "", 14, 8, "EuropaLeague", 12, 3, "SpanishLaLiga", 10, false, "Real Betis" },
                    { "0308", "", "", 14, 8, "EuropaLeague", 14, 3, "SpanishLaLiga", 10, false, "Real Sociedad" },
                    { "0713", "", "", 13, 7, "ChampionsLeague", 14, 6, "NoDomesticLeague", 10, false, "Lille" },
                    { "0405", "", "", 14, 8, "EuropaLeague", 16, 4, "ItalianSerieA", 10, false, "Napoli" },
                    { "0703", "", "", 15, 7, "ChampionsLeague", 13, 6, "NoDomesticLeague", 10, false, "Porto" },
                    { "0701", "", "", 17, 7, "ChampionsLeague", 14, 6, "NoDomesticLeague", 10, false, "Paris Saint Germain" },
                    { "0202", "", "", 17, 7, "ChampionsLeague", 16, 2, "EnglishPremierLeague", 10, false, "Chelsea" },
                    { "0203", "", "", 17, 7, "ChampionsLeague", 15, 2, "EnglishPremierLeague", 10, false, "Liverpool" },
                    { "0205", "", "", 17, 7, "ChampionsLeague", 15, 2, "EnglishPremierLeague", 10, false, "Manchester City" },
                    { "0301", "", "", 17, 7, "ChampionsLeague", 14, 3, "SpanishLaLiga", 10, false, "Real Madrid" },
                    { "0302", "", "", 16, 7, "ChampionsLeague", 13, 3, "SpanishLaLiga", 10, false, "Barcelona" },
                    { "0303", "", "", 16, 7, "ChampionsLeague", 14, 3, "SpanishLaLiga", 10, false, "Atletico Madrid" },
                    { "0304", "", "", 15, 7, "ChampionsLeague", 15, 3, "SpanishLaLiga", 10, false, "Sevilla" },
                    { "0702", "", "", 13, 7, "ChampionsLeague", 12, 6, "NoDomesticLeague", 10, false, "Club Brugge" },
                    { "0305", "", "", 13, 7, "ChampionsLeague", 14, 3, "SpanishLaLiga", 10, false, "Villareal" },
                    { "0402", "", "", 17, 7, "ChampionsLeague", 14, 4, "ItalianSerieA", 10, false, "Internazionale" },
                    { "0403", "", "", 15, 7, "ChampionsLeague", 14, 4, "ItalianSerieA", 10, false, "Juventus" },
                    { "0407", "", "", 14, 7, "ChampionsLeague", 14, 4, "ItalianSerieA", 10, false, "Atalanta" },
                    { "0501", "", "", 18, 7, "ChampionsLeague", 16, 5, "GermanBundesliga", 10, false, "Bayern Munich" },
                    { "0502", "", "", 17, 7, "ChampionsLeague", 13, 5, "GermanBundesliga", 10, false, "Borussia Dortmund" },
                    { "0504", "", "", 15, 7, "ChampionsLeague", 15, 5, "GermanBundesliga", 10, false, "Leipzig" },
                    { "0505", "", "", 13, 7, "ChampionsLeague", 14, 5, "GermanBundesliga", 10, false, "Wolfsburg" },
                    { "0401", "", "", 16, 7, "ChampionsLeague", 14, 4, "ItalianSerieA", 10, false, "Milan" },
                    { "0517", "", "", 8, 10, "NoContinentalLeague", 11, 5, "GermanBundesliga", 10, false, "Arminia Bielefeld" },
                    { "0406", "", "", 15, 8, "EuropaLeague", 12, 4, "ItalianSerieA", 10, false, "Lazio" },
                    { "0514", "", "", 13, 8, "EuropaLeague", 12, 5, "GermanBundesliga", 10, false, "Eintracht Frankfurt" },
                    { "0821", "", "", 12, 8, "EuropaLeague", 11, 6, "NoDomesticLeague", 10, false, "Dinamo Zagreb" },
                    { "0822", "", "", 10, 8, "EuropaLeague", 9, 6, "NoDomesticLeague", 10, false, "Genk" },
                    { "0823", "", "", 10, 8, "EuropaLeague", 9, 6, "NoDomesticLeague", 10, false, "Rapid Wien" },
                    { "0104", "", "", 11, 9, "EuropaConferenceLeague", 11, 1, "GreekSuperLeague", 10, false, "PAOK" },
                    { "0206", "", "", 14, 9, "EuropaConferenceLeague", 13, 2, "EnglishPremierLeague", 10, false, "Tottenham" },
                    { "0404", "", "", 14, 9, "EuropaConferenceLeague", 13, 4, "ItalianSerieA", 10, false, "Roma" }
                });

            migrationBuilder.InsertData(
                schema: "matchsimulation",
                table: "clubs",
                columns: new[] { "Id", "ActiveMatchId", "ActiveSimulationId", "AttackStat", "ContinentalLeagueId", "ContinentalLeagueName", "DefenceStat", "DomesticLeagueId", "DomesticLeagueName", "FormStat", "HasActiveSimulation", "Name" },
                values: new object[,]
                {
                    { "0509", "", "", 13, 9, "EuropaConferenceLeague", 12, 5, "GermanBundesliga", 10, false, "Union Berlin" },
                    { "0820", "", "", 11, 8, "EuropaLeague", 9, 6, "NoDomesticLeague", 10, false, "Ferencvarosi" },
                    { "0901", "", "", 13, 9, "EuropaConferenceLeague", 11, 6, "NoDomesticLeague", 10, false, "Maccabi Tel Aviv" },
                    { "0903", "", "", 10, 9, "EuropaConferenceLeague", 8, 6, "NoDomesticLeague", 10, false, "Helsinki" },
                    { "0904", "", "", 8, 9, "EuropaConferenceLeague", 7, 6, "NoDomesticLeague", 10, false, "Alashkert" },
                    { "0905", "", "", 11, 9, "EuropaConferenceLeague", 12, 6, "NoDomesticLeague", 10, false, "Gent" },
                    { "0906", "", "", 11, 9, "EuropaConferenceLeague", 12, 6, "NoDomesticLeague", 10, false, "Partizan" },
                    { "0907", "", "", 9, 9, "EuropaConferenceLeague", 9, 6, "NoDomesticLeague", 10, false, "Flora" },
                    { "0908", "", "", 10, 9, "EuropaConferenceLeague", 9, 6, "NoDomesticLeague", 10, false, "Anorthosis" },
                    { "0909", "", "", 13, 9, "EuropaConferenceLeague", 11, 6, "NoDomesticLeague", 10, false, "Bodo/Glimt" },
                    { "0902", "", "", 11, 9, "EuropaConferenceLeague", 11, 6, "NoDomesticLeague", 10, false, "LASK Linz" },
                    { "0503", "", "", 14, 8, "EuropaLeague", 13, 5, "GermanBundesliga", 10, false, "Bayer Leverkusen" },
                    { "0819", "", "", 13, 8, "EuropaLeague", 11, 6, "NoDomesticLeague", 10, false, "Celtic" },
                    { "0817", "", "", 11, 8, "EuropaLeague", 11, 6, "NoDomesticLeague", 10, false, "Midtjylland" },
                    { "0801", "", "", 14, 8, "EuropaLeague", 13, 6, "NoDomesticLeague", 10, false, "Lyon" },
                    { "0802", "", "", 11, 8, "EuropaLeague", 11, 6, "NoDomesticLeague", 10, false, "Sparta Praha" },
                    { "0803", "", "", 13, 8, "EuropaLeague", 12, 6, "NoDomesticLeague", 10, false, "Rangers" },
                    { "0804", "", "", 10, 8, "EuropaLeague", 9, 6, "NoDomesticLeague", 10, false, "Brondby" },
                    { "0805", "", "", 14, 8, "EuropaLeague", 13, 6, "NoDomesticLeague", 10, false, "Monaco" },
                    { "0806", "", "", 14, 8, "EuropaLeague", 12, 6, "NoDomesticLeague", 10, false, "PSV Eindhoven" },
                    { "0807", "", "", 10, 8, "EuropaLeague", 9, 6, "NoDomesticLeague", 10, false, "Sturm Graz" },
                    { "0818", "", "", 10, 8, "EuropaLeague", 10, 6, "NoDomesticLeague", 10, false, "Ludogorets" },
                    { "0808", "", "", 11, 8, "EuropaLeague", 11, 6, "NoDomesticLeague", 10, false, "Legia Warsaw" },
                    { "0810", "", "", 12, 8, "EuropaLeague", 10, 6, "NoDomesticLeague", 10, false, "Fenerbahce" },
                    { "0811", "", "", 11, 8, "EuropaLeague", 9, 6, "NoDomesticLeague", 10, false, "Antwerp" },
                    { "0812", "", "", 12, 8, "EuropaLeague", 12, 6, "NoDomesticLeague", 10, false, "Galatasaray" },
                    { "0813", "", "", 13, 8, "EuropaLeague", 12, 6, "NoDomesticLeague", 10, false, "Marseille" },
                    { "0814", "", "", 11, 8, "EuropaLeague", 12, 6, "NoDomesticLeague", 10, false, "Locomotiv Moscow" },
                    { "0815", "", "", 14, 8, "EuropaLeague", 12, 6, "NoDomesticLeague", 10, false, "Braga" },
                    { "0816", "", "", 11, 8, "EuropaLeague", 12, 6, "NoDomesticLeague", 10, false, "Red Star Belgrade" },
                    { "0809", "", "", 12, 8, "EuropaLeague", 10, 6, "NoDomesticLeague", 10, false, "Spartak Moscow" },
                    { "0518", "", "", 9, 10, "NoContinentalLeague", 7, 5, "GermanBundesliga", 10, false, "Greuther Furth" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_clubs_ContinentalLeagueId",
                schema: "matchsimulation",
                table: "clubs",
                column: "ContinentalLeagueId");

            migrationBuilder.CreateIndex(
                name: "IX_clubs_DomesticLeagueId",
                schema: "matchsimulation",
                table: "clubs",
                column: "DomesticLeagueId");

            migrationBuilder.CreateIndex(
                name: "IX_matches_LeagueId",
                schema: "matchsimulation",
                table: "matches",
                column: "LeagueId");

            migrationBuilder.CreateIndex(
                name: "IX_possiblePicks_MatchResultId",
                schema: "matchsimulation",
                table: "possiblePicks",
                column: "MatchResultId");

            migrationBuilder.CreateIndex(
                name: "IX_possiblePicks_RequirementTypeId",
                schema: "matchsimulation",
                table: "possiblePicks",
                column: "RequirementTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_simulations_StatusId",
                schema: "matchsimulation",
                table: "simulations",
                column: "StatusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "clubs",
                schema: "matchsimulation");

            migrationBuilder.DropTable(
                name: "possiblePicks",
                schema: "matchsimulation");

            migrationBuilder.DropTable(
                name: "simulations",
                schema: "matchsimulation");

            migrationBuilder.DropTable(
                name: "matches",
                schema: "matchsimulation");

            migrationBuilder.DropTable(
                name: "matchResult",
                schema: "matchsimulation");

            migrationBuilder.DropTable(
                name: "requirementType",
                schema: "matchsimulation");

            migrationBuilder.DropTable(
                name: "status",
                schema: "matchsimulation");

            migrationBuilder.DropTable(
                name: "league",
                schema: "matchsimulation");
        }
    }
}
