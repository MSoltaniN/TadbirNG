using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Metadata
{
    public partial class ColumnViewModel
    {
        /// <summary>
        /// اطلاعات آبجکت مورد نظر را به صورت متنی برمی گرداند
        /// </summary>
        /// <returns>اطلاعات آبجکت مورد نظر به صورت متنی</returns>
        public override string ToString()
        {
            return Name;
        }

        /// <summary>
        /// نمونه جدیدی با مقادیر کپی شده از نمونه جاری ایجاد کرده و برمی گرداند
        /// </summary>
        /// <returns></returns>
        public ColumnViewModel GetCopy()
        {
            return (ColumnViewModel)MemberwiseClone();
        }
    }
}
