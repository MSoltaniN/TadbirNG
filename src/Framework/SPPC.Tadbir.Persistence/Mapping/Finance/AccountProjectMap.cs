using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPPC.Tadbir.Model.Finance;

namespace SPPC.Tadbir.Persistence.Mapping
{
    internal sealed class AccountProjectMap
    {
        private AccountProjectMap()
        {
        }

        internal static void BuildMapping(EntityTypeBuilder<AccountProject> builder)
        {
            builder.ToTable("AccountProject", "Finance");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("AccountProjectID");
            builder.HasAlternateKey(e => new { e.AccountId, e.ProjectId });
            builder.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");
            builder.Property(e => e.RowGuid)
                .HasColumnName("rowguid")
                .HasDefaultValueSql("(newid())");

            builder.HasOne(d => d.Account)
                .WithMany(p => p.AccountProjects)
                .HasForeignKey(d => d.AccountId);
            builder.HasOne(d => d.Project)
                .WithMany(p => p.AccountProjects)
                .HasForeignKey(d => d.ProjectId);
        }
    }
}
