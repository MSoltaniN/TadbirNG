using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPPC.Tadbir.Model.Corporate;

namespace SPPC.Tadbir.Persistence.Mapping
{
    internal sealed class CompanyMap
    {
        private CompanyMap()
        {
        }

        internal static void BuildMapping(EntityTypeBuilder<Company> builder)
        {
            builder.ToTable("Company", "Corporate");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("CompanyID");
            builder.Property(e => e.Description)
                .HasMaxLength(512);
            builder.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");
            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(128);
            builder.Property(e => e.RowGuid)
                .HasColumnName("rowguid")
                .HasDefaultValueSql("(newid())");
        }
    }
}
