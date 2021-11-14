using System;
using System.Linq;
using System.Threading.Tasks;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.ViewModel.Auth;

namespace SPPC.Tadbir.Persistence.Utility
{
    /// <summary>
    /// امکانات مشترک مرتبط با محاسبات گزارشی را پیاده سازی می کند
    /// </summary>
    public class ReportUtility : IReportUtility
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="context">امکانات مشترک مورد نیاز را برای عملیات دیتابیسی فراهم می کند</param>
        /// <param name="config">امکان خواندن تنظیمات برنامه را فراهم می کند</param>
        public ReportUtility(IRepositoryContext context, IConfigRepository config)
        {
            Config = config;
            _context = context;
        }

        /// <summary>
        /// به روش آسنکرون، تاریخ سند سیستمی با مأخذ داده شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="origin">یکی از مأخذهای تعریف شده برای سندهای سیستمی</param>
        /// <returns>تاریخ سند مورد نظر یا اگر سند مورد نظر پیدا نشود، بدون مقدار</returns>
        public async Task<DateTime?> GetSpecialVoucherDateAsync(VoucherOriginId origin)
        {
            DateTime? voucherDate = null;
            var repository = UnitOfWork.GetAsyncRepository<Voucher>();
            var voucher = await repository.GetFirstByCriteriaAsync(v => v.OriginId == (int)origin
                && v.FiscalPeriodId == UserContext.FiscalPeriodId);
            if (voucher != null)
            {
                voucherDate = voucher.Date;
            }

            return voucherDate;
        }

        /// <summary>
        /// طول کد یکی از مولفه های حساب را در سطح داده شده خوانده و برمی گرداند
        /// </summary>
        /// <param name="viewId">شناسه دیتابیسی مولفه حساب مورد نظر</param>
        /// <param name="level">شماره سطح مورد نظر در ساختار درختی</param>
        /// <returns>طول کد مولفه حساب در سطح مورد نظر</returns>
        public int GetLevelCodeLength(int viewId, int level)
        {
            var fullConfig = Config
                .GetViewTreeConfigByViewAsync(viewId)
                .Result;
            var treeConfig = fullConfig.Current;
            int codeLength = treeConfig.Levels
                .Where(cfg => cfg.No <= level + 1)
                .Select(cfg => (int)cfg.CodeLength)
                .Sum();
            return codeLength;
        }

        /// <summary>
        /// امکان دسترسی به دیتابیس ها و انجام تراکنش های دیتابیسی را فراهم می کند
        /// </summary>
        protected IAppUnitOfWork UnitOfWork
        {
            get { return _context.UnitOfWork; }
        }

        /// <summary>
        /// اطلاعات محیطی و امنیتی کاربر جاری برنامه
        /// </summary>
        protected UserContextViewModel UserContext
        {
            get { return _context.UserContext; }
        }

        /// <summary>
        /// امکان مدیریت تنظیمات شرکتی را فراهم می کند
        /// </summary>
        protected IConfigRepository Config { get; }

        private readonly IRepositoryContext _context;
    }
}
