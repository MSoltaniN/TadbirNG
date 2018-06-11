using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPPC.Tadbir.Model.Auth;
using SPPC.Tadbir.Model.Contact;

namespace SPPC.Tadbir.Persistence.Mapping
{
    internal static class UserMap
    {
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

            builder.HasOne(d => d.Person)
                .WithOne(p => p.User)
                .HasForeignKey<Person>("UserID")
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Contact_Person_Auth_User");
        }
    }
}
