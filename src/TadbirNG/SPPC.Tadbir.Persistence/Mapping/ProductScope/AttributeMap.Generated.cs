// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.2.1596
//     Template Version: 1.0
//     Generation Date: 10/9/2023 4:32:54 PM
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Attribute = SPPC.Tadbir.Model.ProductScope.Attribute;

namespace SPPC.Tadbir.Persistence.Mapping
{
    internal static class AttributeMap
    {
        internal static void BuildMapping(EntityTypeBuilder<Attribute> builder)
        {
            builder.Property(e => e.Id)
                .HasColumnName("AttributeID");

            builder.HasOne(e => e.Branch)
                .WithMany()
                .HasForeignKey(e => e.BranchId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Ignore(e => e.FiscalPeriod);
        }
    }
}