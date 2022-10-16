using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class MenuModify2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.UpdateData(
                table: "ApplicationUser_Roles",
                keyColumn: "Id",
                keyValue: 7,
                column: "PanelMenu",
                value: "[{\"Text\":\"خانه\",\"Link\":\"/home\",\"Icon\":\"flaticon-line-graph\",\"Items\":null},{\"Text\":\"گزارش تسهیلات همراه\",\"Link\":\"/HamrahLoanHeaders/IndexReport\",\"Icon\":\"flaticon-signs-2\",\"Items\":null},{\"Text\":\"گزارشات\",\"Link\":\"/reports\",\"Icon\":\"flaticon-diagram\",\"Items\":null}]");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
                keyValue: 7,
                column: "PanelMenu",
                value: "[{\"Text\":\"خانه\",\"Link\":\"/home\",\"Icon\":\"flaticon-line-graph\",\"Items\":null},{\"Text\":\"گزارش تسهیلات همراه\",\"Link\":\"/HamrahLoanHeaders/IndexReport\",\"Icon\":\"flaticon-signs-2\",\"Items\":null}]");
        }
    }
}
