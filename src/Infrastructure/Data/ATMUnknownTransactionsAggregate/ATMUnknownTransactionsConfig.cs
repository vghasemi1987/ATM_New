using DomainEntities.ATMUnknownTransactionsAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.ATMUnknownTransactionsAggregate
{
    public class ATMUnknownTransactionsConfig : IEntityTypeConfiguration<ATMUnknownTransactions>
    {
        public void Configure(EntityTypeBuilder<ATMUnknownTransactions> builder)
        {
            builder.ToTable("ATMUnknownTransactions");
            builder.Property(current => current.TransactionTime).HasMaxLength(10).IsUnicode();
            builder.Property(current => current.TransactionNumber).HasMaxLength(20).IsUnicode();
            builder.Property(current => current.CardNumber).HasMaxLength(25).IsUnicode();
            builder.Property(current => current.ATM).HasMaxLength(200).IsUnicode();
            builder.Property(current => current.Branch).HasMaxLength(200).IsUnicode();
            builder.Property(current => current.BranchCode).HasMaxLength(20).IsUnicode();
            builder.Property(current => current.TransactionDateShamsi).HasMaxLength(10).IsUnicode();
            builder.Property(current => current.ATMAtletCode).HasMaxLength(20).IsUnicode();
            builder.Property(current => current.TransactionDateShamsi2).HasMaxLength(10).IsUnicode();
            builder.Property(current => current.CardType).HasMaxLength(50).IsUnicode();
            builder.Property(current => current.Status).HasMaxLength(50).IsUnicode();
            builder.Property(current => current.MaskCardNumber).HasMaxLength(30).IsUnicode();
            builder.Property(current => current.UserDescription).HasMaxLength(50).IsUnicode();
            builder.HasMany(o => o.Workfollows)
                .WithOne(o => o.ATMUnknownTransactions)
                .HasForeignKey(o => o.ATMUnknownTransactionsId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
