using DomainEntities.ApplicationUserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.ApplicationUserAggregate
{
    public class ApplicationUserConfig : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.ToTable("ApplicationUserItems");
            builder.Property(o => o.Picture)
                .IsUnicode()
                .HasMaxLength(150);
            builder.Property(o => o.FirstName)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(150);
            builder.Property(o => o.LastName)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(150);
            builder.Property(o => o.PersonnelCode)
                .HasMaxLength(10);

            builder.Property(o => o.RegDateTime)
                .IsRequired();

            builder.HasData(
                new ApplicationUser
                {
                    Id = 1,
                    AccessFailedCount = 0,
                    ConcurrencyStamp = "a0c979d1-f65e-4122-b62f-78b5b8df30da",
                    Email = "info@test.com",
                    EmailConfirmed = false,
                    LockoutEnabled = true,
                    NormalizedEmail = "INFO@TEST.COM",
                    NormalizedUserName = "ADMIN",
                    PasswordHash =
                        "AQAAAAEAACcQAAAAEFQSCRc6wVL8pu5ChTDI4xT2A5LQ2okSnHseUkzOj0SfLwNzOdHLlhSHaf+lR3jv9A==",
                    PhoneNumberConfirmed = false,
                    SecurityStamp = "68bccbf3-564b-4f50-b58e-def000a99746",
                    TwoFactorEnabled = false,
                    FirstName = "توسعه دهنده",
                    LastName = "وب",
                    UserName = "dev",
                    OrganizationalChartId = 1
                },
                new ApplicationUser
                {
                    Id = 2,
                    AccessFailedCount = 0,
                    ConcurrencyStamp = "08ff43bb-9777-467a-abe0-42fe895ff87f",
                    Email = "",
                    EmailConfirmed = false,
                    LockoutEnabled = true,
                    NormalizedEmail = "",
                    NormalizedUserName = "ACCOUNTANT",
                    PasswordHash =
                        "AQAAAAEAACcQAAAAEKAYdENoiu4ygK4V7xueW262vL5ta6fciSNfof79fxkil/A6+11ZVDe3yH0jdn1fWg==",
                    PhoneNumberConfirmed = false,
                    SecurityStamp = "DRM3NPZEDP7P3SYF6QU3RMMOOMHETGLD",
                    TwoFactorEnabled = false,
                    FirstName = "کاربر",
                    LastName = "حسابدار",
                    UserName = "accountant",
                    OrganizationalChartId = 1
                }

            );
        }
    }
}