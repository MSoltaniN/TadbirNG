using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPPC.Tadbir.Model.Core;

namespace SPPC.Tadbir.Persistence.Mapping
{
    internal sealed class ServiceJobMap
    {
        private ServiceJobMap()
        {
        }

        internal static void BuildMapping(EntityTypeBuilder<ServiceJob> builder)
        {
            builder.ToTable("ServiceJob", "Core");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("JobID");
            builder.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");
            builder.Property(e => e.RowGuid)
                .HasColumnName("rowguid")
                .HasDefaultValueSql("(newid())");
        }
    }
}
