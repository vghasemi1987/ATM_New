using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.ApplicationUserAggregate
{
    public class ApplicationRoleClaimsConfig : IEntityTypeConfiguration<IdentityRoleClaim<int>>
    {
        public void Configure(EntityTypeBuilder<IdentityRoleClaim<int>> builder)
        {
            builder.ToTable("ApplicationUser_RoleClaims");

            builder.HasData(
                    new IdentityRoleClaim<int> { Id = 1, RoleId = 1, ClaimType = "Permission", ClaimValue = "UserManagement_Index" },
                    new IdentityRoleClaim<int> { Id = 2, RoleId = 1, ClaimType = "Permission", ClaimValue = "UserManagement_GetDetail" },
                    new IdentityRoleClaim<int> { Id = 3, RoleId = 1, ClaimType = "Permission", ClaimValue = "UserManagement_UpdateProfile" },
                    new IdentityRoleClaim<int> { Id = 4, RoleId = 1, ClaimType = "Permission", ClaimValue = "UserManagement_AddDetail" },
                    new IdentityRoleClaim<int> { Id = 5, RoleId = 1, ClaimType = "Permission", ClaimValue = "UserManagement_EditDetail" },
                    new IdentityRoleClaim<int> { Id = 6, RoleId = 1, ClaimType = "Permission", ClaimValue = "UserManagement_DeleteRows" },
                    new IdentityRoleClaim<int> { Id = 7, RoleId = 1, ClaimType = "Permission", ClaimValue = "UserManagement_ImpersonateUser" },
                    new IdentityRoleClaim<int> { Id = 8, RoleId = 1, ClaimType = "Permission", ClaimValue = "Transactions_Index" },
                    new IdentityRoleClaim<int> { Id = 9, RoleId = 1, ClaimType = "Permission", ClaimValue = "Transactions_All" },
                    new IdentityRoleClaim<int> { Id = 10, RoleId = 1, ClaimType = "Permission", ClaimValue = "Transactions_UploadFile" },
                    new IdentityRoleClaim<int> { Id = 11, RoleId = 1, ClaimType = "Permission", ClaimValue = "Transactions_DeleteRows" },
                    new IdentityRoleClaim<int> { Id = 12, RoleId = 1, ClaimType = "Permission", ClaimValue = "TransactionDetails_Index" },
                    new IdentityRoleClaim<int> { Id = 13, RoleId = 1, ClaimType = "Permission", ClaimValue = "TransactionDetails_Shetabi" },
                    new IdentityRoleClaim<int> { Id = 14, RoleId = 1, ClaimType = "Permission", ClaimValue = "TransactionDetails_BranchTransaction" },
                    new IdentityRoleClaim<int> { Id = 15, RoleId = 1, ClaimType = "Permission", ClaimValue = "TransactionDetails_DeleteRows" },
                    new IdentityRoleClaim<int> { Id = 16, RoleId = 1, ClaimType = "Permission", ClaimValue = "Setting_Email" },
                    new IdentityRoleClaim<int> { Id = 17, RoleId = 1, ClaimType = "Permission", ClaimValue = "Setting_Sms" },
                    new IdentityRoleClaim<int> { Id = 18, RoleId = 1, ClaimType = "Permission", ClaimValue = "Setting_Application" },
                    new IdentityRoleClaim<int> { Id = 19, RoleId = 1, ClaimType = "Permission", ClaimValue = "Notification_Index" },
                    new IdentityRoleClaim<int> { Id = 20, RoleId = 1, ClaimType = "Permission", ClaimValue = "Branches_Index" },
                    new IdentityRoleClaim<int> { Id = 21, RoleId = 1, ClaimType = "Permission", ClaimValue = "Branches_GetDetail" },
                    new IdentityRoleClaim<int> { Id = 22, RoleId = 1, ClaimType = "Permission", ClaimValue = "Branches_AddDetail" },
                    new IdentityRoleClaim<int> { Id = 23, RoleId = 1, ClaimType = "Permission", ClaimValue = "Branches_EditDetail" },
                    new IdentityRoleClaim<int> { Id = 24, RoleId = 1, ClaimType = "Permission", ClaimValue = "Branches_DeleteRows" },
                    new IdentityRoleClaim<int> { Id = 25, RoleId = 1, ClaimType = "Permission", ClaimValue = "AccessRights_Index" },
                    new IdentityRoleClaim<int> { Id = 26, RoleId = 1, ClaimType = "Permission", ClaimValue = "AccessRights_AddDetail" },
                    new IdentityRoleClaim<int> { Id = 27, RoleId = 1, ClaimType = "Permission", ClaimValue = "AccessRights_GetDetail" },
                    new IdentityRoleClaim<int> { Id = 28, RoleId = 1, ClaimType = "Permission", ClaimValue = "AccessRights_EditDetail" },
                    new IdentityRoleClaim<int> { Id = 29, RoleId = 1, ClaimType = "Permission", ClaimValue = "AccessRights_DeleteRows" },
                    new IdentityRoleClaim<int> { Id = 30, RoleId = 3, ClaimType = "Permission", ClaimValue = "UserManagement_Index" },
                    new IdentityRoleClaim<int> { Id = 31, RoleId = 3, ClaimType = "Permission", ClaimValue = "UserManagement_GetDetail" },
                    new IdentityRoleClaim<int> { Id = 32, RoleId = 3, ClaimType = "Permission", ClaimValue = "UserManagement_AddDetail" },
                    new IdentityRoleClaim<int> { Id = 33, RoleId = 3, ClaimType = "Permission", ClaimValue = "UserManagement_EditDetail" },
                    new IdentityRoleClaim<int> { Id = 34, RoleId = 3, ClaimType = "Permission", ClaimValue = "UserManagement_DeleteRows" },
                    new IdentityRoleClaim<int> { Id = 35, RoleId = 4, ClaimType = "Permission", ClaimValue = "TransactionDetails_Index" },
                    new IdentityRoleClaim<int> { Id = 36, RoleId = 5, ClaimType = "Permission", ClaimValue = "Transactions_Index" },
                    new IdentityRoleClaim<int> { Id = 37, RoleId = 5, ClaimType = "Permission", ClaimValue = "Transactions_UploadFile" },
                    new IdentityRoleClaim<int> { Id = 38, RoleId = 5, ClaimType = "Permission", ClaimValue = "Transactions_DeleteRows" },
                    new IdentityRoleClaim<int> { Id = 39, RoleId = 5, ClaimType = "Permission", ClaimValue = "TransactionDetails_Index" },
                    new IdentityRoleClaim<int> { Id = 40, RoleId = 2, ClaimType = "Permission", ClaimValue = "UserManagement_Index" },
                    new IdentityRoleClaim<int> { Id = 41, RoleId = 2, ClaimType = "Permission", ClaimValue = "UserManagement_GetDetail" },
                    new IdentityRoleClaim<int> { Id = 42, RoleId = 2, ClaimType = "Permission", ClaimValue = "UserManagement_AddDetail" },
                    new IdentityRoleClaim<int> { Id = 43, RoleId = 2, ClaimType = "Permission", ClaimValue = "UserManagement_EditDetail" },
                    new IdentityRoleClaim<int> { Id = 44, RoleId = 2, ClaimType = "Permission", ClaimValue = "UserManagement_DeleteRows" },
                    new IdentityRoleClaim<int> { Id = 45, RoleId = 2, ClaimType = "Permission", ClaimValue = "UserManagement_ImpersonateUser" },
                    new IdentityRoleClaim<int> { Id = 46, RoleId = 2, ClaimType = "Permission", ClaimValue = "Transactions_All" },
                    new IdentityRoleClaim<int> { Id = 47, RoleId = 2, ClaimType = "Permission", ClaimValue = "TransactionDetails_Shetabi" },
                    new IdentityRoleClaim<int> { Id = 48, RoleId = 2, ClaimType = "Permission", ClaimValue = "TransactionDetails_BranchTransaction" },
                    new IdentityRoleClaim<int> { Id = 49, RoleId = 2, ClaimType = "Permission", ClaimValue = "Branches_Index" },
                    new IdentityRoleClaim<int> { Id = 50, RoleId = 2, ClaimType = "Permission", ClaimValue = "Branches_GetDetail" },
                    new IdentityRoleClaim<int> { Id = 51, RoleId = 2, ClaimType = "Permission", ClaimValue = "Branches_AddDetail" },
                    new IdentityRoleClaim<int> { Id = 52, RoleId = 2, ClaimType = "Permission", ClaimValue = "Branches_EditDetail" },
                    new IdentityRoleClaim<int> { Id = 53, RoleId = 2, ClaimType = "Permission", ClaimValue = "Branches_DeleteRows" }
                );
        }
    }
}