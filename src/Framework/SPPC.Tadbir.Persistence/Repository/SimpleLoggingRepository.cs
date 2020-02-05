using System;
using System.Diagnostics;
using System.Threading.Tasks;
using SPPC.Tadbir.ViewModel.Core;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای ایجاد لاگ های عملیاتی ساده را فراهم می کند
    /// </summary>
    public abstract class SimpleLoggingRepository : RepositoryBase
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="context">امکانات مشترک مورد نیاز را برای عملیات دیتابیسی فراهم می کند</param>
        /// <param name="logRepository">امکان ایجاد لاگ های عملیاتی را در دیتابیس سیستمی برنامه فراهم می کند</param>
        public SimpleLoggingRepository(IRepositoryContext context, IOperationLogRepository logRepository)
            : base(context)
        {
            _logRepository = logRepository;
        }

        internal abstract OperationSourceId OperationSource { get; }

        /// <summary>
        /// مدل نمایشی لاگ عملیاتی برای عملیات جاری
        /// </summary>
        protected OperationLogViewModel Log { get; private set; }

        internal async Task OnSourceActionAsync(
            OperationId operation, OperationSourceId source, SourceListId list = SourceListId.None)
        {
            int? listId = (list != SourceListId.None) ? (int?)list : null;
            Log = new OperationLogViewModel()
            {
                BranchId = UserContext.BranchId,
                FiscalPeriodId = UserContext.FiscalPeriodId,
                CompanyId = UserContext.CompanyId,
                UserId = UserContext.Id,
                Date = DateTime.Now.Date,
                Time = DateTime.Now.TimeOfDay,
                OperationId = (int)operation,
                SourceId = (int)source,
                SourceListId = listId
            };
            await TrySaveLogAsync();
        }

        internal async Task OnSourceActionAsync(OperationId operation, SourceListId list = SourceListId.None)
        {
            await OnSourceActionAsync(operation, OperationSource, list);
        }

        private async Task TrySaveLogAsync()
        {
            try
            {
                await _logRepository.SaveLogAsync(Log);
            }
            catch (Exception ex)
            {
#if DEBUG
                Debug.WriteLine(Environment.NewLine);
                Debug.WriteLine("WARNING: Could not create operation log.");
                Debug.WriteLine("    More Info : {0}", ex);
#endif

                // Ignored (logging should not throw exception)
            }
        }

        private readonly IOperationLogRepository _logRepository;
    }
}
