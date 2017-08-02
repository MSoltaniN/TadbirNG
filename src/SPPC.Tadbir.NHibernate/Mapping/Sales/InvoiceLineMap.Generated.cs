// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.0.0.0
//     Template Version: 1.0
//     Generation Date: 2017-08-02 8:07:49 PM
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using SPPC.Tadbir.Model.Sales;
using FluentNHibernate.Mapping;

namespace SPPC.Tadbir.NHibernate.Mapping
{
    /// <summary>
    /// Defines fluent mappings for the <see cref="InvoiceLine"/> entity class.
    /// </summary>
    public partial class InvoiceLineMap : ClassMap<InvoiceLine>
    {
        /// <summary>
        /// Initializes a new instance of the InvoiceLineMap class and specifies NHibernate mappings
        /// using Fluent NHibernate API.
        /// </summary>
        public InvoiceLineMap()
        {
            Schema("Sales");
            Table("InvoiceLine");
            Id(x => x.Id)
                .Column("InvoiceLineID")
                .GeneratedBy.Identity();
            Map(x => x.No)
                .Not.Nullable();
            Map(x => x.Quantity)
                .Not.Nullable();
            Map(x => x.UnitPrice)
                .Not.Nullable();
            Map(x => x.CurrencyUnitPrice)
                .Nullable();
            Map(x => x.Discount)
                .Nullable();
            Map(x => x.UnitCost)
                .Nullable();
            Map(x => x.IsActive)
                .Not.Nullable();
            Map(x => x.Description)
                .Length(256)
                .Nullable();
            Map(x => x.CreatedDate)
                .Not.Nullable();
            Map(x => x.ModifiedDate);
            Map(x => x.ConfirmedDate)
                .Nullable();
            Map(x => x.ApprovedDate)
                .Nullable();
            Map(x => x.Timestamp)
                .Not.Nullable();
            Map(x => x.RowGuid, "rowguid")
                .Generated.Insert();

            MapReferences();
        }

        private void MapReferences()
        {
            References(x => x.Invoice)
                .Column("InvoiceID")
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
            References(x => x.Currency)
                .Column("CurrencyID")
                .Cascade.None()
                .Not.LazyLoad();
            References(x => x.RequisitionVoucher)
                .Column("RequisitionVoucherID")
                .Cascade.None()
                .Not.LazyLoad();
            References(x => x.Account)
                .Column("AccountID")
                .Cascade.None()
                .Not.LazyLoad();
            References(x => x.DetailAccount)
                .Column("DetailAccountID")
                .Cascade.None()
                .Not.LazyLoad();
            References(x => x.CostCenter)
                .Column("CostCenterID")
                .Cascade.None()
                .Not.LazyLoad();
            References(x => x.Project)
                .Column("ProjectID")
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
            References(x => x.CreatedBy)
                .Column("CreatedByID")
                .Cascade.None()
                .Not.LazyLoad();
            References(x => x.ModifiedBy)
                .Column("ModifiedByID")
                .Cascade.None()
                .Not.LazyLoad();
            References(x => x.ConfirmedBy)
                .Column("ConfirmedByID")
                .Cascade.None()
                .Not.LazyLoad();
            References(x => x.ApprovedBy)
                .Column("ApprovedByID")
                .Cascade.None()
                .Not.LazyLoad();
        }
    }
}
