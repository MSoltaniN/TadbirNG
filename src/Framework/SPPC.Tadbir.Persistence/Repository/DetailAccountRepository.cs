using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SPPC.Framework.Common;
using SPPC.Framework.Helpers;
using SPPC.Framework.Mapper;
using SPPC.Framework.Persistence;
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
    public class DetailAccountRepository : RepositoryBase, IDetailAccountRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="unitOfWork">پیاده سازی اینترفیس واحد کاری برای انجام عملیات دیتابیسی </param>
        /// <param name="mapper">نگاشت مورد استفاده برای تبدیل کلاس های مدل اطلاعاتی</param>
        /// <param name="metadata">امکان خواندن متادیتا برای یک موجودیت را فراهم می کند</param>
        /// <param name="repository">امکان فیلتر اطلاعات روی سطرها و شعبه ها را فراهم می کند</param>
        public DetailAccountRepository(IAppUnitOfWork unitOfWork, IDomainMapper mapper, IMetadataRepository metadata,
            ISecureRepository repository)
            : base(unitOfWork, mapper, metadata)
        {
            _repository = repository;
        }

        /// <summary>
        /// به روش آسنکرون، کلیه تفصیلی های شناوری را که در دوره مالی و شعبه مشخص شده تعریف شده اند،
        /// از دیتابیس خوانده و برمی گرداند
        /// </summary>
        /// <param name="userAccess">
        /// اطلاعات دسترسی کاربر به منابع محدود شده مانند نقش ها، دوره های مالی و شعبه ها
        /// </param>
        /// <param name="fpId">شناسه عددی یکی از دوره های مالی موجود</param>
        /// <param name="branchId">شناسه عددی یکی از شعب موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از تفصیلی های شناور تعریف شده در دوره مالی و شعبه مشخص شده</returns>
        public async Task<IList<DetailAccountViewModel>> GetDetailAccountsAsync(
            UserAccessViewModel userAccess, int fpId, int branchId, GridOptions gridOptions = null)
        {
            var detailAccounts = await _repository.GetAllAsync<DetailAccount>(
                userAccess, fpId, branchId, ViewName.DetailAccount, gridOptions,
                facc => facc.FiscalPeriod, facc => facc.Branch,
                facc => facc.Parent, facc => facc.Children);
            return detailAccounts
                .Select(item => Mapper.Map<DetailAccountViewModel>(item))
                .ToList();
        }

        /// <summary>
        /// به روش آسنکرون، کلیه تفصیلی های شناوری را که در دوره مالی و شعبه مشخص شده تعریف شده اند،
        /// به صورت مجموعه ای از کد و نام خوانده و برمی گرداند
        /// </summary>
        /// <param name="userAccess">
        /// اطلاعات دسترسی کاربر به منابع محدود شده مانند نقش ها، دوره های مالی و شعبه ها
        /// </param>
        /// <param name="fpId">شناسه عددی یکی از دوره های مالی موجود</param>
        /// <param name="branchId">شناسه عددی یکی از شعب موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از تفصیلی های شناور تعریف شده در دوره مالی و شعبه مشخص شده</returns>
        public async Task<IList<KeyValue>> GetDetailAccountsLookupAsync(
            UserAccessViewModel userAccess, int fpId, int branchId, GridOptions gridOptions = null)
        {
            return await _repository.GetAllLookupAsync<DetailAccount>(
                userAccess, fpId, branchId, ViewName.DetailAccount, gridOptions);
        }

        /// <summary>
        /// به روش آسنکرون، تعداد تفصیلی های شناور تعریف شده در دوره مالی و شعبه مشخص شده را
        /// از دیتابیس خوانده و برمی گرداند
        /// </summary>
        /// <param name="userAccess">
        /// اطلاعات دسترسی کاربر به منابع محدود شده مانند نقش ها، دوره های مالی و شعبه ها
        /// </param>
        /// <param name="fpId">شناسه عددی یکی از دوره های مالی موجود</param>
        /// <param name="branchId">شناسه عددی یکی از شعب موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>تعداد تفصیلی های شناور تعریف شده در دوره مالی و شعبه مشخص شده</returns>
        public async Task<int> GetCountAsync(
            UserAccessViewModel userAccess, int fpId, int branchId, GridOptions gridOptions = null)
        {
            return await _repository.GetCountAsync<DetailAccount>(
                userAccess, fpId, branchId, ViewName.DetailAccount, gridOptions);
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
            var detailAccount = await repository.GetByIDAsync(
                faccountId, facc => facc.FiscalPeriod, facc => facc.Branch, facc => facc.Parent, facc => facc.Children);
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
            var children = new List<AccountItemBriefViewModel>();
            var repository = UnitOfWork.GetAsyncRepository<DetailAccount>();
            var detail = await repository.GetByIDAsync(detailId, facc => facc.Children);
            if (detail != null)
            {
                children.AddRange(detail.Children.Select(facc => Mapper.Map<AccountItemBriefViewModel>(facc)));
            }

            return children;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات فراداده ای تعریف شده برای تفصیلی شناور را از دیتابیس خوانده و برمی گرداند
        /// </summary>
        /// <returns>اطلاعات فراداده ای تعریف شده برای تفصیلی شناور</returns>
        public async Task<EntityViewModel> GetDetailAccountMetadataAsync()
        {
            return await Metadata.GetEntityMetadataAsync<DetailAccount>();
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
                repository.Insert(detailModel);
            }
            else
            {
                detailModel = await repository.GetByIDAsync(
                    detailAccount.Id, facc => facc.FiscalPeriod, facc => facc.Branch);
                if (detailModel != null)
                {
                    UpdateExistingDetailAccount(detailAccount, detailModel);
                    repository.Update(detailModel);
                }
            }

            await UnitOfWork.CommitAsync();
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
                detailAccount.FiscalPeriod = null;
                detailAccount.Branch = null;
                detailAccount.Parent = null;
                repository.Delete(detailAccount);
                await UnitOfWork.CommitAsync();
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
                        && facc.FiscalPeriod.Id == detailAccount.FiscalPeriodId
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

        private static void UpdateExistingDetailAccount(DetailAccountViewModel detailViewModel, DetailAccount detail)
        {
            detail.Code = detailViewModel.Code;
            detail.FullCode = detailViewModel.FullCode;
            detail.Name = detailViewModel.Name;
            detail.Level = detailViewModel.Level;
            detail.Description = detailViewModel.Description;
        }

        private readonly ISecureRepository _repository;
    }
}
