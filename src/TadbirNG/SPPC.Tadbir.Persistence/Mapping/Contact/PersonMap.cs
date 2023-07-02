using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPPC.Tadbir.Model.Contact;

namespace SPPC.Tadbir.Persistence.Mapping
{
    internal static class PersonMap
    {
        internal static void BuildMapping(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("Person", "Contact");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("PersonID");
            builder.Property(e => e.FullName)
                .IsRequired()
                .HasMaxLength(64);
            builder.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");
            builder.Property(e => e.RowGuid)
                .HasColumnName("rowguid")
                .HasDefaultValueSql("(newid())");

            builder.HasOne(d => d.User)
                .WithOne(p => p.Person)
                .HasForeignKey<Person>("UserID")
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Contact_Person_Auth_User");
        }
    }
}
