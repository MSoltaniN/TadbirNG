using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPPC.Tadbir.Model.Contact;

namespace SPPC.Tadbir.Persistence.Mapping
{
    internal sealed class PersonMap
    {
        private PersonMap()
        {
        }

        internal static void BuildMapping(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("Person", "Contact");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("PersonID");
            builder.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(64);
            builder.Property(e => e.LastName)
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
                .HasForeignKey("UserID")
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Contact_Person_Auth_User");
        }
    }
}
