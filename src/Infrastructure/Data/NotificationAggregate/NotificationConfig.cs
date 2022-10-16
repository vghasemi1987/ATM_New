using DomainEntities.NotificationAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.NotificationAggregate
{
    public class NotificationConfig : IEntityTypeConfiguration<NotificationItem>
    {
        public void Configure(EntityTypeBuilder<NotificationItem> builder)
        {
            builder.ToTable("NotificationItems");
            builder.HasKey(o => o.Id);

            builder.Property(o => o.Text)
                .HasMaxLength(300);

            builder.HasOne(o => o.CreatedByUser)
                .WithMany()
                .HasForeignKey(o => o.CreatedByUserId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(o => o.ForUser)
                .WithMany()
                .HasForeignKey(o => o.ForUserId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            //builder.HasOne(o => o.Category)
            //    .WithMany()
            //    .HasForeignKey(o => o.CategoryId);

            //builder.Property(p => p.CategoryId)
            //    .HasConversion(new EnumToNumberConverter<CategoryEnum, int>())
            //    .HasColumnType("int")
            //    .IsRequired();

            //builder.HasOne(o => o.Category)
            //    .WithMany()
            //    .HasForeignKey(o => (int)o.CategoryId);
        }
    }
}