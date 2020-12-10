using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPPC.Licensing.Model;

namespace SPPC.Licensing.Persistence.Mapping
{
    internal static class LicenseMap
    {
        internal static void BuildMapping(EntityTypeBuilder<LicenseModel> builder)
        {
            builder.ToTable("License");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("LicenseID");
            builder.Property(e => e.CustomerKey)
                .IsRequired()
                .HasMaxLength(36);
            builder.Property(e => e.LicenseKey)
                .IsRequired()
                .HasMaxLength(36);
            builder.Property(e => e.ClientKey)
                .HasMaxLength(256);
            builder.Property(e => e.HardwareKey)
                .HasMaxLength(512);
            builder.Property(e => e.Secret)
                .HasMaxLength(32);
            builder.Property(e => e.UserCount)
                .IsRequired();
            builder.Property(e => e.Edition)
                .IsRequired()
                .HasMaxLength(32);
            builder.Property(e => e.StartDate)
                .IsRequired();
            builder.Property(e => e.EndDate)
                .IsRequired();
            builder.Property(e => e.ActiveModules)
                .IsRequired();
            builder.Property(e => e.IsActivated)
                .IsRequired();
            builder.Property(e => e.RowGuid);
            builder.Property(e => e.ModifiedDate);

            builder.HasOne(e => e.Customer)
                .WithMany(p => p.Licenses)
                .HasForeignKey(e => e.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_License_Customer");
        }
    }
}
