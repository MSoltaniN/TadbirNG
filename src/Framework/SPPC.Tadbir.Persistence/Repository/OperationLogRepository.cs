using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Common;
using SPPC.Framework.Extensions;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Model.Auth;
using SPPC.Tadbir.Model.Config;
using SPPC.Tadbir.Model.Core;
using SPPC.Tadbir.ViewModel.Core;

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
        public OperationLogRepository(IRepositoryContext context)
            : base(context)
        {
        }

        #region Company Log Operations

        /// <summary>
        /// به روش آسنکرون، کلیه لاگ های عملیاتی موجود را برای شرکت و کاربر مشخص شده خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه لاگ های عملیاتی موجود</returns>
        public async Task<IList<OperationLogViewModel>> GetLogsAsync(GridOptions gridOptions = null)
        {
            var repository = UnitOfWork.GetAsyncRepository<OperationLog>();
            var list = await repository.GetEntityQuery(
                log => log.Branch, log => log.EntityType, log => log.FiscalPeriod,
                log => log.Operation, log => log.Source, log => log.SourceList)
                .OrderByDescending(log => log.Date)
                .ThenByDescending(log => log.Time)
                .Select(log => Mapper.Map<OperationLogViewModel>(log))
                .Apply(gridOptions)
                .ToListAsync();
            foreach (var item in list)
            {
                await SetSystemValues(item);
            }

            return list;
        }

        /// <summary>
        /// به روش آسنکرون، کلیه لاگ های شرکتی بایگانی شده را  خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه لاگ های شرکتی بایگانی شده</returns>
        public async Task<IList<OperationLogViewModel>> GetLogsArchiveAsync(GridOptions gridOptions = null)
        {
            var repository = UnitOfWork.GetAsyncRepository<OperationLogArchive>();
            var list = await repository.GetEntityQuery(
                log => log.Branch, log => log.EntityType, log => log.FiscalPeriod,
                log => log.Operation, log => log.Source, log => log.SourceList)
                .OrderByDescending(log => log.Date)
                .ThenByDescending(log => log.Time)
                .Select(log => Mapper.Map<OperationLogViewModel>(log))
                .Apply(gridOptions)
                .ToListAsync();
            foreach (var item in list)
            {
                await SetSystemValues(item);
            }

            return list;
        }

        /// <summary>
        /// به روش آسنکرون، تعداد سطرهای لاگ های عملیاتی را خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>تعداد سطرهای لاگ های عملیاتی</returns>
        public async Task<int> GetLogCountAsync(GridOptions gridOptions = null)
        {
            var repository = UnitOfWork.GetAsyncRepository<OperationLog>();
            var items = await repository.GetAllAsync();
            return items
                .Select(log => Mapper.Map<OperationLogViewModel>(log))
                .Apply(gridOptions, false)
                .Count();
        }

        /// <summary>
        /// به روش آسنکرون، تعداد سطرهای لاگ های شرکتی بایگانی شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>تعداد سطرهای لاگ های شرکتی بایگانی شده</returns>
        public async Task<int> GetLogArchiveCountAsync(GridOptions gridOptions = null)
        {
            var repository = UnitOfWork.GetAsyncRepository<OperationLogArchive>();
            var items = await repository.GetAllAsync();
            return items
                .Select(log => Mapper.Map<OperationLogViewModel>(log))
                .Apply(gridOptions, false)
                .Count();
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

        /// <summary>
        /// به روش آسنکرون، کلیه رویدادهای شرکتی ثبت شده در محدوده تاریخی داده شده را به بایگانی منتقل می کند
        /// </summary>
        /// <param name="from">ابتدای محدوده تاریخی برای بایگانی</param>
        /// <param name="to">انتهای محدوده تاریخی برای بایگانی</param>
        public async Task MoveLogsToArchiveAsync(DateTime from, DateTime to)
        {
            var repository = UnitOfWork.GetAsyncRepository<OperationLog>();
            var archiveRepository = UnitOfWork.GetAsyncRepository<OperationLogArchive>();
            var logs = await repository.GetByCriteriaAsync(log => log.Date.Date.IsBetween(from, to));
            foreach (var log in logs)
            {
                var archive = Mapper.Map<OperationLogArchive>(log);
                archiveRepository.Insert(archive);
                repository.Delete(log);
            }

            await UnitOfWork.CommitAsync();
        }

        /// <summary>
        /// به روش آسنکرون، کلیه رویدادهای بایگانی شده در محدوده تاریخی داده شده را
        /// در لاگ های شرکتی بازیابی می کند
        /// </summary>
        /// <param name="from">ابتدای محدوده تاریخی برای بازیابی</param>
        /// <param name="to">انتهای محدوده تاریخی برای بازیابی</param>
        public async Task RecoverLogsFromArchive(DateTime from, DateTime to)
        {
            var repository = UnitOfWork.GetAsyncRepository<OperationLog>();
            var archiveRepository = UnitOfWork.GetAsyncRepository<OperationLogArchive>();
            var archived = await archiveRepository.GetByCriteriaAsync(log => log.Date.Date.IsBetween(from, to));
            foreach (var item in archived)
            {
                var log = Mapper.Map<OperationLog>(item);
                log.Id = 0;
                repository.Insert(log);
                archiveRepository.Delete(item);
            }

            await UnitOfWork.CommitAsync();
        }

        #endregion

        #region System Log Operations

        /// <summary>
        /// به روش آسنکرون، کلیه لاگ های عملیات سیستمی موجود را  خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه لاگ های سیستمی موجود</returns>
        public async Task<IList<OperationLogViewModel>> GetSystemLogsAsync(GridOptions gridOptions = null)
        {
            UnitOfWork.UseSystemContext();
            var repository = UnitOfWork.GetAsyncRepository<SysOperationLog>();
            var list = await repository.GetEntityQuery(
                    log => log.Operation, log => log.EntityType,
                    log => log.Source, log => log.SourceList,
                    log => log.Company, log => log.User)
                .OrderByDescending(log => log.Date)
                .ThenByDescending(log => log.Time)
                .Select(log => Mapper.Map<OperationLogViewModel>(log))
                .Apply(gridOptions)
                .ToListAsync();
            UnitOfWork.UseCompanyContext();
            return list;
        }

        /// <summary>
        /// به روش آسنکرون، کلیه لاگ های سیستمی بایگانی شده را  خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه لاگ های سیستمی بایگانی شده</returns>
        public async Task<IList<OperationLogViewModel>> GetSystemLogsArchiveAsync(GridOptions gridOptions = null)
        {
            UnitOfWork.UseSystemContext();
            var repository = UnitOfWork.GetAsyncRepository<SysOperationLogArchive>();
            var list = await repository.GetEntityQuery(
                    log => log.Operation, log => log.EntityType,
                    log => log.Source, log => log.SourceList,
                    log => log.Company, log => log.User)
                .OrderByDescending(log => log.Date)
                .ThenByDescending(log => log.Time)
                .Select(log => Mapper.Map<OperationLogViewModel>(log))
                .Apply(gridOptions)
                .ToListAsync();
            UnitOfWork.UseCompanyContext();
            return list;
        }

        /// <summary>
        /// به روش آسنکرون، تعداد سطرهای لاگ های عملیات سیستمی را خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>تعداد سطرهای لاگ های سیستمی</returns>
        public async Task<int> GetSystemLogCountAsync(GridOptions gridOptions = null)
        {
            UnitOfWork.UseSystemContext();
            var repository = UnitOfWork.GetAsyncRepository<SysOperationLog>();
            int count = await repository.GetEntityQuery()
                .Select(log => Mapper.Map<OperationLogViewModel>(log))
                .Apply(gridOptions, false)
                .CountAsync();
            UnitOfWork.UseCompanyContext();
            return count;
        }

        /// <summary>
        /// به روش آسنکرون، تعداد سطرهای لاگ های سیستمی بایگانی شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>تعداد سطرهای لاگ های سیستمی بایگانی شده</returns>
        public async Task<int> GetSystemLogArchiveCountAsync(GridOptions gridOptions = null)
        {
            UnitOfWork.UseSystemContext();
            var repository = UnitOfWork.GetAsyncRepository<SysOperationLogArchive>();
            int count = await repository.GetEntityQuery()
                .Select(log => Mapper.Map<OperationLogViewModel>(log))
                .Apply(gridOptions, false)
                .CountAsync();
            UnitOfWork.UseCompanyContext();
            return count;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات داده شده برای یک لاگ عملیات سیستمی جدید را ذخیره می کند
        /// </summary>
        /// <param name="operationLog">اطلاعات لاگ عملیاتی جدید</param>
        public async Task SaveSystemLogAsync(OperationLogViewModel operationLog)
        {
            Verify.ArgumentNotNull(operationLog, "operationLog");
            UnitOfWork.UseSystemContext();
            var repository = UnitOfWork.GetAsyncRepository<SysOperationLog>();
            var newLog = Mapper.Map<SysOperationLog>(operationLog);
            repository.Insert(newLog);
            await UnitOfWork.CommitAsync();
            UnitOfWork.UseCompanyContext();
        }

        /// <summary>
        /// به روش آسنکرون، کلیه رویدادهای سیستمی ثبت شده در محدوده تاریخی داده شده را به بایگانی منتقل می کند
        /// </summary>
        /// <param name="from">ابتدای محدوده تاریخی برای بایگانی</param>
        /// <param name="to">انتهای محدوده تاریخی برای بایگانی</param>
        public async Task MoveSystemLogsToArchiveAsync(DateTime from, DateTime to)
        {
            UnitOfWork.UseSystemContext();
            var repository = UnitOfWork.GetAsyncRepository<SysOperationLog>();
            var archiveRepository = UnitOfWork.GetAsyncRepository<SysOperationLogArchive>();
            var logs = await repository.GetByCriteriaAsync(log => log.Date.Date.IsBetween(from, to));
            foreach (var log in logs)
            {
                var archive = Mapper.Map<SysOperationLogArchive>(log);
                archiveRepository.Insert(archive);
                repository.Delete(log);
            }

            await UnitOfWork.CommitAsync();
            UnitOfWork.UseCompanyContext();
        }

        /// <summary>
        /// به روش آسنکرون، کلیه رویدادهای بایگانی شده در محدوده تاریخی داده شده را
        /// در لاگ های سیستمی بازیابی می کند
        /// </summary>
        /// <param name="from">ابتدای محدوده تاریخی برای بازیابی</param>
        /// <param name="to">انتهای محدوده تاریخی برای بازیابی</param>
        public async Task RecoverSystemLogsFromArchive(DateTime from, DateTime to)
        {
            UnitOfWork.UseSystemContext();
            var repository = UnitOfWork.GetAsyncRepository<SysOperationLog>();
            var archiveRepository = UnitOfWork.GetAsyncRepository<SysOperationLogArchive>();
            var archived = await archiveRepository.GetByCriteriaAsync(log => log.Date.Date.IsBetween(from, to));
            foreach (var item in archived)
            {
                var log = Mapper.Map<SysOperationLog>(item);
                log.Id = 0;
                repository.Insert(log);
                archiveRepository.Delete(item);
            }

            await UnitOfWork.CommitAsync();
            UnitOfWork.UseCompanyContext();
        }

        #endregion

        private async Task SetSystemValues(OperationLogViewModel log)
        {
            UnitOfWork.UseSystemContext();
            var userRepository = UnitOfWork.GetAsyncRepository<User>();
            var user = await userRepository.GetByIDAsync(log.UserId);
            log.UserName = user?.UserName;
            var companyRepository = UnitOfWork.GetAsyncRepository<CompanyDb>();
            var company = await companyRepository.GetByIDAsync(log.CompanyId);
            log.CompanyName = company?.Name;
            UnitOfWork.UseCompanyContext();
        }
    }
}
