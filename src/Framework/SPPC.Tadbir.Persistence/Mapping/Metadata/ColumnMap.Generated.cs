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
    internal static class ColumnMap
    {
        internal static void BuildMapping(EntityTypeBuilder<Column> builder)
        {
            builder.ToTable("Column", "Metadata");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("ColumnID");
            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(64);
            builder.Property(e => e.GroupName) 
                .HasMaxLength(64);
            builder.Property(e => e.Type)
                .HasMaxLength(32);
            builder.Property(e => e.DotNetType)
                .IsRequired()
                .HasMaxLength(64);
            builder.Property(e => e.StorageType)
                .IsRequired()
                .HasMaxLength(32);
            builder.Property(e => e.ScriptType)
                .IsRequired()
                .HasMaxLength(32);
            builder.Property(e => e.Length)
                .IsRequired();
            builder.Property(e => e.MinLength)
                .IsRequired();
            builder.Property(e => e.IsFixedLength)
                .IsRequired();
            builder.Property(e => e.IsNullable)
                .IsRequired();
            builder.Property(e => e.AllowSorting)
                .IsRequired();
            builder.Property(e => e.AllowFiltering)
                .IsRequired();
            builder.Property(e => e.Visibility)
                .HasMaxLength(32);
            builder.Property(e => e.DisplayIndex)
                .IsRequired();
            builder.Property(e => e.Expression)
                .HasMaxLength(64);
            builder.Property(e => e.RowGuid)
                .HasColumnName("rowguid")
                .HasDefaultValueSql("(newid())");
            builder.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.HasOne(e => e.View)
                .WithMany(p => p.Columns)
                .HasForeignKey("ViewID")
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Metadata_Column_Metadata_View");
        }
    }
}
