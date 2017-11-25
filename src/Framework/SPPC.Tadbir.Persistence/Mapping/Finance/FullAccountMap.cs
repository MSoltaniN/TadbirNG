using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPPC.Tadbir.Model.Finance;

namespace SPPC.Tadbir.Persistence.Mapping
{
    internal sealed class FullAccountMap
    {
        private FullAccountMap()
        {
        }

        internal static void BuildMapping(EntityTypeBuilder<FullAccount> builder)
        {
            builder.ToTable("FullAccount", "Finance");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("FullAccountID");
            builder.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");
            builder.Property(e => e.RowGuid)
                .HasColumnName("rowguid")
                .HasDefaultValueSql("(newid())");

            builder.HasOne(d => d.Account)
                .WithMany()
                .HasForeignKey("AccountID")
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Finance_FullAccount_Finance_Account");

            builder.HasOne(d => d.CostCenter)
                .WithMany()
                .HasForeignKey("CostCenterID")
                .HasConstraintName("FK_Finance_FullAccount_Finance_CostCenter");

            builder.HasOne(d => d.Detail)
                .WithMany()
                .HasForeignKey("DetailID")
                .HasConstraintName("FK_Finance_FullAccount_Finance_Detail");

            builder.HasOne(d => d.Project)
                .WithMany()
                .HasForeignKey("ProjectID")
                .HasConstraintName("FK_Finance_FullAccount_Finance_Project");
        }
    }
}
