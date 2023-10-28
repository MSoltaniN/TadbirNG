using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPPC.Tadbir.Model.Metadata;

namespace SPPC.Tadbir.Persistence.Seeding
{

    internal class OperationSourceConfiguration : IEntityTypeConfiguration<OperationSource>
    {
        public void Configure(EntityTypeBuilder<OperationSource> builder)
        {
          
            builder.HasData(new OperationSource { Id = 1,  Name = "Journal",    Description = ""  });
            builder.HasData(new OperationSource { Id = 2,  Name = "AccountBook",    Description = "" });
            builder.HasData(new OperationSource { Id = 3,  Name = "CurrencyBook",    Description = "" });
            builder.HasData(new OperationSource { Id = 4,  Name = "TestBalance",    Description = "" });
            builder.HasData(new OperationSource { Id = 5,  Name = "ItemBalance",    Description = "" });
            builder.HasData(new OperationSource { Id = 6,  Name = "BalanceByAccount",    Description = "" });
            builder.HasData(new OperationSource { Id = 9,  Name = "EnvironmentParams",    Description = "" });
            builder.HasData(new OperationSource { Id = 10, Name = "ProfitLoss",    Description = "" });
            builder.HasData(new OperationSource { Id = 11, Name = "AccountRelations",    Description = "" });
            builder.HasData(new OperationSource { Id = 12, Name = "BalanceSheet",    Description = "" });
            builder.HasData(new OperationSource { Id = 13, Name = "SystemIssue",    Description = "" });
            builder.HasData(new OperationSource { Id = 15, Name = "CheckBookReport",    Description = "" });
            
        }
    }
}