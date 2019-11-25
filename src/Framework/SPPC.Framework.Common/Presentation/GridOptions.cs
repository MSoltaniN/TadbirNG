using System;
using System.Collections.Generic;

namespace SPPC.Framework.Presentation
{
    /// <summary>
    /// گزینه های مختلف برای کنترل نمایش سطرهای اطلاعاتی در یک نمای جدولی را نگهداری می کند
    /// </summary>
    public class GridOptions
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public GridOptions()
        {
            Paging = GridPaging.NoPaging;
            SortColumns = new List<GridOrderBy>();
        }

        /// <summary>
        /// گزینه های مربوط به نحوه صفحه بندی اطلاعات در نمای جدولی
        /// </summary>
        public GridPaging Paging { get; set; }

        /// <summary>
        /// عبارت فیلتر ترکیبی مورد نظر برای اعمال روی اطلاعات نمای جدولی پس از خواندن یا محاسبه اطلاعات
        /// </summary>
        public FilterExpression Filter { get; set; }

        /// <summary>
        /// عبارت فیلتر ترکیبی مورد نظر برای اعمال روی اطلاعات نمای جدولی پیش از خواندن یا محاسبه اطلاعات
        /// </summary>
        public FilterExpression QuickFilter { get; set; }

        /// <summary>
        /// مجموعه ای از ستون های انتخاب شده برای مرتب سازی اطلاعات در نمای جدولی
        /// </summary>
        public IList<GridOrderBy> SortColumns { get; private set; }
    }
}
