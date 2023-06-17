using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPPC.Tadbir.Model.Finance;

namespace SPPC.Tadbir.Persistence.Mapping.Finance
{
    internal static class AccountCurrencyMap
    {
        internal static void BuildMapping(EntityTypeBuilder<AccountCurrency> builder)
        {
            builder.ToTable("AccountCurrency", "Finance");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("AccountCurrencyID");
            builder.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");
            builder.Property(e => e.RowGuid)
                .HasColumnName("rowguid")
                .HasDefaultValueSql("(newid())");

            builder.HasOne(d => d.Account)
                .WithMany(p => p.AccountCurrencies)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(d => d.Currency)
                .WithMany()
                .HasForeignKey(d => d.CurrencyId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(d => d.Branch)
                .WithMany()
                .HasForeignKey(d => d.BranchId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
