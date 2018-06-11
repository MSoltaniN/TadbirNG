using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPPC.Tadbir.Model.Metadata;

namespace SPPC.Tadbir.Persistence.Mapping
{
    internal static class CommandMap
    {
        internal static void BuildMapping(EntityTypeBuilder<Command> builder)
        {
            builder.ToTable("Command", "Metadata");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("CommandID");
            builder.Property(e => e.TitleKey)
                .IsRequired()
                .HasMaxLength(64);
            builder.Property(e => e.RouteUrl)
                .HasMaxLength(256);
            builder.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");
            builder.Property(e => e.RowGuid)
                .HasColumnName("rowguid")
                .HasDefaultValueSql("(newid())");

            builder.HasOne(d => d.Parent)
                .WithMany(p => p.Children)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasForeignKey(d => d.ParentId)
                .HasConstraintName("FK_Metadata_Command_Metadata_Parent");
            builder.HasOne(d => d.Permission)
                .WithMany()
                .HasForeignKey(d => d.PermissionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Metadata_Command_Auth_Permission");
        }
    }
}
