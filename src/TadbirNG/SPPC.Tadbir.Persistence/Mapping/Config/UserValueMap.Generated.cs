// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.2.1508
//     Template Version: 1.0
//     Generation Date: 2023-04-29 7:41:54 PM
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPPC.Tadbir.Model.Config;

namespace SPPC.Tadbir.Persistence.Mapping
{
    internal static class UserValueMap
    {
        internal static void BuildMapping(EntityTypeBuilder<UserValue> builder)
        {
            builder.ToTable("UserValue", "Config");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("ValueID");
            builder.Property(e => e.Value)
                .IsRequired()
                .HasMaxLength(512);
            builder.Property(e => e.RowGuid)
                .HasColumnName("rowguid")
                .HasDefaultValueSql("(newid())");
            builder.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.HasOne(e => e.Category)
                .WithMany(e => e.Values)
                .HasForeignKey(e => e.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Config_UserValue_Config_Category");
        }
    }
}
