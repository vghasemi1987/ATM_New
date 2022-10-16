using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class FileDetailModify : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Workfollow_Transaction_FileDetails_FileDetailId",
                table: "Transaction_Workfollow");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Workfollow_Transaction_FileDetails_FileDetailId",
                table: "Transaction_Workfollow",
                column: "FileDetailId",
                principalTable: "Transaction_FileDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Workfollow_Transaction_FileDetails_FileDetailId",
                table: "Transaction_Workfollow");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Workfollow_Transaction_FileDetails_FileDetailId",
                table: "Transaction_Workfollow",
                column: "FileDetailId",
                principalTable: "Transaction_FileDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
