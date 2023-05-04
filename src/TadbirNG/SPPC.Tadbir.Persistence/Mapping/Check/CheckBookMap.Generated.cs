// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.2.1484
//     Template Version: 1.0
//     Generation Date: 13/12/1401 04:45:52 ب.ظ
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPPC.Tadbir.Model.Check;

namespace SPPC.Tadbir.Persistence.Mapping
{
    internal static class CheckBookMap
    {
        internal static void BuildMapping(EntityTypeBuilder<CheckBook> builder)
        {
            builder.ToTable("CheckBook", "Check");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("CheckBookID");
            builder.Property(e => e.CheckBookNo);
            builder.Property(e => e.SeriesNo)
                .IsRequired()
                .HasMaxLength(32);
            builder.Property(e => e.SayyadStartNo)
                .IsRequired()
                .HasMaxLength(16);
            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(64);
            builder.Property(e => e.IssueDate)
                .IsRequired();
            builder.Property(e => e.StartNo)
                .IsRequired()
                .HasMaxLength(32);
            builder.Property(e => e.EndNo)
                .IsRequired()
                .HasMaxLength(32);
            builder.Property(e => e.BankName)
                .HasMaxLength(32);
            builder.Property(e => e.CreatedById)
                .IsRequired();
            builder.Property(e => e.ModifiedById)
                .IsRequired();
            builder.Property(e => e.CreatedDate)
                .IsRequired();
            builder.Property(e => e.IsArchived);
            builder.Property(e => e.FiscalPeriodId);
            builder.Property(e => e.RowGuid)
                .HasColumnName("rowguid")
                .HasDefaultValueSql("(newid())");
            builder.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");
            builder.Ignore(checkBook => checkBook.No);
            builder.Ignore(checkBook => checkBook.Reference);
            builder.Ignore(checkBook => checkBook.Date);

            builder.HasOne(e => e.Branch)
                .WithMany()
                .HasForeignKey(e => e.BranchId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Check_CheckBook_Corporate_Branch");
            builder.HasOne(e => e.Account)
                .WithMany()
                .HasForeignKey(e => e.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Check_CheckBook_Finance_Account");
            builder.HasOne(e => e.DetailAccount)
                .WithMany()
                .HasForeignKey(e => e.DetailAccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Check_CheckBook_Finance_DetailAccount");
            builder.HasOne(e => e.CostCenter)
                .WithMany()
                .HasForeignKey(e => e.CostCenterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Check_CheckBook_Finance_CostCenter");
            builder.HasOne(e => e.Project)
                .WithMany()
                .HasForeignKey(e => e.ProjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Check_CheckBook_Finance_Project");
        }
    }
}
