using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SPPC.Framework.Common;
using SPPC.Framework.Extensions;
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
    /// عملیات مورد نیاز برای مدیریت اطلاعات مراکز هزینه را پیاده سازی می کند.
    /// </summary>
    public class CostCenterRepository : LoggingRepository<CostCenter, CostCenterViewModel>, ICostCenterRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="unitOfWork">پیاده سازی اینترفیس واحد کاری برای انجام عملیات دیتابیسی </param>
        /// <param name="mapper">نگاشت مورد استفاده برای تبدیل کلاس های مدل اطلاعاتی</param>
        /// <param name="metadata">امکان خواندن متادیتا برای یک موجودیت را فراهم می کند</param>
        /// <param name="log">امکان ایجاد لاگ های عملیاتی را در دیتابیس سیستمی برنامه فراهم می کند</param>
        /// <param name="repository">امکان فیلتر اطلاعات روی سطرها و شعبه ها را فراهم می کند</param>
        public CostCenterRepository(IAppUnitOfWork unitOfWork, IDomainMapper mapper, IMetadataRepository metadata, IOperationLogRepository log,
            ISecureRepository repository)
            : base(unitOfWork, mapper, metadata, log)
        {
            _repository = repository;
        }

        /// <summary>
        /// به روش آسنکرون، کلیه مراکز هزینه ای را که در دوره مالی و شعبه مشخص شده تعریف شده اند،
        /// از دیتابیس خوانده و برمی گرداند
        /// </summary>
        /// <param name="userAccess">
        /// اطلاعات دسترسی کاربر به منابع محدود شده مانند نقش ها، دوره های مالی و شعبه ها
        /// </param>
        /// <param name="fpId">شناسه عددی یکی از دوره های مالی موجود</param>
        /// <param name="branchId">شناسه عددی یکی از شعب موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از مراکز هزینه تعریف شده در دوره مالی و شعبه مشخص شده</returns>
        public async Task<IList<CostCenterViewModel>> GetCostCentersAsync(
            UserAccessViewModel userAccess, int fpId, int branchId, GridOptions gridOptions = null)
        {
            var costCenters = await _repository.GetAllAsync<CostCenter>(
                userAccess, fpId, branchId, ViewName.CostCenter,
                cc => cc.FiscalPeriod, cc => cc.Branch,
                cc => cc.Parent, cc => cc.Children);
            return costCenters
                .Select(item => Mapper.Map<CostCenterViewModel>(item))
                .Apply(gridOptions)
                .ToList();
        }

        /// <summary>
        /// به روش آسنکرون، کلیه مراکز هزینه ای را که در دوره مالی و شعبه مشخص شده تعریف شده اند،
        /// به صورت مجموعه ای از کد و نام خوانده و برمی گرداند
        /// </summary>
        /// <param name="userAccess">
        /// اطلاعات دسترسی کاربر به منابع محدود شده مانند نقش ها، دوره های مالی و شعبه ها
        /// </param>
        /// <param name="fpId">شناسه عددی یکی از دوره های مالی موجود</param>
        /// <param name="branchId">شناسه عددی یکی از شعب موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از مراکز هزینه تعریف شده در دوره مالی و شعبه مشخص شده</returns>
        public async Task<IList<KeyValue>> GetCostCentersLookupAsync(
            UserAccessViewModel userAccess, int fpId, int branchId, GridOptions gridOptions = null)
        {
            return await _repository.GetAllLookupAsync<CostCenter>(
                userAccess, fpId, branchId, ViewName.CostCenter, gridOptions);
        }

        /// <summary>
        /// به روش آسنکرون، تعداد مراکز هزینه تعریف شده در دوره مالی و شعبه مشخص شده را
        /// از دیتابیس خوانده و برمی گرداند
        /// </summary>
        /// <param name="userAccess">
        /// اطلاعات دسترسی کاربر به منابع محدود شده مانند نقش ها، دوره های مالی و شعبه ها
        /// </param>
        /// <param name="fpId">شناسه عددی یکی از دوره های مالی موجود</param>
        /// <param name="branchId">شناسه عددی یکی از شعب موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>تعداد مراکز هزینه تعریف شده در دوره مالی و شعبه مشخص شده</returns>
        public async Task<int> GetCountAsync(
            UserAccessViewModel userAccess, int fpId, int branchId, GridOptions gridOptions = null)
        {
            return await _repository.GetCountAsync<CostCenter>(
                userAccess, fpId, branchId, ViewName.CostCenter, gridOptions);
        }

        /// <summary>
        /// به روش آسنکرون، مرکز هزینه با شناسه عددی مشخص شده را از دیتابیس خوانده و برمی گرداند
        /// </summary>
        /// <param name="costCenterId">شناسه عددی یکی از مراکز هزینه موجود</param>
        /// <returns>مرکز هزینه مشخص شده با شناسه عددی</returns>
        public async Task<CostCenterViewModel> GetCostCenterAsync(int costCenterId)
        {
            CostCenterViewModel item = null;
            var repository = UnitOfWork.GetAsyncRepository<CostCenter>();
            var costCenter = await repository.GetByIDAsync(
                costCenterId, cc => cc.FiscalPeriod, cc => cc.Branch, cc => cc.Parent, cc => cc.Children);
            if (costCenter != null)
            {
                item = Mapper.Map<CostCenterViewModel>(costCenter);
            }

            return item;
        }

        /// <summary>
        /// به روش آسنکرون، مراکز هزینه زیرمجموعه را برای مرکز هزینه مشخص شده خوانده و برمی گرداند
        /// </summary>
        /// <param name="costCenterId">شناسه یکی از مراکز هزینه موجود</param>
        /// <returns>مدل نمایشی مراکز هزینه زیرمجموعه</returns>
        public async Task<IList<AccountItemBriefViewModel>> GetCostCenterChildrenAsync(int costCenterId)
        {
            var children = new List<AccountItemBriefViewModel>();
            var repository = UnitOfWork.GetAsyncRepository<CostCenter>();
            var costCenter = await repository.GetByIDAsync(costCenterId, cc => cc.Children);
            if (costCenter != null)
            {
                children.AddRange(costCenter.Children.Select(cc => Mapper.Map<AccountItemBriefViewModel>(cc)));
            }

            return children;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات فراداده ای تعریف شده برای مرکز هزینه را از دیتابیس خوانده و برمی گرداند
        /// </summary>
        /// <returns>اطلاعات فراداده ای تعریف شده برای مرکز هزینه</returns>
        public async Task<ViewViewModel> GetCostCenterMetadataAsync()
        {
            return await Metadata.GetViewMetadataAsync<CostCenter>();
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات یک مرکز هزینه را در دیتابیس ایجاد یا اصلاح می کند
        /// </summary>
        /// <param name="costCenter">مرکز هزینه مورد نظر برای ایجاد یا اصلاح</param>
        /// <returns>اطلاعات نمایشی مرکز هزینه ایجاد یا اصلاح شده</returns>
        public async Task<CostCenterViewModel> SaveCostCenterAsync(CostCenterViewModel costCenter)
        {
            Verify.ArgumentNotNull(costCenter, "costCenter");
            CostCenter costCenterModel = default(CostCenter);
            var repository = UnitOfWork.GetAsyncRepository<CostCenter>();
            if (costCenter.Id == 0)
            {
                costCenterModel = Mapper.Map<CostCenter>(costCenter);
                await InsertAsync(repository, costCenterModel);
            }
            else
            {
                costCenterModel = await repository.GetByIDAsync(
                    costCenter.Id, cc => cc.FiscalPeriod, cc => cc.Branch);
                if (costCenterModel != null)
                {
                    await UpdateAsync(repository, costCenterModel, costCenter);
                }
            }

            await UnitOfWork.CommitAsync();
            return Mapper.Map<CostCenterViewModel>(costCenterModel);
        }

        /// <summary>
        /// به روش آسنکرون، مرکز هزینه مشخص شده با شناسه عددی را از دیتابیس حذف می کند
        /// </summary>
        /// <param name="costCenterId">شناسه عددی مرکز هزینه مورد نظر برای حذف</param>
        public async Task DeleteCostCenterAsync(int costCenterId)
        {
            var repository = UnitOfWork.GetAsyncRepository<CostCenter>();
            var costCenter = await repository.GetByIDAsync(costCenterId, cc => cc.Branch);
            if (costCenter != null)
            {
                await DeleteAsync(repository, costCenter);
            }
        }

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که آیا کد مرکز هزینه مورد نظر تکراری است یا نه
        /// </summary>
        /// <param name="costCenter">مدل نمایشی مرکز هزینه مورد نظر</param>
        /// <returns>اگر کد مرکز هزینه تکراری باشد مقدار "درست" و در غیر این صورت مقدار "نادرست" برمی گرداند</returns>
        public async Task<bool> IsDuplicateCostCenterAsync(CostCenterViewModel costCenter)
        {
            Verify.ArgumentNotNull(costCenter, "costCenter");
            var repository = UnitOfWork.GetAsyncRepository<CostCenter>();
            var costCenters = await repository
                .GetByCriteriaAsync(
                    cc => cc.Id != costCenter.Id
                        && cc.FiscalPeriod.Id == costCenter.FiscalPeriodId
                        && cc.FullCode == costCenter.FullCode);
            return (costCenters.Count > 0);
        }

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که آیا مرکز هزینه انتخاب شده توسط رکوردهای اطلاعاتی دیگر
        /// در حال استفاده است یا نه
        /// </summary>
        /// <param name="costCenterId">شناسه یکتای یکی از مراکز هزینه موجود</param>
        /// <returns>در حالتی که مرکز هزینه مشخص شده در حال استفاده باشد مقدار "درست" و در غیر این صورت
        /// مقدار "نادرست" را برمی گرداند</returns>
        public async Task<bool> IsUsedCostCenterAsync(int costCenterId)
        {
            var repository = UnitOfWork.GetAsyncRepository<VoucherLine>();
            var articles = await repository
                .GetByCriteriaAsync(art => art.CostCenter.Id == costCenterId);
            return (articles.Count != 0);
        }

        /// <inheritdoc/>
        public async Task<bool> IsRelatedCostCenterAsync(int costCenterId)
        {
            var accCenterRepository = UnitOfWork.GetAsyncRepository<AccountCostCenter>();
            int relatedAccounts = await accCenterRepository.GetCountByCriteriaAsync(
                ac => ac.CostCenterId == costCenterId, null);
            return (relatedAccounts > 0);
        }

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که آیا مرکز هزینه انتخاب شده دارای زیرمجموعه هست یا نه
        /// </summary>
        /// <param name="costCenterId">شناسه یکتای یکی از مراکز هزینه موجود</param>
        /// <returns>در حالتی که مرکز هزینه مشخص شده دارای زیرمجموعه باشد مقدار "درست" و در غیر این صورت
        /// مقدار "نادرست" را برمی گرداند</returns>
        public async Task<bool?> HasChildrenAsync(int costCenterId)
        {
            bool? hasChildren = null;
            var repository = UnitOfWork.GetAsyncRepository<CostCenter>();
            var costCenter = await repository.GetByIDAsync(costCenterId, cc => cc.Children);
            if (costCenter != null)
            {
                hasChildren = costCenter.Children.Count > 0;
            }

            return hasChildren;
        }

        /// <summary>
        /// به روش آسنکرون، مقدار فیلد FullCode والد هر مرکز هزینه را برمیگرداند
        /// </summary>
        /// <param name="parentId">شناسه والد هر مرکز هزینه</param>
        /// <returns>اگر مرکز هزینه والد نداشته باشد مقدار خالی و اگر والد داشته باشد مقدار FullCode والد را برمیگرداند</returns>
        public async Task<string> GetCostCenterFullCodeAsync(int parentId)
        {
            var repository = UnitOfWork.GetAsyncRepository<CostCenter>();
            var costCenter = await repository.GetByIDAsync(parentId);
            if (costCenter == null)
            {
                return string.Empty;
            }

            return costCenter.FullCode;
        }

        /// <summary>
        /// آخرین تغییرات موجودیت را از مدل نمایشی به سطر اطلاعاتی موجود کپی می کند
        /// </summary>
        /// <param name="costCenterViewModel">مدل نمایشی شامل آخرین تغییرات</param>
        /// <param name="costCenter">سطر اطلاعاتی موجود</param>
        protected override void UpdateExisting(CostCenterViewModel costCenterViewModel, CostCenter costCenter)
        {
            costCenter.Code = costCenterViewModel.Code;
            costCenter.FullCode = costCenterViewModel.FullCode;
            costCenter.Name = costCenterViewModel.Name;
            costCenter.Level = costCenterViewModel.Level;
            costCenter.Description = costCenterViewModel.Description;
        }

        /// <summary>
        /// اطلاعات خلاصه سطر اطلاعاتی داده شده را به صورت یک رشته متنی برمی گرداند
        /// </summary>
        /// <param name="entity">یکی از سطرهای اطلاعاتی موجود</param>
        /// <returns>اطلاعات خلاصه سطر اطلاعاتی داده شده به صورت رشته متنی</returns>
        protected override string GetState(CostCenter entity)
        {
            return (entity != null)
               ? String.Format(
                   "Name : {1}{0}Code : {2}{0}FullCode : {3}{0}Description : {4}",
                   Environment.NewLine, entity.Name, entity.Code, entity.FullCode, entity.Description)
               : null;
        }

        private readonly ISecureRepository _repository;
    }
}
