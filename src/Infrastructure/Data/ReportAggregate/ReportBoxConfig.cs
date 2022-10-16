using DomainEntities.ReportAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.ReportAggregate
{
    public class ReportBoxConfig : IEntityTypeConfiguration<ReportBox>
    {
        public void Configure(EntityTypeBuilder<ReportBox> builder)
        {
            builder.ToTable("Report_Boxes");
            builder.HasKey(o => o.Id);
            builder.Property(o => o.AccessRight)
                .HasMaxLength(50);
            builder.Property(o => o.BoxStatus)
                .HasMaxLength(100);
            builder.Property(o => o.Icon)
                .HasMaxLength(50);
            builder.Property(o => o.Key)
                .HasMaxLength(50);
            builder.Property(o => o.Link)
                .HasMaxLength(150);
            builder.Property(o => o.SqlCommand)
                .HasMaxLength(500);
            builder.Property(o => o.Title)
                .HasMaxLength(50);
            builder.Property(o => o.Description)
                .HasMaxLength(100);
            builder.Ignore(b => b.Value);
        }
    }
}
