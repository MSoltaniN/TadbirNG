﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.ViewModel.Auth;
using SPPC.Tadbir.ViewModel.Metadata;
using SPPC.Tadbir.ViewModel.Reporting;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای تهیه و محاسبه اطلاعات گزارش های برنامه را تعریف می کند
    /// </summary>
    public interface IReportRepository
    {
        /// <summary>
        /// اطلاعات محیطی و امنیتی کاربر جاری برنامه را برای کنترل قواعد کاری برنامه تنظیم می کند
        /// </summary>
        /// <param name="userContext">اطلاعات محیطی و امنیتی کاربر جاری برنامه</param>
        void SetCurrentContext(UserContextViewModel userContext);

        /// <summary>
        /// اطلاعات فراداده ای یکی از نماهای اطلاعاتی گزارشی را خوانده و برمی گرداند
        /// </summary>
        /// <param name="viewId">شناسه دیتابیسی نمای اطلاعاتی مورد نظر</param>
        /// <returns>اطلاعات فراداده ای نمای گزارشی</returns>
        Task<ViewViewModel> GetReportMetadataByViewAsync(int viewId);

        /// <summary>
        /// به روش آسنکرون، اطلاعات مورد نیاز در گزارش خلاصه اسناد حسابداری را خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های برنامه برای فیلتر، مرتب سازی و صفحه بندی اطلاعات</param>
        /// <returns>اطلاعات گزارش خلاصه اسناد حسابداری</returns>
        Task<IList<VoucherSummaryViewModel>> GetVoucherSummaryByDateReportAsync(
            GridOptions gridOptions);

        /// <summary>
        /// به روش آسنکرون، نعداد سطرهای اطلاعاتی در گزارش خلاصه اسناد حسابداری را خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های برنامه برای فیلتر، مرتب سازی و صفحه بندی اطلاعات</param>
        /// <returns>تعداد سطرهای گزارش خلاصه اسناد حسابداری</returns>
        Task<int> GetVoucherSummaryByDateCountAsync(GridOptions gridOptions);

        /// <summary>
        /// به روش آسنکرون، اطلاعات مورد نیاز در گزارش فرم مرسوم سند را خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های برنامه برای فیلتر، مرتب سازی و صفحه بندی اطلاعات</param>
        /// <param name="withDetail">مشخص می کند که آیا جزییات سطوح شناور نیز مورد نیاز است یا نه</param>
        /// <returns>اطلاعات گزارش فرم مرسوم سند</returns>
        Task<StandardVoucherViewModel> GetStandardVoucherFormAsync(
            GridOptions gridOptions, bool withDetail = false);
    }
}
