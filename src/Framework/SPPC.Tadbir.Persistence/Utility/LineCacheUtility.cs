using System;
using System.Collections.Generic;
using System.Linq;
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
        public LineCacheUtility(ICacheManager cache)
        {
            _cache = cache;
        }

        /// <summary>
        /// مجموعه ای از آرتیکل ها را در حافظه کش ذخیره می کند
        /// </summary>
        /// <param name="lines">مجموعه آرتیکل های مورد نظر برای ذخیره در حافظه کش</param>
        public void Add(List<VoucherLineDetailViewModel> lines)
        {
            if (!_cache.ContainsKey(_linesCacheKey))
            {
                _cache.Set(_linesCacheKey, lines);
            }
        }

        /// <summary>
        /// آرتیکل جدیدی را به مجموعه موجود در حافظه کش اضافه می کند
        /// </summary>
        /// <param name="line">آرتیکل جدید که باید به حافظه کش اضافه شود</param>
        public void Add(VoucherLineDetailViewModel line)
        {
            if (_cache.ContainsKey(_linesCacheKey))
            {
                var lines = _cache.Get<List<VoucherLineDetailViewModel>>(_linesCacheKey);
                lines.Add(line);
                _cache.Set(_linesCacheKey, lines);
            }
        }

        /// <summary>
        /// اطلاعات جدید آرتیکل را در مجموعه موجود در حافظه کش به روزرسانی می کند
        /// </summary>
        /// <param name="line">آرتیکل تغییریافته که باید در حافظه کش به روزرسانی شود</param>
        public void Update(VoucherLineDetailViewModel line)
        {
            if (_cache.ContainsKey(_linesCacheKey))
            {
                var lines = _cache.Get<List<VoucherLineDetailViewModel>>(_linesCacheKey);
                lines.RemoveAll(vl => line.Id == line.Id);
                lines.Add(line);
                _cache.Set(_linesCacheKey, lines);
            }
        }

        /// <summary>
        /// اطلاعات آرتیکل را از مجموعه موجود در حافظه کش حذف می کند
        /// </summary>
        /// <param name="lineId">شناسه دیتابیسی آرتیکلی که باید از حافظه کش حذف شود</param>
        public void Delete(int lineId)
        {
            if (_cache.ContainsKey(_linesCacheKey))
            {
                var lines = _cache.Get<List<VoucherLineDetailViewModel>>(_linesCacheKey);
                lines.RemoveAll(line => line.Id == lineId);
                _cache.Set(_linesCacheKey, lines);
            }
        }

        /// <summary>
        /// اطلاعات مجموعه ای از آرتیکل ها را از مجموعه موجود در حافظه کش حذف می کند
        /// </summary>
        /// <param name="lineIds">شناسه های دیتابیسی آرتیکل هایی که باید از حافظه کش حذف شوند</param>
        public void Delete(IEnumerable<int> lineIds)
        {
            if (_cache.ContainsKey(_linesCacheKey))
            {
                var lines = _cache.Get<List<VoucherLineDetailViewModel>>(_linesCacheKey);
                lines.RemoveAll(line => lineIds.Contains(line.Id));
                _cache.Set(_linesCacheKey, lines);
            }
        }

        private const string _linesCacheKey = "lines";
        private readonly ICacheManager _cache;
    }
}
