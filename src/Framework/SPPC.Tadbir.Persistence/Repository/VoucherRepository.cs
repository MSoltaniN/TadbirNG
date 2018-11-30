using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SPPC.Framework.Common;
using SPPC.Framework.Extensions;
using SPPC.Framework.Mapper;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Domain;
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
        public VoucherRepository(
            IAppUnitOfWork unitOfWork, IDomainMapper mapper, IMetadataRepository metadata,
            IOperationLogRepository log, ISecureRepository repository)
            : base(unitOfWork, mapper, metadata, log)
        {
            _repository = repository;
        }

        /// <summary>
        /// به روش آسنکرون، کلیه اسناد مالی را که در دوره مالی و شعبه جاری تعریف شده اند، از دیتابیس خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از اسناد مالی تعریف شده در دوره مالی و شعبه جاری</returns>
        public async Task<IList<VoucherViewModel>> GetVouchersAsync(GridOptions gridOptions = null)
        {
            var vouchers = await _repository.GetAllOperationAsync<Voucher>(
                ViewName.Voucher, v => v.Lines, v => v.Status);
            return vouchers
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
            VoucherViewModel voucherViewModel = null;
            var repository = UnitOfWork.GetAsyncRepository<Voucher>();
            var voucher = await repository.GetByIDAsync(voucherId, v => v.Lines, v => v.Status);
            if (voucher != null)
            {
                voucherViewModel = Mapper.Map<VoucherViewModel>(voucher);
            }

            return voucherViewModel;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات فراداده ای تعریف شده برای سند مالی را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <returns>اطلاعات فراداده ای تعریف شده برای سند مالی</returns>
        public async Task<ViewViewModel> GetVoucherMetadataAsync()
        {
            return await Metadata.GetViewMetadataAsync<Voucher>();
        }

        /// <summary>
        /// به روش آسنکرون، تعداد اسناد مالی تعریف شده در دوره مالی و شعبه جاری را
        /// از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>تعداد اسناد مالی تعریف شده در دوره مالی و شعبه مشخص شده</returns>
        public async Task<int> GetCountAsync(GridOptions gridOptions = null)
        {
            return await _repository.GetOperationCountAsync<Voucher>(ViewName.Voucher, gridOptions);
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
        /// به روش آسنکرون، اطلاعات یک سند مالی را در دیتابیس ایجاد یا اصلاح می کند
        /// </summary>
        /// <param name="voucherView">سند مالی برای ایجاد یا اصلاح</param>
        /// <returns>مدل نمایشی سند ایجاد یا اصلاح شده</returns>
        public async Task<VoucherViewModel> SaveVoucherAsync(VoucherViewModel voucherView)
        {
            Verify.ArgumentNotNull(voucherView, "voucherView");
            Voucher voucher = default(Voucher);
            var repository = UnitOfWork.GetAsyncRepository<Voucher>();
            if (voucherView.Id == 0)
            {
                voucher = Mapper.Map<Voucher>(voucherView);
                voucher.StatusId = (int)DocumentStatusValue.Draft;
                voucher.CreatedById = _currentContext.Id;
                voucher.ModifiedById = _currentContext.Id;
                await InsertAsync(repository, voucher);
            }
            else
            {
                voucher = await repository.GetByIDAsync(voucherView.Id);
                if (voucher != null)
                {
                    voucher.ModifiedById = _currentContext.Id;
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
                    && vch.FiscalPeriod.Id == voucher.FiscalPeriodId);
            return (duplicates.Count > 0);
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
            voucher.Date = voucherView.Date;
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
                    "Name : {1}{0}Date : {2}{0}Description : {3}",
                    Environment.NewLine, entity.No, entity.Date, entity.Description)
                : null;
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
    }
}
