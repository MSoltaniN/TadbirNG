// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.2.1484
//     Template Version: 1.0
//     Generation Date: 13/12/1401 01:43:30 ب.ظ
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using SPPC.Tadbir.Domain;
using System;

namespace SPPC.Tadbir.Model.Check
{
    /// <summary>
    /// اطلاعات یکی از برگه های چک در یک دسته چک را نگهداری می کند
    /// </summary>
    public partial class CheckBookPage : CoreEntity
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public CheckBookPage()
        {
            SerialNo = String.Empty;
            ModifiedDate = DateTime.Now;
        }

        /// <summary>
        /// شماره سری برگه
        /// </summary>
        public virtual string SerialNo { get; set; }

        /// <summary>
        /// وضعیت برگه
        /// </summary>
        public virtual CheckBookPageState? Status { get; set; }

        /// <summary>
        /// شناسه چک مرتبط با برگه
        /// </summary>
        public virtual int? CheckId { get; set; }

        /// <summary>
        /// دسته چک ای که برگه ها متعلق به آن هست
        /// </summary>
        public virtual CheckBook CheckBook { get; set; }
    }
}
