using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPPC.Tadbir.Model.Corporate;

namespace SPPC.Tadbir.Persistence.Seeding
{
    internal class BranchConfiguration : IEntityTypeConfiguration<Branch>
    {
        public void Configure(EntityTypeBuilder<Branch> builder)
        {
            builder.HasData(new Branch { Id = 1, ParentId = null , Name = "دفتر مرکزی", Description = "", Level = 0});
            
            builder.HasData(new Branch { Id = 2, ParentId = 1 , Name = "نمایشگاه شمال تهران", Description = "", Level = 1});
        }
    }
}
