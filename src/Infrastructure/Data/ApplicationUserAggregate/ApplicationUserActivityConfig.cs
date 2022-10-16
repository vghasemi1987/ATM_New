using DomainEntities.ApplicationUserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.ApplicationUserAggregate
{
    public class ApplicationUserActivityConfig : IEntityTypeConfiguration<ApplicationUserActivity>
    {
        public void Configure(EntityTypeBuilder<ApplicationUserActivity> builder)
        {
            builder.ToTable("ApplicationUser_ActivityLogs");
            builder.HasKey(o => o.Id);

            builder.Property(o => o.ActionName)
                .HasMaxLength(150);

            builder.Property(o => o.ControllerName)
                .HasMaxLength(150);

            builder.Property(o => o.ActivityTitle)
                .HasMaxLength(200);
        }
    }
}