using Microsoft.EntityFrameworkCore.Migrations;

namespace BettingApp.Services.Wallets.API.Infrastructure.Migrations.Wallets
{
    public partial class ChangeTransactionTypeNameFormat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "transactionType",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "TopUp");

            migrationBuilder.UpdateData(
                table: "transactionType",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Withdraw");

            migrationBuilder.UpdateData(
                table: "transactionType",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "BetPayment");

            migrationBuilder.UpdateData(
                table: "transactionType",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "BetWinnings");

            migrationBuilder.UpdateData(
                table: "transactionType",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "BetRefund");

            migrationBuilder.UpdateData(
                table: "transactionType",
                keyColumn: "Id",
                keyValue: 6,
                column: "Name",
                value: "WelcomeBonus");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "transactionType",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "topup");

            migrationBuilder.UpdateData(
                table: "transactionType",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "withdraw");

            migrationBuilder.UpdateData(
                table: "transactionType",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "betpayment");

            migrationBuilder.UpdateData(
                table: "transactionType",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "betwinnings");

            migrationBuilder.UpdateData(
                table: "transactionType",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "betrefund");

            migrationBuilder.UpdateData(
                table: "transactionType",
                keyColumn: "Id",
                keyValue: 6,
                column: "Name",
                value: "welcomebonus");
        }
    }
}
