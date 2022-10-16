using DomainEntities.ReportAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.ReportAggregate
{
    public class ChartConfig : IEntityTypeConfiguration<Chart>
    {
        public void Configure(EntityTypeBuilder<Chart> builder)
        {
            builder.ToTable("Report_Charts");
            builder.HasKey(o => o.Id);

            builder.Property(o => o.Title)
                .HasMaxLength(100);
        }
    }
}
