using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using SPPC.Tadbir.Model;
using SPPC.Tadbir.Model.ProductScope;
using Version = SPPC.Tadbir.Model.ProductScope.Version;

namespace SPPC.Tadbir.Persistence.Seeding
{
    internal class VersionConfiguration : IEntityTypeConfiguration<Version>
    {
        public void Configure(EntityTypeBuilder<Version> builder)
        {
            builder.HasData(new Version { Id = 1, Number = "2.2.0", ModifiedDate = DateTime.Parse("2022-08-27 13:56:52.150"), RowGuid = new Guid("26452115-8352-42fe-a7b8-4bd3d32f50f6") });
        }
    }
}
