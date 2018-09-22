// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.398
//     Template Version: 1.0
//     Generation Date: 2018-09-18 5:10:19 PM
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPPC.Tadbir.Model.Metadata;

namespace SPPC.Tadbir.Persistence.Mapping
{
    internal static class ViewMap
    {
        internal static void BuildMapping(EntityTypeBuilder<View> builder)
        {
            builder.ToTable("View", "Metadata");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("ViewID");
            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(64);
            builder.Property(e => e.IsHierarchy)
                .IsRequired();
            builder.Property(e => e.IsCartableIntegrated)
                .IsRequired();
            builder.Property(e => e.FetchUrl)
                .IsRequired()
                .HasMaxLength(512);
            builder.Property(e => e.RowGuid)
                .HasColumnName("rowguid")
                .HasDefaultValueSql("(newid())");
            builder.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");
        }
    }
}
