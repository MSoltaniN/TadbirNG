using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Common;
using SPPC.Framework.Extensions;
using SPPC.Framework.Helpers;
using SPPC.Framework.Mapper;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.ViewModel.Auth;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.ViewModel.Metadata;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت اطلاعات تفصیلی های شناور را پیاده سازی می کند.
    /// </summary>
    public class DetailAccountRepository : LoggingRepository<DetailAccount, DetailAccountViewModel>, IDetailAccountRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="unitOfWork">پیاده سازی اینترفیس واحد کاری برای انجام عملیات دیتابیسی </param>
        /// <param name="mapper">نگاشت مورد استفاده برای تبدیل کلاس های مدل اطلاعاتی</param>
        /// <param name="metadata">امکان خواندن متادیتا برای یک موجودیت را فراهم می کند</param>
        /// <param name="log">امکان ایجاد لاگ های عملیاتی را در دیتابیس سیستمی برنامه فراهم می کند</param>
        /// <param name="repository">امکان فیلتر اطلاعات روی سطرها و شعبه ها را فراهم می کند</param>
        /// <param name="config">امکان مدیریت تنظیمات برنامه را در دیتابیس فراهم می کند</param>
        public DetailAccountRepository(IAppUnitOfWork unitOfWork, IDomainMapper mapper, IMetadataRepository metadata, IOperationLogRepository log,
            ISecureRepository repository, IConfigRepository config)
            : base(unitOfWork, mapper, metadata, log)
        {
            _repository = repository;
            _configRepository = config;
        }

        /// <summary>
        /// به روش آسنکرون، کلیه تفصیلی های شناوری را که در دوره مالی و شعبه جاری تعریف شده اند،
        /// از دیتابیس خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از تفصیلی های شناور تعریف شده در دوره مالی و شعبه جاری</returns>
        public async Task<IList<DetailAccountViewModel>> GetDetailAccountsAsync(GridOptions gridOptions = null)
        {
            var detailAccounts = await _repository.GetAllAsync<DetailAccount>(ViewName.DetailAccount, facc => facc.Children);
            return detailAccounts
                .Select(item => Mapper.Map<DetailAccountViewModel>(item))
                .Apply(gridOptions)
                .ToList();
        }

        /// <summary>
        /// به روش آسنکرون، کلیه تفصیلی های شناوری را که در دوره مالی و شعبه جاری تعریف شده اند،
        /// به صورت مجموعه ای از کد و نام خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از تفصیلی های شناور تعریف شده در دوره مالی و شعبه جاری</returns>
        public async Task<IList<KeyValue>> GetDetailAccountsLookupAsync(GridOptions gridOptions = null)
        {
            return await _repository.GetAllLookupAsync<DetailAccount>(ViewName.DetailAccount, gridOptions);
        }

        /// <summary>
        /// به روش آسنکرون، تعداد تفصیلی های شناور تعریف شده در دوره مالی و شعبه جاری را
        /// از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>تعداد تفصیلی های شناور تعریف شده در دوره مالی و شعبه جاری</returns>
        public async Task<int> GetCountAsync(GridOptions gridOptions = null)
        {
            return await _repository.GetCountAsync<DetailAccount>(ViewName.DetailAccount, gridOptions);
        }

        /// <summary>
        /// به روش آسنکرون، تفصیلی شناور با شناسه عددی مشخص شده را از دیتابیس خوانده و برمی گرداند
        /// </summary>
        /// <param name="faccountId">شناسه عددی یکی از تفصیلی های شناور موجود</param>
        /// <returns>تفصیلی شناور مشخص شده با شناسه عددی</returns>
        public async Task<DetailAccountViewModel> GetDetailAccountAsync(int faccountId)
        {
            DetailAccountViewModel item = null;
            var repository = UnitOfWork.GetAsyncRepository<DetailAccount>();
            var detailAccount = await repository.GetByIDAsync(faccountId, facc => facc.Children);
            if (detailAccount != null)
            {
                item = Mapper.Map<DetailAccountViewModel>(detailAccount);
            }

            return item;
        }

        /// <summary>
        /// به روش آسنکرون، شناورهای زیرمجموعه را برای تفصیلی شناور مشخص شده خوانده و برمی گرداند
        /// </summary>
        /// <param name="detailId">شناسه یکی از تفصیلی های شناور موجود</param>
        /// <returns>مدل نمایشی تفصیلی های شناور زیرمجموعه</returns>
        public async Task<IList<AccountItemBriefViewModel>> GetDetailAccountChildrenAsync(int detailId)
        {
            var children = await _repository
                .GetAllQuery<DetailAccount>(ViewName.DetailAccount, facc => facc.Children)
                .Where(facc => facc.ParentId == detailId)
                .Select(facc => Mapper.Map<AccountItemBriefViewModel>(facc))
                .ToListAsync();
            return children;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات فراداده ای تعریف شده برای تفصیلی شناور را از دیتابیس خوانده و برمی گرداند
        /// </summary>
        /// <returns>اطلاعات فراداده ای تعریف شده برای تفصیلی شناور</returns>
        public async Task<ViewViewModel> GetDetailAccountMetadataAsync()
        {
            return await Metadata.GetViewMetadataAsync<DetailAccount>();
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات یک تفصیلی شناور را در دیتابیس ایجاد یا اصلاح می کند
        /// </summary>
        /// <param name="detailAccount">تفصیلی شناور مورد نظر برای ایجاد یا اصلاح</param>
        /// <returns>اطلاعات نمایشی تفصیلی شناور ایجاد یا اصلاح شده</returns>
        public async Task<DetailAccountViewModel> SaveDetailAccountAsync(DetailAccountViewModel detailAccount)
        {
            Verify.ArgumentNotNull(detailAccount, "detailAccount");
            DetailAccount detailModel = default(DetailAccount);
            var repository = UnitOfWork.GetAsyncRepository<DetailAccount>();
            if (detailAccount.Id == 0)
            {
                detailModel = Mapper.Map<DetailAccount>(detailAccount);
                await InsertAsync(repository, detailModel);
                await UpdateLevelUsageAsync(detailModel.Level);
            }
            else
            {
                detailModel = await repository.GetByIDAsync(detailAccount.Id);
                if (detailModel != null)
                {
                    await UpdateAsync(repository, detailModel, detailAccount);
                }
            }

            return Mapper.Map<DetailAccountViewModel>(detailModel);
        }

        /// <summary>
        /// به روش آسنکرون، تفصیلی شناور مشخص شده با شناسه عددی را از دیتابیس حذف می کند
        /// </summary>
        /// <param name="faccountId">شناسه عددی تفصیلی شناور مورد نظر برای حذف</param>
        public async Task DeleteDetailAccountAsync(int faccountId)
        {
            var repository = UnitOfWork.GetAsyncRepository<DetailAccount>();
            var detailAccount = await repository.GetByIDAsync(faccountId);
            if (detailAccount != null)
            {
                await DeleteAsync(repository, detailAccount);
                await UpdateLevelUsageAsync(detailAccount.Level);
            }
        }

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که آیا کد تفصیلی شناور مورد نظر تکراری است یا نه
        /// </summary>
        /// <param name="detailAccount">مدل نمایشی تفصیلی شناور مورد نظر</param>
        /// <returns>اگر کد تفصیلی شناور تکراری باشد مقدار "درست" و در غیر این صورت مقدار "نادرست" برمی گرداند</returns>
        public async Task<bool> IsDuplicateDetailAccountAsync(DetailAccountViewModel detailAccount)
        {
            Verify.ArgumentNotNull(detailAccount, "detailAccount");
            var repository = UnitOfWork.GetAsyncRepository<DetailAccount>();
            var detailAccounts = await repository
                .GetByCriteriaAsync(
                    facc => facc.Id != detailAccount.Id
                        && facc.FiscalPeriod.Id <= detailAccount.FiscalPeriodId
                        && facc.FullCode == detailAccount.FullCode);
            return (detailAccounts.Count > 0);
        }

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که آیا تفصیلی شناور انتخاب شده توسط رکوردهای اطلاعاتی دیگر
        /// در حال استفاده است یا نه
        /// </summary>
        /// <param name="faccountId">شناسه یکتای یکی از تفصیلی های شناور موجود</param>
        /// <returns>در حالتی که تفصیلی شناور مشخص شده در حال استفاده باشد مقدار "درست" و در غیر این صورت
        /// مقدار "نادرست" را برمی گرداند</returns>
        public async Task<bool> IsUsedDetailAccountAsync(int faccountId)
        {
            var repository = UnitOfWork.GetAsyncRepository<VoucherLine>();
            var articles = await repository
                .GetByCriteriaAsync(art => art.DetailAccount.Id == faccountId);
            return (articles.Count != 0);
        }

        /// <inheritdoc/>
        public async Task<bool> IsRelatedDetailAccountAsync(int faccountId)
        {
            var accDetailrepository = UnitOfWork.GetAsyncRepository<AccountDetailAccount>();
            int relatedAccounts = await accDetailrepository.GetCountByCriteriaAsync(
                ada => ada.DetailId == faccountId, null);
            return (relatedAccounts > 0);
        }

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که آیا تفصیلی شناور انتخاب شده دارای شناور زیرمجموعه هست یا نه
        /// </summary>
        /// <param name="faccountId">شناسه یکتای یکی از شناور های موجود</param>
        /// <returns>در حالتی که تفصیلی شناور مشخص شده دارای شناور زیرمجموعه باشد مقدار "درست" و در غیر این صورت
        /// مقدار "نادرست" را برمی گرداند</returns>
        public async Task<bool?> HasChildrenAsync(int faccountId)
        {
            bool? hasChildren = null;
            var repository = UnitOfWork.GetAsyncRepository<DetailAccount>();
            var detailAccount = await repository.GetByIDAsync(faccountId, facc => facc.Children);
            if (detailAccount != null)
            {
                hasChildren = detailAccount.Children.Count > 0;
            }

            return hasChildren;
        }

        /// <summary>
        /// به روش آسنکرون، مقدار فیلد FullCode والد هر تفصیلی شناور را برمیگرداند
        /// </summary>
        /// <param name="parentId">شناسه والد هر تفصیلی شناور</param>
        /// <returns>اگر تفصیلی شناور والد نداشته باشد مقدار خالی و اگر والد داشته باشد مقدار FullCode والد را برمیگرداند</returns>
        public async Task<string> GetDetailAccountFullCodeAsync(int parentId)
        {
            var repository = UnitOfWork.GetAsyncRepository<DetailAccount>();
            var detailAccount = await repository.GetByIDAsync(parentId);
            if (detailAccount == null)
            {
                return string.Empty;
            }

            return detailAccount.FullCode;
        }

        /// <summary>
        /// اطلاعات محیطی کاربر جاری برنامه را برای ایجاد لاگ های عملیاتی تنظیم می کند
        /// </summary>
        /// <param name="userContext">اطلاعات دسترسی کاربر به منابع محدود شده مانند نقش ها، دوره های مالی و شعبه ها</param>
        public void SetCurrentContext(UserContextViewModel userContext)
        {
            _repository.SetCurrentContext(userContext);
            SetLoggingContext(userContext);
        }

        /// <summary>
        /// اطلاعات خلاصه سطر اطلاعاتی داده شده را به صورت یک رشته متنی برمی گرداند
        /// </summary>
        /// <param name="entity">یکی از سطرهای اطلاعاتی موجود</param>
        /// <returns>اطلاعات خلاصه سطر اطلاعاتی داده شده به صورت رشته متنی</returns>
        protected override string GetState(DetailAccount entity)
        {
            return (entity != null)
                ? String.Format(
                    "Name : {1}{0}Code : {2}{0}FullCode : {3}{0}Description : {4}",
                    Environment.NewLine, entity.Name, entity.Code, entity.FullCode, entity.Description)
                : null;
        }

        /// <summary>
        /// آخرین تغییرات موجودیت را از مدل نمایشی به سطر اطلاعاتی موجود کپی می کند
        /// </summary>
        /// <param name="detailAccountViewModel">مدل نمایشی شامل آخرین تغییرات</param>
        /// <param name="detailAccount">سطر اطلاعاتی موجود</param>
        protected override void UpdateExisting(DetailAccountViewModel detailAccountViewModel, DetailAccount detailAccount)
        {
            detailAccount.Code = detailAccountViewModel.Code;
            detailAccount.FullCode = detailAccountViewModel.FullCode;
            detailAccount.Name = detailAccountViewModel.Name;
            detailAccount.Level = detailAccountViewModel.Level;
            detailAccount.Description = detailAccountViewModel.Description;
        }

        /// <summary>
        /// به روش آسنکرون، وضعیت استفاده از یکی از سطوح درختی تفصیلی شناور را در دیتابیس بروزرسانی می کند
        /// </summary>
        /// <param name="level">شماره سطح مورد نظر</param>
        /// <remarks>قابل توجه است که در این متد هیچگونه فیلتری روی دوره مالی، شعبه یا سطرهای قابل دسترسی صورت نمی گیرد.
        /// این به این معنی است که اطلاعات سطح مورد نظر در هر شعبه یا دوره مالی ممکن است ایجاد شده باشد. </remarks>
        private async Task UpdateLevelUsageAsync(int level)
        {
            var repository = UnitOfWork.GetAsyncRepository<DetailAccount>();
            int count = await repository.GetCountByCriteriaAsync(facc => facc.Level == level);
            await _configRepository.SaveTreeLevelUsageAsync(ViewName.DetailAccount, level, count);
        }

        private readonly ISecureRepository _repository;
        private readonly IConfigRepository _configRepository;
    }
}
