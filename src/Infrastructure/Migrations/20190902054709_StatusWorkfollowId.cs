using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class StatusWorkfollowId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<short>(
                name: "StatusWorkfollowId",
                table: "ATMUnknownTransactions",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "ApplicationUserItems",
                keyColumn: "Id",
                keyValue: 1,
                column: "RegDateTime",
                value: new DateTime(2019, 9, 2, 9, 17, 6, 719, DateTimeKind.Local).AddTicks(7639));

            migrationBuilder.UpdateData(
                table: "ApplicationUserItems",
                keyColumn: "Id",
                keyValue: 2,
                column: "RegDateTime",
                value: new DateTime(2019, 9, 2, 9, 17, 6, 723, DateTimeKind.Local).AddTicks(4944));

            migrationBuilder.CreateIndex(
                name: "IX_ATMUnknownTransactions_StatusWorkfollowId",
                table: "ATMUnknownTransactions",
                column: "StatusWorkfollowId");

            migrationBuilder.AddForeignKey(
                name: "FK_ATMUnknownTransactions_Transaction_Status_StatusWorkfollowId",
                table: "ATMUnknownTransactions",
                column: "StatusWorkfollowId",
                principalTable: "Transaction_Status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ATMUnknownTransactions_Transaction_Status_StatusWorkfollowId",
                table: "ATMUnknownTransactions");

            migrationBuilder.DropIndex(
                name: "IX_ATMUnknownTransactions_StatusWorkfollowId",
                table: "ATMUnknownTransactions");

            migrationBuilder.DropColumn(
                name: "StatusWorkfollowId",
                table: "ATMUnknownTransactions");

            migrationBuilder.UpdateData(
                table: "ApplicationUserItems",
                keyColumn: "Id",
                keyValue: 1,
                column: "RegDateTime",
                value: new DateTime(2019, 9, 1, 16, 8, 27, 999, DateTimeKind.Local).AddTicks(5850));

            migrationBuilder.UpdateData(
                table: "ApplicationUserItems",
                keyColumn: "Id",
                keyValue: 2,
                column: "RegDateTime",
                value: new DateTime(2019, 9, 1, 16, 8, 28, 2, DateTimeKind.Local).AddTicks(4398));
        }
    }
}
