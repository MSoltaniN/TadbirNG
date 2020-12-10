using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPPC.Licensing.Model;

namespace SPPC.Licensing.Persistence.Mapping
{
    internal static class CustomerMap
    {
        internal static void BuildMapping(EntityTypeBuilder<CustomerModel> builder)
        {
            builder.ToTable("License");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("CustomerID");
            builder.Property(e => e.CustomerKey)
                .HasMaxLength(36);
            builder.Property(e => e.CompanyName)
                .IsRequired()
                .HasMaxLength(64);
            builder.Property(e => e.Industry)
                .IsRequired()
                .HasMaxLength(64);
            builder.Property(e => e.EmployeeCount)
                .IsRequired()
                .HasMaxLength(64);
            builder.Property(e => e.HeadquartersAddress)
                .IsRequired()
                .HasMaxLength(512);
            builder.Property(e => e.ContactFirstName)
                .IsRequired()
                .HasMaxLength(64);
            builder.Property(e => e.ContactLastName)
                .IsRequired()
                .HasMaxLength(64);
            builder.Property(e => e.WorkPhone)
                .HasMaxLength(16);
            builder.Property(e => e.WorkFax)
                .IsRequired()
                .HasMaxLength(16);
            builder.Property(e => e.CellPhone)
                .IsRequired()
                .HasMaxLength(16);
        }
    }
}
