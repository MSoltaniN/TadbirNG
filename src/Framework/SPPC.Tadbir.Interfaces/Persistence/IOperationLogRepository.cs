using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SPPC.Framework.Presentation;
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
        Task<IList<OperationLogViewModel>> GetLogsAsync(GridOptions gridOptions = null);

        /// <summary>
        /// به روش آسنکرون، تعداد سطرهای لاگ های عملیات شرکتی را خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>تعداد سطرهای لاگ های شرکتی</returns>
        Task<int> GetLogCountAsync(GridOptions gridOptions = null);

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
        Task MoveLogsToArchiveAsync(DateTime from, DateTime to);

        /// <summary>
        /// به روش آسنکرون، کلیه رویدادهای بایگانی شده در محدوده تاریخی داده شده را
        /// در لاگ های شرکتی بازیابی می کند
        /// </summary>
        /// <param name="from">ابتدای محدوده تاریخی برای بازیابی</param>
        /// <param name="to">انتهای محدوده تاریخی برای بازیابی</param>
        Task RecoverLogsFromArchive(DateTime from, DateTime to);

        #endregion

        #region System Log Operations

        /// <summary>
        /// به روش آسنکرون، کلیه لاگ های عملیات سیستمی موجود را  خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه لاگ های سیستمی موجود</returns>
        Task<IList<OperationLogViewModel>> GetSystemLogsAsync(GridOptions gridOptions = null);

        /// <summary>
        /// به روش آسنکرون، تعداد سطرهای لاگ های عملیات سیستمی را خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>تعداد سطرهای لاگ های سیستمی</returns>
        Task<int> GetSystemLogCountAsync(GridOptions gridOptions = null);

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
        Task MoveSystemLogsToArchiveAsync(DateTime from, DateTime to);

        /// <summary>
        /// به روش آسنکرون، کلیه رویدادهای بایگانی شده در محدوده تاریخی داده شده را
        /// در لاگ های سیستمی بازیابی می کند
        /// </summary>
        /// <param name="from">ابتدای محدوده تاریخی برای بازیابی</param>
        /// <param name="to">انتهای محدوده تاریخی برای بازیابی</param>
        Task RecoverSystemLogsFromArchive(DateTime from, DateTime to);

        #endregion

    }
}
