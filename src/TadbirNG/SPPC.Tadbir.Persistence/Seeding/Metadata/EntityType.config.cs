using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPPC.Tadbir.Model.Metadata;

namespace SPPC.Tadbir.Persistence.Seeding
{

    internal class EntityTypeConfiguration : IEntityTypeConfiguration<EntityType>
    {
        public void Configure(EntityTypeBuilder<EntityType> builder)
        {
          
            builder.HasData(new EntityType { Id = 1,      Name = "Account",   Description    = ""   });
            builder.HasData(new EntityType { Id = 2,      Name = "AccountCollectionAccount",    Description = "" });
            builder.HasData(new EntityType { Id = 4,      Name = "AccountGroup",    Description = ""  });
            builder.HasData(new EntityType { Id = 5,      Name = "Branch",    Description = ""  });
            builder.HasData(new EntityType { Id = 6,      Name = "CostCenter",     Description = "" });
            builder.HasData(new EntityType { Id = 7,      Name = "Currency",   Description = ""  });
            builder.HasData(new EntityType { Id = 9,      Name = "DetailAccount",   Description = ""   });
            builder.HasData(new EntityType { Id = 10,     Name = "FiscalPeriod",   Description = ""   });
            builder.HasData(new EntityType { Id = 11,     Name = "OperationLog",   Description = ""   });
            builder.HasData(new EntityType { Id = 12,     Name = "Project",   Description = ""   });
            builder.HasData(new EntityType { Id = 15,     Name = "Setting",   Description = ""   });
            builder.HasData(new EntityType { Id = 17,     Name = "Voucher",   Description = ""   });
            builder.HasData(new EntityType { Id = 18,     Name = "DraftVoucher",   Description = ""   });
            builder.HasData(new EntityType { Id = 19,     Name = "DashboardTab",   Description = ""   });
            builder.HasData(new EntityType { Id = 20,     Name = "Widget",   Description = ""   });
            builder.HasData(new EntityType { Id = 21,     Name = "CheckBook",   Description = ""   });
            builder.HasData(new EntityType { Id = 22,     Name = "CashRegister",   Description = ""   });
            builder.HasData(new EntityType { Id = 23,     Name = "SourceApp",   Description = ""   });
            builder.HasData(new EntityType { Id = 24,     Name = "Receipt",   Description = ""   });
            builder.HasData(new EntityType { Id = 25,     Name = "Payment",   Description = ""   });
            builder.HasData(new EntityType { Id = 100001, Name = "Brand",   Description = ""   });
            builder.HasData(new EntityType { Id = 100002, Name = "Unit",   Description = ""   });
            builder.HasData(new EntityType { Id = 100003, Name = "Property",   Description = ""   });
            builder.HasData(new EntityType { Id = 100004, Name = "Attribute",   Description = ""   });

        }
    }
}