using System;
using System.Diagnostics;
using System.Threading.Tasks;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.ViewModel.Auth;
using SPPC.Tadbir.ViewModel.Core;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات پایه مورد نیاز برای ایجاد لاگ های عملیاتی را پیاده سازی می کند
    /// </summary>
    public abstract class LoggingRepositoryBase : RepositoryBase
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="context">امکانات مشترک مورد نیاز را برای عملیات دیتابیسی فراهم می کند</param>
        /// <param name="logRepository">امکان ایجاد لاگ های عملیاتی را در دیتابیس سیستمی برنامه فراهم می کند</param>
        protected LoggingRepositoryBase(IRepositoryContext context, IOperationLogRepository logRepository)
            : base(context)
        {
            _logRepository = logRepository;
        }

        internal virtual OperationSourceId OperationSource
        {
            get { return OperationSourceId.None; }
        }

        /// <summary>
        /// به روش آسنکرون، لاگ عملیاتی را در صورت نیاز برای عملیات خواندن لیست موجودیت ها ایجاد می کند
        /// </summary>
        /// <param name="gridOptions">اطلاعات مورد نیاز برای ایجاد لاگ</param>
        /// <param name="description">شرح اختیاری برای رویداد</param>
        protected virtual async Task ReadAsync(GridOptions gridOptions, string description = null)
        {
            var options = gridOptions ?? new GridOptions();
            await OnSourceActionAsync(options, SourceListId.None);
        }

        /// <summary>
        /// مدل نمایشی لاگ عملیاتی برای عملیات جاری
        /// </summary>
        internal OperationLogViewModel Log { get; set; }

        internal async Task OnSourceActionAsync(OperationId operation, string description = null,
            string entityName = null, string entityCode = null, string entityDescription = null)
        {
            Log = new OperationLogViewModel()
            {
                BranchId = UserContext.BranchId,
                FiscalPeriodId = UserContext.FiscalPeriodId,
                CompanyId = UserContext.CompanyId,
                UserId = UserContext.Id,
                Date = DateTime.Now.Date,
                Time = DateTime.Now.TimeOfDay,
                OperationId = (int)operation,
                SourceId = (int)OperationSource,
                Description = description ?? String.Empty,
                EntityName = entityName ?? String.Empty,
                EntityCode = entityCode ?? String.Empty,
                EntityDescription = entityDescription ?? String.Empty
            };
            await TrySaveLogAsync();
        }

        internal async Task OnSourceActionAsync(
            GridOptions gridOptions, SourceListId list, string description = null)
        {
            await OnSourceActionAsync(gridOptions, OperationSource, list, description);
        }

        internal async Task OnSourceActionAsync(
            GridOptions gridOptions, OperationSourceId source, SourceListId list, string description = null)
        {
            int? listId = (list != SourceListId.None) ? (int?)list : null;
            if (gridOptions.ListChanged)
            {
                Log = new OperationLogViewModel()
                {
                    BranchId = UserContext.BranchId,
                    FiscalPeriodId = UserContext.FiscalPeriodId,
                    CompanyId = UserContext.CompanyId,
                    UserId = UserContext.Id,
                    Date = DateTime.Now.Date,
                    Time = DateTime.Now.TimeOfDay,
                    OperationId = gridOptions.Operation,
                    SourceId = (int)source,
                    SourceListId = listId,
                    Description = description ?? String.Empty
                };
                await TrySaveLogAsync();
            }
        }

        internal async Task OnSystemLoginAsync(int? userId, string description)
        {
            Log = new OperationLogViewModel()
            {
                Date = DateTime.Now.Date,
                Time = DateTime.Now.TimeOfDay,
                SourceId = (int)OperationSourceId.AppLogin,
                OperationId = (int)OperationId.FailedLogin,
                UserId = userId,
                Description = description
            };
            await TrySaveLogAsync();
        }

        internal async Task OnEnvironmentChangeAsync(
            CompanyLoginViewModel currentLogin, CompanyLoginViewModel newLogin)
        {
            Log = new OperationLogViewModel()
            {
                Date = DateTime.Now.Date,
                Time = DateTime.Now.TimeOfDay,
                SourceId = (int)OperationSourceId.AppEnvironment,
                CompanyId = currentLogin.CompanyId > 0
                    ? currentLogin.CompanyId
                    : null,
                UserId = currentLogin.UserId
            };

            string description;
            if (currentLogin.CompanyId == 0
                || currentLogin.CompanyId != newLogin.CompanyId)
            {
                Log.CompanyId = newLogin.CompanyId;
                Log.OperationId = (int)OperationId.CompanyLogin;
                description = String.Format("{0} : '{1}', {2} : '{3}', {4} : '{5}'",
                    AppStrings.Company, newLogin.CompanyName,
                    AppStrings.FiscalPeriod, newLogin.FiscalPeriodName,
                    AppStrings.Branch, newLogin.BranchName);
                Log.Description = Context.Localize(description);
                await TrySaveLogAsync();
            }
            else
            {
                if (currentLogin.FiscalPeriodId != newLogin.FiscalPeriodId)
                {
                    Log.OperationId = (int)OperationId.SwitchFiscalPeriod;
                    description = String.Format("{0} : '{1}', {2} : '{3}'",
                        AppStrings.CurrentValue, currentLogin.FiscalPeriodName,
                        AppStrings.NewValue, newLogin.FiscalPeriodName);
                    Log.Description = Context.Localize(description);
                    await TrySaveLogAsync();
                }

                if (currentLogin.BranchId != newLogin.BranchId)
                {
                    Log.OperationId = (int)OperationId.SwitchBranch;
                    description = String.Format("{0} : '{1}', {2} : '{3}'",
                        AppStrings.CurrentValue, currentLogin.BranchName,
                        AppStrings.NewValue, newLogin.BranchName);
                    Log.Description = Context.Localize(description);
                    await TrySaveLogAsync();
                }
            }
        }

        /// <summary>
        /// در صورت بروز خطا هنگام ایجاد رکورد لاگ، جزییات خطا را در کنسول ویژوال استودیو گزارش می دهد
        /// </summary>
        /// <param name="ex">خطای ایجاد شده</param>
        protected static void ReportLoggingError(Exception ex)
        {
#if DEBUG
            Debug.WriteLine(Environment.NewLine);
            Debug.WriteLine("WARNING: Could not create operation log.");
            Debug.WriteLine("    More Info : {0}", ex);
#endif
        }

        /// <summary>
        /// رکورد لاگ عملیاتی را در جدول مرتبط ایجاد می کند.
        /// </summary>
        /// <remarks>توجه : هر گونه خطای زمان اجرا حین عملیات، نادیده گرفته می‌شود</remarks>
        protected virtual async Task TrySaveLogAsync()
        {
            try
            {
                await _logRepository.SaveLogAsync(Log);
            }
            catch (Exception ex)
            {
                ReportLoggingError(ex);

                // Ignored (logging should not throw exception)
            }
        }

        /// <summary>
        /// امکان ایجاد لاگ های عملیاتی را در دیتابیس سیستمی برنامه فراهم می کند
        /// </summary>
        protected readonly IOperationLogRepository _logRepository;
    }
}
