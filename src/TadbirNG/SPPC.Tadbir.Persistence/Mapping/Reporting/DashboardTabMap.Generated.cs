// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.2.1425
//     Template Version: 1.0
//     Generation Date: 2022-08-31 11:04:12 PM
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPPC.Tadbir.Model.Reporting;

namespace SPPC.Tadbir.Persistence.Mapping
{
    internal static class DashboardTabMap
    {
        internal static void BuildMapping(EntityTypeBuilder<DashboardTab> builder)
        {
            builder.ToTable("DashboardTab", "Reporting");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("DashboardTabID");
            builder.Property(e => e.Index)
                .IsRequired();
            builder.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(128);
            builder.Property(e => e.RowGuid)
                .HasColumnName("rowguid")
                .HasDefaultValueSql("(newid())");
            builder.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.HasOne(e => e.Dashboard)
                .WithMany(e => e.Tabs)
                .HasForeignKey(e => e.DashboardId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Reporting_DashboardTab_Reporting_Dashboard");
        }
    }
}
