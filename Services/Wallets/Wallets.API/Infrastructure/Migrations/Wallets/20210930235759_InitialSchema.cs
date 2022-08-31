using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BettingApp.Services.Wallets.API.Infrastructure.Migrations.Wallets
{
    public partial class InitialSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "transactionType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IdentifierName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transactionType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "wallets",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GamblerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PreviousBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LastTimeUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastTransactionId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalTransactions = table.Column<int>(type: "int", nullable: false),
                    TotalWageredAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalWinningsAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalTopUpAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalWithdrawAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wallets", x => x.Id);
                    table.UniqueConstraint("AK_wallets_GamblerId", x => x.GamblerId);
                });

            migrationBuilder.CreateTable(
                name: "transactions",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    WalletId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DateTimeCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WalletBalanceBefore = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WalletBalanceAfter = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransactionTypeId = table.Column<int>(type: "int", nullable: false),
                    TransactionTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdentifierId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdentifierName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_transactions_transactionType_TransactionTypeId",
                        column: x => x.TransactionTypeId,
                        principalTable: "transactionType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_transactions_wallets_WalletId",
                        column: x => x.WalletId,
                        principalTable: "wallets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "transactionType",
                columns: new[] { "Id", "IdentifierName", "Name" },
                values: new object[,]
                {
                    { 1, "requestId", "topup" },
                    { 2, "requestId", "withdraw" },
                    { 3, "betId", "betpayment" },
                    { 4, "betId", "betwinnings" },
                    { 5, "betId", "betrefund" },
                    { 6, "gamblerId", "welcomebonus" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_transactions_TransactionTypeId",
                table: "transactions",
                column: "TransactionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_transactions_WalletId",
                table: "transactions",
                column: "WalletId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "transactions");

            migrationBuilder.DropTable(
                name: "transactionType");

            migrationBuilder.DropTable(
                name: "wallets");
        }
    }
}
