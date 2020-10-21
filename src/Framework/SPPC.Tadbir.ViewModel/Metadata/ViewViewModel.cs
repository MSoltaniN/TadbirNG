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

        public ViewViewModel GetCopy()
        {
            return (ViewViewModel)MemberwiseClone();
        }

        public void SetColumns(IEnumerable<ColumnViewModel> columns)
        {
            Columns.Clear();
            Columns.AddRange(columns);
        }
    }
}
