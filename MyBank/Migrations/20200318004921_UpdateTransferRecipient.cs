using Microsoft.EntityFrameworkCore.Migrations;

namespace MyBank.Migrations
{
    public partial class UpdateTransferRecipient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transfers_Accounts_RecepientId",
                table: "Transfers");

            migrationBuilder.DropIndex(
                name: "IX_Transfers_RecepientId",
                table: "Transfers");

            migrationBuilder.DropColumn(
                name: "RecepientId",
                table: "Transfers");

            migrationBuilder.AddColumn<int>(
                name: "RecipientId",
                table: "Transfers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Transfers_RecipientId",
                table: "Transfers",
                column: "RecipientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transfers_Accounts_RecipientId",
                table: "Transfers",
                column: "RecipientId",
                principalTable: "Accounts",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transfers_Accounts_RecipientId",
                table: "Transfers");

            migrationBuilder.DropIndex(
                name: "IX_Transfers_RecipientId",
                table: "Transfers");

            migrationBuilder.DropColumn(
                name: "RecipientId",
                table: "Transfers");

            migrationBuilder.AddColumn<int>(
                name: "RecepientId",
                table: "Transfers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Transfers_RecepientId",
                table: "Transfers",
                column: "RecepientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transfers_Accounts_RecepientId",
                table: "Transfers",
                column: "RecepientId",
                principalTable: "Accounts",
                principalColumn: "Id");
        }
    }
}
