﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPPC.Tadbir.Model.Auth;

namespace SPPC.Tadbir.Persistence.Mapping
{
    internal sealed class PermissionMap
    {
        private PermissionMap()
        {
        }

        internal static void BuildMapping(EntityTypeBuilder<Permission> builder)
        {
            builder.ToTable("Permission", "Auth");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("PermissionID");
            builder.Property(e => e.Description)
                .HasMaxLength(512);
            builder.Property(e => e.Flag)
                .HasDefaultValueSql("((0))");
            builder.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");
            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(128);
            builder.Property(e => e.RowGuid)
                .HasColumnName("rowguid")
                .HasDefaultValueSql("(newid())");

            builder.HasOne(d => d.Group)
                .WithMany(p => p.Permissions)
                .HasForeignKey("GroupID")
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Auth_Permission_Auth_PermissionGroup");
        }
    }
}
