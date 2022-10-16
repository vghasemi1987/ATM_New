using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class MenuModify : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ApplicationUserItems",
                keyColumn: "Id",
                keyValue: 1,
                column: "RegDateTime",
                value: new DateTime(2020, 7, 14, 10, 10, 57, 308, DateTimeKind.Local).AddTicks(3586));

            migrationBuilder.UpdateData(
                table: "ApplicationUserItems",
                keyColumn: "Id",
                keyValue: 2,
                column: "RegDateTime",
                value: new DateTime(2020, 7, 14, 10, 10, 57, 311, DateTimeKind.Local).AddTicks(9750));

            migrationBuilder.UpdateData(
                table: "ApplicationUser_Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "PanelMenu",
                value: "[{\"Text\":\"خانه\",\"Link\":\"/home\",\"Icon\":\"flaticon-line-graph\",\"Items\":null},{\"Text\":\"مدیریت کاربران\",\"Link\":\"/usermanagement\",\"Icon\":\"flaticon-piggy-bank\",\"Items\":null},{\"Text\":\"تراکنش ها فزونی\",\"Link\":\"\",\"Icon\":\"fas fa-sitemap\",\"Items\":[{\"Text\":\"فایل تراکنش ها\",\"Link\":\"/transactions/all\",\"Icon\":\"fas fa-sitemap\",\"Items\":null},{\"Text\":\"فرایند تراکنش ها\",\"Link\":\"/transactiondetails\",\"Icon\":\"fas fa-sitemap\",\"Items\":null},{\"Text\":\"لیست تراکنش های شعبه\",\"Link\":\"/transactiondetails/BranchDetail\",\"Icon\":\"flaticon-signs-2\",\"Items\":null}]},{\"Text\":\"سند برداشت تجمعی\",\"Link\":\"/transactiondetails/branchtransaction\",\"Icon\":\"fas fa-sitemap\",\"Items\":null},{\"Text\":\"دریافت فایل شتابی\",\"Link\":\"/transactiondetails/shetabi\",\"Icon\":\"fas fa-sitemap\",\"Items\":null},{\"Text\":\"لیست شعبه ها\",\"Link\":\"/branches\",\"Icon\":\"fas fa-sitemap\",\"Items\":null},{\"Text\":\"گزارشات\",\"Link\":\"/reports\",\"Icon\":\"flaticon-diagram\",\"Items\":null}]");

            migrationBuilder.UpdateData(
                table: "ApplicationUser_Roles",
                keyColumn: "Id",
                keyValue: 4,
                column: "PanelMenu",
                value: "[{\"Text\":\"خانه\",\"Link\":\"/home\",\"Icon\":\"flaticon-line-graph\",\"Items\":null},{\"Text\":\"لیست تراکنش ها\",\"Link\":\"/transactiondetails\",\"Icon\":\"fas fa-sitemap\",\"Items\":null},{\"Text\":\"لیست تراکنش های شعبه\",\"Link\":\"/transactiondetails/BranchDetail\",\"Icon\":\"fas fa-sitemap\",\"Items\":null},{\"Text\":\"بررسی تراکنش های بلاتکلیف\",\"Link\":\"/ATMUnknown\",\"Icon\":\"flaticon-signs-2\",\"Items\":null},{\"Text\":\"تسهیلات همراه\",\"Link\":\"/HamrahLoanHeaders\",\"Icon\":\"flaticon-signs-2\",\"Items\":null},{\"Text\":\"گزارش تراکنش های بلاتکلیف\",\"Link\":\"/ATMUnknown/IndexReport\",\"Icon\":\"flaticon-signs-2\",\"Items\":null},{\"Text\":\"گزارش تسهیلات همراه\",\"Link\":\"/HamrahLoanHeaders/IndexReport\",\"Icon\":\"flaticon-signs-2\",\"Items\":null},{\"Text\":\"گزارشات\",\"Link\":\"/reports\",\"Icon\":\"flaticon-diagram\",\"Items\":null}]");

            migrationBuilder.UpdateData(
                table: "ApplicationUser_Roles",
                keyColumn: "Id",
                keyValue: 5,
                column: "PanelMenu",
                value: "[{\"Text\":\"خانه\",\"Link\":\"/home\",\"Icon\":\"flaticon-line-graph\",\"Items\":null},{\"Text\":\"فایل تراکنش ها\",\"Link\":\"/transactions\",\"Icon\":\"flaticon-signs-2\",\"Items\":null},{\"Text\":\"لیست تراکنش های شعبه\",\"Link\":\"/transactiondetails/BranchDetail\",\"Icon\":\"fas fa-sitemap\",\"Items\":null},{\"Text\":\"بررسی تراکنش های بلاتکلیف\",\"Link\":\"/ATMUnknown\",\"Icon\":\"flaticon-signs-2\",\"Items\":null},{\"Text\":\"تسهیلات همراه\",\"Link\":\"/HamrahLoanHeaders\",\"Icon\":\"flaticon-signs-2\",\"Items\":null},{\"Text\":\"گزارش تراکنش های بلاتکلیف\",\"Link\":\"/ATMUnknown/IndexReport\",\"Icon\":\"flaticon-signs-2\",\"Items\":null},{\"Text\":\"گزارش تسهیلات همراه\",\"Link\":\"/HamrahLoanHeaders/IndexReport\",\"Icon\":\"flaticon-signs-2\",\"Items\":null},{\"Text\":\"گزارشات\",\"Link\":\"/reports\",\"Icon\":\"flaticon-diagram\",\"Items\":null}]");

            migrationBuilder.InsertData(
                table: "ApplicationUser_Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName", "PanelMenu" },
                values: new object[,]
                {
                    { 6, "AdminATMUnknown", "ادمین تراکنش های نامعلوم", "ادمین تراکنش های نامعلوم", "[{\"Text\":\"خانه\",\"Link\":\"/home\",\"Icon\":\"flaticon-line-graph\",\"Items\":null},{\"Text\":\"گزارش تراکنش های بلاتکلیف\",\"Link\":\"/ATMUnknown/IndexReport\",\"Icon\":\"flaticon-signs-2\",\"Items\":null}]" },
                    { 7, "AdminHamrahLoan", "ادمین وام همراه", "ادمین وام همراه", "[{\"Text\":\"خانه\",\"Link\":\"/home\",\"Icon\":\"flaticon-line-graph\",\"Items\":null},{\"Text\":\"گزارش تسهیلات همراه\",\"Link\":\"/HamrahLoanHeaders/IndexReport\",\"Icon\":\"flaticon-signs-2\",\"Items\":null}]" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ApplicationUser_Roles",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ApplicationUser_Roles",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.UpdateData(
                table: "ApplicationUserItems",
                keyColumn: "Id",
                keyValue: 1,
                column: "RegDateTime",
                value: new DateTime(2020, 6, 14, 12, 15, 27, 771, DateTimeKind.Local).AddTicks(5775));

            migrationBuilder.UpdateData(
                table: "ApplicationUserItems",
                keyColumn: "Id",
                keyValue: 2,
                column: "RegDateTime",
                value: new DateTime(2020, 6, 14, 12, 15, 27, 776, DateTimeKind.Local).AddTicks(3553));

            migrationBuilder.UpdateData(
                table: "ApplicationUser_Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "PanelMenu",
                value: "[{\"Text\":\"خانه\",\"Link\":\"/home\",\"Icon\":\"flaticon-line-graph\",\"Items\":null},{\"Text\":\"مدیریت کاربران\",\"Link\":\"/usermanagement\",\"Icon\":\"flaticon-piggy-bank\",\"Items\":null},{\"Text\":\"تمامی تراکنش ها\",\"Link\":\"/transactions/all\",\"Icon\":\"fas fa-sitemap\",\"Items\":null},{\"Text\":\"سند برداشت تجمعی\",\"Link\":\"/transactiondetails/branchtransaction\",\"Icon\":\"fas fa-sitemap\",\"Items\":null},{\"Text\":\"تراکنش های فزونی\",\"Link\":\"/transactiondetails/shetabi\",\"Icon\":\"fas fa-sitemap\",\"Items\":null},{\"Text\":\"لیست شعبه ها\",\"Link\":\"/branches\",\"Icon\":\"fas fa-sitemap\",\"Items\":null},{\"Text\":\"گزارشات\",\"Link\":\"/reports\",\"Icon\":\"flaticon-diagram\",\"Items\":null}]");

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
    }
}
