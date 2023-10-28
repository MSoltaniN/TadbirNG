using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPPC.Tadbir.Model.Metadata;

namespace SPPC.Tadbir.Persistence.Seeding
{

    internal class SysSubSystemConfiguration : IEntityTypeConfiguration<Subsystem>
    {
        public void Configure(EntityTypeBuilder<Subsystem> builder)
        {
          
            builder.HasData(new Subsystem {    Name = "Administration",    Id =  1    });
          
            builder.HasData(new Subsystem {    Name = "Accounting",    Id =  2  });
          
            builder.HasData(new Subsystem {    Name = "Treasury",    Id =  3    });
          
            builder.HasData(new Subsystem {    Name = "ProductScope",    Id =  100000    });
            
        }
    }
}