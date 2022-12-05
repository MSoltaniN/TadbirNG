using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPPC.CodeChallenge.Model.Metadata;

namespace SPPC.CodeChallenge.Persistence.Mapping
{
    internal static class CityMap
    {
        internal static void BuildMapping(EntityTypeBuilder<City> builder)
        {
            builder.ToTable("City", "Metadata");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("CityID");
            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(64);
            builder.Property(e => e.Code)
                .HasMaxLength(16);
            builder.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");
            builder.Property(e => e.RowGuid)
                .HasColumnName("rowguid")
                .HasDefaultValueSql("(newid())");

            builder.HasOne(e => e.Province)
                .WithMany(p => p.Cities)
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey(d => d.ProvinceId)
                .HasConstraintName("FK_Metadata_City_Metadata_Province");
        }
    }
}
