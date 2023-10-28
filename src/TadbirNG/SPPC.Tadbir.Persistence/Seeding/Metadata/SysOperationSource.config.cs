using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPPC.Tadbir.Model.Metadata;

namespace SPPC.Tadbir.Persistence.Seeding
{

    internal class SysOperationSourceConfiguration : IEntityTypeConfiguration<OperationSource>
    {
        public void Configure(EntityTypeBuilder<OperationSource> builder)
        {
          
            builder.HasData(new OperationSource {    Name = "AppLogin",    Id =  7 ,Description    = ""  });
          
            builder.HasData(new OperationSource {    Name = "AppEnvironment",    Id =  8, Description = "" });
          
            builder.HasData(new OperationSource {    Name = "SystemSettings",    Id =  14, Description = ""   });
            
        }
    }
}