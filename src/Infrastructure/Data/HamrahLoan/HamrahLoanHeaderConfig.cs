using DomainEntities.HamrahLoan;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.HamrahLoan
{
    public class HamrahLoanHeaderConfig : IEntityTypeConfiguration<HamrahLoanHeader>
    {
        public void Configure(EntityTypeBuilder<HamrahLoanHeader> builder)
        {
            builder.ToTable("HamrahLoanHeader");
            builder.HasKey(o => o.Id);

            builder.Property(o => o.Title).IsUnicode().HasMaxLength(50);

            builder.Property(o => o.CreateDate).HasColumnType("date");
            builder.Property(o => o.StatusDate).HasColumnType("date");
        }
    }
}
