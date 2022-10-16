using DomainEntities.TransactionFileAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.TransactionFileAggregate
{
    public class TransactionFileConfig : IEntityTypeConfiguration<File>
    {
        public void Configure(EntityTypeBuilder<File> builder)
        {
            builder.ToTable("Transaction_Files");

            builder.Property(o => o.Name).IsUnicode().HasMaxLength(150);
        }
    }
}