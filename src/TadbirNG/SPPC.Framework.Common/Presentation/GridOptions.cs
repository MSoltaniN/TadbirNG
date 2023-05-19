using System;
using System.Collections.Generic;
using System.Linq;

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
            ListChanged = true;
            Operation = 1;      // Defaults to View operation
        }

        /// <summary>
        /// نمونه کمکی برای تنظیمات لیستی پیش فرض
        /// </summary>
        public static GridOptions Empty
        {
            get
            {
                return new GridOptions()
                {
                    Paging = GridPaging.DefaultPaging,
                    SortColumns = new List<GridOrderBy>(),
                    ListChanged = false,
                    Operation = 1      // Defaults to View operation
                };
            }
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

        /// <summary>
        /// کد عملیات مورد نظر برای خواندن اطلاعات، مانند مشاهده، چاپ، ارسال به فایل اکسل و غیره
        /// </summary>
        public int Operation { get; set; }

        /// <summary>
        /// مشخص می کند که آیا لیست اطلاعاتی جاری تغییر کرده یا نه؟
        /// </summary>
        /// <remarks>
        /// با توجه به فراخوانی مکرر متدهای خواندن اطلاعات با تغییر صفحه، لازم است
        /// پیش از اولین بار که اطلاعات خوانده می شود، مقدار این ویژگی "درست" و بلافاصله پس از
        /// خواندن اولین صفحه، مقدار این ویژگی "نادرست" باشد
        /// </remarks>
        public bool ListChanged { get; set; }

        /// <summary>
        /// مشخص می کند که آبجکت جاری تنظیمات فیلتر، مرتب سازی یا صفحه بندی دارد یا نه
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                return Filter == null
                    && QuickFilter == null
                    && !SortColumns.Any();
            }
        }
    }
}
