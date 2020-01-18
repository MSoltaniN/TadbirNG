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

        #endregion

    }
}
