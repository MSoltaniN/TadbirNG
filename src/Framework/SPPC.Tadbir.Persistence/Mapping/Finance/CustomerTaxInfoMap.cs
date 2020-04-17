using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPPC.Tadbir.Model.Finance;

namespace SPPC.Tadbir.Persistence.Mapping
{
    internal static class CustomerTaxInfoMap
    {
        internal static void BuildMapping(EntityTypeBuilder<CustomerTaxInfo> builder)
        {
            builder.ToTable("CustomerTaxInfo", "Finance");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("CustomerTaxInfoID");
            builder.Property(e => e.AccountID)
                .IsRequired();
            builder.Property(e => e.CustomerFirstName)
                .HasMaxLength(64);
            builder.Property(e => e.CustomerName)
                .IsRequired()
                .HasMaxLength(128);
            builder.Property(e => e.PersonType)
                .IsRequired();
            builder.Property(e => e.BuyerType)
                .IsRequired();
            builder.Property(e => e.EconomicCode)
                .HasMaxLength(12);
            builder.Property(e => e.Address)
                .IsRequired()
                .HasMaxLength(256);
            builder.Property(e => e.NationalCode)
                .IsRequired()
                .HasMaxLength(11);
            builder.Property(e => e.PerCityCode)
                .IsRequired()
                .HasMaxLength(10);
            builder.Property(e => e.PhoneNo)
                .IsRequired()
                .HasMaxLength(64);
            builder.Property(e => e.MobileNo)
                .IsRequired()
                .HasMaxLength(64);
            builder.Property(e => e.PostalCode)
                .IsRequired()
                .HasMaxLength(10);
            builder.Property(e => e.Description)
                .HasMaxLength(1024);
            builder.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");
            builder.Property(e => e.RowGuid)
                .HasColumnName("rowguid")
                .HasDefaultValueSql("(newid())");

            builder.HasOne(d => d.Account)
                .WithOne(p => p.CustomerTaxInfo)
                .HasForeignKey<CustomerTaxInfo>("AccountID")
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Finance_CustomerTaxInfo_Finance_Account");
        }
    }
}
