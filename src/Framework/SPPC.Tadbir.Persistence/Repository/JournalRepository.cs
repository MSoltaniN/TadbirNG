using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Mapper;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.Values;
using SPPC.Tadbir.ViewModel.Auth;
using SPPC.Tadbir.ViewModel.Reporting;

namespace SPPC.Tadbir.Persistence
{
    public class JournalRepository : IJournalRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="unitOfWork">پیاده سازی اینترفیس واحد کاری برای انجام عملیات دیتابیسی</param>
        /// <param name="mapper">نگاشت مورد استفاده برای تبدیل کلاس های مدل اطلاعاتی</param>
        /// <param name="repository">عملیات مورد نیاز برای اعمال دسترسی امنیتی در سطح سطرهای اطلاعاتی را تعریف می کند</param>
        /// <param name="configRepository">امکان خواندن تنظیمات برنامه را فراهم می کند</param>
        public JournalRepository(
            IAppUnitOfWork unitOfWork, IDomainMapper mapper, ISecureRepository repository,
            IConfigRepository configRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _repository = repository;
            _configRepository = configRepository;
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
        /// به روش آسنکرون، اطلاعات گزارش دفتر روزنامه بر حسب تاریخ را خوانده و برمی گرداند
        /// </summary>
        /// <param name="journalMode">حالت مورد نظر برای نمایش و جمع بندی اطلاعات</param>
        /// <param name="from">تاریخ ابتدا در دوره گزارشگیری مورد نظر</param>
        /// <param name="to">تاریخ انتها در دوره گزارشگیری مورد نظر</param>
        /// <returns></returns>
        public async Task<JournalViewModel> GetJournalByDateAsync(
            JournalMode journalMode, DateTime from, DateTime to)
        {
            var journal = default(JournalViewModel);
            if (journalMode == JournalMode.ByRows)
            {
                journal = await GetJournalByDateByRowAsync(from, to);
            }

            return journal;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات گزارش دفتر روزنامه بر حسب تاریخ و مطابق با ردیف های سند
        /// را خوانده و برمی گرداند
        /// </summary>
        /// <param name="from">تاریخ ابتدا در دوره گزارشگیری مورد نظر</param>
        /// <param name="to">تاریخ انتها در دوره گزارشگیری مورد نظر</param>
        /// <returns>اطلاعات گزارش دفتر روزنامه</returns>
        private async Task<JournalViewModel> GetJournalByDateByRowAsync(DateTime from, DateTime to)
        {
            var lines = await GetRawJournalByDateLinesAsync(from, to);
            var journalItems = GetJournalByDateByRowItems(lines);
            return BuildJournal(journalItems);
        }

        private async Task<IList<VoucherLine>> GetRawJournalByDateLinesAsync(DateTime from, DateTime to)
        {
            return await _repository
                .GetAllOperationQuery<VoucherLine>(ViewName.VoucherLine,
                    art => art.Voucher, art => art.Account, art => art.Branch)
                .Where(art => art.Voucher.Date >= from && art.Voucher.Date <= to)
                .ToListAsync();
        }

        private IList<JournalItemViewModel> GetJournalByDateByRowItems(IEnumerable<VoucherLine> voucherLines)
        {
            return voucherLines
                .OrderBy(art => art.Voucher.Date)
                    .ThenBy(art => Int32.Parse(art.Voucher.No))
                .Select(art => _mapper.Map<JournalItemViewModel>(art))
                .ToList();
        }

        private JournalViewModel BuildJournal(IEnumerable<JournalItemViewModel> journalItems)
        {
            var journal = new JournalViewModel();
            journal.Items.AddRange(journalItems);
            return journal;
        }

        private readonly IAppUnitOfWork _unitOfWork;
        private readonly IDomainMapper _mapper;
        private readonly ISecureRepository _repository;
        private readonly IConfigRepository _configRepository;
        private UserContextViewModel _currentContext;
    }
}
