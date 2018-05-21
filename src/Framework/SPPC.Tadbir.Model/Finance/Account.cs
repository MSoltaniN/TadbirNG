using System;
using System.Collections.Generic;
using System.Text;

namespace SPPC.Tadbir.Model.Finance
{
    public partial class Account
    {
        /// <summary>
        /// مجموعه ای از سرفصل های حسابداری زیرشاخه (فرزند) در ساختار درختی
        /// </summary>
        public IList<Account> Children { get; protected set; }

        /// <summary>
        /// شناسه دیتابیسی حساب والد این حساب در ساختار درختی
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// مجموعه ای از تفصیلی های شناور مرتبط با این سرفصل حسابداری
        /// </summary>
        public IList<AccountDetailAccount> AccountDetailAccounts { get; protected set; }

        /// <summary>
        /// مجموعه ای از مراکز هزینه مرتبط با این سرفصل حسابداری
        /// </summary>
        public IList<AccountCostCenter> AccountCostCenters { get; protected set; }

        /// <summary>
        /// مجموعه ای از پروژه های مرتبط با این سرفصل حسابداری
        /// </summary>
        public IList<AccountProject> AccountProjects { get; protected set; }
    }
}
