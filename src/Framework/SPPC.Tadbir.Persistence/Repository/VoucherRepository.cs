using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Common;
using SPPC.Framework.Extensions;
using SPPC.Framework.Helpers;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Extensions;
using SPPC.Tadbir.Model.Core;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.ViewModel.Reporting;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت اطلاعات اسناد مالی و آرتیکل های آنها را پیاده سازی می کند.
    /// </summary>
    public class VoucherRepository : LoggingRepository<Voucher, VoucherViewModel>, IVoucherRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="context">امکانات مشترک مورد نیاز را برای عملیات دیتابیسی فراهم می کند</param>
        /// <param name="system">امکانات مورد نیاز در دیتابیس های سیستمی را فراهم می کند</param>
        /// <param name="userRepository">امکان خواندن اطلاعات کاربران برنامه را فراهم می کند</param>
        public VoucherRepository(IRepositoryContext context, ISystemRepository system, IUserRepository userRepository)
            : base(context, system?.Logger)
        {
            _system = system;
            _userRepository = userRepository;
        }

        /// <summary>
        /// به روش آسنکرون، کلیه اسناد مالی از نوع مفهومی سند حسابداری را که در دوره مالی و شعبه جاری
        /// تعریف شده اند، از دیتابیس خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از اسناد مالی تعریف شده در دوره مالی و شعبه جاری</returns>
        public async Task<IList<VoucherViewModel>> GetVouchersAsync(GridOptions gridOptions = null)
        {
            var vouchers = await Repository.GetAllOperationAsync<Voucher>(
                ViewName.Voucher, v => v.Lines, v => v.Status);
            return vouchers
                .Where(item => item.SubjectType == 0)
                .OrderBy(item => item.Date)
                .Select(item => Mapper.Map<VoucherViewModel>(item))
                .Apply(gridOptions)
                .ToList();
        }

        /// <summary>
        /// به روش آسنکرون، سند مالی با شناسه دیتابیسی مشخص شده را از دیتابیس خوانده و برمی گرداند
        /// </summary>
        /// <param name="voucherId">شناسه دیتابیسی یکی از اسناد مالی موجود</param>
        /// <returns>سند مالی مشخص شده با شناسه دیتابیسی</returns>
        public async Task<VoucherViewModel> GetVoucherAsync(int voucherId)
        {
            VoucherViewModel voucher = null;
            var repository = UnitOfWork.GetAsyncRepository<Voucher>();
            var existing = await repository.GetByIDAsync(voucherId, v => v.Lines, v => v.Status);
            if (existing != null)
            {
                voucher = Mapper.Map<VoucherViewModel>(existing);
                voucher.IsBalanced = voucher.DebitSum.AlmostEquals(voucher.CreditSum);
            }

            return voucher;
        }

        /// <summary>
        /// به روش آسنکرون، سند مالی جدیدی را با مقادیر پیشنهادی ایجاد کرده و برمی گرداند
        /// </summary>
        /// <returns>سند مالی جدید با مقادیر پیشنهادی</returns>
        public async Task<VoucherViewModel> GetNewVoucherAsync()
        {
            int lastNo = await GetLastVoucherNoAsync();
            DateTime lastDate = await GetLastVoucherDateAsync();
            var newVoucher = new VoucherViewModel()
            {
                Date = lastDate,
                No = lastNo + 1,
                BranchId = UserContext.BranchId,
                FiscalPeriodId = UserContext.FiscalPeriodId,
                Type = 0,
                SubjectType = 0,
                SaveCount = 0
            };

            return await SaveVoucherAsync(newVoucher);
        }

        /// <summary>
        /// به روش آسنکرون، سند مالی با شماره مشخص شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="voucherNo">شماره یکی از اسناد مالی موجود</param>
        /// <returns>سند مالی مشخص شده با شماره</returns>
        public async Task<VoucherViewModel> GetVoucherByNoAsync(int voucherNo)
        {
            var voucherByNo = await Repository
                .GetAllOperationQuery<Voucher>(ViewName.Voucher)
                .Where(voucher => voucher.No == voucherNo)
                .FirstOrDefaultAsync();
            return voucherByNo != null
                ? Mapper.Map<VoucherViewModel>(voucherByNo)
                : null;
        }

        /// <summary>
        /// به روش آسنکرون، اولین سند مالی را خوانده و برمی گرداند
        /// </summary>
        /// <returns>اولین سند مالی</returns>
        public async Task<VoucherViewModel> GetFirstVoucherAsync()
        {
            var firstVoucher = await Repository
                .GetAllOperationQuery<Voucher>(ViewName.Voucher)
                .OrderBy(voucher => voucher.No)
                .FirstOrDefaultAsync();
            return firstVoucher != null
                ? Mapper.Map<VoucherViewModel>(firstVoucher)
                : null;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات سند مالی قبلی را خوانده و برمی گرداند
        /// </summary>
        /// <param name="currentNo">شماره سند مالی جاری در برنامه</param>
        /// <returns>سند مالی قبلی</returns>
        public async Task<VoucherViewModel> GetPreviousVoucherAsync(int currentNo)
        {
            var previous = await Repository
                .GetAllOperationQuery<Voucher>(ViewName.Voucher)
                .Where(voucher => voucher.No < currentNo)
                .OrderByDescending(voucher => voucher.No)
                .FirstOrDefaultAsync();
            return previous != null
                ? Mapper.Map<VoucherViewModel>(previous)
                : null;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات سند مالی بعدی را خوانده و برمی گرداند
        /// </summary>
        /// <param name="currentNo">شماره سند مالی جاری در برنامه</param>
        /// <returns>سند مالی بعدی</returns>
        public async Task<VoucherViewModel> GetNextVoucherAsync(int currentNo)
        {
            var next = await Repository
                .GetAllOperationQuery<Voucher>(ViewName.Voucher)
                .Where(voucher => voucher.No > currentNo)
                .OrderBy(voucher => voucher.No)
                .FirstOrDefaultAsync();
            return next != null
                ? Mapper.Map<VoucherViewModel>(next)
                : null;
        }

        /// <summary>
        /// به روش آسنکرون، آخرین سند مالی را خوانده و برمی گرداند
        /// </summary>
        /// <returns>آخرین سند مالی</returns>
        public async Task<VoucherViewModel> GetLastVoucherAsync()
        {
            var lastVoucher = await Repository
                .GetAllOperationQuery<Voucher>(ViewName.Voucher)
                .OrderByDescending(voucher => voucher.No)
                .FirstOrDefaultAsync();
            return lastVoucher != null
                ? Mapper.Map<VoucherViewModel>(lastVoucher)
                : null;
        }

        /// <summary>
        /// به روش آسنکرون، تعداد اسناد مالی تعریف شده در دوره مالی و شعبه جاری را
        /// از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <typeparam name="TViewModel">نوع مدل نمایشی که برای نمایش اطلاعات موجودیت استفاده می شود</typeparam>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>تعداد اسناد مالی تعریف شده در دوره مالی و شعبه مشخص شده</returns>
        public async Task<int> GetCountAsync<TViewModel>(GridOptions gridOptions = null)
            where TViewModel : class, new()
        {
            return await Repository.GetAllOperationQuery<Voucher>(ViewName.Voucher)
                .Where(item => item.SubjectType == 0)
                .Select(item => Mapper.Map<TViewModel>(item))
                .Apply(gridOptions, false)
                .CountAsync();
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات محدوده سندهای قابل دسترسی توسط کاربر جاری برنامه را خوانده و برمی گرداند
        /// </summary>
        /// <returns>محدوده سندهای قابل دسترسی توسط کاربر جاری</returns>
        public async Task<NumberedItemRangeViewModel> GetVoucherRangeInfoAsync()
        {
            var query = Repository.GetAllOperationQuery<Voucher>(ViewName.Voucher)
                .OrderBy(voucher => voucher.No);
            var first = await query.FirstOrDefaultAsync();
            var last = await query.LastOrDefaultAsync();
            return new NumberedItemRangeViewModel()
            {
                ViewId = ViewName.Voucher,
                FirstNo = (first != null) ? first.No : 0,
                LastNo = (last != null) ? last.No : 0
            };
        }

        /// <summary>
        /// به روش آسنکرون، مدل نمایشی دوره مالی مورد استفاده در یک سند مالی را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="voucher">مدل نمایشی سند مالی مورد نظر</param>
        /// <returns>مدل نمایشی دوره مالی به کار رفته در سند مالی</returns>
        public async Task<FiscalPeriodViewModel> GetVoucherFiscalPeriodAsync(VoucherViewModel voucher)
        {
            Verify.ArgumentNotNull(voucher, "voucher");
            var repository = UnitOfWork.GetAsyncRepository<FiscalPeriod>();
            var fiscalPeriod = await repository.GetByIDAsync(voucher.FiscalPeriodId);
            return Mapper.Map<FiscalPeriodViewModel>(fiscalPeriod);
        }

        /// <summary>
        /// به روش آسنکرون، شماره روزانه سند را با توجه به سندهای موجود در یک تاریخ تنظیم می کند
        /// </summary>
        /// <param name="voucher">اطلاعات نمایشی سند جدید</param>
        public async Task SetVoucherDailyNoAsync(VoucherViewModel voucher)
        {
            voucher.DailyNo = await GetNextDailyNoAsync(voucher);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات یک سند مالی را در دیتابیس ایجاد یا اصلاح می کند
        /// </summary>
        /// <param name="voucherView">سند مالی برای ایجاد یا اصلاح</param>
        /// <returns>مدل نمایشی سند ایجاد یا اصلاح شده</returns>
        public async Task<VoucherViewModel> SaveVoucherAsync(VoucherViewModel voucherView)
        {
            Verify.ArgumentNotNull(voucherView, "voucherView");
            Voucher voucher = default(Voucher);
            var displayName = await _userRepository.GetCurrentUserDisplayNameAsync();
            var repository = UnitOfWork.GetAsyncRepository<Voucher>();
            if (voucherView.Id == 0)
            {
                voucher = Mapper.Map<Voucher>(voucherView);
                voucher.StatusId = (int)DocumentStatusValue.Draft;
                voucher.IssuedById = UserContext.Id;
                voucher.ModifiedById = UserContext.Id;
                voucher.IssuerName = voucher.ModifierName = displayName;
                await InsertAsync(repository, voucher);
            }
            else
            {
                voucher = await repository.GetByIDAsync(voucherView.Id);
                if (voucher != null)
                {
                    voucher.ModifiedById = UserContext.Id;
                    voucher.ModifierName = displayName;
                    await UpdateAsync(repository, voucher, voucherView);
                }
            }

            await ManageDocumentAsync(voucher);
            return Mapper.Map<VoucherViewModel>(voucher);
        }

        /// <summary>
        /// به روش آسنکرون، سند مالی مشخص شده با شناسه دیتابیسی را از دیتابیس حذف می کند
        /// </summary>
        /// <param name="voucherId">شناسه دیتابیسی سند مالی برای حذف</param>
        public async Task DeleteVoucherAsync(int voucherId)
        {
            var repository = UnitOfWork.GetAsyncRepository<Voucher>();
            var voucher = await repository.GetByIDWithTrackingAsync(voucherId, txn => txn.Lines);
            if (voucher != null)
            {
                voucher.Lines.Clear();
                await DeleteAsync(repository, voucher);
            }
        }

        /// <summary>
        /// به روش آسنکرون، اسناد مالی مشخص شده با شناسه عددی را حذف می کند
        /// </summary>
        /// <param name="items">مجموعه شناسه های دیتابیسی سطرهای مورد نظر برای حذف</param>
        public async Task DeleteVouchersAsync(IEnumerable<int> items)
        {
            var repository = UnitOfWork.GetAsyncRepository<Voucher>();
            foreach (int item in items)
            {
                var voucher = await repository.GetByIDWithTrackingAsync(item, txn => txn.Lines);
                if (voucher != null)
                {
                    voucher.Lines.Clear();
                    await DeleteNoLogAsync(repository, voucher);
                }
            }

            await OnEntityGroupDeleted(items);
        }

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که آیا شماره سند مورد نظر تکراری است یا نه
        /// </summary>
        /// <param name="voucher">سند مالی که تکراری بودن شماره آن باید بررسی شود</param>
        /// <returns>مقدار بولی درست در صورت تکراری بودن شماره، در غیر این صورت مقدار بولی نادرست</returns>
        public async Task<bool> IsDuplicateVoucherNoAsync(VoucherViewModel voucher)
        {
            Verify.ArgumentNotNull(voucher, "voucher");
            var repository = UnitOfWork.GetAsyncRepository<Voucher>();
            var duplicates = await repository
                .GetByCriteriaAsync(vch => vch.Id != voucher.Id
                    && vch.No == voucher.No
                    && vch.SubjectType == voucher.SubjectType
                    && vch.FiscalPeriod.Id == voucher.FiscalPeriodId);
            return (duplicates.Count > 0);
        }

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که آیا شماره روزانه سند مورد نظر، با توجه به تاریخ سند، تکراری است یا نه
        /// </summary>
        /// <param name="voucher">سند مالی که تکراری بودن شماره روزانه آن باید بررسی شود</param>
        /// <returns>مقدار بولی درست در صورت تکراری بودن شماره، در غیر این صورت مقدار بولی نادرست</returns>
        public async Task<bool> IsDuplicateVoucherDailyNoAsync(VoucherViewModel voucher)
        {
            bool isDuplicate = false;
            Verify.ArgumentNotNull(voucher, nameof(voucher));
            if (voucher.Id > 0)
            {
                var repository = UnitOfWork.GetAsyncRepository<Voucher>();
                int count = await repository.GetCountByCriteriaAsync(
                    v => v.Id != voucher.Id
                        && voucher.Date.CompareWith(v.Date) == 0
                        && v.DailyNo != 0
                        && v.DailyNo == voucher.DailyNo
                        && v.FiscalPeriodId == voucher.FiscalPeriodId);
                isDuplicate = (count > 0);
            }

            return isDuplicate;
        }

        /// <summary>
        /// وضعیت ثبتی سند مالی را به وضعیت داده شده تغییر می دهد
        /// </summary>
        /// <param name="voucherId">شناسه دیتابیسی یکی از اسناد مالی موجود</param>
        /// <param name="status">وضعیت جدید مورد نظر برای سند مالی</param>
        public async Task SetVoucherStatusAsync(int voucherId, DocumentStatusValue status)
        {
            Verify.EnumValueIsDefined(typeof(DocumentStatusValue), "status", (int)status);
            var repository = UnitOfWork.GetAsyncRepository<Voucher>();
            var voucher = await repository.GetByIDAsync(voucherId);
            if (voucher != null)
            {
                voucher.StatusId = (int)status;
                repository.Update(voucher);
                OnDocumentStatus(status);
                Log.Description = GetState(voucher);
                await FinalizeActionAsync(voucher);
            }
        }

        /// <summary>
        /// به روش آسنکرون، وضعیت تایید سند مشخص شده را تغییر می دهد
        /// </summary>
        /// <param name="voucherId">شناسه دیتابیسی سند مورد نظر</param>
        /// <param name="isConfirmed">مشخص می کند که سند مورد نظر تایید شده است یا نه؟ مقدار بولی درست
        /// یعنی سند تایید شده و مقدار بولی نادرست یعنی سند برگشت از تایید شده است.</param>
        public async Task SetVoucherConfirmationAsync(int voucherId, bool isConfirmed)
        {
            var repository = UnitOfWork.GetAsyncRepository<Voucher>();
            var voucher = await repository.GetByIDAsync(voucherId);
            if (voucher != null)
            {
                voucher.ConfirmedById = isConfirmed ? UserContext.Id : (int?)null;
                voucher.ConfirmerName = isConfirmed ? GetCurrentUserDisplayName() : null;
                repository.Update(voucher);
                OnDocumentConfirmation(isConfirmed);
                Log.Description = GetState(voucher);
                await FinalizeActionAsync(voucher);
            }
        }

        /// <summary>
        /// به روش آسنکرون، وضعیت تصویب سند مشخص شده را تغییر می دهد
        /// </summary>
        /// <param name="voucherId">شناسه دیتابیسی سند مورد نظر</param>
        /// <param name="isApproved">مشخص می کند که سند مورد نظر تصویب شده است یا نه؟ مقدار بولی درست
        /// یعنی سند تصویب شده و مقدار بولی نادرست یعنی سند برگشت از تصویب شده است.</param>
        public async Task SetVoucherApprovalAsync(int voucherId, bool isApproved)
        {
            var repository = UnitOfWork.GetAsyncRepository<Voucher>();
            var voucher = await repository.GetByIDAsync(voucherId);
            if (voucher != null)
            {
                voucher.ApprovedById = isApproved ? UserContext.Id : (int?)null;
                voucher.ApproverName = isApproved ? GetCurrentUserDisplayName() : null;
                repository.Update(voucher);
                OnDocumentApproval(isApproved);
                Log.Description = GetState(voucher);
                await FinalizeActionAsync(voucher);
            }
        }

        /// <summary>
        /// عمل داده شده را روی سند با شناسه دیتابیسی مشخص شده بررسی و اعتبارسنجی می کند
        /// </summary>
        /// <param name="voucherId">شناسه دیتابیسی سند مورد نظر</param>
        /// <param name="action">عمل مورد نظر</param>
        /// <returns>در صورت مجاز بودن عمل، مقدار خالی و در غیر این صورت
        /// آخرین وضعیت سند را برمی گرداند</returns>
        public async Task<string> ValidateVoucherActionAsync(int voucherId, string action)
        {
            string error = String.Empty;
            var status = await GetLatestVoucherStatusAsync(voucherId);
            var transition = GetValidTransitions()
                .Where(kv => kv.Key == status && kv.Value == action)
                .FirstOrDefault();
            if (transition == null)
            {
                error = status;
            }

            return error;
        }

        /// <summary>
        /// به روش آسنکرون، لیست و تعداد اسناد فاقد آرتیکل را برمیگرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <param name="from">تاریخ شروع گزارش</param>
        /// <param name="to">تاریخ پایان گزارش</param>
        /// <returns>لیست و تعداد اسناد فاقد آرتیکل</returns>
        public async Task<(IList<VoucherViewModel>, int)> GetVouchersWithNoArticleAsync(GridOptions gridOptions, DateTime from, DateTime to)
        {
            var vouchers = Repository.GetAllOperationQuery<Voucher>(
                ViewName.Voucher, voucher => voucher.Lines, voucher => voucher.Status)
                .Where(voucher => voucher.Lines.Count == 0 && voucher.Date.Date >= from.Date && voucher.Date.Date <= to.Date)
                .Select(item => Mapper.Map<VoucherViewModel>(item));

            return await GetListAndCountAsync(gridOptions, vouchers);
        }

        /// <summary>
        /// به روش آسنکرون، لیست و تعداد اسناد دارای نا تراز را برمیگرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <param name="from">تاریخ شروع گزارش</param>
        /// <param name="to">تاریخ پایان گزارش</param>
        /// <returns>لیست و تعداد اسناد نا تراز</returns>
        public async Task<(IList<VoucherViewModel>, int)> GetUnbalancedVouchersAsync(GridOptions gridOptions, DateTime from, DateTime to)
        {
            var vouchers = Repository.GetAllOperationQuery<Voucher>(
                ViewName.Voucher, voucher => voucher.Lines, voucher => voucher.Status)
                .Where(voucher => !voucher.IsBalanced && voucher.Date.Date >= from.Date && voucher.Date.Date <= to.Date)
                .Select(item => Mapper.Map<VoucherViewModel>(item));

            return await GetListAndCountAsync(gridOptions, vouchers);
        }

        /// <summary>
        /// به روش آسنکرون، لیست و تعداد شماره اسناد جا افتاده را برمیگرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <param name="from">تاریخ شروع گزارش</param>
        /// <param name="to">تاریخ پایان گزارش</param>
        /// <returns>لیست و تعداد شماره اسناد جا افتاده</returns>
        public async Task<(IList<NumberListViewModel>, int)> GetMissingVoucherNumbersAsync(GridOptions gridOptions, DateTime from, DateTime to)
        {
            var missNumberList = new List<NumberListViewModel>();
            var vouchers = await Repository.GetAllOperationQuery<Voucher>(ViewName.Voucher)
                .Where(voucher => voucher.Date.Date >= from.Date && voucher.Date.Date <= to.Date)
                .Select(item => Mapper.Map<VoucherViewModel>(item))
                .Apply(gridOptions, false)
                .ToListAsync();

            if (vouchers.Count() > 0)
            {
                var existNumber = vouchers.Select(voucher => voucher.No);

                var minNumber = existNumber.Min();
                var maxNumber = existNumber.Max();

                var numRange = Enumerable.Range(minNumber, maxNumber - minNumber + 1);

                var missNumber = numRange
                    .Where(num => !existNumber.Contains(num));

                int count = missNumber.Count();

                missNumber = missNumber
                    .OrderBy(num => num)
                    .ApplyPaging(gridOptions);

                foreach (var item in missNumber)
                {
                    missNumberList.Add(new NumberListViewModel() { Number = item });
                }

                return (missNumberList, count);
            }

            return (missNumberList, 0);
        }

        internal override int? EntityType
        {
            get { return (int)EntityTypeId.Voucher; }
        }

        /// <summary>
        /// آخرین تغییرات موجودیت را از مدل نمایشی به سطر اطلاعاتی موجود کپی می کند
        /// </summary>
        /// <param name="voucherView">مدل نمایشی شامل آخرین تغییرات</param>
        /// <param name="voucher">سطر اطلاعاتی موجود</param>
        protected override void UpdateExisting(VoucherViewModel voucherView, Voucher voucher)
        {
            voucher.No = voucherView.No;
            voucher.DailyNo = voucherView.DailyNo;
            voucher.Type = voucherView.Type;
            voucher.Date = voucherView.Date;
            voucher.Reference = voucherView.Reference;
            voucher.Association = voucherView.Association;
            voucher.ModifiedById = UserContext.Id;
            voucher.SaveCount++;
            voucher.Description = voucherView.Description;
        }

        /// <summary>
        /// اطلاعات خلاصه سطر اطلاعاتی داده شده را به صورت یک رشته متنی برمی گرداند
        /// </summary>
        /// <param name="entity">یکی از سطرهای اطلاعاتی موجود</param>
        /// <returns>اطلاعات خلاصه سطر اطلاعاتی داده شده به صورت رشته متنی</returns>
        protected override string GetState(Voucher entity)
        {
            return (entity != null)
                ? String.Format(
                    "No : {1}{0}Date : {2}{0}Description : {3}{0}Reference : {4}{0}Association : {5}{0}",
                    Environment.NewLine, entity.No, entity.Date, entity.Description, entity.Reference, entity.Association)
                : null;
        }

        private static async Task<(IList<VoucherViewModel>, int)> GetListAndCountAsync(
            GridOptions gridOptions, IQueryable<VoucherViewModel> vouchers)
        {
            var filteredList = vouchers
                .Apply(gridOptions, false);

            var vouchersList = await filteredList
                .OrderBy(voucher => voucher.Date)
                .ThenBy(voucher => voucher.No)
                .ApplyPaging(gridOptions)
                .ToListAsync();

            return (vouchersList, await filteredList.CountAsync());
        }

        private ISecureRepository Repository
        {
            get { return _system.Repository; }
        }

        private async Task<DateTime> GetLastVoucherDateAsync()
        {
            var repository = UnitOfWork.GetAsyncRepository<Voucher>();
            var lastByDate = await repository
                .GetEntityQuery()
                .Where(voucher => voucher.FiscalPeriodId == UserContext.FiscalPeriodId)
                .OrderByDescending(voucher => voucher.Date)
                .FirstOrDefaultAsync();
            DateTime lastDate;
            if (lastByDate != null)
            {
                lastDate = lastByDate.Date;
            }
            else
            {
                var periodRepository = UnitOfWork.GetAsyncRepository<FiscalPeriod>();
                var fiscalPeriod = await periodRepository.GetByIDAsync(UserContext.FiscalPeriodId);
                lastDate = (fiscalPeriod != null) ? fiscalPeriod.StartDate : DateTime.Now;
            }

            return lastDate;
        }

        private async Task<int> GetLastVoucherNoAsync()
        {
            var repository = UnitOfWork.GetAsyncRepository<Voucher>();
            var lastByNo = await repository
                .GetEntityQuery()
                .Where(voucher => voucher.FiscalPeriodId == UserContext.FiscalPeriodId)
                .OrderByDescending(voucher => voucher.No)
                .FirstOrDefaultAsync();
            return (lastByNo != null) ? lastByNo.No : 0;
        }

        /// <summary>
        /// به روش آسنکرون، شماره روزانه بعدی را برای سند مورد نظر به دست آورده و برمی گرداند
        /// </summary>
        /// <param name="voucher">مدل نمایشی سند مورد نظر</param>
        /// <returns>شماره روزانه بعدی</returns>
        private async Task<int> GetNextDailyNoAsync(VoucherViewModel voucher)
        {
            var repository = UnitOfWork.GetAsyncRepository<Voucher>();
            var sameDate = await repository
                .GetByCriteriaAsync(v => v.Date == voucher.Date
                    && v.FiscalPeriodId == voucher.FiscalPeriodId
                    && v.SubjectType == voucher.SubjectType);
            int lastNo = sameDate
                .OrderByDescending(v => v.DailyNo)
                .Select(v => v.DailyNo)
                .FirstOrDefault();
            return (lastNo + 1);
        }

        private async Task ManageDocumentAsync(Voucher voucher)
        {
            if (voucher != null)
            {
                var repository = UnitOfWork.GetAsyncRepository<Document>();
                var voucherRepository = UnitOfWork.GetAsyncRepository<Voucher>();
                var document = await repository.GetSingleByCriteriaAsync(
                    doc => doc.EntityId == voucher.Id && doc.Type.Id == (int)DocumentTypeValue.Voucher,
                    doc => doc.Actions);
                if (document == null)
                {
                    document = new Document()
                    {
                        EntityId = voucher.Id,
                        TypeId = (int)DocumentTypeValue.Voucher
                    };
                    var action = new DocumentAction()
                    {
                        CreatedById = UserContext.Id,
                        ModifiedById = UserContext.Id,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now
                    };
                    action.Document = document;
                    document.Actions.Add(action);
                    repository.Insert(document, doc => doc.Actions);
                    voucher.DocumentId = document.Id;
                    voucherRepository.Update(voucher);
                }
                else
                {
                    var action = document.Actions.Single();
                    action.ModifiedById = UserContext.Id;
                    action.ModifiedDate = DateTime.Now;
                    repository.Update(document, doc => doc.Actions);
                }

                await UnitOfWork.CommitAsync();
            }
        }

        private string GetCurrentUserDisplayName()
        {
            return String.Format("{0}, {1}", UserContext.PersonLastName, UserContext.PersonFirstName);
        }

        private List<KeyValue> GetValidTransitions()
        {
            var transitions = new List<KeyValue>
            {
                new KeyValue(VoucherStatus.Balanced, VoucherAction.Check),
                new KeyValue(VoucherStatus.Checked, VoucherAction.Finalize),
                new KeyValue(VoucherStatus.Checked, VoucherAction.Confirm),
                new KeyValue(VoucherStatus.Checked, VoucherAction.UndoCheck),
                new KeyValue(VoucherStatus.Confirmed, VoucherAction.Approve),
                new KeyValue(VoucherStatus.Confirmed, VoucherAction.UndoConfirm),
                new KeyValue(VoucherStatus.Approved, VoucherAction.UndoApprove),
                new KeyValue(VoucherStatus.Approved, VoucherAction.Finalize)
            };
            return transitions;
        }

        private async Task<string> GetLatestVoucherStatusAsync(int voucherId)
        {
            string status = null;
            var repository = UnitOfWork.GetAsyncRepository<Voucher>();
            var voucher = await repository.GetByIDAsync(voucherId);
            if (voucher != null)
            {
                if (voucher.StatusId == (int)DocumentStatusValue.Finalized)
                {
                    status = VoucherStatus.Finalized;
                }
                else if (voucher.ApprovedById != null)
                {
                    status = VoucherStatus.Approved;
                }
                else if (voucher.ConfirmedById != null)
                {
                    status = VoucherStatus.Confirmed;
                }
                else if (voucher.StatusId == (int)DocumentStatusValue.Checked)
                {
                    status = VoucherStatus.Checked;
                }
                else if (voucher.IsBalanced)
                {
                    status = VoucherStatus.Balanced;
                }
                else
                {
                    status = VoucherStatus.NotBalanced;
                }
            }

            return status;
        }

        private readonly ISystemRepository _system;
        private readonly IUserRepository _userRepository;
    }
}
