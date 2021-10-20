using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPPC.Tadbir.Model.Finance;

namespace SPPC.Tadbir.Persistence.Mapping.Finance
{
    internal static class AccountHolderMap
    {
        internal static void BuildMapping(EntityTypeBuilder<AccountHolder> builder)
        {
            builder.ToTable("AccountHolder", "Finance");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("AccountHolderID");
            builder.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");
            builder.Property(e => e.RowGuid)
                .HasColumnName("rowguid")
                .HasDefaultValueSql("(newid())");
            builder.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(64);
            builder.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(64);
            builder.Property(e => e.HasSignature)
                .IsRequired();

            builder.HasOne(d => d.AccountOwner)
                .WithMany(p => p.AccountHolders)
                .HasForeignKey(d => d.AccountOwnerId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
