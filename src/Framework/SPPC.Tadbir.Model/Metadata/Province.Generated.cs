// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.0.0.0
//     Template Version: 1.0
//     Generation Date: 4/30/2020 3:47:39 PM
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using System;

namespace SPPC.Tadbir.Model.Metadata
{
    /// <summary>
    /// اطلاعات یک استان را نگهداری می کند
    /// </summary>
    public partial class Province : CoreEntity
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public Province()
        {
            this.Name = String.Empty;
            this.Code = String.Empty;
            this.ModifiedDate = DateTime.Now;
            InitReferences();
        }

        /// <summary>
        /// نام استان
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// کد استان
        /// </summary>
        public virtual string Code { get; set; }

        private void InitReferences()
        {
        }
    }
}
