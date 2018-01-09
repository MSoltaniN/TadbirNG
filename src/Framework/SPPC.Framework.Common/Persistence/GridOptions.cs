using System;
using System.Collections.Generic;
using System.Text;
using SPPC.Framework.Values;

namespace SPPC.Framework.Persistence
{
    /// <summary>
    /// گزینه های مختلف برای کنترل سطرهای اطلاعاتی نمایش داده شده در یک نمای جدولی را نگهداری می کند
    /// </summary>
    public class GridOptions
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public GridOptions()
        {
            Paging = new GridPaging();
            Filters = new List<GridFilter>();
            SortColumns = new List<GridOrderBy>();
        }

        /// <summary>
        /// گزینه های مربوط به نحوه صفحه بندی اطلاعات در نمای جدولی
        /// </summary>
        public GridPaging Paging { get; set; }

        /// <summary>
        /// مجموعه ای از فیلترهای مورد نظر برای اعمال روی اطلاعات در نمای جدولی
        /// </summary>
        public IList<GridFilter> Filters { get; private set; }

        /// <summary>
        /// مجموعه ای از ستون های انتخاب شده برای مرتب سازی اطلاعات در نمای جدولی
        /// </summary>
        public IList<GridOrderBy> SortColumns { get; private set; }
    }
}
