using DomainEntities.TransactionFileDetailAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.TransactionFileDetailAggregate
{
    public class FileDetailConfig : IEntityTypeConfiguration<FileDetail>
    {
        public void Configure(EntityTypeBuilder<FileDetail> builder)
        {
            builder.ToTable("Transaction_FileDetails");

            builder.Property(o => o.Amount).HasColumnType("decimal");

            builder.Property(o => o.AtmCode).IsUnicode(false).HasMaxLength(8);
            builder.Property(o => o.CardNumber).IsUnicode(false).HasMaxLength(16);
            builder.Property(o => o.Date).IsUnicode(false).HasMaxLength(10);
            builder.Property(o => o.Operation).IsUnicode(false).HasMaxLength(6);
            builder.Property(o => o.Time).IsUnicode(false).HasMaxLength(6);
            builder.Property(o => o.TransactionNumber).IsUnicode(false).HasMaxLength(10); // orginal is 6

            builder.Property(o => o.UserDescription).IsUnicode().HasMaxLength(100);

            builder.HasOne(o => o.File).WithMany(o => o.FileDetails).HasForeignKey(o => o.FileId).OnDelete(DeleteBehavior.Cascade);
            //builder.HasMany(o => o.Workfollows).WithOne(o => o.FileDetail).HasForeignKey(o => o.FileDetailId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}