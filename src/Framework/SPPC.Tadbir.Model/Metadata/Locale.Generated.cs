// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.0.0.0
//     Template Version: 1.0
//     Generation Date: 2018-02-26 2:04:24 PM
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using System;
using SPPC.Framework.Domain;

namespace SPPC.Tadbir.Model.Metadata
{
    /// <summary>
    /// اطلاعات فراداده ای یکی از زبان های مورد پشتیبانی توسط برنامه را نگهداری می کند
    /// </summary>
    public partial class Locale : IEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Locale"/> class.
        /// </summary>
        public Locale()
        {
            Name = String.Empty;
            LocalName = String.Empty;
            ModifiedDate = DateTime.Now;
            InitReferences();
        }

        /// <summary>
        /// Gets or sets the unique identifier for this entity. This property is auto-generated.
        /// </summary>
        public virtual int Id { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for the database row for this entity. This property is auto-generated.
        /// </summary>
        public virtual Guid RowGuid { get; set; }

        /// <summary>
        /// Gets or sets the date when database row for this entity was last modified. This property is auto-generated.
        /// </summary>
        public virtual DateTime ModifiedDate { get; set; }

        /// <summary>
        /// Gets or sets the نام زبان به صورت غیر محلی شده - به زبان انگلیسی
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets or sets the نام زبان به صورت محلی شده
        /// </summary>
        public virtual string LocalName { get; set; }

        private void InitReferences()
        {
        }
    }
}
