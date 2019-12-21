using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.ViewModel.Core;
using SPPC.Tadbir.ViewModel.Metadata;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت لاگ های عملیاتی برنامه را تعریف می کند
    /// </summary>
    public interface IOperationLogRepository
    {
        /// <summary>
        /// به روش آسنکرون، کلیه لاگ های عملیاتی موجود را برای شرکت و کاربر مشخص شده خوانده و برمی گرداند
        /// </summary>
        /// <param name="userId">شناسه دیتابیسی اختیاری برای فیلتر لاگ های عملیاتی برای یکی از کاربران</param>
        /// <param name="companyId">شناسه دیتابیسی اختیاری برای فیلتر لاگ های عملیاتی برای یکی از شرکت ها</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه لاگ های عملیاتی موجود</returns>
        Task<IList<SysOperationLogViewModel>> GetLogsAsync(
            int? userId, int? companyId, GridOptions gridOptions = null);

        /// <summary>
        /// به روش آسنکرون، تعداد سطرهای لاگ های عملیاتی را خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>تعداد سطرهای لاگ های عملیاتی</returns>
        Task<int> GetLogCountAsync(GridOptions gridOptions = null);

        /// <summary>
        /// به روش آسنکرون، اطلاعات داده شده برای یک لاگ عملیاتی جدید را ذخیره می کند
        /// </summary>
        /// <param name="operationLog">اطلاعات لاگ عملیاتی جدید</param>
        Task SaveLogAsync(OperationLogViewModel operationLog);
    }
}
