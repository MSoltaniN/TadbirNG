using System.Collections.Generic;

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
        /// مجموعه ای از ارزهای انتخاب شده در شعب مختلف برای این سرفصل حسابداری
        /// </summary>
        public IList<AccountCurrency> AccountCurrencies { get; set; }

        ///// <summary>
        ///// مجموعه ای از مجموعه حساب های این سرفصل حسابداری
        ///// </summary>
        //public IList<AccountCollectionAccount> AccountCollectionAccounts { get; set; }

        /// <summary>
        /// اطلاعات مالیاتی طرف حساب
        /// </summary>
        public CustomerTaxInfo CustomerTaxInfo { get; set; }
    }
}
