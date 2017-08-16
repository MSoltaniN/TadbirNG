// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.0.0.0
//     Template Version: 1.0
//     Generation Date: 2017-08-03 7:01:06 PM
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using SPPC.Tadbir.Model.Procurement;
using FluentNHibernate.Mapping;

namespace SPPC.Tadbir.NHibernate.Mapping
{
    /// <summary>
    /// Defines fluent mappings for the <see cref="RequisitionVoucherLine"/> entity class.
    /// </summary>
    public partial class RequisitionVoucherLineMap : ClassMap<RequisitionVoucherLine>
    {
        /// <summary>
        /// Initializes a new instance of the RequisitionVoucherLineMap class and specifies NHibernate mappings
        /// using Fluent NHibernate API.
        /// </summary>
        public RequisitionVoucherLineMap()
        {
            Schema("Procurement");
            Table("RequisitionVoucherLine");
            Id(x => x.Id)
                .Column("LineID")
                .GeneratedBy.Identity();
            Map(x => x.No)
                .Not.Nullable();
            Map(x => x.OrderedQuantity)
                .Not.Nullable();
            Map(x => x.DeliveredQuantity)
                .Nullable();
            Map(x => x.ReservedQuantity)
                .Nullable();
            Map(x => x.LastOrderedQuantity)
                .Nullable();
            Map(x => x.RequiredDate)
                .Not.Nullable();
            Map(x => x.PromisedDate)
                .Nullable();
            Map(x => x.DeliveredDate)
                .Nullable();
            Map(x => x.LastOrderedDate)
                .Nullable();
            Map(x => x.IsActive)
                .Not.Nullable();
            Map(x => x.Description)
                .Length(256)
                .Nullable();
            Map(x => x.Timestamp)
                .Not.Nullable()
                .Generated.Always();
            Map(x => x.ModifiedDate);
            Map(x => x.RowGuid, "rowguid")
                .Generated.Insert();

            MapReferences();
        }

        private void MapReferences()
        {
            References(x => x.Voucher)
                .Column("VoucherID")
                .Cascade.None()
                .Not.LazyLoad();
            References(x => x.Warehouse)
                .Column("WarehouseID")
                .Cascade.None()
                .Not.LazyLoad();
            References(x => x.Product)
                .Column("ProductID")
                .Cascade.None()
                .Not.LazyLoad();
            References(x => x.Uom)
                .Column("UomID")
                .Cascade.None()
                .Not.LazyLoad();
            References(x => x.Branch)
                .Column("BranchID")
                .Cascade.None()
                .Not.LazyLoad();
            References(x => x.FiscalPeriod)
                .Column("FiscalPeriodID")
                .Cascade.None()
                .Not.LazyLoad();
            References(x => x.FullAccount)
                .Column("FullAccountID")
                .Cascade.SaveUpdate()
                .Not.LazyLoad();
            References(x => x.FullDetail)
                .Column("FullDetailID")
                .Cascade.None()
                .Not.LazyLoad();
            References(x => x.Action)
                .Column("ActionID")
                .Cascade.None()
                .Not.LazyLoad();
        }
    }
}
