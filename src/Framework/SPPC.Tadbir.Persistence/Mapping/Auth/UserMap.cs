using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPPC.Tadbir.Model.Auth;

namespace SPPC.Tadbir.Persistence.Mapping
{
    internal sealed class UserMap
    {
        private UserMap()
        {
        }

        internal static void BuildMapping(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User", "Auth");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("UserID");
            builder.Property(e => e.LastLoginDate)
                .HasColumnType("datetime");
            builder.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");
            builder.Property(e => e.PasswordHash)
                .IsRequired()
                .HasMaxLength(256)
                .IsUnicode(false);
            builder.Property(e => e.RowGuid)
                .HasColumnName("rowguid")
                .HasDefaultValueSql("(newid())");
            builder.Property(e => e.UserName)
                .IsRequired()
                .HasMaxLength(64);
        }
    }
}
