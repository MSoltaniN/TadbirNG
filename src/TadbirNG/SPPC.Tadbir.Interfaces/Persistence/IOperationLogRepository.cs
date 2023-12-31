﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Utility;
using SPPC.Tadbir.ViewModel.Core;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت لاگ های عملیاتی برنامه را تعریف می کند
    /// </summary>
    public interface IOperationLogRepository
    {
        #region Company Log Operations

        /// <summary>
        /// به روش آسنکرون، کلیه لاگ های عملیات شرکتی موجود را  خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه لاگ های شرکتی موجود</returns>
        Task<PagedList<OperationLogViewModel>> GetLogsAsync(GridOptions gridOptions);

        /// <summary>
        /// به روش آسنکرون، کلیه لاگ های شرکتی بایگانی شده را  خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه لاگ های شرکتی بایگانی شده</returns>
        Task<PagedList<OperationLogViewModel>> GetLogsArchiveAsync(GridOptions gridOptions);

        /// <summary>
        /// به روش آسنکرون، کلیه لاگ های عملیات شرکتی موجود را به همراه لاگ های بایگانی شده
        /// خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه لاگ های شرکتی موجود و لاگ های بایگانی شده</returns>
        Task<PagedList<OperationLogViewModel>> GetMergedLogsAsync(GridOptions gridOptions);

        /// <summary>
        /// به روش آسنکرون، اطلاعات داده شده برای یک لاگ عملیات شرکتی جدید را ذخیره می کند
        /// </summary>
        /// <param name="operationLog">اطلاعات لاگ عملیاتی جدید</param>
        Task SaveLogAsync(OperationLogViewModel operationLog);

        /// <summary>
        /// به روش آسنکرون، کلیه رویدادهای شرکتی ثبت شده در محدوده تاریخی داده شده را به بایگانی منتقل می کند
        /// </summary>
        /// <param name="from">ابتدای محدوده تاریخی برای بایگانی</param>
        /// <param name="to">انتهای محدوده تاریخی برای بایگانی</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        Task MoveLogsToArchiveAsync(DateTime from, DateTime to, GridOptions gridOptions);

        /// <summary>
        /// به روش آسنکرون، رویدادهای شرکتی انتخاب شده را به بایگانی منتقل می کند
        /// </summary>
        /// <param name="archivedIds">مجموعه شناسه های دیتابیسی رکوردهای انتخاب شده برای بایگانی</param>
        Task MoveLogsToArchiveAsync(IEnumerable<int> archivedIds);

        /// <summary>
        /// به روش آسنکرون، کلیه رویدادهای بایگانی شده در محدوده تاریخی داده شده را
        /// در لاگ های شرکتی بازیابی می کند
        /// </summary>
        /// <param name="from">ابتدای محدوده تاریخی برای بازیابی</param>
        /// <param name="to">انتهای محدوده تاریخی برای بازیابی</param>
        Task RecoverLogsFromArchive(DateTime from, DateTime to);

        /// <summary>
        /// مجموعه ای از لاگ های شرکتی بایگانی شده را به صورت گروهی حذف می کند
        /// </summary>
        /// <param name="deletedIds">مجموعه شناسه های دیتابیسی رکوردهای انتخاب شده برای حذف</param>
        Task DeleteArchivedLogsAsync(IEnumerable<int> deletedIds);

        #endregion

        #region System Log Operations

        /// <summary>
        /// به روش آسنکرون، کلیه لاگ های عملیات سیستمی موجود را  خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه لاگ های سیستمی موجود</returns>
        Task<PagedList<OperationLogViewModel>> GetSystemLogsAsync(GridOptions gridOptions);

        /// <summary>
        /// به روش آسنکرون، کلیه لاگ های سیستمی بایگانی شده را  خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه لاگ های سیستمی بایگانی شده</returns>
        Task<PagedList<OperationLogViewModel>> GetSystemLogsArchiveAsync(GridOptions gridOptions);

        /// <summary>
        /// به روش آسنکرون، کلیه لاگ های عملیات سیستمی موجود را به همراه لاگ های بایگانی شده
        /// خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه لاگ های سیستمی موجود و لاگ های بایگانی شده</returns>
        Task<PagedList<OperationLogViewModel>> GetMergedSystemLogsAsync(GridOptions gridOptions);

        /// <summary>
        /// به روش آسنکرون، اطلاعات داده شده برای یک لاگ عملیات سیستمی جدید را ذخیره می کند
        /// </summary>
        /// <param name="operationLog">اطلاعات لاگ عملیاتی جدید</param>
        Task SaveSystemLogAsync(OperationLogViewModel operationLog);

        /// <summary>
        /// به روش آسنکرون، کلیه رویدادهای سیستمی ثبت شده در محدوده تاریخی داده شده را به بایگانی منتقل می کند
        /// </summary>
        /// <param name="from">ابتدای محدوده تاریخی برای بایگانی</param>
        /// <param name="to">انتهای محدوده تاریخی برای بایگانی</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        Task MoveSystemLogsToArchiveAsync(DateTime from, DateTime to, GridOptions gridOptions);

        /// <summary>
        /// به روش آسنکرون، رویدادهای سیستمی انتخاب شده را به بایگانی منتقل می کند
        /// </summary>
        /// <param name="archivedIds">مجموعه شناسه های دیتابیسی رکوردهای انتخاب شده برای بایگانی</param>
        Task MoveSystemLogsToArchiveAsync(IEnumerable<int> archivedIds);

        /// <summary>
        /// به روش آسنکرون، کلیه رویدادهای بایگانی شده در محدوده تاریخی داده شده را
        /// در لاگ های سیستمی بازیابی می کند
        /// </summary>
        /// <param name="from">ابتدای محدوده تاریخی برای بازیابی</param>
        /// <param name="to">انتهای محدوده تاریخی برای بازیابی</param>
        Task RecoverSystemLogsFromArchive(DateTime from, DateTime to);

        /// <summary>
        /// مجموعه ای از لاگ های سیستمی بایگانی شده را به صورت گروهی حذف می کند
        /// </summary>
        /// <param name="deletedIds">مجموعه شناسه های دیتابیسی رکوردهای انتخاب شده برای حذف</param>
        Task DeleteArchivedSystemLogsAsync(IEnumerable<int> deletedIds);

        #endregion
    }
}
