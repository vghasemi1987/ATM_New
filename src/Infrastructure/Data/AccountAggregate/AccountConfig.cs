using DomainEntities.AccountAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.AccountAggregate
{
    public class AccountConfig : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("Common_Accounts");
            builder.HasKey(ci => ci.Id);

            builder.Property(current => current.BranchId)
                .HasMaxLength(5);

            builder.Property(current => current.AccountNo)
                .HasMaxLength(20);
        }
    }
}
