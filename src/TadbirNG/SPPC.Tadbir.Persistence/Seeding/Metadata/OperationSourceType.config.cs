using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPPC.Tadbir.Model.Metadata;

namespace SPPC.Tadbir.Persistence.Seeding
{

    internal class OperationSourceTypeConfiguration : IEntityTypeConfiguration<OperationSourceType>
    {
        public void Configure(EntityTypeBuilder<OperationSourceType> builder)
        {
          
            builder.HasData(new OperationSourceType { Id = 1 , Name = "BaseData",    });
          
            builder.HasData(new OperationSourceType { Id = 2 , Name = "OperationalForms",    });
          
            builder.HasData(new OperationSourceType { Id = 3 ,   Name = "Reports",   });
            
        }
    }
}