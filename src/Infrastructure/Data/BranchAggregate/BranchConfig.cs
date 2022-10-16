using DomainEntities.BranchAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.BranchAggregate
{
    public class BranchConfig: IEntityTypeConfiguration<Branch>
    {
        public void Configure(EntityTypeBuilder<Branch> builder)
        {
            builder.ToTable("Common_Branches");
            builder.HasKey(ci => ci.Id);

            //builder.Property(current => current.Id)
            //    .ValueGeneratedNever();

            builder.Property(current => current.BranchHeadId)
                .IsRequired(false);

            builder.Property(current => current.Title)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode();

            builder.HasOne(x => x.BranchHead)
                .WithMany(x => x.SubBranches)
                .HasForeignKey(x => x.BranchHeadId);

            builder.HasData(
                new Branch {Id = 1,Code=1, Title = "اداره منطقه یک تهران"},
                new Branch {Id = 2,Code=2, Title = "اداره منطقه دو تهران"}

            );
        }
    }
}
