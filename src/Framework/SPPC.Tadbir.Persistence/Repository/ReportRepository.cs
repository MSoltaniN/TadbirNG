using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Common;
using SPPC.Framework.Extensions;
using SPPC.Framework.Mapper;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.Model.Metadata;
using SPPC.Tadbir.Model.Reporting;
using SPPC.Tadbir.ViewModel.Auth;
using SPPC.Tadbir.ViewModel.Reporting;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای تهیه و محاسبه اطلاعات گزارش های برنامه را پیاده سازی می کند
    /// </summary>
    public class ReportRepository : IReportRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="unitOfWork">پیاده سازی اینترفیس واحد کاری برای انجام عملیات دیتابیسی</param>
        /// <param name="mapper">نگاشت مورد استفاده برای تبدیل کلاس های مدل اطلاعاتی</param>
        /// <param name="repository">عملیات مورد نیاز برای اعمال دسترسی امنیتی در سطح سطرهای اطلاعاتی را تعریف می کند</param>
        /// <param name="lookupRepository">امکان خواندن اطلاعات موجود را به صورت لوکاپ فراهم می کند</param>
        public ReportRepository(
            IAppUnitOfWork unitOfWork, IDomainMapper mapper, ISecureRepository repository,
            ILookupRepository lookupRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _repository = repository;
            _lookupRepository = lookupRepository;
        }

        /// <summary>
        /// اطلاعات محیطی و امنیتی کاربر جاری برنامه را برای کنترل قواعد کاری برنامه تنظیم می کند
        /// </summary>
        /// <param name="userContext">اطلاعات محیطی و امنیتی کاربر جاری برنامه</param>
        public void SetCurrentContext(UserContextViewModel userContext)
        {
            _currentContext = userContext;
            _repository.SetCurrentContext(userContext);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات مورد نیاز در گزارش خلاصه اسناد حسابداری را خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های برنامه برای فیلتر، مرتب سازی و صفحه بندی اطلاعات</param>
        /// <returns>اطلاعات گزارش خلاصه اسناد حسابداری</returns>
        public async Task<IList<VoucherSummaryViewModel>> GetVoucherSummaryByDateReportAsync(
            GridOptions gridOptions)
        {
            Verify.ArgumentNotNull(gridOptions, nameof(gridOptions));
            var userMap = await _lookupRepository.GetUserPersonsAsync();
            var vouchers = await _repository
                .GetAllOperationQuery<Voucher>(
                    ViewName.Voucher, voucher => voucher.Lines, voucher => voucher.Status)
                .Select(voucher => _mapper.Map<VoucherSummaryViewModel>(voucher))
                .Apply(gridOptions)
                .ToListAsync();
            Array.ForEach(vouchers.ToArray(),
                voucher => voucher.PreparedBy = userMap[voucher.PreparedById]);
            return vouchers;
        }

        /// <summary>
        /// به روش آسنکرون، نعداد سطرهای اطلاعاتی در گزارش خلاصه اسناد حسابداری را خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های برنامه برای فیلتر، مرتب سازی و صفحه بندی اطلاعات</param>
        /// <returns>تعداد سطرهای گزارش خلاصه اسناد حسابداری</returns>
        public async Task<int> GetVoucherSummaryByDateCountAsync(GridOptions gridOptions)
        {
            Verify.ArgumentNotNull(gridOptions, nameof(gridOptions));
            int count = await _repository
                .GetAllOperationQuery<Voucher>(ViewName.Voucher, voucher => voucher.Lines)
                .Select(voucher => _mapper.Map<VoucherSummaryViewModel>(voucher))
                .Apply(gridOptions, false)
                .CountAsync();
            return count;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات مورد نیاز در گزارش فرم مرسوم سند را خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های برنامه برای فیلتر، مرتب سازی و صفحه بندی اطلاعات</param>
        /// <param name="withDetail">مشخص می کند که آیا جزییات سطوح شناور نیز مورد نیاز است یا نه</param>
        /// <returns>اطلاعات گزارش فرم مرسوم سند</returns>
        public async Task<StandardVoucherViewModel> GetStandardVoucherFormAsync(
            GridOptions gridOptions, bool withDetail = false)
        {
            var standardForm = default(StandardVoucherViewModel);
            var voucher = await GetStandardVoucherFormQuery(withDetail)
                .Apply(gridOptions)
                .FirstOrDefaultAsync();
            if (voucher != null)
            {
                standardForm = _mapper.Map<StandardVoucherViewModel>(voucher);
                var lineItems = new List<StandardVoucherLineViewModel>();
                foreach (var line in voucher.Lines)
                {
                    lineItems.Add(new StandardVoucherLineViewModel()
                    {
                        AccountFullCode = String.Empty,
                        Description = line.Description,
                        PartialAmount = Math.Max(line.Debit, line.Credit)
                    });
                    if (withDetail)
                    {
                        AddFloatingStandardLineItems(line, lineItems);
                    }

                    AddGeneralStandardLineItems(line, lineItems);
                    AddAuxiliaryStandardLineItems(line, lineItems);
                    AddDetailStandardLineItems(line, lineItems);

                    lineItems.Reverse();
                    standardForm.Lines.AddRange(lineItems);
                    lineItems.Clear();
                }
            }

            return standardForm;
        }

        public async Task<IList<JournalViewModel>> GetJournalByDateByRowAsync(GridOptions gridOptions)
        {
            var journal = await _repository
                .GetAllOperationQuery<VoucherLine>(ViewName.VoucherLine, art => art.Voucher, art => art.Account)
                .OrderBy(art => art.Voucher.Date)
                    .ThenBy(art => art.Voucher.No)
                .Select(art => _mapper.Map<JournalViewModel>(art))
                .Apply(gridOptions)
                .ToListAsync();
            return journal;
        }

        private static void AddGeneralStandardLineItems(
            VoucherLine line, IList<StandardVoucherLineViewModel> lineItems)
        {
            if (line.Account.Level == 0)
            {
                lineItems.Add(new StandardVoucherLineViewModel()
                {
                    AccountFullCode = line.Account.FullCode,
                    Description = line.Account.Name,
                    Debit = line.Debit,
                    Credit = line.Credit
                });
            }
        }

        private static void AddAuxiliaryStandardLineItems(
            VoucherLine line, IList<StandardVoucherLineViewModel> lineItems)
        {
            if (line.Account.Level == 1)
            {
                lineItems.Add(new StandardVoucherLineViewModel()
                {
                    AccountFullCode = line.Account.FullCode,
                    Description = line.Account.Name
                });
                lineItems.Add(new StandardVoucherLineViewModel()
                {
                    AccountFullCode = line.Account.Parent.FullCode,
                    Description = line.Account.Parent.Name,
                    Debit = line.Debit,
                    Credit = line.Credit
                });
            }
        }

        private static void AddDetailStandardLineItems(
            VoucherLine line, IList<StandardVoucherLineViewModel> lineItems)
        {
            if (line.Account.Level == 2)
            {
                lineItems.Add(new StandardVoucherLineViewModel()
                {
                    AccountFullCode = line.Account.FullCode,
                    Description = line.Account.Name
                });
                lineItems.Add(new StandardVoucherLineViewModel()
                {
                    AccountFullCode = line.Account.Parent.FullCode,
                    Description = line.Account.Parent.Name
                });
                lineItems.Add(new StandardVoucherLineViewModel()
                {
                    AccountFullCode = line.Account.Parent.Parent.FullCode,
                    Description = line.Account.Parent.Parent.Name,
                    Debit = line.Debit,
                    Credit = line.Credit
                });
            }
        }

        private static void AddFloatingStandardLineItems(
            VoucherLine line, IList<StandardVoucherLineViewModel> lineItems)
        {
            if (line.Project?.Id > 0)
            {
                lineItems.Add(new StandardVoucherLineViewModel()
                {
                    AccountFullCode = line.Project.FullCode,
                    Description = line.Project.Name
                });
            }

            if (line.CostCenter?.Id > 0)
            {
                lineItems.Add(new StandardVoucherLineViewModel()
                {
                    AccountFullCode = line.CostCenter.FullCode,
                    Description = line.CostCenter.Name
                });
            }

            if (line.DetailAccount?.Id > 0)
            {
                lineItems.Add(new StandardVoucherLineViewModel()
                {
                    AccountFullCode = line.DetailAccount.FullCode,
                    Description = line.DetailAccount.Name
                });
            }
        }

        private IQueryable<Voucher> GetStandardVoucherFormQuery(bool withDetail = false)
        {
            var repository = _unitOfWork.GetAsyncRepository<Voucher>();
            IQueryable<Voucher> query = repository
                .GetEntityQuery()
                .Include(v => v.Lines)
                    .ThenInclude(vl => vl.Account)
                        .ThenInclude(acc => acc.Parent)
                            .ThenInclude(acc => acc.Parent);
            if (withDetail)
            {
                query = query
                    .Include(v => v.Lines)
                        .ThenInclude(vl => vl.DetailAccount)
                    .Include(v => v.Lines)
                        .ThenInclude(vl => vl.CostCenter)
                    .Include(v => v.Lines)
                        .ThenInclude(vl => vl.Project);
            }

            return query;
        }

        private readonly IAppUnitOfWork _unitOfWork;
        private readonly IDomainMapper _mapper;
        private readonly ISecureRepository _repository;
        private readonly ILookupRepository _lookupRepository;
        private UserContextViewModel _currentContext;
    }
}
