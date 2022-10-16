using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class WorkfollowATMUnknownTransactionsModify2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ApplicationUserItems",
                keyColumn: "Id",
                keyValue: 1,
                column: "RegDateTime",
                value: new DateTime(2020, 8, 9, 13, 40, 22, 442, DateTimeKind.Local).AddTicks(8298));

            migrationBuilder.UpdateData(
                table: "ApplicationUserItems",
                keyColumn: "Id",
                keyValue: 2,
                column: "RegDateTime",
                value: new DateTime(2020, 8, 9, 13, 40, 22, 446, DateTimeKind.Local).AddTicks(3514));

            migrationBuilder.CreateIndex(
                name: "IX_ATMUnknownTransactions_Workfollow_UserId",
                table: "ATMUnknownTransactions_Workfollow",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ATMUnknownTransactions_Workfollow_ApplicationUserItems_UserId",
                table: "ATMUnknownTransactions_Workfollow",
                column: "UserId",
                principalTable: "ApplicationUserItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ATMUnknownTransactions_Workfollow_ApplicationUserItems_UserId",
                table: "ATMUnknownTransactions_Workfollow");

            migrationBuilder.DropIndex(
                name: "IX_ATMUnknownTransactions_Workfollow_UserId",
                table: "ATMUnknownTransactions_Workfollow");

            migrationBuilder.UpdateData(
                table: "ApplicationUserItems",
                keyColumn: "Id",
                keyValue: 1,
                column: "RegDateTime",
                value: new DateTime(2020, 8, 9, 13, 2, 44, 332, DateTimeKind.Local).AddTicks(6281));

            migrationBuilder.UpdateData(
                table: "ApplicationUserItems",
                keyColumn: "Id",
                keyValue: 2,
                column: "RegDateTime",
                value: new DateTime(2020, 8, 9, 13, 2, 44, 336, DateTimeKind.Local).AddTicks(4430));
        }
    }
}
