using SPPC.Tadbir.Model.Check;
using System;
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

        /// <summary>
        /// اطلاعات مالیاتی طرف حساب
        /// </summary>
        public CustomerTaxInfo CustomerTaxInfo { get; set; }

        /// <summary>
        /// اطلاعات حساب بانکی
        /// </summary>
        public AccountOwner AccountOwner { get; set; }

        /// <summary>
        /// مجموعه ای از دسته چک های مرتبط با این حساب
        /// </summary>
        public virtual IList<CheckBook> CheckBooks { get; protected set; }

        /// <summary>
        /// اطلاعات موجودیت را به صورت متنی ساخته و برمی گرداند
        /// </summary>
        /// <returns>اطلاعات موجودیت به صورت متنی</returns>
        /// 
        public override string ToString()
        {
            return String.Format("{0} ({1})", Name, FullCode);
        }
    }
}
