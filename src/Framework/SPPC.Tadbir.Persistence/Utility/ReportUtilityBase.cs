﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Extensions;
using SPPC.Framework.Mapper;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.ViewModel;

namespace SPPC.Tadbir.Persistence.Utility
{
    /// <summary>
    /// امکانات مشترک مرتبط با محاسبات گزارشی را پیاده سازی می کند
    /// </summary>
    public class ReportUtilityBase : IReportUtility
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="config">امکان مدیریت تنظیمات شرکتی را فراهم می کند</param>
        /// <param name="mapper">امکان تبدیل کلاس های مختلف به یکدیگر را فراهم می کند</param>
        public ReportUtilityBase(IConfigRepository config, IDomainMapper mapper)
        {
            Config = config;
            _mapper = mapper;
        }

        /// <summary>
        /// آرتیکل های فیلتر شده و تجمیع نشده را برای یک گزارش خوانده و برمی گرداند
        /// </summary>
        /// <typeparam name="TModel">نوع مدل نمایشی مورد نیاز برای نگاشت اطلاعات آرتیکل</typeparam>
        /// <param name="query">پرس و جوی آماده شده برای اعمال فیلترهای اضافه</param>
        /// <param name="from">تاریخ شروع در دوره گزارشگیری</param>
        /// <param name="to">تاریخ پایان در دوره گزارشگیری</param>
        /// <param name="gridOptions">فیلترهای سریع مورد نیاز که لازم است پیش از تجمیع اطلاعات گزارش اعمال شوند</param>
        /// <returns>آرتیکل های اولیه برای استفاده در گزارش</returns>
        public async Task<List<TModel>> GetRawReportByDateLinesAsync<TModel>(IQueryable<VoucherLine> query,
            DateTime from, DateTime to, GridOptions gridOptions)
        {
            var lines = await query
                .Where(art => art.Voucher.Date.IsBetween(from, to))
                .OrderBy(art => art.Voucher.Date)
                    .ThenBy(art => art.Voucher.No)
                .Select(art => _mapper.Map<TModel>(art))
                .ToListAsync();
            return lines
                .ApplyQuickFilter(gridOptions)
                .ToList();
        }

        /// <summary>
        /// آرتیکل های فیلتر شده و تجمیع نشده و به تفکیک شعبه را برای یک گزارش خوانده و برمی گرداند
        /// </summary>
        /// <typeparam name="TModel">نوع مدل نمایشی مورد نیاز برای نگاشت اطلاعات آرتیکل</typeparam>
        /// <param name="query">پرس و جوی آماده شده برای اعمال فیلترهای اضافه</param>
        /// <param name="from">تاریخ شروع در دوره گزارشگیری</param>
        /// <param name="to">تاریخ پایان در دوره گزارشگیری</param>
        /// <param name="gridOptions">فیلترهای سریع مورد نیاز که لازم است پیش از تجمیع اطلاعات گزارش اعمال شوند</param>
        /// <returns>آرتیکل های اولیه برای استفاده در گزارش</returns>
        public async Task<List<TModel>> GetRawReportByDateByBranchLinesAsync<TModel>(IQueryable<VoucherLine> query,
            DateTime from, DateTime to, GridOptions gridOptions)
        {
            var lines = await query
                .Where(art => art.Voucher.Date.IsBetween(from, to))
                .OrderBy(art => art.Voucher.Date)
                    .ThenBy(art => art.Voucher.No)
                        .ThenBy(art => art.BranchId)
                .Select(art => _mapper.Map<TModel>(art))
                .ToListAsync();
            return lines
                .ApplyQuickFilter(gridOptions)
                .ToList();
        }

        /// <summary>
        /// آرتیکل های فیلتر شده و تجمیع نشده را برای یک گزارش خوانده و برمی گرداند
        /// </summary>
        /// <typeparam name="TModel">نوع مدل نمایشی مورد نیاز برای نگاشت اطلاعات آرتیکل</typeparam>
        /// <param name="query">پرس و جوی آماده شده برای اعمال فیلترهای اضافه</param>
        /// <param name="from">شماره اولین سند در دوره گزارشگیری</param>
        /// <param name="to">شماره آخرین سند در دوره گزارشگیری</param>
        /// <param name="gridOptions">فیلترهای سریع مورد نیاز که لازم است پیش از تجمیع اطلاعات گزارش اعمال شوند</param>
        /// <returns>آرتیکل های اولیه برای استفاده در گزارش</returns>
        public async Task<List<TModel>> GetRawReportByNumberLinesAsync<TModel>(IQueryable<VoucherLine> query,
            int from, int to, GridOptions gridOptions)
        {
            var lines = await query
                .Where(art => art.Voucher.No >= from
                    && art.Voucher.No <= to)
                .OrderBy(art => art.Voucher.No)
                .Select(art => _mapper.Map<TModel>(art))
                .ToListAsync();
            return lines
                .ApplyQuickFilter(gridOptions)
                .ToList();
        }

