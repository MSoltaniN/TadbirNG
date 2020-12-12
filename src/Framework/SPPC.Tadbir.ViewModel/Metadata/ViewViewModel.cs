using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Metadata
{
    public partial class ViewViewModel
    {
        /// <summary>
        /// مجموعه ای از ویژگی های تعریف شده برای موجودیت
        /// </summary>
        public List<ColumnViewModel> Columns { get; set; }

        /// <summary>
        /// کپی جدیدی از این کلاس با مقادیر موجود در نمونه جاری ساخته و برمی گرداند
        /// </summary>
        /// <returns>کپی جدید با اطلاعات نمونه جاری</returns>
        public ViewViewModel GetCopy()
        {
            return (ViewViewModel)MemberwiseClone();
        }
    }
}
