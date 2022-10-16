using DomainEntities.HamrahLoan;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.HamrahLoan
{
    public class HamrahLoanDetailConfig : IEntityTypeConfiguration<HamrahLoanDetail>
    {
        public void Configure(EntityTypeBuilder<HamrahLoanDetail> builder)
        {
            builder.ToTable("HamrahLoanDetail");
            builder.HasKey(o => o.Id);

            builder.Property(o => o.LoanNumber).IsUnicode().HasMaxLength(30);

            builder.Property(o => o.CreateDate).HasColumnType("date");
            builder.Property(o => o.StatusDate).HasColumnType("date");
            builder.Property(o => o.LoanDate).HasColumnType("date");
        }
    }
}