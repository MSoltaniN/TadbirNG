using System;
using System.Collections.Generic;
using System.Linq;
using SPPC.Framework.Common;
using SPPC.Tadbir.CrossCutting;
using SPPC.Tadbir.ViewModel.Finance;

namespace SPPC.Tadbir.Persistence.Utility
{
    /// <summary>
    /// امکان مدیریت اطلاعات آرتیکل های سند را در حافظه کش فراهم می کند
    /// </summary>
    public class LineCacheUtility : ICacheUtility<VoucherLineDetailViewModel>
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="cache">امکان انجام عملیات مختلف با سرور کش را فراهم می کند</param>
        /// <param name="repository"></param>
        public LineCacheUtility(ICacheManager cache, ISecureCacheRepository repository)
        {
            _cache = cache;
            _repository = repository;
        }

        /// <summary>
        /// کلید متنی مورد نیاز برای دسترسی به اطلاعات در حافظه کش
        /// </summary>
        public string CacheKey
        {
            get { return "lines"; }
        }

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
        public IEnumerable<VoucherLineDetailViewModel> Get()
        {
            var outItems = new List<VoucherLineDetailViewModel>();
            if (!HasData())
            {
                return outItems;
            }

            var lines = _cache.Get<List<VoucherLineDetailViewModel>>(CacheKey);
            return _repository.ApplyOperationBranchFilter(lines);
        }

        /// <summary>
        /// مجموعه ای از آرتیکل ها را در حافظه کش ذخیره می کند
        /// </summary>
        /// <param name="lines">مجموعه آرتیکل های مورد نظر برای ذخیره در حافظه کش</param>
        public void Add(List<VoucherLineDetailViewModel> lines)
        {
            Verify.ArgumentNotNull(lines, nameof(lines));
            if (!HasData())
            {
                _cache.Set(CacheKey, lines);
            }
        }

        /// <summary>
        /// آرتیکل جدیدی را به مجموعه موجود در حافظه کش اضافه می کند
        /// </summary>
        /// <param name="line">آرتیکل جدید که باید به حافظه کش اضافه شود</param>
        public void Insert(VoucherLineDetailViewModel line)
        {
            Verify.ArgumentNotNull(line, nameof(line));
            if (HasData())
            {
                var lines = _cache.Get<List<VoucherLineDetailViewModel>>(CacheKey);
                lines.Add(line);
                _cache.Set(CacheKey, lines);
            }
        }

        /// <summary>
        /// اطلاعات جدید آرتیکل را در مجموعه موجود در حافظه کش به روزرسانی می کند
        /// </summary>
        /// <param name="line">آرتیکل تغییریافته که باید در حافظه کش به روزرسانی شود</param>
        public void Update(VoucherLineDetailViewModel line)
        {
            Verify.ArgumentNotNull(line, nameof(line));
            if (HasData())
            {
                var lines = _cache.Get<List<VoucherLineDetailViewModel>>(CacheKey);
                lines.RemoveAll(vl => line.Id == line.Id);
                lines.Add(line);
                _cache.Set(CacheKey, lines);
            }
        }

        /// <summary>
        /// اطلاعات آرتیکل را از مجموعه موجود در حافظه کش حذف می کند
        /// </summary>
        /// <param name="lineId">شناسه دیتابیسی آرتیکلی که باید از حافظه کش حذف شود</param>
        public void Delete(int lineId)
        {
            if (HasData())
            {
                var lines = _cache.Get<List<VoucherLineDetailViewModel>>(CacheKey);
                lines.RemoveAll(line => line.Id == lineId);
                _cache.Set(CacheKey, lines);
            }
        }

        /// <summary>
        /// اطلاعات مجموعه ای از آرتیکل ها را از مجموعه موجود در حافظه کش حذف می کند
        /// </summary>
        /// <param name="lineIds">شناسه های دیتابیسی آرتیکل هایی که باید از حافظه کش حذف شوند</param>
        public void Delete(IEnumerable<int> lineIds)
        {
            Verify.ArgumentNotNull(lineIds, nameof(lineIds));
            if (HasData())
            {
                var lines = _cache.Get<List<VoucherLineDetailViewModel>>(CacheKey);
                lines.RemoveAll(line => lineIds.Contains(line.Id));
                _cache.Set(CacheKey, lines);
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
