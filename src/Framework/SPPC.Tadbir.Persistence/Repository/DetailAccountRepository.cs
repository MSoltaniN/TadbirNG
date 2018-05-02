using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SPPC.Framework.Common;
using SPPC.Framework.Mapper;
using SPPC.Framework.Persistence;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.ViewModel.Metadata;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت اطلاعات تفصیلی های شناور را پیاده سازی می کند.
    /// </summary>
    public class DetailAccountRepository : IDetailAccountRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="unitOfWork">پیاده سازی اینترفیس واحد کاری برای انجام عملیات دیتابیسی </param>
        /// <param name="mapper">نگاشت مورد استفاده برای تبدیل کلاس های مدل اطلاعاتی</param>
        /// <param name="decorator">امکان ضمیمه کردن متادیتا به اطلاعات خوانده شده را فراهم می کند</param>
        public DetailAccountRepository(IUnitOfWork unitOfWork, IDomainMapper mapper, IMetadataDecorator decorator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _decorator = decorator;
        }

        /// <summary>
        /// به روش آسنکرون، کلیه تفصیلی های شناوری را که در دوره مالی و شعبه مشخص شده تعریف شده اند،
        /// از دیتابیس خوانده و برمی گرداند
        /// </summary>
        /// <param name="fpId">شناسه عددی یکی از دوره های مالی موجود</param>
        /// <param name="branchId">شناسه عددی یکی از شعب موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از تفصیلی های شناور تعریف شده در دوره مالی و شعبه مشخص شده</returns>
        public async Task<IList<DetailAccountViewModel>> GetDetailAccountsAsync(
            int fpId, int branchId, GridOptions gridOptions = null)
        {
            var repository = _unitOfWork.GetAsyncRepository<DetailAccount>();
            var detailAccounts = await repository
                .GetByCriteriaAsync(
                    facc => facc.FiscalPeriod.Id == fpId
                        && facc.Branch.Id == branchId,
                    gridOptions,
                    facc => facc.FiscalPeriod, facc => facc.Branch);
            return detailAccounts
                .Select(item => _mapper.Map<DetailAccountViewModel>(item))
                .ToList();
        }

        /// <summary>
        /// به روش آسنکرون، تعداد تفصیلی های شناور تعریف شده در دوره مالی و شعبه مشخص شده را
        /// از دیتابیس خوانده و برمی گرداند
        /// </summary>
        /// <param name="fpId">شناسه عددی یکی از دوره های مالی موجود</param>
        /// <param name="branchId">شناسه عددی یکی از شعب موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>تعداد تفصیلی های شناور تعریف شده در دوره مالی و شعبه مشخص شده</returns>
        public async Task<int> GetCountAsync(int fpId, int branchId, GridOptions gridOptions = null)
        {
            var repository = _unitOfWork.GetAsyncRepository<DetailAccount>();
            var count = await repository
                .GetCountByCriteriaAsync(
                    facc => facc.FiscalPeriod.Id == fpId && facc.Branch.Id == branchId,
                    gridOptions);
            return count;
        }

        /// <summary>
        /// به روش آسنکرون، تفصیلی شناور با شناسه عددی مشخص شده را از دیتابیس خوانده و برمی گرداند
        /// </summary>
        /// <param name="faccountId">شناسه عددی یکی از تفصیلی های شناور موجود</param>
        /// <returns>تفصیلی شناور مشخص شده با شناسه عددی</returns>
        public async Task<DetailAccountViewModel> GetDetailAccountAsync(int faccountId)
        {
            DetailAccountViewModel item = null;
            var repository = _unitOfWork.GetAsyncRepository<DetailAccount>();
            var detailAccount = await repository.GetByIDAsync(
                faccountId, facc => facc.FiscalPeriod, facc => facc.Branch);
            if (detailAccount != null)
            {
                item = _mapper.Map<DetailAccountViewModel>(detailAccount);
            }

            return item;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات فراداده ای تعریف شده برای تفصیلی شناور را از دیتابیس خوانده و برمی گرداند
        /// </summary>
        /// <returns>اطلاعات فراداده ای تعریف شده برای تفصیلی شناور</returns>
        public async Task<EntityItemViewModel<DetailAccountViewModel>> GetDetailAccountMetadataAsync()
        {
            return await _decorator.GetDecoratedItemAsync<DetailAccount, DetailAccountViewModel>(null);
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
            var repository = _unitOfWork.GetAsyncRepository<DetailAccount>();
            if (detailAccount.Id == 0)
            {
                detailModel = _mapper.Map<DetailAccount>(detailAccount);
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

            await _unitOfWork.CommitAsync();
            return _mapper.Map<DetailAccountViewModel>(detailModel);
        }

        /// <summary>
        /// به روش آسنکرون، تفصیلی شناور مشخص شده با شناسه عددی را از دیتابیس حذف می کند
        /// </summary>
        /// <param name="faccountId">شناسه عددی تفصیلی شناور مورد نظر برای حذف</param>
        public async Task DeleteDetailAccountAsync(int faccountId)
        {
            var repository = _unitOfWork.GetAsyncRepository<DetailAccount>();
            var detailAccount = await repository.GetByIDAsync(faccountId);
            if (detailAccount != null)
            {
                detailAccount.FiscalPeriod = null;
                detailAccount.Branch = null;
                detailAccount.Parent = null;
                repository.Delete(detailAccount);
                await _unitOfWork.CommitAsync();
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
            var repository = _unitOfWork.GetAsyncRepository<DetailAccount>();
            var detailAccounts = await repository
                .GetByCriteriaAsync(
                    facc => facc.Id != detailAccount.Id
                        && facc.FiscalPeriod.Id == detailAccount.FiscalPeriodId
                        && facc.Code == detailAccount.Code);
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
            var repository = _unitOfWork.GetAsyncRepository<VoucherLine>();
            var articles = await repository
                .GetByCriteriaAsync(art => art.DetailAccount.Id == faccountId);
            return (articles.Count != 0);
        }

        private static void UpdateExistingDetailAccount(DetailAccountViewModel detailViewModel, DetailAccount detail)
        {
            detail.Code = detailViewModel.Code;
            detail.FullCode = detailViewModel.FullCode;
            detail.Name = detailViewModel.Name;
            detail.Level = detailViewModel.Level;
            detail.Description = detailViewModel.Description;
        }

        private IUnitOfWork _unitOfWork;
        private IDomainMapper _mapper;
        private IMetadataDecorator _decorator;
    }
}
