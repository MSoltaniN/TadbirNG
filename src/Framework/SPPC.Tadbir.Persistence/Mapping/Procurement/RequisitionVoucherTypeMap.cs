using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPPC.Tadbir.Model.Procurement;

namespace SPPC.Tadbir.Persistence.Mapping
{
    internal sealed class RequisitionVoucherTypeMap
    {
        private RequisitionVoucherTypeMap()
        {
        }

        internal static void BuildMapping(EntityTypeBuilder<RequisitionVoucherType> builder)
        {
            builder.ToTable("RequisitionVoucherType", "Procurement");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("VoucherTypeID");
            builder.Property(e => e.Description).HasMaxLength(256);
            builder.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");
            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(64);
            builder.Property(e => e.RowGuid)
                .HasColumnName("rowguid")
                .HasDefaultValueSql("(newid())");
        }
    }
}
