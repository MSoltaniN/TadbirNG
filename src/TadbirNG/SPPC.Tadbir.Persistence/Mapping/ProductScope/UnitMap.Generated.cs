// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.2.1587
//     Template Version: 1.0
//     Generation Date: 09/26/2023 4:41:05 PM
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPPC.Tadbir.Model.ProductScope;

namespace SPPC.Tadbir.Persistence.Mapping
{
    internal static class UnitMap
    {
        internal static void BuildMapping(EntityTypeBuilder<Unit> builder)
        {
            builder.Property(e => e.Id)
                .HasColumnName("UnitID");

            builder.HasOne(e => e.Branch)
                .WithMany()
                .HasForeignKey(e => e.BranchId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
