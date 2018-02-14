using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Core
{
    /// <summary>
    /// این مدل نمایشی اطلاعات تکمیلی مربوط به یک اقدام را نشان می دهد.
    /// </summary>
    public class ActionDetailViewModel
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد.
        /// </summary>
        public ActionDetailViewModel()
        {
            Items = new List<int>();
        }

        /// <summary>
        /// پاراف متنی اختیاری که کاربر پیش از اقدام می تواند وارد کند
        /// </summary>
        public string Paraph { get; set; }

        /// <summary>
        /// مجموعه ای از شناسه های دیتابیسی مستندات مورد نظر برای اقدام گروهی
        /// </summary>
        public IList<int> Items { get; private set; }
    }
}
