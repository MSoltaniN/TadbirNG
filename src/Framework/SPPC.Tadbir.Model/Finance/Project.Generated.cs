// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.0.0.0
//     Template Version: 1.0
//     Generation Date: 2017-08-02 7:42:27 PM
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using SPPC.Framework.Domain;

namespace SPPC.Tadbir.Model.Finance
{
    /// <summary>
    /// TODO: Add description...
    /// </summary>
    public partial class Project : IEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Project"/> class.
        /// </summary>
        public Project()
        {
            this.Code = String.Empty;
            this.FullCode = String.Empty;
            this.Name = String.Empty;
            this.ModifiedDate = DateTime.Now;
            InitReferences();
        }

        /// <summary>
        /// Gets or sets the unique identifier for this entity. This property is auto-generated.
        /// </summary>
        public virtual int Id { get; set; }

        /// <summary>
        /// TODO: Add description...
        /// </summary>
        public virtual string Code { get; set; }

        /// <summary>
        /// TODO: Add description...
        /// </summary>
        public virtual string FullCode { get; set; }

        /// <summary>
        /// TODO: Add description...
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// TODO: Add description...
        /// </summary>
        public virtual short Level { get; set; }

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
        public virtual Project Parent { get; set; }

        private void InitReferences()
        {
        }
    }
}
