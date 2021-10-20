using System;

namespace SPPC.Tadbir.Model.Finance
{
    public partial class AccountCollectionAccount : FiscalEntity
    {
        /// <summary>
        /// شناسه دیتابیسی سرفصل حسابداری در این ارتباط
        /// </summary>
        public virtual int AccountId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی مجموعه حساب در این ارتباط
        /// </summary>
        public virtual int CollectionId { get; set; }
    }
}
