using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Finance
{
    /// <summary>
    /// اطلاعات نمایشی یکی از سطوح مورد استفاده از مولفه های حساب را نگهداری می کند
    /// </summary>
    public class AccountLevelViewModel
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public AccountLevelViewModel()
        {
        }

        /// <summary>
        /// ایندکس عددی هر سطح برای نمایش در کنترل لوکاپ
        /// </summary>
        public int Key { get; set; }

        /// <summary>
        /// شناسه دیتابیسی نمای اطلاعاتی مربوط به سطح
        /// </summary>
        public int ViewId { get; set; }

        /// <summary>
        /// عنوان سطح مورد نظر مطابق با پیکربندی موجود برای ساختار درختی
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// شماره سطح مرتبط
        /// </summary>
        public int Level { get; set; }
    }
}
