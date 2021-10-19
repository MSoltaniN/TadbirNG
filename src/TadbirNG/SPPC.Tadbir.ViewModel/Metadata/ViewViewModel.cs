using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Metadata
{
    public partial class ViewViewModel
    {
        /// <summary>
        /// مجموعه ای از ویژگی های تعریف شده برای موجودیت
        /// </summary>
        public List<ColumnViewModel> Columns { get; }

        /// <summary>
        /// اطلاعات فراداده ای ستونها را با اطلاعات جدید جایگزین می کند
        /// </summary>
        /// <param name="columns">اطلاعات فراداده ای جدید برای ستونها</param>
        public void SetColumns(IEnumerable<ColumnViewModel> columns)
        {
            Columns.Clear();
            Columns.AddRange(columns);
        }

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
