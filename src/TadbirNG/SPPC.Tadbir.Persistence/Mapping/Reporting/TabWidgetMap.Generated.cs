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
    internal static class TabWidgetMap
    {
        internal static void BuildMapping(EntityTypeBuilder<TabWidget> builder)
        {
            builder.ToTable("TabWidget", "Reporting");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("TabWidgetID");
            builder.Property(e => e.Settings)
                .IsRequired()
                .HasMaxLength(1024);
            builder.Property(e => e.DefaultSettings)
                .IsRequired()
                .HasMaxLength(1024);
            builder.Property(e => e.RowGuid)
                .HasColumnName("rowguid")
                .HasDefaultValueSql("(newid())");
            builder.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.HasOne(e => e.Tab)
                .WithMany(e => e.Widgets)
                .HasForeignKey(e => e.TabId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reporting_TabWidget_Reporting_DashboardTab");
            builder.HasOne(e => e.Widget)
                .WithMany()
                .HasForeignKey(e => e.WidgetId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Reporting_TabWidget_Reporting_Widget");
        }
    }
}
