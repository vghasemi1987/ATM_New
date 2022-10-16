using ApplicationCommon;
using DomainEntities.ApplicationUserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Infrastructure.Data.ApplicationUserAggregate
{
    public class ApplicationRoleConfig : IEntityTypeConfiguration<ApplicationRole>
    {
        public void Configure(EntityTypeBuilder<ApplicationRole> builder)
        {
            builder.ToTable("ApplicationUser_Roles");

            var DeveloperMenu = new List<UserPanelMenu>
            {
                new UserPanelMenu { Text = "خانه", Icon = "flaticon-line-graph", Link = "/home", Items = null },
                new UserPanelMenu { Text = "مدیریت کاربران", Icon = "flaticon-piggy-bank", Link = "/usermanagement", Items = null },

                new UserPanelMenu { Text = "فایل تراکنش ها",Link = "/transactions",Icon="flaticon-signs-2", Items = null },
                new UserPanelMenu { Text = "لیست تراکنش ها",Link = "/transactiondetails",Icon="fas fa-sitemap", Items = null },
                new UserPanelMenu { Text = "تمامی تراکنش ها",Link = "/transactions/all",Icon="fas fa-sitemap", Items = null },
                new UserPanelMenu { Text = "سند برداشت تجمعی",Link = "/transactiondetails/branchtransaction",Icon="fas fa-sitemap", Items = null },
                new UserPanelMenu { Text = "تراکنش های فزونی",Link = "/transactiondetails/shetabi",Icon="fas fa-sitemap", Items = null },
                new UserPanelMenu { Text = "لیست شعبه ها",Link = "/branches",Icon="fas fa-sitemap", Items = null },


                new UserPanelMenu { Text = "وظایف", Icon = "flaticon-list-3", Items = new List<UserPanelMenu>{
                    new UserPanelMenu { Text = "تمامی وظایف",Link = "/todotask", Items = null },
                    new UserPanelMenu { Text = "وظایف من",Link = "/todotask?AssignedToUserId=1&oper=And&ToDoTaskStateId=1&ToDoTaskStateId=2&ToDoTaskStateId=4", Items = null },
                    new UserPanelMenu { Text = "وظایف بازگشتی",Link = "/todotask?CreatorUserId=1&oper=and&ToDoTaskStateId=4", Items = null },
                    new UserPanelMenu { Text = "وظایف انجام شده من",Link = "/todotask?AssignedToUserId=1&oper=and&ToDoTaskStateId=3", Items = null },
                    new UserPanelMenu { Text = "وظایف تعریف شده توسط من",Link = "/todotask?CreatorUserId=1&oper=and", Items = null },
                    new UserPanelMenu { Text = "جستجوی پیشرفته",Link = "/todotask/search", Items = null }
                    }
                },
                new UserPanelMenu { Text = "گزارشات", Icon = "flaticon-diagram", Link = "/reports", Items = null },
                new UserPanelMenu { Text = "تنظیمات", Icon = "fas fa-cogs", Items = new List<UserPanelMenu>{
                    new UserPanelMenu { Text = "پست الکترونیکی",Link = "/setting/email", Items = null },
                    new UserPanelMenu { Text = "سامانه",Link = "/setting/application", Items = null },
                    new UserPanelMenu { Text = "سطوح دسترسی", Link = "/accessrights", Items = null },
                    }
                }
            };
            var AccountingMenu = new List<UserPanelMenu>
            {
                new UserPanelMenu { Text = "خانه", Icon = "flaticon-line-graph", Link = "/home", Items = null },
                new UserPanelMenu { Text = "مدیریت کاربران", Icon = "flaticon-piggy-bank", Link = "/usermanagement", Items = null },
                new UserPanelMenu { Text = "تراکنش ها فزونی", Icon = "fas fa-sitemap", Link = "",
                    Items =new List<UserPanelMenu> {
                        new UserPanelMenu { Text = "فایل تراکنش ها",Link = "/transactions/all",Icon="fas fa-sitemap", Items = null },
                        new UserPanelMenu { Text = "فرایند تراکنش ها",Link = "/transactiondetails",Icon="fas fa-sitemap", Items = null },
                        new UserPanelMenu { Text = "لیست تراکنش های شعبه",Link = "/transactiondetails/BranchDetail",Icon="flaticon-signs-2", Items = null }
                    }
                },
                new UserPanelMenu { Text = "سند برداشت تجمعی",Link = "/transactiondetails/branchtransaction",Icon="fas fa-sitemap", Items = null },
                new UserPanelMenu { Text = "دریافت فایل شتابی",Link = "/transactiondetails/shetabi",Icon="fas fa-sitemap", Items = null },
                new UserPanelMenu { Text = "لیست شعبه ها",Link = "/branches",Icon="fas fa-sitemap", Items = null },
                new UserPanelMenu { Text = "گزارشات", Icon = "flaticon-diagram", Link = "/reports", Items = null },
            };
            var BranchHeadMenu = new List<UserPanelMenu>
            {
                new UserPanelMenu { Text = "خانه", Icon = "flaticon-line-graph", Link = "/home", Items = null },
                new UserPanelMenu { Text = "مدیریت کاربران", Icon = "flaticon-piggy-bank", Link = "/usermanagement", Items = null },
                new UserPanelMenu { Text = "گزارشات", Icon = "flaticon-diagram", Link = "/reports", Items = null },
            };
            var BranchBossMenu = new List<UserPanelMenu>
            {
                new UserPanelMenu { Text = "خانه", Icon = "flaticon-line-graph", Link = "/home", Items = null },
                new UserPanelMenu { Text = "لیست تراکنش ها",Link = "/transactiondetails",Icon="fas fa-sitemap", Items = null },
                new UserPanelMenu { Text = "لیست تراکنش های شعبه",Link = "/transactiondetails/BranchDetail",Icon="fas fa-sitemap", Items = null },
                new UserPanelMenu { Text = "بررسی تراکنش های بلاتکلیف",Link = "/ATMUnknown",Icon="flaticon-signs-2", Items = null },
                new UserPanelMenu { Text = "تسهیلات همراه",Link = "/HamrahLoanHeaders",Icon="flaticon-signs-2", Items = null },
                new UserPanelMenu { Text = "گزارش تراکنش های بلاتکلیف",Link = "/ATMUnknown/IndexReport",Icon="flaticon-signs-2", Items = null },
                new UserPanelMenu { Text = "گزارش تسهیلات همراه",Link = "/HamrahLoanHeaders/IndexReport",Icon="flaticon-signs-2", Items = null },
                new UserPanelMenu { Text = "گزارشات", Icon = "flaticon-diagram", Link = "/reports", Items = null },
            };
            var OperatorMenu = new List<UserPanelMenu>
            {
                new UserPanelMenu { Text = "خانه", Icon = "flaticon-line-graph", Link = "/home", Items = null },
                new UserPanelMenu { Text = "فایل تراکنش ها",Link = "/transactions",Icon="flaticon-signs-2", Items = null },
                new UserPanelMenu { Text = "لیست تراکنش های شعبه",Link = "/transactiondetails/BranchDetail",Icon="fas fa-sitemap", Items = null },
                new UserPanelMenu { Text = "بررسی تراکنش های بلاتکلیف",Link = "/ATMUnknown",Icon="flaticon-signs-2", Items = null },
                new UserPanelMenu { Text = "تسهیلات همراه",Link = "/HamrahLoanHeaders",Icon="flaticon-signs-2", Items = null },
                new UserPanelMenu { Text = "گزارش تراکنش های بلاتکلیف",Link = "/ATMUnknown/IndexReport",Icon="flaticon-signs-2", Items = null },
                new UserPanelMenu { Text = "گزارش تسهیلات همراه",Link = "/HamrahLoanHeaders/IndexReport",Icon="flaticon-signs-2", Items = null },
                new UserPanelMenu { Text = "گزارشات", Icon = "flaticon-diagram", Link = "/reports", Items = null },
            };
            var AdminATMUnknownMenu = new List<UserPanelMenu>
            {
                new UserPanelMenu { Text = "خانه", Icon = "flaticon-line-graph", Link = "/home", Items = null },
                new UserPanelMenu { Text = "گزارش تراکنش های بلاتکلیف",Link = "/ATMUnknown/IndexReport",Icon="flaticon-signs-2", Items = null },

            };
            var AdminHamrahLoanMenu = new List<UserPanelMenu>
            {
                new UserPanelMenu { Text = "خانه", Icon = "flaticon-line-graph", Link = "/home", Items = null },
                new UserPanelMenu { Text = "گزارش تسهیلات همراه",Link = "/HamrahLoanHeaders/IndexReport",Icon="flaticon-signs-2", Items = null },
                new UserPanelMenu { Text = "گزارشات", Icon = "flaticon-diagram", Link = "/reports", Items = null },

            };
            builder.HasData(
                new ApplicationRole
                {
                    Id = (int)RolesEnum.Developer,
                    Name = RolesEnum.Developer.DescriptionAttr(),
                    NormalizedName = RolesEnum.Developer.DescriptionAttr().ToUpper(),
                    ConcurrencyStamp = "15d0ff7d-57e4-43cc-a3bd-7c87d7b9be7d",
                    PanelMenu = JsonConvert.SerializeObject(DeveloperMenu)
                },
                new ApplicationRole
                {
                    Id = (int)RolesEnum.Accounting,
                    Name = RolesEnum.Accounting.DescriptionAttr(),
                    NormalizedName = RolesEnum.Accounting.DescriptionAttr().ToUpper(),
                    ConcurrencyStamp = "a1a62ada-65d4-42ef-ac91-faf6f10ca028",
                    PanelMenu = JsonConvert.SerializeObject(AccountingMenu)
                },
                new ApplicationRole
                {
                    Id = (int)RolesEnum.BranchHead,
                    Name = RolesEnum.BranchHead.DescriptionAttr(),
                    NormalizedName = RolesEnum.BranchHead.DescriptionAttr().ToUpper(),
                    ConcurrencyStamp = "a1a62ada-65d4-42ef-ac91-faf6f10ca027",
                    PanelMenu = JsonConvert.SerializeObject(BranchHeadMenu)
                },
                new ApplicationRole
                {
                    Id = (int)RolesEnum.BranchBoss,
                    Name = RolesEnum.BranchBoss.DescriptionAttr(),
                    NormalizedName = RolesEnum.BranchBoss.DescriptionAttr().ToUpper(),
                    ConcurrencyStamp = "a1a62ada-65d4-42ef-ac91-faf6f10ca025",
                    PanelMenu = JsonConvert.SerializeObject(BranchBossMenu)
                },
                new ApplicationRole
                {
                    Id = (int)RolesEnum.Operator,
                    Name = RolesEnum.Operator.DescriptionAttr(),
                    NormalizedName = RolesEnum.Operator.DescriptionAttr().ToUpper(),
                    ConcurrencyStamp = "15d0ff7d-57e4-43cc-a3bd-7c87d7b9be7d",
                    PanelMenu = JsonConvert.SerializeObject(OperatorMenu)
                },
                new ApplicationRole
                {
                    Id = (int)RolesEnum.AdminATMUnknown,
                    Name = RolesEnum.AdminATMUnknown.DescriptionAttr(),
                    NormalizedName = RolesEnum.AdminATMUnknown.DescriptionAttr().ToUpper(),
                    ConcurrencyStamp = "AdminATMUnknown",
                    PanelMenu = JsonConvert.SerializeObject(AdminATMUnknownMenu)
                }
                ,
                new ApplicationRole
                {
                    Id = (int)RolesEnum.AdminHamrahLoan,
                    Name = RolesEnum.AdminHamrahLoan.DescriptionAttr(),
                    NormalizedName = RolesEnum.AdminHamrahLoan.DescriptionAttr().ToUpper(),
                    ConcurrencyStamp = "AdminHamrahLoan",
                    PanelMenu = JsonConvert.SerializeObject(AdminHamrahLoanMenu)
                });
        }
    }
}