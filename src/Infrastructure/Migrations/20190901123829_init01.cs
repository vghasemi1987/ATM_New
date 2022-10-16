using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class init01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplicationUser_OrganizationalCharts",
                columns: table => new
                {
                    Id = table.Column<short>(nullable: false),
                    Title = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUser_OrganizationalCharts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUser_Roles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PanelMenu = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUser_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ATMUnknownTransactions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ATMUnknownTransactionsID = table.Column<int>(nullable: true),
                    TransactionAmount = table.Column<int>(nullable: true),
                    TransactionDate = table.Column<DateTime>(nullable: false),
                    TransactionTime = table.Column<string>(maxLength: 10, nullable: true),
                    TransactionNumber = table.Column<string>(maxLength: 20, nullable: true),
                    CardNumber = table.Column<string>(maxLength: 25, nullable: true),
                    DeterminationDate = table.Column<DateTime>(nullable: false),
                    ATMID = table.Column<int>(nullable: true),
                    ATM = table.Column<string>(maxLength: 200, nullable: true),
                    Branch = table.Column<string>(maxLength: 200, nullable: true),
                    BranchID = table.Column<int>(nullable: true),
                    BranchCode = table.Column<string>(maxLength: 20, nullable: true),
                    TransactionDateShamsi = table.Column<string>(maxLength: 10, nullable: true),
                    ATMAtletCode = table.Column<string>(maxLength: 20, nullable: true),
                    TransactionDateShamsi2 = table.Column<string>(maxLength: 10, nullable: true),
                    CardType = table.Column<string>(maxLength: 50, nullable: true),
                    ATMDailyJournalTransactionsID = table.Column<int>(nullable: true),
                    StatusID = table.Column<int>(nullable: true),
                    Status = table.Column<string>(maxLength: 50, nullable: true),
                    ATMUnkhownTransactionDailyStatusID = table.Column<int>(nullable: true),
                    AfterCutover = table.Column<bool>(nullable: true),
                    SuccessfullTransactionID = table.Column<int>(nullable: true),
                    ATMHasJournal = table.Column<bool>(nullable: true),
                    MaskCardNumber = table.Column<string>(maxLength: 30, nullable: true),
                    DailyConflictID = table.Column<int>(nullable: true),
                    IsManually = table.Column<bool>(nullable: true),
                    ManualResolveDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ATMUnknownTransactions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Common_Branches",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BranchHeadId = table.Column<int>(nullable: true),
                    Code = table.Column<int>(nullable: false),
                    Title = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Common_Branches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Common_Branches_Common_Branches_BranchHeadId",
                        column: x => x.BranchHeadId,
                        principalTable: "Common_Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Common_Priorities",
                columns: table => new
                {
                    Id = table.Column<short>(nullable: false),
                    Title = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Common_Priorities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Notification_Categories",
                columns: table => new
                {
                    Id = table.Column<short>(nullable: false),
                    Title = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Report_Boxes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(maxLength: 50, nullable: true),
                    Description = table.Column<string>(maxLength: 100, nullable: true),
                    Icon = table.Column<string>(maxLength: 50, nullable: true),
                    Key = table.Column<string>(maxLength: 50, nullable: true),
                    Link = table.Column<string>(maxLength: 150, nullable: true),
                    AccessRight = table.Column<string>(maxLength: 50, nullable: true),
                    BoxStatus = table.Column<string>(maxLength: 100, nullable: true),
                    SqlCommand = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Report_Boxes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Report_Charts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(maxLength: 100, nullable: true),
                    Type = table.Column<string>(nullable: true),
                    Command = table.Column<string>(nullable: true),
                    SeriesName = table.Column<string>(nullable: true),
                    ClassName = table.Column<string>(nullable: true),
                    Style = table.Column<string>(nullable: true),
                    Color = table.Column<string>(nullable: true),
                    ChartOptions = table.Column<string>(nullable: true),
                    AccessRight = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Report_Charts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    Id = table.Column<short>(nullable: false),
                    EmailUsername = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    EmailPassword = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    EmailSmtpServer = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    EmailPortNumber = table.Column<short>(nullable: true),
                    EnableSsl = table.Column<bool>(nullable: true),
                    WelcomeText = table.Column<string>(nullable: true),
                    WebSiteTitle = table.Column<string>(maxLength: 100, nullable: true),
                    SmsServiceNumber = table.Column<string>(maxLength: 50, nullable: true),
                    SmsUserName = table.Column<string>(nullable: true),
                    SmsPassword = table.Column<string>(maxLength: 100, nullable: true),
                    ThanksMsg = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transaction_Status",
                columns: table => new
                {
                    Id = table.Column<short>(nullable: false),
                    Title = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction_Status", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transaction_Types",
                columns: table => new
                {
                    Id = table.Column<short>(nullable: false),
                    Title = table.Column<string>(unicode: false, maxLength: 10, nullable: true),
                    Extension = table.Column<string>(unicode: false, maxLength: 5, nullable: true),
                    Content = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Separation = table.Column<string>(unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction_Types", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUser_RoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<int>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUser_RoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationUser_RoleClaims_ApplicationUser_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "ApplicationUser_Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUserItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 150, nullable: false),
                    LastName = table.Column<string>(maxLength: 150, nullable: false),
                    PersonnelCode = table.Column<string>(maxLength: 10, nullable: true),
                    Picture = table.Column<string>(maxLength: 150, nullable: true),
                    RegDateTime = table.Column<DateTime>(nullable: false),
                    BranchId = table.Column<int>(nullable: true),
                    BranchHeadId = table.Column<int>(nullable: true),
                    OrganizationalChartId = table.Column<short>(nullable: true),
                    RegisterByUserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationUserItems_Common_Branches_BranchHeadId",
                        column: x => x.BranchHeadId,
                        principalTable: "Common_Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ApplicationUserItems_Common_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Common_Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ApplicationUserItems_ApplicationUserItems_RegisterByUserId",
                        column: x => x.RegisterByUserId,
                        principalTable: "ApplicationUserItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Common_Accounts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BranchId = table.Column<int>(maxLength: 5, nullable: true),
                    AccountNo = table.Column<int>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Common_Accounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Common_Accounts_Common_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Common_Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ATMUnknownTransactions_Workfollow",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ATMUnknownTransactionsId = table.Column<int>(nullable: true),
                    StatusId = table.Column<short>(nullable: true),
                    UpdateDateTime = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ATMUnknownTransactions_Workfollow", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ATMUnknownTransactions_Workfollow_ATMUnknownTransactions_ATMUnknownTransactionsId",
                        column: x => x.ATMUnknownTransactionsId,
                        principalTable: "ATMUnknownTransactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ATMUnknownTransactions_Workfollow_Transaction_Status_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Transaction_Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUser_ActivityLogs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ActivityTitle = table.Column<string>(maxLength: 200, nullable: true),
                    ActionName = table.Column<string>(maxLength: 150, nullable: true),
                    ControllerName = table.Column<string>(maxLength: 150, nullable: true),
                    UserId = table.Column<int>(nullable: false),
                    EntryDateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUser_ActivityLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationUser_ActivityLogs_ApplicationUserItems_UserId",
                        column: x => x.UserId,
                        principalTable: "ApplicationUserItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUser_Claims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUser_Claims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationUser_Claims_ApplicationUserItems_UserId",
                        column: x => x.UserId,
                        principalTable: "ApplicationUserItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUser_Logins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUser_Logins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_ApplicationUser_Logins_ApplicationUserItems_UserId",
                        column: x => x.UserId,
                        principalTable: "ApplicationUserItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUser_Tokens",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUser_Tokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_ApplicationUser_Tokens_ApplicationUserItems_UserId",
                        column: x => x.UserId,
                        principalTable: "ApplicationUserItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUser_UserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUser_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_ApplicationUser_UserRoles_ApplicationUser_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "ApplicationUser_Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUser_UserRoles_ApplicationUserItems_UserId",
                        column: x => x.UserId,
                        principalTable: "ApplicationUserItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NotificationItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EntryDateTime = table.Column<DateTime>(nullable: false),
                    Text = table.Column<string>(maxLength: 300, nullable: true),
                    CreatedByUserId = table.Column<int>(nullable: false),
                    ForUserId = table.Column<int>(nullable: false),
                    IsRead = table.Column<bool>(nullable: false),
                    IsSent = table.Column<bool>(nullable: false),
                    CategoryId = table.Column<short>(nullable: false),
                    Url = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NotificationItems_Notification_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Notification_Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NotificationItems_ApplicationUserItems_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "ApplicationUserItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NotificationItems_ApplicationUserItems_ForUserId",
                        column: x => x.ForUserId,
                        principalTable: "ApplicationUserItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Transaction_Files",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 150, nullable: true),
                    FileData = table.Column<byte[]>(nullable: true),
                    StatusId = table.Column<short>(nullable: true),
                    RegDateTime = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    BranchId = table.Column<int>(nullable: false),
                    BranchHeadId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction_Files", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transaction_Files_Common_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Common_Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transaction_Files_Transaction_Status_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Transaction_Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transaction_Files_ApplicationUserItems_UserId",
                        column: x => x.UserId,
                        principalTable: "ApplicationUserItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transaction_FileDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FileId = table.Column<int>(nullable: false),
                    Date = table.Column<string>(unicode: false, maxLength: 6, nullable: true),
                    Time = table.Column<string>(unicode: false, maxLength: 6, nullable: true),
                    Operation = table.Column<string>(unicode: false, maxLength: 6, nullable: true),
                    AtmCode = table.Column<string>(unicode: false, maxLength: 8, nullable: true),
                    CardNumber = table.Column<string>(unicode: false, maxLength: 16, nullable: true),
                    Amount = table.Column<decimal>(type: "decimal", nullable: true),
                    TransactionNumber = table.Column<string>(unicode: false, maxLength: 10, nullable: true),
                    TypeId = table.Column<short>(nullable: true),
                    ResponseCode = table.Column<string>(nullable: true),
                    IsRefahi = table.Column<bool>(nullable: false),
                    StatusId = table.Column<short>(nullable: true),
                    IsShetabiPrinted = table.Column<bool>(nullable: false),
                    IsDocumentPrinted = table.Column<bool>(nullable: false),
                    DocumentCode = table.Column<int>(nullable: true),
                    UserDescription = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction_FileDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transaction_FileDetails_Transaction_Files_FileId",
                        column: x => x.FileId,
                        principalTable: "Transaction_Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transaction_FileDetails_Transaction_Status_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Transaction_Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transaction_FileDetails_Transaction_Types_TypeId",
                        column: x => x.TypeId,
                        principalTable: "Transaction_Types",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Transaction_Workfollow",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FileDetailId = table.Column<int>(nullable: true),
                    StatusId = table.Column<short>(nullable: true),
                    UpdateDateTime = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction_Workfollow", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transaction_Workfollow_Transaction_FileDetails_FileDetailId",
                        column: x => x.FileDetailId,
                        principalTable: "Transaction_FileDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transaction_Workfollow_Transaction_Status_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Transaction_Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.InsertData(
                table: "ApplicationUserItems",
                columns: new[] { "Id", "AccessFailedCount", "BranchHeadId", "BranchId", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "OrganizationalChartId", "PasswordHash", "PersonnelCode", "PhoneNumber", "PhoneNumberConfirmed", "Picture", "RegDateTime", "RegisterByUserId", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { 1, 0, null, null, "a0c979d1-f65e-4122-b62f-78b5b8df30da", "info@test.com", false, "توسعه دهنده", "وب", true, null, "INFO@TEST.COM", "ADMIN", (short)1, "AQAAAAEAACcQAAAAEFQSCRc6wVL8pu5ChTDI4xT2A5LQ2okSnHseUkzOj0SfLwNzOdHLlhSHaf+lR3jv9A==", null, null, false, null, new DateTime(2019, 9, 1, 16, 8, 27, 999, DateTimeKind.Local).AddTicks(5850), null, "68bccbf3-564b-4f50-b58e-def000a99746", false, "dev" },
                    { 2, 0, null, null, "08ff43bb-9777-467a-abe0-42fe895ff87f", "", false, "کاربر", "حسابدار", true, null, "", "ACCOUNTANT", (short)1, "AQAAAAEAACcQAAAAEKAYdENoiu4ygK4V7xueW262vL5ta6fciSNfof79fxkil/A6+11ZVDe3yH0jdn1fWg==", null, null, false, null, new DateTime(2019, 9, 1, 16, 8, 28, 2, DateTimeKind.Local).AddTicks(4398), null, "DRM3NPZEDP7P3SYF6QU3RMMOOMHETGLD", false, "accountant" }
                });

            migrationBuilder.InsertData(
                table: "ApplicationUser_OrganizationalCharts",
                columns: new[] { "Id", "Title" },
                values: new object[,]
                {
                    { (short)1, "رئیس" },
                    { (short)2, "معاون" },
                    { (short)3, "کارشناس مسئول" },
                    { (short)4, "کارشناس" }
                });

            migrationBuilder.InsertData(
                table: "ApplicationUser_Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName", "PanelMenu" },
                values: new object[,]
                {
                    { 1, "15d0ff7d-57e4-43cc-a3bd-7c87d7b9be7d", "توسعه دهنده", "توسعه دهنده", "[{\"Text\":\"خانه\",\"Link\":\"/home\",\"Icon\":\"flaticon-line-graph\",\"Items\":null},{\"Text\":\"مدیریت کاربران\",\"Link\":\"/usermanagement\",\"Icon\":\"flaticon-piggy-bank\",\"Items\":null},{\"Text\":\"فایل تراکنش ها\",\"Link\":\"/transactions\",\"Icon\":\"flaticon-signs-2\",\"Items\":null},{\"Text\":\"لیست تراکنش ها\",\"Link\":\"/transactiondetails\",\"Icon\":\"fas fa-sitemap\",\"Items\":null},{\"Text\":\"تمامی تراکنش ها\",\"Link\":\"/transactions/all\",\"Icon\":\"fas fa-sitemap\",\"Items\":null},{\"Text\":\"سند برداشت تجمعی\",\"Link\":\"/transactiondetails/branchtransaction\",\"Icon\":\"fas fa-sitemap\",\"Items\":null},{\"Text\":\"تراکنش های فزونی\",\"Link\":\"/transactiondetails/shetabi\",\"Icon\":\"fas fa-sitemap\",\"Items\":null},{\"Text\":\"لیست شعبه ها\",\"Link\":\"/branches\",\"Icon\":\"fas fa-sitemap\",\"Items\":null},{\"Text\":\"وظایف\",\"Link\":null,\"Icon\":\"flaticon-list-3\",\"Items\":[{\"Text\":\"تمامی وظایف\",\"Link\":\"/todotask\",\"Icon\":null,\"Items\":null},{\"Text\":\"وظایف من\",\"Link\":\"/todotask?AssignedToUserId=1&oper=And&ToDoTaskStateId=1&ToDoTaskStateId=2&ToDoTaskStateId=4\",\"Icon\":null,\"Items\":null},{\"Text\":\"وظایف بازگشتی\",\"Link\":\"/todotask?CreatorUserId=1&oper=and&ToDoTaskStateId=4\",\"Icon\":null,\"Items\":null},{\"Text\":\"وظایف انجام شده من\",\"Link\":\"/todotask?AssignedToUserId=1&oper=and&ToDoTaskStateId=3\",\"Icon\":null,\"Items\":null},{\"Text\":\"وظایف تعریف شده توسط من\",\"Link\":\"/todotask?CreatorUserId=1&oper=and\",\"Icon\":null,\"Items\":null},{\"Text\":\"جستجوی پیشرفته\",\"Link\":\"/todotask/search\",\"Icon\":null,\"Items\":null}]},{\"Text\":\"گزارشات\",\"Link\":\"/reports\",\"Icon\":\"flaticon-diagram\",\"Items\":null},{\"Text\":\"تنظیمات\",\"Link\":null,\"Icon\":\"fas fa-cogs\",\"Items\":[{\"Text\":\"پست الکترونیکی\",\"Link\":\"/setting/email\",\"Icon\":null,\"Items\":null},{\"Text\":\"سامانه\",\"Link\":\"/setting/application\",\"Icon\":null,\"Items\":null},{\"Text\":\"سطوح دسترسی\",\"Link\":\"/accessrights\",\"Icon\":null,\"Items\":null}]}]" },
                    { 2, "a1a62ada-65d4-42ef-ac91-faf6f10ca028", "حسابدار", "حسابدار", "[{\"Text\":\"خانه\",\"Link\":\"/home\",\"Icon\":\"flaticon-line-graph\",\"Items\":null},{\"Text\":\"مدیریت کاربران\",\"Link\":\"/usermanagement\",\"Icon\":\"flaticon-piggy-bank\",\"Items\":null},{\"Text\":\"تمامی تراکنش ها\",\"Link\":\"/transactions/all\",\"Icon\":\"fas fa-sitemap\",\"Items\":null},{\"Text\":\"سند برداشت تجمعی\",\"Link\":\"/transactiondetails/branchtransaction\",\"Icon\":\"fas fa-sitemap\",\"Items\":null},{\"Text\":\"تراکنش های فزونی\",\"Link\":\"/transactiondetails/shetabi\",\"Icon\":\"fas fa-sitemap\",\"Items\":null},{\"Text\":\"لیست شعبه ها\",\"Link\":\"/branches\",\"Icon\":\"fas fa-sitemap\",\"Items\":null},{\"Text\":\"گزارشات\",\"Link\":\"/reports\",\"Icon\":\"flaticon-diagram\",\"Items\":null}]" },
                    { 3, "a1a62ada-65d4-42ef-ac91-faf6f10ca027", "امور شعب", "امور شعب", "[{\"Text\":\"خانه\",\"Link\":\"/home\",\"Icon\":\"flaticon-line-graph\",\"Items\":null},{\"Text\":\"مدیریت کاربران\",\"Link\":\"/usermanagement\",\"Icon\":\"flaticon-piggy-bank\",\"Items\":null},{\"Text\":\"گزارشات\",\"Link\":\"/reports\",\"Icon\":\"flaticon-diagram\",\"Items\":null}]" },
                    { 4, "a1a62ada-65d4-42ef-ac91-faf6f10ca025", "رئیس شعبه", "رئیس شعبه", "[{\"Text\":\"خانه\",\"Link\":\"/home\",\"Icon\":\"flaticon-line-graph\",\"Items\":null},{\"Text\":\"لیست تراکنش ها\",\"Link\":\"/transactiondetails\",\"Icon\":\"fas fa-sitemap\",\"Items\":null},{\"Text\":\"گزارشات\",\"Link\":\"/reports\",\"Icon\":\"flaticon-diagram\",\"Items\":null}]" },
                    { 5, "15d0ff7d-57e4-43cc-a3bd-7c87d7b9be7d", "متصدی", "متصدی", "[{\"Text\":\"خانه\",\"Link\":\"/home\",\"Icon\":\"flaticon-line-graph\",\"Items\":null},{\"Text\":\"فایل تراکنش ها\",\"Link\":\"/transactions\",\"Icon\":\"flaticon-signs-2\",\"Items\":null},{\"Text\":\"گزارشات\",\"Link\":\"/reports\",\"Icon\":\"flaticon-diagram\",\"Items\":null}]" }
                });

            migrationBuilder.InsertData(
                table: "Common_Branches",
                columns: new[] { "Id", "BranchHeadId", "Code", "Title" },
                values: new object[,]
                {
                    { 1, null, 1, "اداره منطقه یک تهران" },
                    { 2, null, 2, "اداره منطقه دو تهران" }
                });

            migrationBuilder.InsertData(
                table: "Common_Priorities",
                columns: new[] { "Id", "Title" },
                values: new object[,]
                {
                    { (short)3, "پایین" },
                    { (short)2, "معمولی" },
                    { (short)1, "ضروری" }
                });

            migrationBuilder.InsertData(
                table: "Notification_Categories",
                columns: new[] { "Id", "Title" },
                values: new object[,]
                {
                    { (short)3, "موعد انجام وظیفه" },
                    { (short)2, "تغییر وضعیت وظیفه" },
                    { (short)1, "وظیفه ارسالی" },
                    { (short)4, "موعد انجام تست" }
                });

            migrationBuilder.InsertData(
                table: "Settings",
                columns: new[] { "Id", "EmailPassword", "EmailPortNumber", "EmailSmtpServer", "EmailUsername", "EnableSsl", "SmsPassword", "SmsServiceNumber", "SmsUserName", "ThanksMsg", "WebSiteTitle", "WelcomeText" },
                values: new object[] { (short)1, null, null, null, null, null, null, null, null, null, "سامانه تست نفوذ", null });

            migrationBuilder.InsertData(
                table: "Transaction_Status",
                columns: new[] { "Id", "Title" },
                values: new object[,]
                {
                    { (short)1, "ثبت و پردازش شده" },
                    { (short)2, "ارسال به رئیس شعبه" },
                    { (short)3, "ارسال به حسابداری" },
                    { (short)4, "ثبت نهایی" },
                    { (short)5, "بازگشت به متصدی" },
                    { (short)6, "بازگشت به رئیس شعبه" }
                });

            migrationBuilder.InsertData(
                table: "Transaction_Types",
                columns: new[] { "Id", "Content", "Extension", "Separation", "Title" },
                values: new object[,]
                {
                    { (short)3, "START RETRACT", "txt", "OP.", "hyo" },
                    { (short)1, "RETRACTED FAIL", "log", "\\n========================================", "grg" },
                    { (short)2, "RETRACT ACTION FINISHED", "log", "\\n========================================", "grg" },
                    { (short)4, "CASH RETRACT", "jrn", "OP.", "wincor" }
                });

            migrationBuilder.InsertData(
                table: "ApplicationUser_RoleClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "RoleId" },
                values: new object[,]
                {
                    { 1, "Permission", "UserManagement_Index", 1 },
                    { 41, "Permission", "UserManagement_GetDetail", 2 },
                    { 42, "Permission", "UserManagement_AddDetail", 2 },
                    { 43, "Permission", "UserManagement_EditDetail", 2 },
                    { 44, "Permission", "UserManagement_DeleteRows", 2 },
                    { 45, "Permission", "UserManagement_ImpersonateUser", 2 },
                    { 46, "Permission", "Transactions_All", 2 },
                    { 47, "Permission", "TransactionDetails_Shetabi", 2 },
                    { 48, "Permission", "TransactionDetails_BranchTransaction", 2 },
                    { 49, "Permission", "Branches_Index", 2 },
                    { 50, "Permission", "Branches_GetDetail", 2 },
                    { 51, "Permission", "Branches_AddDetail", 2 },
                    { 52, "Permission", "Branches_EditDetail", 2 },
                    { 53, "Permission", "Branches_DeleteRows", 2 },
                    { 30, "Permission", "UserManagement_Index", 3 },
                    { 31, "Permission", "UserManagement_GetDetail", 3 },
                    { 32, "Permission", "UserManagement_AddDetail", 3 },
                    { 33, "Permission", "UserManagement_EditDetail", 3 },
                    { 34, "Permission", "UserManagement_DeleteRows", 3 },
                    { 35, "Permission", "TransactionDetails_Index", 4 },
                    { 36, "Permission", "Transactions_Index", 5 },
                    { 37, "Permission", "Transactions_UploadFile", 5 },
                    { 38, "Permission", "Transactions_DeleteRows", 5 },
                    { 39, "Permission", "TransactionDetails_Index", 5 },
                    { 40, "Permission", "UserManagement_Index", 2 },
                    { 29, "Permission", "AccessRights_DeleteRows", 1 },
                    { 28, "Permission", "AccessRights_EditDetail", 1 },
                    { 14, "Permission", "TransactionDetails_BranchTransaction", 1 },
                    { 4, "Permission", "UserManagement_AddDetail", 1 },
                    { 5, "Permission", "UserManagement_EditDetail", 1 },
                    { 6, "Permission", "UserManagement_DeleteRows", 1 },
                    { 7, "Permission", "UserManagement_ImpersonateUser", 1 },
                    { 8, "Permission", "Transactions_Index", 1 },
                    { 9, "Permission", "Transactions_All", 1 },
                    { 10, "Permission", "Transactions_UploadFile", 1 },
                    { 11, "Permission", "Transactions_DeleteRows", 1 },
                    { 12, "Permission", "TransactionDetails_Index", 1 },
                    { 13, "Permission", "TransactionDetails_Shetabi", 1 },
                    { 27, "Permission", "AccessRights_GetDetail", 1 },
                    { 15, "Permission", "TransactionDetails_DeleteRows", 1 },
                    { 16, "Permission", "Setting_Email", 1 },
                    { 17, "Permission", "Setting_Sms", 1 },
                    { 18, "Permission", "Setting_Application", 1 },
                    { 19, "Permission", "Notification_Index", 1 },
                    { 20, "Permission", "Branches_Index", 1 },
                    { 21, "Permission", "Branches_GetDetail", 1 },
                    { 22, "Permission", "Branches_AddDetail", 1 },
                    { 23, "Permission", "Branches_EditDetail", 1 },
                    { 24, "Permission", "Branches_DeleteRows", 1 },
                    { 25, "Permission", "AccessRights_Index", 1 },
                    { 26, "Permission", "AccessRights_AddDetail", 1 },
                    { 3, "Permission", "UserManagement_UpdateProfile", 1 },
                    { 2, "Permission", "UserManagement_GetDetail", 1 }
                });

            migrationBuilder.InsertData(
                table: "ApplicationUser_UserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUser_ActivityLogs_UserId",
                table: "ApplicationUser_ActivityLogs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUser_Claims_UserId",
                table: "ApplicationUser_Claims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUser_Logins_UserId",
                table: "ApplicationUser_Logins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUser_RoleClaims_RoleId",
                table: "ApplicationUser_RoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "ApplicationUser_Roles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUser_UserRoles_RoleId",
                table: "ApplicationUser_UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserItems_BranchHeadId",
                table: "ApplicationUserItems",
                column: "BranchHeadId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserItems_BranchId",
                table: "ApplicationUserItems",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "ApplicationUserItems",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "ApplicationUserItems",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserItems_RegisterByUserId",
                table: "ApplicationUserItems",
                column: "RegisterByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ATMUnknownTransactions_Workfollow_ATMUnknownTransactionsId",
                table: "ATMUnknownTransactions_Workfollow",
                column: "ATMUnknownTransactionsId");

            migrationBuilder.CreateIndex(
                name: "IX_ATMUnknownTransactions_Workfollow_StatusId",
                table: "ATMUnknownTransactions_Workfollow",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Common_Accounts_BranchId",
                table: "Common_Accounts",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Common_Branches_BranchHeadId",
                table: "Common_Branches",
                column: "BranchHeadId");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationItems_CategoryId",
                table: "NotificationItems",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationItems_CreatedByUserId",
                table: "NotificationItems",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationItems_ForUserId",
                table: "NotificationItems",
                column: "ForUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_FileDetails_FileId",
                table: "Transaction_FileDetails",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_FileDetails_StatusId",
                table: "Transaction_FileDetails",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_FileDetails_TypeId",
                table: "Transaction_FileDetails",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_Files_BranchId",
                table: "Transaction_Files",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_Files_StatusId",
                table: "Transaction_Files",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_Files_UserId",
                table: "Transaction_Files",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_Workfollow_FileDetailId",
                table: "Transaction_Workfollow",
                column: "FileDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_Workfollow_StatusId",
                table: "Transaction_Workfollow",
                column: "StatusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUser_ActivityLogs");

            migrationBuilder.DropTable(
                name: "ApplicationUser_Claims");

            migrationBuilder.DropTable(
                name: "ApplicationUser_Logins");

            migrationBuilder.DropTable(
                name: "ApplicationUser_OrganizationalCharts");

            migrationBuilder.DropTable(
                name: "ApplicationUser_RoleClaims");

            migrationBuilder.DropTable(
                name: "ApplicationUser_Tokens");

            migrationBuilder.DropTable(
                name: "ApplicationUser_UserRoles");

            migrationBuilder.DropTable(
                name: "ATMUnknownTransactions_Workfollow");

            migrationBuilder.DropTable(
                name: "Common_Accounts");

            migrationBuilder.DropTable(
                name: "Common_Priorities");

            migrationBuilder.DropTable(
                name: "NotificationItems");

            migrationBuilder.DropTable(
                name: "Report_Boxes");

            migrationBuilder.DropTable(
                name: "Report_Charts");

            migrationBuilder.DropTable(
                name: "Settings");

            migrationBuilder.DropTable(
                name: "Transaction_Workfollow");

            migrationBuilder.DropTable(
                name: "ApplicationUser_Roles");

            migrationBuilder.DropTable(
                name: "ATMUnknownTransactions");

            migrationBuilder.DropTable(
                name: "Notification_Categories");

            migrationBuilder.DropTable(
                name: "Transaction_FileDetails");

            migrationBuilder.DropTable(
                name: "Transaction_Files");

            migrationBuilder.DropTable(
                name: "Transaction_Types");

            migrationBuilder.DropTable(
                name: "Transaction_Status");

            migrationBuilder.DropTable(
                name: "ApplicationUserItems");

            migrationBuilder.DropTable(
                name: "Common_Branches");
        }
    }
}
