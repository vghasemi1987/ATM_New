using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class FileDetails_Date_1400 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ATMUnknownTransactions_Workfollow_Transaction_Status_StatusWorkfollowId",
                table: "ATMUnknownTransactions_Workfollow");

            migrationBuilder.DropIndex(
                name: "IX_ATMUnknownTransactions_Workfollow_StatusWorkfollowId",
                table: "ATMUnknownTransactions_Workfollow");

            migrationBuilder.AlterColumn<string>(
                name: "Date",
                table: "Transaction_FileDetails",
                type: "varchar(10)",
                unicode: false,
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(6)",
                oldUnicode: false,
                oldMaxLength: 6,
                oldNullable: true);

            migrationBuilder.AddColumn<short>(
                name: "StatusWorkfollowId1",
                table: "ATMUnknownTransactions_Workfollow",
                type: "smallint",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "ApplicationUserItems",
                keyColumn: "Id",
                keyValue: 1,
                column: "RegDateTime",
                value: new DateTime(2021, 2, 22, 12, 55, 37, 564, DateTimeKind.Local).AddTicks(521));

            migrationBuilder.UpdateData(
                table: "ApplicationUserItems",
                keyColumn: "Id",
                keyValue: 2,
                column: "RegDateTime",
                value: new DateTime(2021, 2, 22, 12, 55, 37, 572, DateTimeKind.Local).AddTicks(2884));

            migrationBuilder.CreateIndex(
                name: "IX_ATMUnknownTransactions_Workfollow_StatusWorkfollowId1",
                table: "ATMUnknownTransactions_Workfollow",
                column: "StatusWorkfollowId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ATMUnknownTransactions_Workfollow_Transaction_Status_StatusWorkfollowId1",
                table: "ATMUnknownTransactions_Workfollow",
                column: "StatusWorkfollowId1",
                principalTable: "Transaction_Status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ATMUnknownTransactions_Workfollow_Transaction_Status_StatusWorkfollowId1",
                table: "ATMUnknownTransactions_Workfollow");

            migrationBuilder.DropIndex(
                name: "IX_ATMUnknownTransactions_Workfollow_StatusWorkfollowId1",
                table: "ATMUnknownTransactions_Workfollow");

            migrationBuilder.DropColumn(
                name: "StatusWorkfollowId1",
                table: "ATMUnknownTransactions_Workfollow");

            migrationBuilder.AlterColumn<string>(
                name: "Date",
                table: "Transaction_FileDetails",
                type: "varchar(6)",
                unicode: false,
                maxLength: 6,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldUnicode: false,
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "ApplicationUserItems",
                keyColumn: "Id",
                keyValue: 1,
                column: "RegDateTime",
                value: new DateTime(2021, 1, 10, 10, 5, 32, 375, DateTimeKind.Local).AddTicks(9645));

            migrationBuilder.UpdateData(
                table: "ApplicationUserItems",
                keyColumn: "Id",
                keyValue: 2,
                column: "RegDateTime",
                value: new DateTime(2021, 1, 10, 10, 5, 32, 383, DateTimeKind.Local).AddTicks(2820));

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
    }
}
