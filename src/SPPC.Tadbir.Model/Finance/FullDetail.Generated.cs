// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.0.0.0
//     Template Version: 1.0
//     Generation Date: 2017-08-04 5:08:27 PM
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using BabakSoft.Platform.Domain;

namespace SPPC.Tadbir.Model.Finance
{
    /// <summary>
    /// TODO: Add description...
    /// </summary>
    public partial class FullDetail : IEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FullDetail"/> class.
        /// </summary>
        public FullDetail()
        {
            this.ModifiedDate = DateTime.Now;
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
        /// Gets or sets the todo: add description...
        /// </summary>
        public virtual FullDetailType Type { get; set; }

        /// <summary>
        /// Gets or sets the todo: add description...
        /// </summary>
        public virtual DetailAccount Detail2 { get; set; }

        /// <summary>
        /// Gets or sets the todo: add description...
        /// </summary>
        public virtual DetailAccount Detail3 { get; set; }

        /// <summary>
        /// Gets or sets the todo: add description...
        /// </summary>
        public virtual DetailAccount Detail4 { get; set; }

        /// <summary>
        /// Gets or sets the todo: add description...
        /// </summary>
        public virtual DetailAccount Detail5 { get; set; }

        /// <summary>
        /// Gets or sets the todo: add description...
        /// </summary>
        public virtual DetailAccount Detail6 { get; set; }

        /// <summary>
        /// Gets or sets the todo: add description...
        /// </summary>
        public virtual DetailAccount Detail7 { get; set; }

        /// <summary>
        /// Gets or sets the todo: add description...
        /// </summary>
        public virtual DetailAccount Detail8 { get; set; }

        /// <summary>
        /// Gets or sets the todo: add description...
        /// </summary>
        public virtual DetailAccount Detail9 { get; set; }

        private void InitReferences()
        {
        }
    }
}
