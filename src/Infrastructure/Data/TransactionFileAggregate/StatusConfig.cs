using DomainEntities.TransactionFileAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.TransactionFileAggregate
{
    public class StatusConfig : IEntityTypeConfiguration<Status>
    {
        public void Configure(EntityTypeBuilder<Status> builder)
        {
            builder.ToTable("Transaction_Status");

            builder.HasKey(o => o.Id);
            builder.Property(current => current.Id)
                .ValueGeneratedNever();

            builder.Property(o => o.Title)
                .IsUnicode()
                .HasMaxLength(50);

            builder.HasData(
                new Status { Id = EnumStatus.OperatorProcessing, Title = "ثبت و پردازش شده" },
                new Status { Id = EnumStatus.SendToBranchBoss, Title = "ارسال به رئیس شعبه" },
                new Status { Id = EnumStatus.SendToAccounting, Title = "ارسال به حسابداری" },
                new Status { Id = EnumStatus.FinalRegistration, Title = "ثبت نهایی" },
                new Status { Id = EnumStatus.BackToOperator, Title = "بازگشت به متصدی" },
                new Status { Id = EnumStatus.BackToBranchBoss, Title = "بازگشت به رئیس شعبه" }
                );
        }
    }
}