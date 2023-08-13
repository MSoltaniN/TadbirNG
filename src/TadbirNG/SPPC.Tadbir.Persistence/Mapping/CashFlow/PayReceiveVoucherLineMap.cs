using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPPC.Tadbir.Model.CashFlow;

namespace SPPC.Tadbir.Persistence.Mapping
{
    internal class PayReceiveVoucherLineMap
    {
        internal static void BuildMapping(EntityTypeBuilder<PayReceiveVoucherLine> builder)
        {
            builder.ToTable("PayReceiveVoucherLine", "CashFlow");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("PayReceiveVoucherLineID");
            builder.HasAlternateKey(e => new { e.PayReceiveId, e.VoucherLineId });
            builder.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");
            builder.Property(e => e.RowGuid)
                .HasColumnType("rowguid")
                .HasDefaultValueSql("(newid())");

            builder.HasOne(e => e.PayReceive)
                .WithMany(p => p.PayReceiveVoucherLines)
                .HasForeignKey(e => e.PayReceiveId);
            builder.HasOne(e => e.VoucherLine)
                .WithOne()
                .HasForeignKey<PayReceiveVoucherLine>(e => e.VoucherLineId);    
        }
    }
}
