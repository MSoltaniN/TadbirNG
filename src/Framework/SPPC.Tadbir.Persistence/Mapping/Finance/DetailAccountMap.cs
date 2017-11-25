﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPPC.Tadbir.Model.Finance;

namespace SPPC.Tadbir.Persistence.Mapping
{
    internal sealed class DetailAccountMap
    {
        private DetailAccountMap()
        {
        }

        internal static void BuildMapping(EntityTypeBuilder<DetailAccount> builder)
        {
            builder.ToTable("DetailAccount", "Finance");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("DetailID");
            builder.Property(e => e.Code)
                .IsRequired()
                .HasMaxLength(16);
            builder.Property(e => e.FullCode)
                .IsRequired()
                .HasMaxLength(256);
            builder.Property(e => e.Level)
                .HasDefaultValueSql("((0))");
            builder.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");
            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(256);
            builder.Property(e => e.RowGuid)
                .HasColumnName("rowguid")
                .HasDefaultValueSql("(newid())");
            builder.HasOne(d => d.Parent)
                .WithMany(p => p.Children)
                .HasForeignKey("ParentID")
                .HasConstraintName("FK_Finance_DetailAccount_Finance_Parent");
        }
    }
}
