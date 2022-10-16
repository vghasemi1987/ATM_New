using DomainEntities.ApplicationUserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.ApplicationUserAggregate
{
    public class OrganizationalChartConfig : IEntityTypeConfiguration<OrganizationalChart>
    {
        public void Configure(EntityTypeBuilder<OrganizationalChart> builder)
        {
            builder.ToTable("ApplicationUser_OrganizationalCharts");
            builder.HasKey(o => o.Id);
            builder.Property(current => current.Id)
                .ValueGeneratedNever();

            builder.Property(o => o.Title)
                .HasMaxLength(100);

            builder.HasData(new OrganizationalChart { Id = 1, Title = "رئیس" }, 
                new OrganizationalChart { Id = 2, Title = "معاون" },
                new OrganizationalChart { Id = 3, Title = "کارشناس مسئول" },
                new OrganizationalChart { Id = 4, Title = "کارشناس" });
        }
    }
}