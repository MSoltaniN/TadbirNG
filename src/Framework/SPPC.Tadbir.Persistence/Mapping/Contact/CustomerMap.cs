using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPPC.Tadbir.Model.Contact;

namespace SPPC.Tadbir.Persistence.Mapping
{
    internal sealed class CustomerMap
    {
        private CustomerMap()
        {
        }

        internal static void BuildMapping(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customer", "Contact");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("CustomerID");
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
