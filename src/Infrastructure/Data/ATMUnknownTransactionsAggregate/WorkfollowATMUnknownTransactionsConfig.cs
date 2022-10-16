using DomainEntities.ATMUnknownTransactionsAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.ATMUnknownTransactionsAggregate
{
    public class WorkfollowATMUnknownTransactionsConfig : IEntityTypeConfiguration<WorkfollowATMUnknownTransactions>
    {
        public void Configure(EntityTypeBuilder<WorkfollowATMUnknownTransactions> builder)
        {
            builder.ToTable("ATMUnknownTransactions_Workfollow");

            //builder.HasOne(o => o.Status)
            //    .WithMany()
            //    .HasForeignKey(o => o.StatusId)
            //    .OnDelete(DeleteBehavior.SetNull);
            builder.Property(current => current.Status).HasMaxLength(50).IsUnicode();
            builder.Property(current => current.UserMessage).HasMaxLength(200).IsUnicode();
        }
    }
}
