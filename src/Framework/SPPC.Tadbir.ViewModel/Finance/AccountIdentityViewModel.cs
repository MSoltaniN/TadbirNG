using System;
using System.Collections.Generic;
using System.Text;

namespace SPPC.Tadbir.ViewModel.Finance
{
    /// <summary>
    /// اطلاعات حساب مورد نیاز برای لیست مجموعه حساب ها
    /// </summary>
    public class AccountIdentityViewModel
    {
        /// <summary>
        /// شناسه دیتابیسی حساب
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// نام حساب
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// کد کامل حساب
        /// </summary>
        public string FullCode { get; set; }

        /// <summary>
        /// سطح حساب
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// تعداد حساب های زیرمجموعه این حساب در ساختار درختی
        /// </summary>
        public int ChildCount { get; set; }
    }
}
