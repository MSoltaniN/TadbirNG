using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPPC.Tadbir.Model.Metadata;

namespace SPPC.Tadbir.Persistence.Seeding
{

    internal class SubSystemConfiguration : IEntityTypeConfiguration<Subsystem>
    {
        public void Configure(EntityTypeBuilder<Subsystem> builder)
        {
          
            builder.HasData(new Subsystem { Id = 1 ,  Name = "Administration"  });
          
            builder.HasData(new Subsystem { Id = 2,  Name = "Accounting"    });
          
            builder.HasData(new Subsystem { Id = 3 ,  Name = "Treasury"    });
          
            builder.HasData(new Subsystem { Id = 100000  ,   Name = "ProductScope"   });
            
        }
    }
}