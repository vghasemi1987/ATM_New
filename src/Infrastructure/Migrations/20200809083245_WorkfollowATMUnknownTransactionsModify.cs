using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class WorkfollowATMUnknownTransactionsModify : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ATMUnknownTransactions_Workfollow_Transaction_Status_StatusId",
                table: "ATMUnknownTransactions_Workfollow");

            migrationBuilder.DropIndex(
                name: "IX_ATMUnknownTransactions_Workfollow_StatusId",
                table: "ATMUnknownTransactions_Workfollow");

            migrationBuilder.RenameColumn(
                name: "StatusId",
                table: "ATMUnknownTransactions_Workfollow",
                newName: "StatusID");

            migrationBuilder.RenameColumn(
                name: "UpdateDateTime",
                table: "ATMUnknownTransactions_Workfollow",
                newName: "CreateDateTime");

            migrationBuilder.AlterColumn<int>(
                name: "StatusID",
                table: "ATMUnknownTransactions_Workfollow",
                nullable: true,
                oldClrType: typeof(short),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "ATMUnknownTransactions_Workfollow",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<short>(
                name: "StatusWorkfollowId",
                table: "ATMUnknownTransactions_Workfollow",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserMessage",
                table: "ATMUnknownTransactions_Workfollow",
                maxLength: 200,
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_ATMUnknownTransactions_Workfollow_StatusWorkfollowId",
                table: "ATMUnknownTransactions_Workfollow",
                column: "StatusWorkfollowId");

            migrationBuilder.AddForeignKey(
                name: "FK_ATMUnknownTransactions_Workfollow_Transaction_Status_StatusWorkfollowId",
                table: "ATMUnknownTransactions_Workfollow",
                column: "StatusWorkfollowId",
                principalTable: "Transaction_Status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ATMUnknownTransactions_Workfollow_Transaction_Status_StatusWorkfollowId",
                table: "ATMUnknownTransactions_Workfollow");

            migrationBuilder.DropIndex(
                name: "IX_ATMUnknownTransactions_Workfollow_StatusWorkfollowId",
                table: "ATMUnknownTransactions_Workfollow");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "ATMUnknownTransactions_Workfollow");

            migrationBuilder.DropColumn(
                name: "StatusWorkfollowId",
                table: "ATMUnknownTransactions_Workfollow");

            migrationBuilder.DropColumn(
                name: "UserMessage",
                table: "ATMUnknownTransactions_Workfollow");

            migrationBuilder.RenameColumn(
                name: "StatusID",
                table: "ATMUnknownTransactions_Workfollow",
                newName: "StatusId");

            migrationBuilder.RenameColumn(
                name: "CreateDateTime",
                table: "ATMUnknownTransactions_Workfollow",
                newName: "UpdateDateTime");

            migrationBuilder.AlterColumn<short>(
                name: "StatusId",
                table: "ATMUnknownTransactions_Workfollow",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "ApplicationUserItems",
                keyColumn: "Id",
                keyValue: 1,
                column: "RegDateTime",
                value: new DateTime(2020, 7, 14, 13, 7, 17, 647, DateTimeKind.Local).AddTicks(5861));

            migrationBuilder.UpdateData(
                table: "ApplicationUserItems",
                keyColumn: "Id",
                keyValue: 2,
                column: "RegDateTime",
                value: new DateTime(2020, 7, 14, 13, 7, 17, 655, DateTimeKind.Local).AddTicks(8891));

            migrationBuilder.CreateIndex(
                name: "IX_ATMUnknownTransactions_Workfollow_StatusId",
                table: "ATMUnknownTransactions_Workfollow",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_ATMUnknownTransactions_Workfollow_Transaction_Status_StatusId",
                table: "ATMUnknownTransactions_Workfollow",
                column: "StatusId",
                principalTable: "Transaction_Status",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
