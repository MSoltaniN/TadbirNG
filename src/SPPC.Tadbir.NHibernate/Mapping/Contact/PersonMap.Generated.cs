// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.0.0.0
//     Template Version: 1.0
//     Generation Date: 2017-02-15 2:58:07 PM
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using SPPC.Tadbir.Model.Contact;
using FluentNHibernate.Mapping;

namespace SPPC.Tadbir.NHibernate.Mapping
{
    /// <summary>
    /// Defines fluent mappings for the <see cref="Person"/> entity class.
    /// </summary>
    public partial class PersonMap : ClassMap<Person>
    {
        /// <summary>
        /// Initializes a new instance of the PersonMap class and specifies NHibernate mappings
        /// using Fluent NHibernate API.
        /// </summary>
        public PersonMap()
        {
            Schema("Contact");
            Table("Person");
            Id(x => x.Id)
                .Column("PersonID")
                .GeneratedBy.Identity();
            Map(x => x.FirstName)
                .Length(64)
                .Nullable();
            Map(x => x.LastName)
                .Length(64)
                .Nullable();
            Map(x => x.RowGuid, "rowguid")
                .Generated.Insert();
            Map(x => x.ModifiedDate);

            MapReferences();
        }

        private void MapReferences()
        {
            References(x => x.User)
                .Column("UserID")
                .Unique()
                .Not.LazyLoad()
                .Cascade.None();
        }
    }
}
