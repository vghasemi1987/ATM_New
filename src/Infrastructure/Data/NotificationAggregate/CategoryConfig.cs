using DomainEntities.NotificationAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.NotificationAggregate
{
    public class CategoryConfig : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Notification_Categories");
            builder.HasKey(o => o.Id);
            builder.Property(current => current.Id)
                .ValueGeneratedNever();

            builder.Property(o => o.Title)
                .HasMaxLength(100);
            
            builder.HasData(new Category {Id = 1, Title = "وظیفه ارسالی"},
                new Category {Id = 2, Title = "تغییر وضعیت وظیفه"},
                new Category {Id = 3, Title = "موعد انجام وظیفه"},
                new Category { Id = 4, Title = "موعد انجام تست" });
        }
    }
}