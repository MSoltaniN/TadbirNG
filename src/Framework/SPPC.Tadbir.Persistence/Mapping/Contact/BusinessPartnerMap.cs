using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPPC.Tadbir.Model.Contact;

namespace SPPC.Tadbir.Persistence.Mapping
{
    internal sealed class BusinessPartnerMap
    {
        private BusinessPartnerMap()
        {
        }

        internal static void BuildMapping(EntityTypeBuilder<BusinessPartner> builder)
        {
            builder.ToTable("BusinessPartner", "Contact");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("PartnerID");
            builder.Property(e => e.Address)
                .HasMaxLength(256);
            builder.Property(e => e.CommerceCode)
                .HasMaxLength(64);
            builder.Property(e => e.Email)
                .HasMaxLength(64);
            builder.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");
            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(128);
            builder.Property(e => e.Phone)
                .HasMaxLength(64);
            builder.Property(e => e.RowGuid)
                .HasColumnName("rowguid")
                .HasDefaultValueSql("(newid())");
        }
    }
}
