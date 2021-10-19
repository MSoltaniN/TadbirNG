using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Reporting
{
    /// <summary>
    /// اطلاعات نمایشی مورد نیاز برای پیش نمایش و طراحی یک گزارش را نگهداری می کند
    /// </summary>
    public class PrintInfoViewModel
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس ی سازد
        /// </summary>
        public PrintInfoViewModel()
        {
            Parameters = new List<ParameterViewModel>();
        }

        /// <summary>
        /// شناسه دیتابیسی گزارش ذخیره شده در جدول اصلی
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// آدرس وب برای خواندن اطلاعات گزارش
        /// </summary>
        public string ServiceUrl { get; set; }

        /// <summary>
        /// الگوی طراحی و نمایش چاپ به صورت محلی شده با زبان جاری برنامه
        /// </summary>
        public string Template { get; set; }

        /// <summary>
        /// شناسه دیتابیسی گزارش ذخیره شده در جدول اصلی
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// مجموعه ای از اطلاعات نمایشی پارامترهای مورد نیاز برای پیش نمایش یا چاپ گزارش
        /// </summary>
        public IList<ParameterViewModel> Parameters { get; protected set; }
    }
}