        /// <summary>
        /// آرتیکل های فیلتر شده و تجمیع نشده و به تفکیک شعبه را برای یک گزارش خوانده و برمی گرداند
        /// </summary>
        /// <typeparam name="TModel">نوع مدل نمایشی مورد نیاز برای نگاشت اطلاعات آرتیکل</typeparam>
        /// <param name="query">پرس و جوی آماده شده برای اعمال فیلترهای اضافه</param>
        /// <param name="from">تاریخ شروع در دوره گزارشگیری</param>
        /// <param name="to">تاریخ پایان در دوره گزارشگیری</param>
        /// <param name="gridOptions">فیلترهای سریع مورد نیاز که لازم است پیش از تجمیع اطلاعات گزارش اعمال شوند</param>
        /// <returns>آرتیکل های اولیه برای استفاده در گزارش</returns>
        public async Task<List<TModel>> GetRawReportByNumberByBranchLinesAsync<TModel>(IQueryable<VoucherLine> query,
            int from, int to, GridOptions gridOptions)
        {
            var lines = await query
                .Where(art => art.Voucher.No >= from
                    && art.Voucher.No <= to)
                .OrderBy(art => art.Voucher.No)
                    .ThenBy(art => art.BranchId)
                .Select(art => _mapper.Map<TModel>(art))
                .ToListAsync();
            return lines
                .ApplyQuickFilter(gridOptions)
                .ToList();
        }

        /// <summary>
        /// آرتیکل های داده شده را بر حسب یکی از سطوح درختی مولفه حساب گروه بندی می کند
        /// </summary>
        /// <typeparam name="TModel">نوع مدل نمایشی ردیف های گزارش در نمای لیستی</typeparam>
        /// <param name="lines">فهرست آرتیکل های داده شده برای گروه بندی</param>
        /// <param name="groupLevel">سطح مورد نظر در ساختار درختی برای گروه بندی</param>
        /// <param name="lineFilter">فیلتر تکمیلی مورد نیاز برای اعمال پیش از گروه بندی</param>
        /// <returns>مجموعه گروه های به دست آمده با توجه به سایر پارامترهای متد</returns>
        public IEnumerable<IGrouping<string, TModel>> GetTurnoverGroups<TModel>(
            IEnumerable<TModel> lines, int groupLevel, Func<TModel, bool> lineFilter)
            where TModel : class, IAccountView
        {
            var selector = GetGroupSelector<TModel>(groupLevel);
            return GetGroupByItems(lines.Where(lineFilter), selector);
        }

        /// <summary>
        /// طول کد یکی از مولفه های حساب را در سطح داده شده خوانده و برمی گرداند
        /// </summary>
        /// <param name="viewId">شناسه دیتابیسی مولفه حساب مورد نظر</param>
        /// <param name="level">شماره سطح مورد نظر در ساختار درختی</param>
        /// <returns>طول کد مولفه حساب در سطح مورد نظر</returns>
        public int GetLevelCodeLength(int viewId, int level)
        {
            var fullConfig = Config
                .GetViewTreeConfigByViewAsync(viewId)
                .Result;
            var treeConfig = fullConfig.Current;
            int codeLength = treeConfig.Levels
                .Where(cfg => cfg.No <= level + 1)
                .Select(cfg => (int)cfg.CodeLength)
                .Sum();
            return codeLength;
        }

        /// <summary>
        /// مجموعه آرتیکل های داده شده را بر حسب یک فیلد گروه بندی می کند
        /// </summary>
        /// <typeparam name="TModel">نوع مدل نمایشی ردیف های گزارش در نمای لیستی</typeparam>
        /// <typeparam name="TKey1">نوع فیلد مورد نظر برای گروه بندی</typeparam>
        /// <param name="items">مجموعه آرتیکل های داده شده برای گروه بندی</param>
        /// <param name="selector1">عبارت لامدای مورد نظر برای گروه بندی</param>
        /// <returns>مجموعه ای از گروه های به دست آمده</returns>
        public IEnumerable<IGrouping<TKey1, TModel>> GetGroupByItems<TModel, TKey1>(
            IEnumerable<TModel> items, Func<TModel, TKey1> selector1)
        {
            foreach (var byFirst in items
                .OrderBy(selector1)
                .GroupBy(selector1))
            {
                yield return byFirst;
            }
        }

