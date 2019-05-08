﻿using System;
using System.Threading.Tasks;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Values;
using SPPC.Tadbir.ViewModel.Auth;
using SPPC.Tadbir.ViewModel.Reporting;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای محاسبه اطلاعات گزارش دفتر روزنامه را تعریف می کند
    /// </summary>
    public interface IJournalRepository
    {
        /// <summary>
        /// اطلاعات محیطی و امنیتی کاربر جاری برنامه را برای کنترل قواعد کاری برنامه تنظیم می کند
        /// </summary>
        /// <param name="userContext">اطلاعات محیطی و امنیتی کاربر جاری برنامه</param>
        void SetCurrentContext(UserContextViewModel userContext);

        /// <summary>
        /// به روش آسنکرون، اطلاعات گزارش دفتر روزنامه بر حسب تاریخ را خوانده و برمی گرداند
        /// </summary>
        /// <param name="journalMode">حالت مورد نظر برای نمایش و جمع بندی اطلاعات</param>
        /// <param name="from">تاریخ ابتدا در دوره گزارشگیری مورد نظر</param>
        /// <param name="to">تاریخ انتها در دوره گزارشگیری مورد نظر</param>
        /// <param name="gridOptions">گزینه های برنامه برای فیلتر، مرتب سازی و صفحه بندی اطلاعات</param>
        /// <returns>اطلاعات دفتر روزنامه بر حسب تاریخ</returns>
        Task<JournalViewModel> GetJournalByDateAsync(
            JournalMode journalMode, DateTime from, DateTime to, GridOptions gridOptions = null);

        /// <summary>
        /// به روش آسنکرون، اطلاعات گزارش دفتر روزنامه بر حسب تاریخ و به تفکیک شعبه را خوانده و برمی گرداند
        /// </summary>
        /// <param name="journalMode">حالت مورد نظر برای نمایش و جمع بندی اطلاعات</param>
        /// <param name="from">تاریخ ابتدا در دوره گزارشگیری مورد نظر</param>
        /// <param name="to">تاریخ انتها در دوره گزارشگیری مورد نظر</param>
        /// <param name="gridOptions">گزینه های برنامه برای فیلتر، مرتب سازی و صفحه بندی اطلاعات</param>
        /// <returns>اطلاعات دفتر روزنامه بر حسب تاریخ و به تفکیک شعبه</returns>
        Task<JournalViewModel> GetJournalByDateByBranchAsync(
            JournalMode journalMode, DateTime from, DateTime to, GridOptions gridOptions = null);

        /// <summary>
        /// به روش آسنکرون، اطلاعات گزارش دفتر روزنامه بر حسب شماره سند را خوانده و برمی گرداند
        /// </summary>
        /// <param name="journalMode">حالت مورد نظر برای نمایش و جمع بندی اطلاعات</param>
        /// <param name="from">شماره ابتدا در دوره گزارشگیری مورد نظر</param>
        /// <param name="to">شماره انتها در دوره گزارشگیری مورد نظر</param>
        /// <param name="gridOptions">گزینه های برنامه برای فیلتر، مرتب سازی و صفحه بندی اطلاعات</param>
        /// <returns>اطلاعات دفتر روزنامه بر حسب شماره سند</returns>
        Task<JournalViewModel> GetJournalByNoAsync(
            JournalMode journalMode, int from, int to, GridOptions gridOptions = null);

        /// <summary>
        /// به روش آسنکرون، اطلاعات گزارش دفتر روزنامه بر حسب شماره سند و به تفکیک شعبه را خوانده و برمی گرداند
        /// </summary>
        /// <param name="journalMode">حالت مورد نظر برای نمایش و جمع بندی اطلاعات</param>
        /// <param name="from">شماره ابتدا در دوره گزارشگیری مورد نظر</param>
        /// <param name="to">شماره انتها در دوره گزارشگیری مورد نظر</param>
        /// <param name="gridOptions">گزینه های برنامه برای فیلتر، مرتب سازی و صفحه بندی اطلاعات</param>
        /// <returns>اطلاعات دفتر روزنامه بر حسب شماره سند و به تفکیک شعبه</returns>
        Task<JournalViewModel> GetJournalByNoByBranchAsync(
            JournalMode journalMode, int from, int to, GridOptions gridOptions = null);
    }
}