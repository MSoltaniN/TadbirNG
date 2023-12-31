﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using SPPC.Tadbir.Configuration.Models;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.ViewModel.Config;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای ذخیره و بازیابی تنظیمات برنامه را تعریف می کند
    /// </summary>
    public interface IConfigRepository : IBaseConfigRepository
    {
        /// <summary>
        /// محدوده تاریخی پیش فرض را با توجه به دوره مالی جاری برنامه خوانده و برمی گرداند
        /// </summary>
        /// <param name="start">پارامتر خروجی برای تنظیم تاریخ ابتدا در محدوده تاریخی پیش فرض</param>
        /// <param name="end">پارامتر خروجی برای تنظیم تاریخ انتها در محدوده تاریخی پیش فرض</param>
        void GetDefaultFiscalDateRange(out DateTime start, out DateTime end);

        /// <summary>
        /// محدوده تاریخی دوره مالی جاری برنامه را خوانده و برمی گرداند
        /// </summary>
        /// <param name="start">پارامتر خروجی برای تنظیم تاریخ ابتدا در محدوده تاریخی</param>
        /// <param name="end">پارامتر خروجی برای تنظیم تاریخ انتها در محدوده تاریخی</param>
        void GetCurrentFiscalDateRange(out DateTime start, out DateTime end);

        /// <summary>
        /// به روش آسنکرون، نوع تقویم پیش فرض برنامه را خوانده و برمی گرداند
        /// </summary>
        /// <returns>مقدار عددی متناظر با نوع شمارشی موجود برای تقویم پیش فرض</returns>
        Task<CalendarType> GetCurrentCalendarTypeAsync();

        /// <summary>
        /// به روش آسنکرون، تقویم پیش فرض برنامه را خوانده و برمی گرداند
        /// </summary>
        /// <returns>پیاده سازی استاندارد موجود برای تقویم پیش فرض</returns>
        Task<Calendar> GetCurrentCalendarAsync();

        /// <summary>
        /// به روش آسنکرون، تاریخ داده شده را با توجه به تنظیمات تقویم پیش فرض به صورت رشته متنی برمی گرداند
        /// </summary>
        /// <param name="date">تاریخ مورد نظر برای نمایش متنی</param>
        /// <returns>تاریخ داده شده به صورت رشته متنی</returns>
        Task<string> GetDateDisplayAsync(DateTime date);

        /// <summary>
        /// به روش آسنکرون، تنظیمات کاربری موجود برای جستجوی سریع در یکی از فرم های لیستی را
        /// برای کاربر مشخص شده خوانده و برمی گرداند
        /// </summary>
        /// <param name="userId">شناسه دیتابیسی یکی از کاربران موجود</param>
        /// <param name="viewId">شناسه دیتابیسی یکی از مدل های نمایشی موجود</param>
        /// <returns>تنظیمات کاربری موجود برای جستجوی سریع</returns>
        Task<QuickSearchConfig> GetQuickSearchConfigAsync(int userId, int viewId);

        /// <summary>
        /// به روش آسنکرون، تنظیمات کاربری برای جستجوی سریع در یکی از فرم های لیستی را ذخیره می کند
        /// </summary>
        /// <param name="userId">شناسه دیتابیسی یکی از کاربران موجود</param>
        /// <param name="userConfig">تنظیمات کاربری برای جستجوی سریع</param>
        Task SaveQuickSearchConfigAsync(int userId, QuickSearchConfig userConfig);

        /// <summary>
        /// به روش آسنکرون، تنظیمات موجود برای ساختار همه نماهای درختی را خوانده و برمی گرداند
        /// </summary>
        /// <returns>تنظیمات موجود برای ساختار همه نماهای درختی</returns>
        Task<IList<ViewTreeFullConfig>> GetAllViewTreeConfigAsync();

        /// <summary>
        /// به روش آسنکرون، تنظیمات موجود برای ساختار نمای درختی مشخص شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="viewId">شناسه دیتابیسی یکی از مدل های نمایشی موجود</param>
        /// <returns>تنظیمات موجود برای ساختار نمای درختی مشخص شده</returns>
        Task<ViewTreeFullConfig> GetViewTreeConfigByViewAsync(int viewId);

        /// <summary>
        /// به روش آسنکرون، آخرین تغییرات مجموعه ای از تنظیمات نماهای درختی را ذخیره می کند
        /// </summary>
        /// <param name="configItems">مجموعه ای از تنظیمات نماهای درختی</param>
        Task SaveViewTreeConfigAsync(List<ViewTreeFullConfig> configItems);

        /// <summary>
        /// به روش آسنکرون،وضعیت استفاده از یک سطح از ساختار درختی را برای یکی از موجودیت های درختی بروزرسانی می کند
        /// </summary>
        /// <param name="viewId">شناسه دیتابیسی یکی از مدل های درختی موجود</param>
        /// <param name="level">شماره سطحی که وضعیت استفاده از آن باید تغییر کند</param>
        /// <param name="itemCount">تعداد سطرهای اطلاعاتی موجود در سطح مورد نظر</param>
        Task SaveTreeLevelUsageAsync(int viewId, int level, int itemCount);

        /// <summary>
        /// به روش آسنکرون، آخرین وضعیت پیکربندی سیستم را ذخیره می کند
        /// </summary>
        /// <param name="configItem">تنظیمات پیکربندی سیستم</param>
        Task SaveSystemConfigAsync(SettingBriefViewModel configItem);

        /// <summary>
        /// به روش آسنکرون، امکان تغییر سیستم ثبت دوره مالی جاری را بررسی می کند
        /// </summary>
        /// <returns>آیا تغییر سیستم ثبت دوره مالی جاری امکان دارد</returns>
        Task<bool> ValidateInventoryModeChangeAsync();

        /// <summary>
        /// به روش آسنکرون، تنظیمات موجود برای عناوین سفارشی فرم گزارشی مشخص شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="formId">شناسه دیتابیسی فرم گزارشی</param>
        /// <param name="localeId">شناسه دیتابیسی زبان مورد نظر برای محلی سازی متن عناوین</param>
        /// <returns>تنظیمات موجود برای عناوین سفارشی</returns>
        Task<FormLabelFullConfig> GetFormLabelConfigAsync(int formId, int localeId);

        /// <summary>
        /// به روش آسنکرون، آخرین وضعیت عناوین سفارشی یک فرم گزارشی را ذخیره می کند
        /// </summary>
        /// <param name="labelConfig">اطلاعات تنظیمات عناوین سفارشی</param>
        Task SaveFormLabelConfigAsync(FormLabelConfig labelConfig);

        /// <summary>
        /// به روش آسنکرون، تنظیمات کاربری موجود را خوانده و برمی گرداند
        /// </summary>
        /// <param name="userId">شناسه دیتابیسی کاربر مورد نظر</param>
        /// <returns>مقادیر جاری تنظیمات کاربری برای کاربر مورد نظر</returns>
        Task<UserProfileConfig> GetUserProfileConfigAsync(int userId);

        /// <summary>
        /// به روش آسنکرون، آخرین وضعیت تنظیمات کاربری را ذخیره می کند
        /// </summary>
        /// <param name="userId">شناسه دیتابیسی کاربر مورد نظر</param>
        /// <param name="profile">آخرین وضعیت تنظیمات کاربری برای کاربر مورد نظر</param>
        Task SaveUserProfileConfigAsync(int userId, UserProfileConfig profile);

        /// <summary>
        ///
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        int GetLevelCodeLength(int level);

        /// <summary>
        ///
        /// </summary>
        /// <param name="viewId"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        int GetLevelCodeLength(int viewId, int level);
    }
}
