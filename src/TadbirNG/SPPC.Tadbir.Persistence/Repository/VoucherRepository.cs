﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Common;
using SPPC.Framework.Extensions;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Model.CashFlow;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.Persistence.Utility;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.Utility;
using SPPC.Tadbir.ViewModel.Finance;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت اطلاعات اسناد مالی و آرتیکل های آنها را پیاده سازی می کند.
    /// </summary>
    public partial class VoucherRepository
        : EntityLoggingRepository<Voucher, VoucherViewModel>, IVoucherRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="context">امکانات مشترک مورد نیاز را برای عملیات دیتابیسی فراهم می کند</param>
        /// <param name="system">امکانات مورد نیاز در دیتابیس های سیستمی را فراهم می کند</param>
        /// <param name="userRepository">امکان خواندن اطلاعات کاربران برنامه را فراهم می کند</param>
        /// <param name="utility">امکانات تکمیلی برای کار با مجموعه های حساب را پیاده سازی می کند</param>
        /// <param name="report"></param>
        public VoucherRepository(IRepositoryContext context, ISystemRepository system,
            IUserRepository userRepository, IAccountCollectionUtility utility,
            IReportDirectUtility report)
            : base(context, system?.Logger)
        {
            _system = system;
            _userRepository = userRepository;
            _report = report;
            _utility = utility;
        }

        /// <summary>
        /// به روش آسنکرون، کلیه اسناد مالی از نوع مفهومی سند حسابداری را که در دوره مالی و شعبه جاری
        /// تعریف شده اند، از دیتابیس خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از اسناد مالی تعریف شده در دوره مالی و شعبه جاری</returns>
        public async Task<PagedList<VoucherViewModel>> GetVouchersAsync(GridOptions gridOptions)
        {
            Verify.ArgumentNotNull(gridOptions, nameof(gridOptions));
            var vouchers = new List<VoucherViewModel>();
            if (gridOptions.Operation != (int)OperationId.Print)
            {
                vouchers.AddRange(await GetVoucherItemsAsync(gridOptions, GetColumnSorting(gridOptions)));
            }

            await ReadAsync(gridOptions);
            return new PagedList<VoucherViewModel>(vouchers, gridOptions);
        }

        /// <summary>
        /// به روش آسنکرون، سند مالی با شناسه دیتابیسی مشخص شده را از دیتابیس خوانده و برمی گرداند
        /// </summary>
        /// <param name="voucherId">شناسه دیتابیسی یکی از اسناد مالی موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>سند مالی مشخص شده با شناسه دیتابیسی</returns>
        public async Task<VoucherViewModel> GetVoucherAsync(int voucherId, GridOptions gridOptions = null)
        {
            VoucherViewModel voucher = null;
            var repository = UnitOfWork.GetAsyncRepository<Voucher>();
            var existing = await repository.GetByIDAsync(
                voucherId, v => v.Lines, v => v.Status, v => v.Origin);
            if (existing != null)
            {
                voucher = Mapper.Map<VoucherViewModel>(existing);
                await SetVoucherNavigationAsync(voucher, gridOptions);
            }

            return voucher;
        }

        /// <summary>
        /// به روش آسنکرون، سند مالی جدیدی را با مقادیر پیشنهادی ایجاد کرده و برمی گرداند
        /// </summary>
        /// <param name="subject">نوع مفهومی مورد نظر برای سند جدید که پیش فرض آن سند عادی است</param>
        /// <returns>سند مالی جدید با مقادیر پیشنهادی</returns>
        public async Task<VoucherViewModel> GetNewVoucherAsync(SubjectType subject = SubjectType.Normal)
        {
            int lastNo = await GetLastVoucherNoAsync(subject);
            DateTime lastDate = await GetLastVoucherDateAsync(subject);
            var newVoucher = new VoucherViewModel()
            {
                Date = lastDate,
                No = lastNo + 1,
                BranchId = UserContext.BranchId,
                FiscalPeriodId = UserContext.FiscalPeriodId,
                Type = (short)VoucherType.NormalVoucher,
                SubjectType = (short)subject,
                OriginId = (int)VoucherOriginId.NormalVoucher,
                SaveCount = 0
            };

            newVoucher = await SaveVoucherAsync(newVoucher);
            await SetVoucherNavigationAsync(newVoucher);
            return newVoucher;
        }

        /// <summary>
        /// به روش آسنکرون، سند مالی با شماره مشخص شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="voucherNo">شماره یکی از اسناد مالی موجود</param>
        /// <param name="subject">نوع مفهومی مورد نظر برای سند که پیش فرض آن سند عادی است</param>
        /// <returns>سند مالی مشخص شده با شماره</returns>
        public async Task<VoucherViewModel> GetVoucherByNoAsync(
            int voucherNo, SubjectType subject = SubjectType.Normal)
        {
            var byNo = default(VoucherViewModel);
            var repository = UnitOfWork.GetAsyncRepository<Voucher>();
            var voucherByNo = Repository.ApplyRowFilter(await repository.GetFirstByCriteriaAsync(
                v => v.FiscalPeriodId == UserContext.FiscalPeriodId
                    && v.No == voucherNo
                    && v.SubjectType == (short)subject, v => v.Lines), ViewId.Voucher);
            if (voucherByNo != null)
            {
                byNo = Mapper.Map<VoucherViewModel>(voucherByNo);
                await SetVoucherNavigationAsync(byNo);
            }

            return byNo;
        }

        /// <summary>
        /// به روش آسنکرون، اولین سند مالی را خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>اولین سند مالی</returns>
        public async Task<VoucherViewModel> GetFirstVoucherAsync(GridOptions gridOptions)
        {
            var vouchers = await GetVoucherItemsAsync(gridOptions, DefaultSorting);
            var firstVoucher = vouchers
                .Apply(gridOptions, false)
                .FirstOrDefault();
            if (firstVoucher != null)
            {
                await SetVoucherNavigationAsync(firstVoucher, gridOptions);
            }

            return firstVoucher;
        }

        /// <summary>
        /// به روش آسنکرون، اولین سند مالی از نوع مشخص شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="subject">نوع مفهومی سند مورد نظر که به صورت پیش فرض سند عادی است</param>
        /// <returns>اولین سند مالی از نوع مشخص شده</returns>
        public async Task<VoucherViewModel> GetFirstVoucherAsync(SubjectType subject = SubjectType.Normal)
        {
            var firstVoucher = default(VoucherViewModel);
            var first = await Repository
                .GetAllOperationQuery<Voucher>(ViewId.Voucher, v => v.Status, v => v.Origin)
                .Where(v => v.SubjectType == (short)subject)
                .OrderBy(v => v.No)
                .FirstOrDefaultAsync();
            if (first != null)
            {
                firstVoucher = Mapper.Map<VoucherViewModel>(first);
                await SetVoucherNavigationAsync(firstVoucher);
            }

            return firstVoucher;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات سند مالی قبلی را خوانده و برمی گرداند
        /// </summary>
        /// <param name="currentNo">شماره سند مالی جاری در برنامه</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>سند مالی قبلی</returns>
        public async Task<VoucherViewModel> GetPreviousVoucherAsync(int currentNo, GridOptions gridOptions)
        {
            var vouchers = await GetVoucherItemsAsync(gridOptions, DefaultDescendingSorting);
            var previousVoucher = vouchers
                .Apply(gridOptions, false)
                .Where(v => v.No < currentNo)
                .FirstOrDefault();
            if (previousVoucher != null)
            {
                await SetVoucherNavigationAsync(previousVoucher, gridOptions);
            }

            return previousVoucher;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات سند مالی قبلی از نوع مشخص شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="currentNo">شماره سند مالی جاری در برنامه</param>
        /// <param name="subject">نوع مفهومی سند مورد نظر که به صورت پیش فرض سند عادی است</param>
        /// <returns>سند مالی قبلی از نوع مشخص شده</returns>
        public async Task<VoucherViewModel> GetPreviousVoucherAsync(int currentNo, SubjectType subject = SubjectType.Normal)
        {
            var previousVoucher = default(VoucherViewModel);
            var previous = await Repository
                .GetAllOperationQuery<Voucher>(ViewId.Voucher, v => v.Status, v => v.Origin)
                .Where(v => v.SubjectType == (short)subject
                    && v.No < currentNo)
                .OrderByDescending(v => v.No)
                .FirstOrDefaultAsync();
            if (previous != null)
            {
                previousVoucher = Mapper.Map<VoucherViewModel>(previous);
                await SetVoucherNavigationAsync(previousVoucher);
            }

            return previousVoucher;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات سند مالی بعدی را خوانده و برمی گرداند
        /// </summary>
        /// <param name="currentNo">شماره سند مالی جاری در برنامه</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>سند مالی بعدی</returns>
        public async Task<VoucherViewModel> GetNextVoucherAsync(int currentNo, GridOptions gridOptions)
        {
            var vouchers = await GetVoucherItemsAsync(gridOptions, DefaultSorting);
            var nextVoucher = vouchers
                .Apply(gridOptions, false)
                .Where(v => v.No > currentNo)
                .FirstOrDefault();
            if (nextVoucher != null)
            {
                await SetVoucherNavigationAsync(nextVoucher, gridOptions);
            }

            return nextVoucher;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات سند مالی بعدی از نوع مشخص شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="currentNo">شماره سند مالی جاری در برنامه</param>
        /// <param name="subject">نوع مفهومی سند مورد نظر که به صورت پیش فرض سند عادی است</param>
        /// <returns>سند مالی بعدی از نوع مشخص شده</returns>
        public async Task<VoucherViewModel> GetNextVoucherAsync(int currentNo, SubjectType subject = SubjectType.Normal)
        {
            var nextVoucher = default(VoucherViewModel);
            var next = await Repository
                .GetAllOperationQuery<Voucher>(ViewId.Voucher, v => v.Status, v => v.Origin)
                .Where(v => v.SubjectType == (short)subject
                    && v.No > currentNo)
                .OrderBy(v => v.No)
                .FirstOrDefaultAsync();
            if (next != null)
            {
                nextVoucher = Mapper.Map<VoucherViewModel>(next);
                await SetVoucherNavigationAsync(nextVoucher);
            }

            return nextVoucher;
        }

        /// <summary>
        /// به روش آسنکرون، آخرین سند مالی را خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>آخرین سند مالی</returns>
        public async Task<VoucherViewModel> GetLastVoucherAsync(GridOptions gridOptions)
        {
            var vouchers = await GetVoucherItemsAsync(gridOptions, DefaultDescendingSorting);
            var lastVoucher = vouchers
                .Apply(gridOptions, false)
                .FirstOrDefault();
            if (lastVoucher != null)
            {
                await SetVoucherNavigationAsync(lastVoucher, gridOptions);
            }

            return lastVoucher;
        }

        /// <summary>
        /// به روش آسنکرون، آخرین سند مالی از نوع مشخص شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="subject">نوع مفهومی سند مورد نظر که به صورت پیش فرض سند عادی است</param>
        /// <returns>آخرین سند مالی از نوع مشخص شده</returns>
        public async Task<VoucherViewModel> GetLastVoucherAsync(SubjectType subject = SubjectType.Normal)
        {
            var lastVoucher = default(VoucherViewModel);
            var last = await Repository
                .GetAllOperationQuery<Voucher>(ViewId.Voucher, v => v.Status, v => v.Origin)
                .Where(v => v.SubjectType == (short)subject)
                .OrderByDescending(v => v.No)
                .FirstOrDefaultAsync();
            if (last != null)
            {
                lastVoucher = Mapper.Map<VoucherViewModel>(last);
                await SetVoucherNavigationAsync(lastVoucher);
            }

            return lastVoucher;
        }

        /// <summary>
        /// به روش آسنکرون، تعداد اسناد مالی تعریف شده در دوره مالی و شعبه جاری را
        /// خوانده و برمی گرداند
        /// </summary>
        /// <typeparam name="TViewModel">نوع مدل نمایشی که برای نمایش اطلاعات موجودیت استفاده می شود</typeparam>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>تعداد اسناد مالی تعریف شده در دوره مالی و شعبه مشخص شده</returns>
        public async Task<int> GetCountAsync<TViewModel>(GridOptions gridOptions = null)
            where TViewModel : class, new()
        {
            var repository = UnitOfWork.GetAsyncRepository<Voucher>();
            var vouchers = await repository
                .GetEntityQuery()
                .Where(v => v.FiscalPeriodId == UserContext.FiscalPeriodId
                    && v.SubjectType != (short)SubjectType.Draft)
                .Select(item => Mapper.Map<TViewModel>(item))
                .ToListAsync();
            return vouchers
                .Apply(gridOptions, false)
                .Count();
        }

        /// <summary>
        /// به روش آسنکرون، تعداد سندهای دوره مالی جاری با وضعیت ثبتی داده شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="status">وضعیت ثبتی مورد نظر برای سند</param>
        /// <param name="subject">نوع مفهومی اسناد مورد نظر که به طور پیش فرض سند عادی است</param>
        /// <returns>تعداد سندهای دوره مالی جاری با وضعیت ثبتی مورد نظر</returns>
        public async Task<int> GetCountByStatusAsync(DocumentStatusId status, SubjectType subject = SubjectType.Normal)
        {
            var repository = UnitOfWork.GetAsyncRepository<Voucher>();
            return await repository.GetCountByCriteriaAsync(
                v => v.FiscalPeriodId == UserContext.FiscalPeriodId
                && v.SubjectType == (short)subject
                && v.StatusId == (int)status);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات محدوده سندهای قابل دسترسی توسط کاربر جاری برنامه را خوانده و برمی گرداند
        /// </summary>
        /// <returns>محدوده سندهای قابل دسترسی توسط کاربر جاری</returns>
        public async Task<NumberedItemRangeViewModel> GetVoucherRangeInfoAsync()
        {
            var query = Repository.GetAllOperationQuery<Voucher>(ViewId.Voucher)
                .OrderBy(voucher => voucher.No);
            var first = await query.FirstOrDefaultAsync();
            var last = await query.LastOrDefaultAsync();
            return new NumberedItemRangeViewModel()
            {
                ViewId = ViewId.Voucher,
                FirstNo = (first != null) ? first.No : 0,
                LastNo = (last != null) ? last.No : 0
            };
        }

        /// <summary>
        /// به روش آسنکرون، مدل نمایشی دوره مالی مورد استفاده در یک سند مالی را خوانده و برمی گرداند
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
            voucher.DailyNo = await GetNextDailyNoAsync(voucher.Date, (SubjectType)voucher.SubjectType);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات یک سند مالی را در دیتابیس ایجاد یا اصلاح می کند
        /// </summary>
        /// <param name="voucherView">سند مالی برای ایجاد یا اصلاح</param>
        /// <returns>مدل نمایشی سند ایجاد یا اصلاح شده</returns>
        public async Task<VoucherViewModel> SaveVoucherAsync(VoucherViewModel voucherView)
        {
            Verify.ArgumentNotNull(voucherView, "voucherView");
            Voucher voucher;
            var displayName = await _userRepository.GetCurrentUserDisplayNameAsync();
            var repository = UnitOfWork.GetAsyncRepository<Voucher>();
            if (voucherView.Id == 0)
            {
                voucher = Mapper.Map<Voucher>(voucherView);
                voucher.StatusId = (int)DocumentStatusId.NotChecked;
                voucher.CreatedById = UserContext.Id;
                voucher.ModifiedById = UserContext.Id;
                voucher.IssuerName =
                    voucher.ModifierName = displayName;
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

            return Mapper.Map<VoucherViewModel>(voucher);
        }

        /// <summary>
        /// به روش آسنکرون، سند مالی مشخص شده با شناسه دیتابیسی را از دیتابیس حذف می کند
        /// </summary>
        /// <param name="voucherId">شناسه دیتابیسی سند مالی برای حذف</param>
        public async Task DeleteVoucherAsync(int voucherId)
        {
            var repository = UnitOfWork.GetAsyncRepository<Voucher>();
            var voucher = await repository.GetByIDWithTrackingAsync(voucherId, v => v.Lines);
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
            Verify.ArgumentNotNull(voucher, nameof(voucher));
            var repository = UnitOfWork.GetAsyncRepository<Voucher>();
            int count = await repository
                .GetCountByCriteriaAsync(vch => vch.Id != voucher.Id
                    && vch.No == voucher.No
                    && vch.SubjectType == voucher.SubjectType
                    && vch.FiscalPeriodId == voucher.FiscalPeriodId);
            return count > 0;
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
                        && voucher.Date.Date == v.Date.Date
                        && v.DailyNo != 0
                        && v.DailyNo == voucher.DailyNo
                        && v.SubjectType == voucher.SubjectType
                        && v.FiscalPeriodId == voucher.FiscalPeriodId);
                isDuplicate = (count > 0);
            }

            return isDuplicate;
        }

        /// <summary>
        /// مشخص می کند که سند حسابداری داده شده قابل تبدیل به سند پیش نویس هست یا نه؟
        /// </summary>
        /// <param name="voucher">اطلاعات نمایشی سند حسابداری مورد نظر</param>
        /// <returns>اگر سند داده شده از نوی مفهومی پیش نویس باشد یا در یکی از وضعیت های ثبت نشده یا ثبت شده
        /// باشد مقدار بولی درست و در غیر این صورت مقدار بولی نادرست را برمی گرداند</returns>
        public bool CanSaveAsDraftVoucher(VoucherViewModel voucher)
        {
            Verify.ArgumentNotNull(voucher, nameof(voucher));
            bool canSave = true;
            if (voucher.SubjectType == (short)SubjectType.Draft)
            {
                canSave = voucher.StatusId != (int)DocumentStatusId.Finalized
                    && !voucher.IsConfirmed;    // checking IsApproved is redundant here
            }

            return canSave;
        }

        /// <inheritdoc/>
        public async Task<VoucherViewModel> SetVoucherStatusAsync(int voucherId, DocumentStatusId status)
        {
            Verify.EnumValueIsDefined(typeof(DocumentStatusId), "status", (int)status);
            var repository = UnitOfWork.GetAsyncRepository<Voucher>();
            var voucher = await repository.GetByIDWithTrackingAsync(voucherId);
            var oldStatus = (DocumentStatusId)voucher.StatusId;
            if (voucher != null)
            {
                voucher.StatusId = (int)status;
                repository.Update(voucher);
                OnDocumentStatus(status, oldStatus);
                await FinalizeActionAsync(voucher);
            }

            return Mapper.Map<VoucherViewModel>(voucher);
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
                voucher.ConfirmerName = isConfirmed ? GetCurrentUserFullName() : null;
                repository.Update(voucher);
                OnDocumentConfirmation(isConfirmed);
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
                voucher.ApproverName = isApproved ? GetCurrentUserFullName() : null;
                repository.Update(voucher);
                OnDocumentApproval(isApproved);
                await FinalizeActionAsync(voucher);
            }
        }

        /// /// <summary>
        /// به روش آسنکرون، وضعیت ثبتی اسناد مالی مشخص شده با شناسه عددی راتغییر می دهد
        /// </summary>
        /// <param name="items">مجموعه شناسه های دیتابیسی سطرهای مورد نظر برای تغییر وضعیت</param>
        /// <param name="status">وضعیت جدید مورد نظر برای سند مالی</param>
        public async Task SetVouchersStatusAsync(IEnumerable<int> items, DocumentStatusId status)
        {
            Verify.EnumValueIsDefined(typeof(DocumentStatusId), "status", (int)status);
            if (!items.Any())
            {
                return;
            }

            var repository = UnitOfWork.GetAsyncRepository<Voucher>();
            var first = await repository.GetByIDAsync(items.First());
            if (first != null)
            {
                var oldStatus = (DocumentStatusId)first.StatusId;
                foreach (int item in items)
                {
                    var voucher = await repository.GetByIDAsync(item);
                    if (voucher != null)
                    {
                        voucher.StatusId = (int)status;
                        repository.Update(voucher);
                    }
                }

                var operationId = GetGroupOperationCode(status, oldStatus);
                await OnEntityGroupChangeAsync(items, operationId);
            }
        }

        /// <summary>
        /// به روش آسنکرون، وضعیت تأیید یا تصویب اسناد مالی مشخص شده را تغییر می دهد
        /// </summary>
        /// <param name="items">مجموعه شناسه های دیتابیسی سطرهای مورد نظر برای تغییر وضعیت</param>
        /// <param name="isConfirmed">مشخص می کند که تغییر مورد نظر تأیید/تصویب است یا رفع تأیید/تصویب</param>
        public async Task SetVouchersConfirmApproveStatusAsync(IEnumerable<int> items, bool isConfirmed)
        {
            var repository = UnitOfWork.GetAsyncRepository<Voucher>();
            foreach (int item in items)
            {
                var voucher = await repository.GetByIDAsync(item);
                if (voucher != null)
                {
                    voucher.ConfirmedById = isConfirmed ? UserContext.Id : (int?)null;
                    voucher.ApprovedById = isConfirmed ? UserContext.Id : (int?)null;
                    if (isConfirmed)
                    {
                        voucher.ConfirmerName = GetCurrentUserFullName();
                        voucher.ApproverName = GetCurrentUserFullName();
                    }

                    repository.Update(voucher);
                }

                var operationId = isConfirmed ? OperationId.GroupConfirm : OperationId.GroupUndoConfirm;
                await OnEntityGroupChangeAsync(items, operationId);
            }
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات خلاصه سند مالی را خوانده و برمی گرداند
        /// </summary>
        /// <param name="voucherId">شناسه دیتابیسی سند مالی مورد نظر</param>
        /// <returns>اطلاعات خلاصه سند مالی با شناسه دیتابیسی داده شده</returns>
        public async Task<VoucherInfoViewModel> GetVoucherInfoAsync(int voucherId)
        {
            var repository = UnitOfWork.GetAsyncRepository<Voucher>();
            return await repository
                .GetEntityQuery()
                .Where(v => v.Id == voucherId)
                .Select(v => Mapper.Map<VoucherInfoViewModel>(v))
                .FirstOrDefaultAsync();
        }

        /// <inheritdoc/>
        public async Task<bool> HasSystemicArticleAsync(int voucherId)
        {
            var repository = UnitOfWork.GetAsyncRepository<PayReceiveVoucherLine>();
            return await repository.GetEntityQuery()
                .AnyAsync(pv => pv.VoucherLine.VoucherId == voucherId);
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
            voucher.SubjectType = voucherView.SubjectType;
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
            string dateValue = Config.GetDateDisplayAsync(entity.Date).Result;
            return (entity != null)
                ? String.Format(
                    "{0} : {1} , {2} : {3} , {4} : {5} , {6} : {7} , {8} : {9}",
                    AppStrings.No, entity.No, AppStrings.Date, dateValue,
                    AppStrings.Description, entity.Description, AppStrings.Reference, entity.Reference,
                    AppStrings.Association, entity.Association)
                : null;
        }

        /// <summary>
        /// به روش آسنکرون، شماره آخرین سند موجود با نوع مفهومی داده شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="type">نوع مفهومی مورد نظر برای سند</param>
        /// <returns>شماره آخرین سند موجود با نوع داده شده</returns>
        protected async Task<int> GetLastVoucherNoAsync(SubjectType type = SubjectType.Normal)
        {
            var repository = UnitOfWork.GetAsyncRepository<Voucher>();
            var lastByNo = await repository
                .GetEntityQuery()
                .Where(voucher => voucher.FiscalPeriodId == UserContext.FiscalPeriodId
                    && voucher.SubjectType == (short)type)
                .OrderByDescending(voucher => voucher.No)
                .FirstOrDefaultAsync();
            return (lastByNo != null) ? lastByNo.No : 0;
        }

        private ISecureRepository Repository
        {
            get { return _system.Repository; }
        }

        private IConfigRepository Config
        {
            get { return _system.Config; }
        }

        private static string GetTypeName(short subjectType)
        {
            string typeName = SubjectType.Normal.ToString();
            switch (subjectType)
            {
                case (short)SubjectType.Draft:
                    typeName = SubjectType.Draft.ToString();
                    break;
                case (short)SubjectType.Budgeting:
                    typeName = SubjectType.Budgeting.ToString();
                    break;
                default:
                    break;
            }

            return typeName;
        }

        private static string TranslateQuery(string query)
        {
            return query
                .Replace("== null", " IS NULL")
                .Replace("!= null", " IS NOT NULL")
                .Replace("\"", "'")
                .Replace("&&", "AND")
                .Replace("||", "OR")
                .Replace("==", "=")
                .Replace("!=", "<>")
                .Replace("Voucher", "v.")
                .Replace("BranchId", "BranchID")
                .Replace("BranchID", "v.BranchID")
                .Replace("BranchName", "br.Name")
                .Replace("StatusName", "st.Name")
                .Replace("OriginName", "vo.Name");
        }

        private static string GetColumnSorting(GridOptions gridOptions)
        {
            string sorting = DefaultSorting;
            if (gridOptions.SortColumns.Count > 0)
            {
                sorting = String.Join(", ", gridOptions.SortColumns.Select(col => col.ToString()));
            }

            return sorting;
        }

        private async Task<Voucher> GetNewVoucherAsync(string description, VoucherOriginId origin)
        {
            var subject = SubjectType.Normal;
            string fullName = GetCurrentUserFullName();
            DateTime date = await GetLastVoucherDateAsync();
            int no = await GetLastVoucherNoAsync();
            int dailyNo = await GetNextDailyNoAsync(date, subject);
            return new Voucher()
            {
                BranchId = UserContext.BranchId,
                DailyNo = dailyNo,
                Date = date,
                Description = description,
                FiscalPeriodId = UserContext.FiscalPeriodId,
                IsBalanced = true,
                CreatedById = UserContext.Id,
                IssuerName = fullName,
                ModifiedById = UserContext.Id,
                ModifierName = fullName,
                No = no + 1,
                StatusId = (int)DocumentStatusId.NotChecked,
                SubjectType = (short)subject,
                Type = (short)VoucherType.NormalVoucher,
                OriginId = (int)origin
            };
        }

        private async Task<DateTime> GetLastVoucherDateAsync(SubjectType type = SubjectType.Normal)
        {
            var repository = UnitOfWork.GetAsyncRepository<Voucher>();
            var lastByDate = await repository
                .GetEntityQuery()
                .Where(voucher => voucher.FiscalPeriodId == UserContext.FiscalPeriodId
                    && voucher.SubjectType == (short)type)
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

        private async Task<int> GetNextDailyNoAsync(DateTime date, SubjectType subject)
        {
            var repository = UnitOfWork.GetAsyncRepository<Voucher>();
            var sameDate = await repository
                .GetByCriteriaAsync(v => v.Date == date
                    && v.FiscalPeriodId == UserContext.FiscalPeriodId
                    && v.SubjectType == (short)subject);
            int lastNo = sameDate
                .OrderByDescending(v => v.DailyNo)
                .Select(v => v.DailyNo)
                .FirstOrDefault();
            return (lastNo + 1);
        }

        private async Task<int> GetVoucherLineCountAsync(int voucherId)
        {
            var repository = UnitOfWork.GetAsyncRepository<VoucherLine>();
            return await repository.GetCountByCriteriaAsync(line => line.VoucherId == voucherId);
        }

        private async Task SetVoucherNavigationAsync(VoucherViewModel voucher, GridOptions gridOptions = null)
        {
            int nextCount, prevCount;
            var options = gridOptions ?? new GridOptions();
            var query = Repository
                .GetAllOperationQuery<Voucher>(ViewId.Voucher, v => v.Status, v => v.Origin)
                .Where(v => v.No < voucher.No
                    && v.SubjectType == voucher.SubjectType);
            if (!options.IsEmpty)
            {
                var items = await query.ToListAsync();
                prevCount = items
                    .Select(v => Localize(Mapper.Map<VoucherViewModel>(v)))
                    .ApplyQuickFilter(options, false)
                    .Apply(options, false)
                    .Count();
            }
            else
            {
                prevCount = await query.CountAsync();
            }

            query = Repository
                .GetAllOperationQuery<Voucher>(ViewId.Voucher, v => v.Status, v => v.Origin)
                .Where(v => v.No > voucher.No
                    && v.SubjectType == voucher.SubjectType);
            if (!options.IsEmpty)
            {
                var items = await query.ToListAsync();
                nextCount = items
                    .Select(v => Localize(Mapper.Map<VoucherViewModel>(v)))
                    .ApplyQuickFilter(options, false)
                    .Apply(options, false)
                    .Count();
            }
            else
            {
                nextCount = await query.CountAsync();
            }

            voucher.HasPrevious = prevCount > 0;
            voucher.HasNext = nextCount > 0;
        }

        private async Task<IEnumerable<VoucherViewModel>> GetVoucherItemsAsync(
            GridOptions gridOptions, string orderBy)
        {
            var options = gridOptions ?? new GridOptions();
            DbConsole.ConnectionString = UnitOfWork.CompanyConnection;
            string filters = await GetEnvironmentFiltersAsync(options);
            string listQuery = String.Format(
                VoucherQuery.EnvironmentVouchers, filters, orderBy);
            var query = new ReportQuery(listQuery);
            var result = DbConsole.ExecuteQuery(query.Query);
            var vouchers = new List<VoucherViewModel>();
            vouchers.AddRange(result.Rows.Cast<DataRow>()
                .Select(row => GetVoucherItem(row)));
            vouchers = Repository.ApplyRowFilter(vouchers, ViewId.Voucher);
            Array.ForEach(vouchers.ToArray(), v => Localize(v));
            return vouchers;
        }

        private async Task<string> GetEnvironmentFiltersAsync(GridOptions gridOptions)
        {
            var predicates = new List<string>
            {
                String.Format("v.FiscalPeriodID = {0}", UserContext.FiscalPeriodId)
            };
            if (gridOptions.QuickFilter != null)
            {
                predicates.Add(gridOptions.QuickFilter.ToString());
                string branchFilter = await GetBranchFilterAsync(gridOptions);
                if (branchFilter.Contains("IN"))
                {
                    predicates.Add(branchFilter);
                }
            }

            return TranslateQuery(String.Join(" AND ", predicates));
        }

        private VoucherViewModel GetVoucherItem(DataRow row)
        {
            var subjectType = _report.ValueOrDefault<short>(row, "SubjectType");
            int confirmedById = _report.ValueOrDefault<int>(row, "ConfirmedById");
            int approvedById = _report.ValueOrDefault<int>(row, "ApprovedById");
            var voucherItem = new VoucherViewModel()
            {
                Id = _report.ValueOrDefault<int>(row, "VoucherID"),
                No = _report.ValueOrDefault<int>(row, "No"),
                Date = _report.ValueOrDefault<DateTime>(row, "Date"),
                Description = _report.ValueOrDefault(row, "Description"),
                StatusName = _report.ValueOrDefault(row, "StatusName"),
                Reference = _report.ValueOrDefault(row, "Reference"),
                Association = _report.ValueOrDefault(row, "Association"),
                DailyNo = _report.ValueOrDefault<int>(row, "DailyNo"),
                IsBalanced = _report.ValueOrDefault<bool>(row, "IsBalanced"),
                ConfirmerName = _report.ValueOrDefault(row, "ConfirmerName"),
                ApproverName = _report.ValueOrDefault(row, "ApproverName"),
                IsConfirmed = confirmedById > 0,
                IsApproved = approvedById > 0,
                BranchId = _report.ValueOrDefault<int>(row, "BranchID"),
                BranchName = _report.ValueOrDefault(row, "BranchName"),
                IssuerName = _report.ValueOrDefault(row, "IssuerName"),
                OriginName = _report.ValueOrDefault(row, "OriginName"),
                SubjectType = subjectType,
                TypeName = GetTypeName(subjectType)
            };
            return voucherItem;
        }

        private VoucherViewModel Localize(VoucherViewModel voucher)
        {
            if (voucher != null)
            {
                voucher.StatusName = Context.Localize(voucher.StatusName);
                voucher.OriginName = Context.Localize(voucher.OriginName);
                voucher.TypeName = Context.Localize(voucher.TypeName);
                voucher.Description = Context.Localize(voucher.Description);
            }

            return voucher;
        }

        private const string DefaultSorting = "v.No";
        private const string DefaultDescendingSorting = "v.No DESC";
        private readonly ISystemRepository _system;
        private readonly IUserRepository _userRepository;
        private readonly IReportDirectUtility _report;
        private readonly IAccountCollectionUtility _utility;
    }
}
