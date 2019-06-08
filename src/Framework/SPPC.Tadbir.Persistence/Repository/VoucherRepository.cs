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
using SPPC.Tadbir.Extensions;
using SPPC.Tadbir.Model.Core;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.ViewModel.Auth;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.ViewModel.Metadata;

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
        /// <param name="unitOfWork">پیاده سازی اینترفیس واحد کاری برای انجام عملیات دیتابیسی </param>
        /// <param name="mapper">نگاشت مورد استفاده برای تبدیل کلاس های مدل اطلاعاتی</param>
        /// <param name="metadata">امکان خواندن متادیتا برای یک موجودیت را فراهم می کند</param>
        /// <param name="log">امکان ایجاد لاگ های عملیاتی را در دیتابیس سیستمی برنامه فراهم می کند</param>
        /// <param name="repository">امکان فیلتر اطلاعات روی سطرها و شعبه ها را فراهم می کند</param>
        /// <param name="userRepository">امکان خواندن اطلاعات کاربران برنامه را فراهم می کند</param>
        public VoucherRepository(
            IAppUnitOfWork unitOfWork, IDomainMapper mapper, IMetadataRepository metadata,
            IOperationLogRepository log, ISecureRepository repository, IUserRepository userRepository)
            : base(unitOfWork, mapper, metadata, log)
        {
            _repository = repository;
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
            var vouchers = await _repository.GetAllOperationAsync<Voucher>(
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
                BranchId = _currentContext.BranchId,
                FiscalPeriodId = _currentContext.FiscalPeriodId,
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
            var voucherByNo = await _repository
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
            var firstVoucher = await _repository
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
            var previous = await _repository
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
            var next = await _repository
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
            var lastVoucher = await _repository
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
            return await _repository.GetAllOperationQuery<Voucher>(ViewName.Voucher)
                .Where(item => item.SubjectType == 0)
                .Select(item => Mapper.Map<TViewModel>(item))
                .Apply(gridOptions, false)
                .CountAsync();
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
                voucher.IssuedById = _currentContext.Id;
                voucher.ModifiedById = _currentContext.Id;
                voucher.IssuerName = voucher.ModifierName = displayName;
                await InsertAsync(repository, voucher);
            }
            else
            {
                voucher = await repository.GetByIDAsync(voucherView.Id);
                if (voucher != null)
                {
                    voucher.ModifiedById = _currentContext.Id;
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
                        && v.DailyNo == voucher.DailyNo
                        && v.FiscalPeriodId == voucher.FiscalPeriodId);
                isDuplicate = (count > 0);
            }

            return isDuplicate;
        }

        /// <summary>
        /// اطلاعات محیطی و امنیتی کاربر جاری برنامه را برای کنترل قواعد کاری برنامه تنظیم می کند
        /// <para>توجه : فراخوانی این متد با اطلاعات محیطی معتبر برای موفقیت سایر عملیات این کلاس الزامی است</para>
        /// </summary>
        /// <param name="userContext">اطلاعات محیطی و امنیتی کاربر جاری برنامه</param>
        public override void SetCurrentContext(UserContextViewModel userContext)
        {
            base.SetCurrentContext(userContext);
            _repository.SetCurrentContext(userContext);
            _userRepository.SetCurrentContext(userContext);
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
                await UnitOfWork.CommitAsync();
            }
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
            voucher.ModifiedById = _currentContext.Id;
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
                    "Name : {1}{0}Date : {2}{0}Description : {3}{0}Reference : {4}{0}Association : {5}{0}",
                    Environment.NewLine, entity.No, entity.Date, entity.Description, entity.Reference, entity.Association)
                : null;
        }

        private async Task<DateTime> GetLastVoucherDateAsync()
        {
            var repository = UnitOfWork.GetAsyncRepository<Voucher>();
            var lastByDate = await repository
                .GetEntityQuery()
                .Where(voucher => voucher.FiscalPeriodId == _currentContext.FiscalPeriodId)
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
                var fiscalPeriod = await periodRepository.GetByIDAsync(_currentContext.FiscalPeriodId);
                lastDate = (fiscalPeriod != null) ? fiscalPeriod.StartDate : DateTime.Now;
            }

            return lastDate;
        }

        private async Task<int> GetLastVoucherNoAsync()
        {
            var repository = UnitOfWork.GetAsyncRepository<Voucher>();
            var lastByNo = await repository
                .GetEntityQuery()
                .Where(voucher => voucher.FiscalPeriodId == _currentContext.FiscalPeriodId)
                .OrderByDescending(voucher => voucher.No)
                .FirstOrDefaultAsync();
            return (lastByNo != null) ? lastByNo.No : 1;
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
                        CreatedById = _currentContext.Id,
                        ModifiedById = _currentContext.Id,
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
                    action.ModifiedById = _currentContext.Id;
                    action.ModifiedDate = DateTime.Now;
                    repository.Update(document, doc => doc.Actions);
                }

                await UnitOfWork.CommitAsync();
            }
        }

        private readonly ISecureRepository _repository;
        private readonly IUserRepository _userRepository;
    }
}
