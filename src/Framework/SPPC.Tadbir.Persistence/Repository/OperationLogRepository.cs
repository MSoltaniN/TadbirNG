using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Common;
using SPPC.Framework.Extensions;
using SPPC.Framework.Mapper;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Model.Core;
using SPPC.Tadbir.ViewModel.Core;
using SPPC.Tadbir.ViewModel.Metadata;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت لاگ های عملیاتی برنامه را پیاده سازی می کند
    /// </summary>
    public class OperationLogRepository : RepositoryBase, IOperationLogRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="unitOfWork">پیاده سازی اینترفیس واحد کاری برای انجام عملیات دیتابیسی</param>
        /// <param name="mapper">نگاشت مورد استفاده برای تبدیل کلاس های مدل اطلاعاتی</param>
        /// <param name="metadata">امکان خواندن متادیتا برای یک موجودیت را فراهم می کند</param>
        public OperationLogRepository(IAppUnitOfWork unitOfWork, IDomainMapper mapper, IMetadataRepository metadata)
            : base(unitOfWork, mapper, metadata)
        {
            UnitOfWork.UseSystemContext();
        }

        /// <summary>
        /// به روش آسنکرون، کلیه لاگ های عملیاتی موجود را برای شرکت و کاربر مشخص شده خوانده و برمی گرداند
        /// </summary>
        /// <param name="userId">شناسه دیتابیسی اختیاری برای فیلتر لاگ های عملیاتی برای یکی از کاربران</param>
        /// <param name="companyId">شناسه دیتابیسی اختیاری برای فیلتر لاگ های عملیاتی برای یکی از شرکت ها</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه لاگ های عملیاتی موجود</returns>
        public async Task<IList<OperationLogViewModel>> GetLogsAsync(
            int? userId, int? companyId, GridOptions gridOptions = null)
        {
            var repository = UnitOfWork.GetAsyncRepository<OperationLog>();
            var query = repository.GetEntityQuery(log => log.User, log => log.Company);
            if (companyId.HasValue)
            {
                query = query.Where(log => log.Company.Id == companyId.Value);
            }

            if (userId.HasValue)
            {
                query = query.Where(log => log.User.Id == userId.Value);
            }

            return await query
                .OrderByDescending(log => log.Date)
                .ThenByDescending(log => log.Time)
                .Select(log => Mapper.Map<OperationLogViewModel>(log))
                .Apply(gridOptions)
                .ToListAsync();
        }

        /// <summary>
        /// به روش آسنکرون، تعداد سطرهای لاگ های عملیاتی را خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>تعداد سطرهای لاگ های عملیاتی</returns>
        public async Task<int> GetLogCountAsync(GridOptions gridOptions = null)
        {
            var repository = UnitOfWork.GetAsyncRepository<OperationLog>();
            var count = await repository.GetCountByCriteriaAsync(null, gridOptions);
            return count;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات فراداده ای تعریف شده برای لاگ عملیاتی را خوانده و برمی گرداند
        /// </summary>
        /// <returns>اطلاعات فراداده ای تعریف شده برای لاگ عملیاتی</returns>
        public async Task<ViewViewModel> GetLogMetadataAsync()
        {
            return await Metadata.GetViewMetadataAsync<OperationLog>();
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات داده شده برای یک لاگ عملیاتی جدید را ذخیره می کند
        /// </summary>
        /// <param name="operationLog">اطلاعات لاگ عملیاتی جدید</param>
        public async Task SaveLogAsync(OperationLogViewModel operationLog)
        {
            Verify.ArgumentNotNull(operationLog, "operationLog");
            var repository = UnitOfWork.GetAsyncRepository<OperationLog>();
            var newLog = Mapper.Map<OperationLog>(operationLog);
            repository.Insert(newLog);
            await UnitOfWork.CommitAsync();
        }
    }
}