        /// <summary>
        /// مجموعه آرتیکل های داده شده را بر حسب دو فیلد گروه بندی می کند
        /// </summary>
        /// <typeparam name="TModel">نوع مدل نمایشی ردیف های گزارش در نمای لیستی</typeparam>
        /// <typeparam name="TKey1">نوع فیلد اول مورد نظر برای گروه بندی</typeparam>
        /// <typeparam name="TKey2">نوع فیلد دوم مورد نظر برای گروه بندی</typeparam>
        /// <param name="items">مجموعه آرتیکل های داده شده برای گروه بندی</param>
        /// <param name="selector1">عبارت لامدای اول برای گروه بندی</param>
        /// <param name="selector2">عبارت لامدای دوم برای گروه بندی</param>
        /// <returns>مجموعه ای از گروه های به دست آمده</returns>
        public IEnumerable<IGrouping<TKey2, TModel>> GetGroupByItems<TModel, TKey1, TKey2>(
            IEnumerable<TModel> items, Func<TModel, TKey1> selector1, Func<TModel, TKey2> selector2)
        {
            foreach (var byFirst in items
                .OrderBy(selector1)
                .GroupBy(selector1))
            {
                foreach (var bySecond in byFirst
                    .OrderBy(selector2)
                    .GroupBy(selector2))
                {
                    yield return bySecond;
                }
            }
        }

        /// <summary>
        /// مجموعه آرتیکل های داده شده را بر حسب سه فیلد گروه بندی می کند
        /// </summary>
        /// <typeparam name="TModel">نوع مدل نمایشی ردیف های گزارش در نمای لیستی</typeparam>
        /// <typeparam name="TKey1">نوع فیلد اول مورد نظر برای گروه بندی</typeparam>
        /// <typeparam name="TKey2">نوع فیلد دوم مورد نظر برای گروه بندی</typeparam>
        /// <typeparam name="TKey3">نوع فیلد سوم مورد نظر برای گروه بندی</typeparam>
        /// <param name="items">مجموعه آرتیکل های داده شده برای گروه بندی</param>
        /// <param name="selector1">عبارت لامدای اول برای گروه بندی</param>
        /// <param name="selector2">عبارت لامدای دوم برای گروه بندی</param>
        /// <param name="selector3">عبارت لامدای سوم برای گروه بندی</param>
        /// <returns>مجموعه ای از گروه های به دست آمده</returns>
        public IEnumerable<IGrouping<TKey3, TModel>> GetGroupByItems<TModel, TKey1, TKey2, TKey3>(
            IEnumerable<TModel> items, Func<TModel, TKey1> selector1, Func<TModel, TKey2> selector2,
            Func<TModel, TKey3> selector3)
        {
            foreach (var byFirst in items
                .OrderBy(selector1)
                .GroupBy(selector1))
            {
                foreach (var bySecond in byFirst
                    .OrderBy(selector2)
                    .GroupBy(selector2))
                {
                    foreach (var byThird in bySecond
                        .OrderBy(selector3)
                        .GroupBy(selector3))
                    {
                        yield return byThird;
                    }
                }
            }
        }

        /// <summary>
        /// امکان مدیریت تنظیمات شرکتی را فراهم می کند
        /// </summary>
        protected IConfigRepository Config { get; }

        /// <summary>
        /// عبارت لامدای مورد نیاز برای گروه بندی مولفه های حساب را برمی گرداند
        /// </summary>
        /// <typeparam name="TModel">مدل نمایشی سطر گزارش در نمای لیستی</typeparam>
        /// <param name="groupLevel">شماره سطح درختی مورد نظر برای گروه بندی</param>
        /// <returns>عبارت لامدای مورد نیاز برای گروه بندی</returns>
        protected virtual Func<TModel, string> GetGroupSelector<TModel>(int groupLevel)
            where TModel : class, IAccountView
        {
            int codeLength = GetLevelCodeLength(ViewName.Account, groupLevel);
            return item => item.AccountFullCode.Substring(0, codeLength);
        }

        private readonly IDomainMapper _mapper;
    }
}
