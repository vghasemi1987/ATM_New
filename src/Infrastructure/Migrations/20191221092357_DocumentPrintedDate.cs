using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class DocumentPrintedDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DocumentPrintedDate",
                table: "Transaction_FileDetails",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ShetabiPrintedDate",
                table: "Transaction_FileDetails",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "ApplicationUserItems",
                keyColumn: "Id",
                keyValue: 1,
                column: "RegDateTime",
                value: new DateTime(2019, 12, 21, 12, 53, 55, 385, DateTimeKind.Local).AddTicks(398));

            migrationBuilder.UpdateData(
                table: "ApplicationUserItems",
                keyColumn: "Id",
                keyValue: 2,
                column: "RegDateTime",
                value: new DateTime(2019, 12, 21, 12, 53, 55, 389, DateTimeKind.Local).AddTicks(4001));

            migrationBuilder.UpdateData(
                table: "ApplicationUser_Roles",
                keyColumn: "Id",
                keyValue: 4,
                column: "PanelMenu",
                value: "[{\"Text\":\"خانه\",\"Link\":\"/home\",\"Icon\":\"flaticon-line-graph\",\"Items\":null},{\"Text\":\"لیست تراکنش ها\",\"Link\":\"/transactiondetails\",\"Icon\":\"fas fa-sitemap\",\"Items\":null},{\"Text\":\"بررسی تراکنش های بلاتکلیف\",\"Link\":\"/ATMUnknown\",\"Icon\":\"flaticon-signs-2\",\"Items\":null},{\"Text\":\"گزارشات\",\"Link\":\"/reports\",\"Icon\":\"flaticon-diagram\",\"Items\":null}]");

            migrationBuilder.UpdateData(
                table: "ApplicationUser_Roles",
                keyColumn: "Id",
                keyValue: 5,
                column: "PanelMenu",
                value: "[{\"Text\":\"خانه\",\"Link\":\"/home\",\"Icon\":\"flaticon-line-graph\",\"Items\":null},{\"Text\":\"فایل تراکنش ها\",\"Link\":\"/transactions\",\"Icon\":\"flaticon-signs-2\",\"Items\":null},{\"Text\":\"بررسی تراکنش های بلاتکلیف\",\"Link\":\"/ATMUnknown\",\"Icon\":\"flaticon-signs-2\",\"Items\":null},{\"Text\":\"گزارشات\",\"Link\":\"/reports\",\"Icon\":\"flaticon-diagram\",\"Items\":null}]");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DocumentPrintedDate",
                table: "Transaction_FileDetails");

            migrationBuilder.DropColumn(
                name: "ShetabiPrintedDate",
                table: "Transaction_FileDetails");

            migrationBuilder.UpdateData(
                table: "ApplicationUserItems",
                keyColumn: "Id",
                keyValue: 1,
                column: "RegDateTime",
                value: new DateTime(2019, 9, 3, 17, 0, 1, 100, DateTimeKind.Local).AddTicks(538));

            migrationBuilder.UpdateData(
                table: "ApplicationUserItems",
                keyColumn: "Id",
                keyValue: 2,
                column: "RegDateTime",
                value: new DateTime(2019, 9, 3, 17, 0, 1, 102, DateTimeKind.Local).AddTicks(3441));

            migrationBuilder.UpdateData(
                table: "ApplicationUser_Roles",
                keyColumn: "Id",
                keyValue: 4,
                column: "PanelMenu",
                value: "[{\"Text\":\"خانه\",\"Link\":\"/home\",\"Icon\":\"flaticon-line-graph\",\"Items\":null},{\"Text\":\"لیست تراکنش ها\",\"Link\":\"/transactiondetails\",\"Icon\":\"fas fa-sitemap\",\"Items\":null},{\"Text\":\"گزارشات\",\"Link\":\"/reports\",\"Icon\":\"flaticon-diagram\",\"Items\":null}]");

            migrationBuilder.UpdateData(
                table: "ApplicationUser_Roles",
                keyColumn: "Id",
                keyValue: 5,
                column: "PanelMenu",
                value: "[{\"Text\":\"خانه\",\"Link\":\"/home\",\"Icon\":\"flaticon-line-graph\",\"Items\":null},{\"Text\":\"فایل تراکنش ها\",\"Link\":\"/transactions\",\"Icon\":\"flaticon-signs-2\",\"Items\":null},{\"Text\":\"گزارشات\",\"Link\":\"/reports\",\"Icon\":\"flaticon-diagram\",\"Items\":null}]");
        }
    }
}
