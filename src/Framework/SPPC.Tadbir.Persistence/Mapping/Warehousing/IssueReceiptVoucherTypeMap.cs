using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPPC.Tadbir.Model.Warehousing;

namespace SPPC.Tadbir.Persistence.Mapping
{
    internal sealed class IssueReceiptVoucherTypeMap
    {
        private IssueReceiptVoucherTypeMap()
        {
        }

        internal static void BuildMapping(EntityTypeBuilder<IssueReceiptVoucherType> builder)
        {
            builder.ToTable("IssueReceiptVoucherType", "Warehousing");
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
