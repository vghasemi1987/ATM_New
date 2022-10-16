using DomainEntities.WorkfollowAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.WorkFollow
{
    public class WorkfollowConfig : IEntityTypeConfiguration<Workfollow>
    {
        public void Configure(EntityTypeBuilder<Workfollow> builder)
        {
            builder.ToTable("Transaction_Workfollow");

            builder.HasOne(o => o.Status).WithMany().HasForeignKey(o => o.StatusId).OnDelete(DeleteBehavior.SetNull);

            //builder.HasOne(o => o.FileDetailId).WithMany().HasForeignKey(o => o.FileDetailId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}