﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.ViewModel.Metadata;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت اطلاعات پروژه ها را تعریف می کند.
    /// </summary>
    public interface IProjectRepository
    {
        /// <summary>
        /// به روش آسنکرون، کلیه پروژه هایی را که در دوره مالی و شعبه مشخص شده تعریف شده اند،
        /// از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="fpId">شناسه عددی یکی از دوره های مالی موجود</param>
        /// <param name="branchId">شناسه عددی یکی از شعب موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از پروژه های تعریف شده در دوره مالی و شعبه مشخص شده</returns>
        Task<IList<ProjectViewModel>> GetProjectsAsync(int fpId, int branchId, GridOptions gridOptions = null);

        /// <summary>
        /// به روش آسنکرون، تعداد پروژه های تعریف شده در دوره مالی و شعبه مشخص شده را
        /// از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="fpId">شناسه عددی یکی از دوره های مالی موجود</param>
        /// <param name="branchId">شناسه عددی یکی از شعب موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>تعداد پروژه های تعریف شده در دوره مالی و شعبه مشخص شده</returns>
        Task<int> GetCountAsync(int fpId, int branchId, GridOptions gridOptions = null);

        /// <summary>
        /// به روش آسنکرون، پروژه با شناسه عددی مشخص شده را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="projectId">شناسه عددی یکی از پروژه های موجود</param>
        /// <returns>پروژه مشخص شده با شناسه عددی</returns>
        Task<ProjectViewModel> GetProjectAsync(int projectId);

        /// <summary>
        /// به روش آسنکرون، اطلاعات فراداده ای تعریف شده برای پروژه را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <returns>اطلاعات فراداده ای تعریف شده برای پروژه</returns>
        Task<EntityItemViewModel<ProjectViewModel>> GetProjectMetadataAsync();

        /// <summary>
        /// به روش آسنکرون، اطلاعات یک پروژه را در محل ذخیره ایجاد یا اصلاح می کند
        /// </summary>
        /// <param name="project">پروژه مورد نظر برای ایجاد یا اصلاح</param>
        /// <returns>اطلاعات نمایشی پروژه ایجاد یا اصلاح شده</returns>
        Task<ProjectViewModel> SaveProjectAsync(ProjectViewModel project);

        /// <summary>
        /// به روش آسنکرون، پروژه مشخص شده با شناسه عددی را از محل ذخیره حذف می کند
        /// </summary>
        /// <param name="projectId">شناسه عددی پروژه مورد نظر برای حذف</param>
        Task DeleteProjectAsync(int projectId);

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که آیا کد پروژه مورد نظر تکراری است یا نه
        /// </summary>
        /// <param name="project">مدل نمایشی پروژه مورد نظر</param>
        /// <returns>اگر کد پروژه تکراری باشد مقدار "درست" و در غیر این صورت مقدار "نادرست" برمی گرداند</returns>
        Task<bool> IsDuplicateProjectAsync(ProjectViewModel project);

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که آیا پروژه انتخاب شده توسط رکوردهای اطلاعاتی دیگر
        /// در حال استفاده است یا نه
        /// </summary>
        /// <param name="projectId">شناسه یکتای یکی از پروژه های موجود</param>
        /// <returns>در حالتی که پروژه مشخص شده در حال استفاده باشد مقدار "درست" و در غیر این صورت
        /// مقدار "نادرست" را برمی گرداند</returns>
        Task<bool> IsUsedProjectAsync(int projectId);

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که آیا پروژه انتخاب شده دارای زیرمجموعه هست یا نه
        /// </summary>
        /// <param name="projectId">شناسه یکتای یکی از پروژه های موجود</param>
        /// <returns>در حالتی که پروژه مشخص شده دارای زیرمجموعه باشد مقدار "درست" و در غیر این صورت
        /// مقدار "نادرست" را برمی گرداند</returns>
        Task<bool?> HasChildrenAsync(int projectId);
    }
}