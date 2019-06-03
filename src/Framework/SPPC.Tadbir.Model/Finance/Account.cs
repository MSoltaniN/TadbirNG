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
        /// شناسه دیتابیسی پول یا ارز مورد استفاده در این سرفصل حسابداری
        /// </summary>
        public int? CurrencyId { get; set; }
    }
}
