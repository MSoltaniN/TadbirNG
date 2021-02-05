using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Persistence.Utility
{
    /// <summary>
    /// امکان مدیریت اطلاعات را در حافظه کش فراهم می کند
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    public interface ICacheUtility<TItem>
        where TItem : class
    {
        /// <summary>
        /// مجموعه ای از موجودیت ها را در حافظه کش ذخیره می کند
        /// </summary>
        /// <param name="items">مجموعه موجودیت های مورد نظر برای ذخیره در حافظه کش</param>
        void Add(List<TItem> items);

        /// <summary>
        /// موجودیت جدیدی را به مجموعه موجود در حافظه کش اضافه می کند
        /// </summary>
        /// <param name="item">موجودیت جدید که باید به حافظه کش اضافه شود</param>
        void Add(TItem item);

        /// <summary>
        /// اطلاعات جدید موجودیت را در مجموعه موجود در حافظه کش به روزرسانی می کند
        /// </summary>
        /// <param name="item">موجودیت تغییریافته که باید در حافظه کش به روزرسانی شود</param>
        void Update(TItem item);

        /// <summary>
        /// اطلاعات موجودیت را از مجموعه موجود در حافظه کش حذف می کند
        /// </summary>
        /// <param name="itemId">شناسه دیتابیسی موجودیتی که باید از حافظه کش حذف شود</param>
        void Delete(int itemId);

        /// <summary>
        /// اطلاعات مجموعه ای از موجودیت ها را از مجموعه موجود در حافظه کش حذف می کند
        /// </summary>
        /// <param name="itemIds">شناسه های دیتابیسی موجودیت هایی که باید از حافظه کش حذف شوند</param>
        void Delete(IEnumerable<int> itemIds);
    }
}
