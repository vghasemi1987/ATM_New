using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class HamrahLoan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HamrahLoanHeader",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(maxLength: 50, nullable: true),
                    Status = table.Column<byte>(nullable: false),
                    CreateDate = table.Column<DateTime>(type: "date", nullable: false),
                    StatusDate = table.Column<DateTime>(type: "date", nullable: true),
                    UserCreateId = table.Column<int>(nullable: false),
                    BranchId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HamrahLoanHeader", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HamrahLoanHeader_Common_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Common_Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HamrahLoanHeader_ApplicationUserItems_UserCreateId",
                        column: x => x.UserCreateId,
                        principalTable: "ApplicationUserItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HamrahLoanDetail",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    HeaderId = table.Column<int>(nullable: false),
                    Status = table.Column<byte>(nullable: false),
                    CreateDate = table.Column<DateTime>(type: "date", nullable: false),
                    StatusDate = table.Column<DateTime>(type: "date", nullable: true),
                    UserChangeStatusId = table.Column<int>(nullable: true),
                    LoanNumber = table.Column<string>(maxLength: 30, nullable: true),
                    Amount = table.Column<long>(nullable: false),
                    LoanDate = table.Column<DateTime>(type: "date", nullable: false),
                    FolowNumber = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HamrahLoanDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HamrahLoanDetail_HamrahLoanHeader_HeaderId",
                        column: x => x.HeaderId,
                        principalTable: "HamrahLoanHeader",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HamrahLoanDetail_ApplicationUserItems_UserChangeStatusId",
                        column: x => x.UserChangeStatusId,
                        principalTable: "ApplicationUserItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_HamrahLoanDetail_HeaderId",
                table: "HamrahLoanDetail",
                column: "HeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_HamrahLoanDetail_UserChangeStatusId",
                table: "HamrahLoanDetail",
                column: "UserChangeStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_HamrahLoanHeader_BranchId",
                table: "HamrahLoanHeader",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_HamrahLoanHeader_UserCreateId",
                table: "HamrahLoanHeader",
                column: "UserCreateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HamrahLoanDetail");

            migrationBuilder.DropTable(
                name: "HamrahLoanHeader");

            migrationBuilder.UpdateData(
                table: "ApplicationUserItems",
                keyColumn: "Id",
                keyValue: 1,
                column: "RegDateTime",
                value: new DateTime(2020, 1, 19, 16, 52, 29, 947, DateTimeKind.Local).AddTicks(833));

            migrationBuilder.UpdateData(
                table: "ApplicationUserItems",
                keyColumn: "Id",
                keyValue: 2,
                column: "RegDateTime",
                value: new DateTime(2020, 1, 19, 16, 52, 29, 952, DateTimeKind.Local).AddTicks(9409));
        }
    }
}
