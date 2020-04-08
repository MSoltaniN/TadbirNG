using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPPC.Tadbir.Model.Finance;

namespace SPPC.Tadbir.Persistence.Mapping.Finance
{
    public static class AccountOwnerMap
    {
        internal static void BuildMapping(EntityTypeBuilder<AccountOwner> builder)
        {
            builder.ToTable("AccountOwner", "Finance");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("AccountOwnerID");
            builder.Property(e => e.AccountID)
                .IsRequired();
            builder.Property(e => e.BankName)
                .IsRequired()
                .HasMaxLength(64);
            builder.Property(e => e.AccountType)
                .IsRequired();
            builder.Property(e => e.BankBranchName)
                .IsRequired()
                .HasMaxLength(64);
            builder.Property(e => e.BranchIndex)
                .IsRequired()
                .HasMaxLength(64);
            builder.Property(e => e.AccountNumber)
                .IsRequired()
                .HasMaxLength(32);
            builder.Property(e => e.CardNumber)
                .HasMaxLength(32);
            builder.Property(e => e.ShabaNumber)
                .HasMaxLength(32);
            builder.Property(e => e.Description)
                .HasMaxLength(512);
            builder.Property(e => e.Description)
                .HasMaxLength(1024);
            builder.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");
            builder.Property(e => e.RowGuid)
                .HasColumnName("rowguid")
                .HasDefaultValueSql("(newid())");

            builder.HasOne(d => d.Account)
                .WithOne(p => p.AccountOwner)
                .HasForeignKey<AccountOwner>("AccountID")
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Finance_AccountOwner_Finance_Account");
        }
    }
}
