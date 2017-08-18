// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.0.0.0
//     Template Version: 1.0
//     Generation Date: 2017-04-27 12:33:07 PM
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using SPPC.Tadbir.Model.Finance;
using FluentNHibernate.Mapping;

namespace SPPC.Tadbir.NHibernate.Mapping
{
    /// <summary>
    /// Defines fluent mappings for the <see cref="Transaction"/> entity class.
    /// </summary>
    public partial class TransactionMap : ClassMap<Transaction>
    {
        /// <summary>
        /// Initializes a new instance of the TransactionMap class and specifies NHibernate mappings
        /// using Fluent NHibernate API.
        /// </summary>
        public TransactionMap()
        {
            Schema("Finance");
            Table("[Transaction]");
            Id(x => x.Id)
                .Column("TransactionID")
                .GeneratedBy.Identity();
            Map(x => x.No)
                .Length(64)
                .Not.Nullable();
            Map(x => x.Date)
                .Not.Nullable();
            Map(x => x.Description)
                .Length(512)
                .Nullable();
            Map(x => x.RowGuid, "rowguid")
                .Generated.Insert();
            Map(x => x.ModifiedDate);

            MapReferences();
        }

        private void MapReferences()
        {
            References(x => x.Branch)
                .Column("BranchID")
                .Not.LazyLoad()
                .Cascade.None();
            References(x => x.FiscalPeriod)
                .Column("FiscalPeriodID")
                .Not.LazyLoad()
                .Cascade.None();
            References(x => x.Document)
                .Column("DocumentID")
                .Not.LazyLoad()
                .Cascade.All();
            HasMany(x => x.Lines)
                .KeyColumn("TransactionID")
                .LazyLoad()
                .Inverse()
                .Cascade.DeleteOrphan();
        }
    }
}
