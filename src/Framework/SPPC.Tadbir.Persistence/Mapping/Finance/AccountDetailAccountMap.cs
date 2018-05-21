using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPPC.Tadbir.Model.Finance;

namespace SPPC.Tadbir.Persistence.Mapping
{
    internal sealed class AccountDetailAccountMap
    {
        private AccountDetailAccountMap()
        {
        }

        internal static void BuildMapping(EntityTypeBuilder<AccountDetailAccount> builder)
        {
            builder.ToTable("AccountDetailAccount", "Finance");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("AccountDetailAccountID");
            builder.HasAlternateKey(e => new { e.AccountId, e.DetailId });
            builder.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");
            builder.Property(e => e.RowGuid)
                .HasColumnName("rowguid")
                .HasDefaultValueSql("(newid())");

            builder.HasOne(d => d.Account)
                .WithMany(p => p.AccountDetailAccounts)
                .HasForeignKey(d => d.AccountId);
            builder.HasOne(d => d.DetailAccount)
                .WithMany(p => p.AccountDetailAccounts)
                .HasForeignKey(d => d.DetailId);
        }
    }
}
