using DomainEntities.ApplicationUserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.ApplicationUserAggregate
{
    public class ApplicationUserRoleConfig : IEntityTypeConfiguration<ApplicationUserRole>
    {
        public void Configure(EntityTypeBuilder<ApplicationUserRole> builder)
        {
            builder.ToTable("ApplicationUser_UserRoles");

            builder.HasKey(ur => new { ur.UserId, ur.RoleId });

            builder.HasOne(ur => ur.ApplicationRole)
                .WithMany(r => r.ApplicationUserRoles)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();

            builder.HasOne(ur => ur.ApplicationUser)
                .WithMany(r => r.ApplicationUserRoles)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();

            builder.HasData(new ApplicationUserRole { UserId = 1, RoleId = 1 }, new ApplicationUserRole { UserId = 2, RoleId = 2 });
        }
    }
}