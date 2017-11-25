using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPPC.Tadbir.Model.Finance;

namespace SPPC.Tadbir.Persistence.Mapping
{
    internal sealed class FullDetailMap
    {
        private FullDetailMap()
        {
        }

        internal static void BuildMapping(EntityTypeBuilder<FullDetail> builder)
        {
            builder.ToTable("FullDetail", "Finance");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("FullDetailID");
            builder.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");
            builder.Property(e => e.RowGuid)
                .HasColumnName("rowguid")
                .HasDefaultValueSql("(newid())");

            builder.HasOne(d => d.Detail2)
                .WithMany()
                .HasForeignKey("Detail2ID")
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Finance_FullDetail_Finance_Detail2");
            builder.HasOne(d => d.Detail3)
                .WithMany()
                .HasForeignKey("Detail3ID")
                .HasConstraintName("FK_Finance_FullDetail_Finance_Detail3");
            builder.HasOne(d => d.Detail4)
                .WithMany()
                .HasForeignKey("Detail4ID")
                .HasConstraintName("FK_Finance_FullDetail_Finance_Detail4");
            builder.HasOne(d => d.Detail5)
                .WithMany()
                .HasForeignKey("Detail5ID")
                .HasConstraintName("FK_Finance_FullDetail_Finance_Detail5");
            builder.HasOne(d => d.Detail6)
                .WithMany()
                .HasForeignKey("Detail6ID")
                .HasConstraintName("FK_Finance_FullDetail_Finance_Detail6");
            builder.HasOne(d => d.Detail7)
                .WithMany()
                .HasForeignKey("Detail7ID")
                .HasConstraintName("FK_Finance_FullDetail_Finance_Detail7");
            builder.HasOne(d => d.Detail8)
                .WithMany()
                .HasForeignKey("Detail8ID")
                .HasConstraintName("FK_Finance_FullDetail_Finance_Detail8");
            builder.HasOne(d => d.Detail9)
                .WithMany()
                .HasForeignKey("Detail9ID")
                .HasConstraintName("FK_Finance_FullDetail_Finance_Detail9");
            builder.HasOne(d => d.Type)
                .WithOne(p => p.FullDetail)
                .HasForeignKey("TypeID")
                .HasConstraintName("FK_Finance_FullDetail_Finance_FullDetailType");
        }
    }
}
