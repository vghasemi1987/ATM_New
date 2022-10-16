using DomainEntities.TransactionFileDetailAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.TransactionFileAggregate
{
    public class TypeConfig : IEntityTypeConfiguration<Type>
    {
        public void Configure(EntityTypeBuilder<Type> builder)
        {
            builder.ToTable("Transaction_Types");
            builder.HasKey(o => o.Id);
            builder.Property(current => current.Id)
                .ValueGeneratedNever();

            builder.Property(o => o.Title)
                .IsUnicode(false)
                .HasMaxLength(10);

            builder.Property(o => o.Extension)
                .IsUnicode(false)
                .HasMaxLength(5);

            builder.Property(o => o.Content)
                .IsUnicode(false)
                .HasMaxLength(50);

            builder.Property(o => o.Separation)
                .IsUnicode(false)
                .HasMaxLength(100);

            builder.HasData(
                new Type { Id = 1, Title = "grg", Extension = "log", Content = "RETRACTED FAIL", Separation = "\\n========================================" },
                new Type { Id = 2, Title = "grg", Extension = "log", Content = "RETRACT ACTION FINISHED", Separation = "\\n========================================" },
                new Type { Id = 3, Title = "hyo", Extension = "txt", Content = "START RETRACT", Separation = "OP." },
                new Type { Id = 4, Title = "wincor", Extension = "jrn", Content = "CASH RETRACT", Separation = "OP." });
        }
    }
}