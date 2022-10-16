using DomainEntities.SettingAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.SettingAggregate
{
    public class SettingConfig : IEntityTypeConfiguration<Setting>
    {
        public void Configure(EntityTypeBuilder<Setting> builder)
        {
            builder.ToTable("Settings");
            builder.HasKey(current => current.Id);
            builder.Property(current => current.Id)
                .ValueGeneratedNever();
            builder.Property(current => current.EmailUsername)
                .IsUnicode(false)
                .HasMaxLength(100);
            builder.Property(current => current.EmailPassword)
                .IsUnicode(false)
                .HasMaxLength(100);
            builder.Property(current => current.EmailSmtpServer)
                .IsUnicode(false)
                .HasMaxLength(100);
            builder.Property(current => current.SmsPassword)
                .IsUnicode()
                .HasMaxLength(100);
            builder.Property(current => current.SmsServiceNumber)
                .IsUnicode()
                .HasMaxLength(50);
            builder.Property(current => current.WebSiteTitle)
                .IsUnicode()
                .HasMaxLength(100);
            builder.HasData(new Setting { Id = 1, WebSiteTitle = "سامانه تست نفوذ" });
        }
    }
}