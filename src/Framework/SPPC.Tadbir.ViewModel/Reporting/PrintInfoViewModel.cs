using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Reporting
{
    public class PrintInfoViewModel
    {
        public PrintInfoViewModel()
        {
            Parameters = new List<ParameterViewModel>();
        }

        /// <summary>
        /// شناسه دیتابیسی گزارش ذخیره شده در جدول اصلی
        /// </summary>
        public int Id { get; set; }

        public int LocaleId { get; set; }

        public string Template { get; set; }

        public IList<ParameterViewModel> Parameters { get; protected set; }
    }
}
