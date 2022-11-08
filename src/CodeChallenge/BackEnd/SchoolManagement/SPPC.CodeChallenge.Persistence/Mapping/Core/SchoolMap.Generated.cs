// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.2.1448
//     Template Version: 1.0
//     Generation Date: 2022-11-08 12:30:08 PM
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPPC.CodeChallenge.Model.Core;

namespace SPPC.CodeChallenge.Persistence.Mapping
{
    internal static class SchoolMap
    {
        internal static void BuildMapping(EntityTypeBuilder<School> builder)
        {
            builder.ToTable("School", "Core");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("SchoolID");
            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(64);
            builder.Property(e => e.AdminSystem)
                .IsRequired()
                .HasMaxLength(32);
            builder.Property(e => e.Manager)
                .HasMaxLength(32);
            builder.Property(e => e.Capacity)
                .IsRequired();
            builder.Property(e => e.Tuition)
                .IsRequired();
            builder.Property(e => e.Address)
                .HasMaxLength(256);
            builder.Property(e => e.FoundedDate)
                .IsRequired();
            builder.Property(e => e.IsListed)
                .IsRequired();
            builder.Property(e => e.RowGuid)
                .HasColumnName("rowguid")
                .HasDefaultValueSql("(newid())");
            builder.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.HasOne(e => e.City)
                .WithMany()
                .HasForeignKey(e => e.CityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Core_School_Metadata_City");
            builder.HasOne(e => e.Province)
                .WithMany()
                .HasForeignKey(e => e.ProvinceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Core_School_Metadata_Province");
        }
    }
}
