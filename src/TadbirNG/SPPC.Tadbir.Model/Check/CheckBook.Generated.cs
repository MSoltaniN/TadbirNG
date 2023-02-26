// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.2.1484
//     Template Version: 1.0
//     Generation Date: 06/12/1401 03:55:38 ب.ظ
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using SPPC.Tadbir.Model.Finance;

namespace SPPC.Tadbir.Model.Check
{
    /// <summary>
    /// اطلاعات یک دسته چک را نگهداری می کند
    /// </summary>
    public partial class CheckBook : OperationalEntity
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public CheckBook()
        {
            CheckBookNo = String.Empty;
            Name = String.Empty;
            SartNo = String.Empty;
            EndNo = String.Empty;
            BankName = String.Empty;
            IssueDate = DateTime.Now;
            ModifiedDate = DateTime.Now;
        }

        /// <summary>
        /// شناسه دسته چک
        /// </summary>
        public virtual int CheckBookID { get; set; }

        /// <summary>
        /// شماره دسته چک
        /// </summary>
        public virtual string CheckBookNo { get; set; }

        /// <summary>
        /// نام دسته چک
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// تاریخ صدور
        /// </summary>
        public virtual DateTime IssueDate { get; set; }

        /// <summary>
        /// شماره اولین برگ
        /// </summary>
        public virtual string SartNo { get; set; }

        /// <summary>
        /// شماره آخرین برگ
        /// </summary>
        public virtual string EndNo { get; set; }

        /// <summary>
        /// نام بانک
        /// </summary>
        public virtual string BankName { get; set; }

        /// <summary>
        /// شناسه سرفصل حسابداری در بردار حساب بانک
        /// </summary>
        public virtual int? AccountID { get; set; }

        /// <summary>
        /// شناسه تفصیلی شناور در بردار حساب بانک
        /// </summary>
        public virtual int? DetailAccountID { get; set; }

        /// <summary>
        /// شناسه مرکز هزینه در بردار حساب بانک
        /// </summary>
        public virtual int? CostCenterID { get; set; }

        /// <summary>
        /// شناسه پروژه در بردار حساب بانک
        /// </summary>
        public virtual int? ProjectID { get; set; }

        /// <summary>
        /// وضعیت بایگانی
        /// </summary>
        public virtual bool? IsArchived { get; set; }

        /// <summary>
        /// برگه های دسته چک متعلق به این دسته چک
        /// </summary>
        public virtual IList<CheckBookPage> ChekBookPages { get; protected set; }

        /// <summary>
        /// مولفه سرفصل حسابداری از بردار حساب مورد استفاده در این دسته چک
        /// </summary>
        public virtual Account Account { get; set; }

        /// <summary>
        /// مولفه تفصیلی شناور از بردار حساب مورد استفاده در این دسته چک
        /// </summary>
        public virtual DetailAccount DetailAccount { get; set; }

        /// <summary>
        /// مولفه مرکز هزینه از بردار حساب مورد استفاده در این دسته چک
        /// </summary>
        public virtual CostCenter CostCenter { get; set; }

        /// <summary>
        ///  مولفه پروژه از بردار حساب مورد استفاده در این دسته چک
        /// </summary>
        public virtual Project Project { get; set; }
    }
}
