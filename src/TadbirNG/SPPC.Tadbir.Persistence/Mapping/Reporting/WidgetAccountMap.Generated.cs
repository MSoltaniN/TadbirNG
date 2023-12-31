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
    internal static class WidgetAccountMap
    {
        internal static void BuildMapping(EntityTypeBuilder<WidgetAccount> builder)
        {
            builder.ToTable("WidgetAccount", "Reporting");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("WidgetAccountID");
            builder.Property(e => e.RowGuid)
                .HasColumnName("rowguid")
                .HasDefaultValueSql("(newid())");
            builder.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.HasOne(e => e.Widget)
                .WithMany(e => e.Accounts)
                .HasForeignKey(e => e.WidgetId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Reporting_WidgetAccount_Reporting_Widget");
            builder.HasOne(e => e.Account)
                .WithMany()
                .HasForeignKey(e => e.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reporting_WidgetAccount_Finance_Account");
            builder.HasOne(e => e.DetailAccount)
                .WithMany()
                .HasForeignKey(e => e.DetailAccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reporting_WidgetAccount_Finance_DetailAccount");
            builder.HasOne(e => e.CostCenter)
                .WithMany()
                .HasForeignKey(e => e.CostCenterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reporting_WidgetAccount_Finance_CostCenter");
            builder.HasOne(e => e.Project)
                .WithMany()
                .HasForeignKey(e => e.ProjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reporting_WidgetAccount_Finance_Project");
        }
    }
}
