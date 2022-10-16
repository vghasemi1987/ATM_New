using DomainEntities.SettingAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.SettingAggregate
{
    public class PriorityConfig : IEntityTypeConfiguration<Priority>
    {
        public void Configure(EntityTypeBuilder<Priority> builder)
        {
            builder.ToTable("Common_Priorities");
            builder.HasKey(o => o.Id);
            builder.Property(current => current.Id)
                .ValueGeneratedNever();

            builder.Property(o => o.Title)
                .IsUnicode()
                .HasMaxLength(20);

            builder.HasData(new Priority { Id = 1, Title = "ضروری" },
                new Priority { Id = 2, Title = "معمولی" },
                new Priority { Id = 3, Title = "پایین" });
        }
    }
}