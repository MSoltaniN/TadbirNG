using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPPC.Tadbir.Model.Metadata;

namespace SPPC.Tadbir.Persistence.Seeding
{

    internal class SysEntityTypeConfiguration : IEntityTypeConfiguration<EntityType>
    {
        public void Configure(EntityTypeBuilder<EntityType> builder)
        {
          
            builder.HasData(new EntityType {    Name = "CompanyDb",    Id =  1 ,Description    = ""   });
          
            builder.HasData(new EntityType {    Name = "Role",    Id =  2, Description = "" });
          
            builder.HasData(new EntityType {    Name = "Setting",    Id =  4, Description = ""  });
            
            builder.HasData(new EntityType {    Name = "SysOperationLog",    Id =  5, Description = ""  });
            
            builder.HasData(new EntityType {    Name = "User",    Id =  6, Description = "" });
            
            builder.HasData(new EntityType {    Name = "ViewRowPermission",    Id =  8, Description = ""  });
            
            builder.HasData(new EntityType {    Name = "UserReport",    Id =  9, Description = ""   });
            
        }
    }
}