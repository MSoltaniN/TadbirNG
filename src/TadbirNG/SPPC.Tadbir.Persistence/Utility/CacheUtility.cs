using System;
using System.Collections.Generic;
using System.Linq;
using SPPC.Framework.Common;
using SPPC.Tadbir.CrossCutting;
using SPPC.Tadbir.Domain;

namespace SPPC.Tadbir.Persistence.Utility
{
    /// <summary>
    /// امکان مدیریت اطلاعات موجودیت ها را در حافظه کش فراهم می کند
    /// </summary>
    public class CacheUtility<T> : ICacheUtility<T>
        where T : class, IFiscalEntity
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="cache">امکان انجام عملیات مختلف با سرور کش را فراهم می کند</param>
        /// <param name="repository"></param>
        public CacheUtility(ICacheManager cache, ISecureCacheRepository repository)
        {
            _cache = cache;
            _repository = repository;
        }

        /// <summary>
        /// کلید متنی مورد نیاز برای دسترسی به اطلاعات در حافظه کش
        /// </summary>
        public string CacheKey { get; set; }

        /// <summary>
        /// مشخص می کند که اطلاعاتی در حافظه کش وجود دارد یا نه
        /// </summary>
        /// <returns>در صورت وجود اطلاعات در حافظه کش مقدار بولی "درست" و در غیر این صورت
        /// مقدار بولی "نادرست" را برمی گرداند</returns>
        public bool HasData()
        {
            return _cache.ContainsKey(CacheKey);
        }

        /// <summary>
        /// اطلاعات موجود در حافظه کش را برمی گرداند
        /// </summary>
        /// <returns>اطلاعات موجود در حافظه کش پس از تبدیل به نوع مورد نیاز</returns>
        public IEnumerable<T> Get()
        {
            var outItems = new List<T>();
            if (!HasData())
            {
                return outItems;
            }

            var items = _cache.Get<List<T>>(CacheKey);
            return _repository.ApplyOperationBranchFilter(items);
        }

        /// <summary>
        /// مجموعه ای از موجودیت ها را در حافظه کش ذخیره می کند
        /// </summary>
        /// <param name="items">مجموعه موجودیت های مورد نظر برای ذخیره در حافظه کش</param>
        public void Add(List<T> items)
        {
            Verify.ArgumentNotNull(items, nameof(items));
            if (!HasData())
            {
                _cache.Set(CacheKey, items);
            }
        }

        /// <summary>
        /// موجودیت جدیدی را به مجموعه موجود در حافظه کش اضافه می کند
        /// </summary>
        /// <param name="item">موجودیت جدید که باید به حافظه کش اضافه شود</param>
        public void Insert(T item)
        {
            Verify.ArgumentNotNull(item, nameof(item));
            if (HasData())
            {
                var items = _cache.Get<List<T>>(CacheKey);
                items.Add(item);
                _cache.Set(CacheKey, items);
            }
        }

        /// <summary>
        /// اطلاعات جدید موجودیت را در مجموعه موجود در حافظه کش به روزرسانی می کند
        /// </summary>
        /// <param name="item">موجودیت تغییریافته که باید در حافظه کش به روزرسانی شود</param>
        public void Update(T item)
        {
            Verify.ArgumentNotNull(item, nameof(item));
            if (HasData())
            {
                var items = _cache.Get<List<T>>(CacheKey);
                items.RemoveAll(vl => item.Id == item.Id);
                items.Add(item);
                _cache.Set(CacheKey, items);
            }
        }

        /// <summary>
        /// اطلاعات موجودیت را از مجموعه موجود در حافظه کش حذف می کند
        /// </summary>
        /// <param name="itemId">شناسه دیتابیسی موجودیتی که باید از حافظه کش حذف شود</param>
        public void Delete(int itemId)
        {
            if (HasData())
            {
                var items = _cache.Get<List<T>>(CacheKey);
                items.RemoveAll(item => item.Id == itemId);
                _cache.Set(CacheKey, items);
            }
        }

        /// <summary>
        /// اطلاعات مجموعه ای از موجودیت ها را از مجموعه موجود در حافظه کش حذف می کند
        /// </summary>
        /// <param name="itemIds">شناسه های دیتابیسی موجودیت هایی که باید از حافظه کش حذف شوند</param>
        public void Delete(IEnumerable<int> itemIds)
        {
            Verify.ArgumentNotNull(itemIds, nameof(itemIds));
            if (HasData())
            {
                var items = _cache.Get<List<T>>(CacheKey);
                items.RemoveAll(item => itemIds.Contains(item.Id));
                _cache.Set(CacheKey, items);
            }
        }

        /// <summary>
        /// اطلاعات موجودیت ها را به طور کامل از حافظه کش پاک می کند
        /// </summary>
        public void Clear()
        {
            if (HasData())
            {
                _cache.Delete(CacheKey);
            }
        }

        private readonly ICacheManager _cache;
        private readonly ISecureCacheRepository _repository;
    }
}
