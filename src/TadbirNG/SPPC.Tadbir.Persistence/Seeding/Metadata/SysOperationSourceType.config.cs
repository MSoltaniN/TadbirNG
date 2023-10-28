using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPPC.Tadbir.Model.Metadata;

namespace SPPC.Tadbir.Persistence.Seeding
{

    internal class SysOperationSourceTypeConfiguration : IEntityTypeConfiguration<OperationSourceType>
    {
        public void Configure(EntityTypeBuilder<OperationSourceType> builder)
        {
          
            builder.HasData(new OperationSourceType {    Name = "BaseData",    Id =  1    });
          
            builder.HasData(new OperationSourceType {    Name = "OperationalForms",    Id =  2   });
          
            builder.HasData(new OperationSourceType {    Name = "Reports",    Id =  3   });
            
        }
    }
}