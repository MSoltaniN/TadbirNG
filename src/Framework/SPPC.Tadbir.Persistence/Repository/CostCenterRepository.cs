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
    /// عملیات مورد نیاز برای مدیریت اطلاعات مراکز هزینه را پیاده سازی می کند.
    /// </summary>
    public class CostCenterRepository : ICostCenterRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="unitOfWork">پیاده سازی اینترفیس واحد کاری برای انجام عملیات دیتابیسی </param>
        /// <param name="mapper">نگاشت مورد استفاده برای تبدیل کلاس های مدل اطلاعاتی</param>
        /// <param name="decorator">امکان ضمیمه کردن متادیتا به اطلاعات خوانده شده را فراهم می کند</param>
        public CostCenterRepository(IUnitOfWork unitOfWork, IDomainMapper mapper, IMetadataDecorator decorator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _decorator = decorator;
        }

        /// <summary>
        /// به روش آسنکرون، کلیه مراکز هزینه ای را که در دوره مالی و شعبه مشخص شده تعریف شده اند،
        /// از دیتابیس خوانده و برمی گرداند
        /// </summary>
        /// <param name="fpId">شناسه عددی یکی از دوره های مالی موجود</param>
        /// <param name="branchId">شناسه عددی یکی از شعب موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از مراکز هزینه تعریف شده در دوره مالی و شعبه مشخص شده</returns>
        public async Task<IList<CostCenterViewModel>> GetCostCentersAsync(
            int fpId, int branchId, GridOptions gridOptions = null)
        {
            var repository = _unitOfWork.GetAsyncRepository<CostCenter>();
            var costCenters = await repository
                .GetByCriteriaAsync(
                    cc => cc.FiscalPeriod.Id == fpId
                        && cc.Branch.Id == branchId,
                    gridOptions,
                    cc => cc.FiscalPeriod, cc => cc.Branch, cc => cc.Parent, cc => cc.Children);
            return costCenters
                .Select(item => _mapper.Map<CostCenterViewModel>(item))
                .ToList();
        }

        /// <summary>
        /// به روش آسنکرون، تعداد مراکز هزینه تعریف شده در دوره مالی و شعبه مشخص شده را
        /// از دیتابیس خوانده و برمی گرداند
        /// </summary>
        /// <param name="fpId">شناسه عددی یکی از دوره های مالی موجود</param>
        /// <param name="branchId">شناسه عددی یکی از شعب موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>تعداد مراکز هزینه تعریف شده در دوره مالی و شعبه مشخص شده</returns>
        public async Task<int> GetCountAsync(int fpId, int branchId, GridOptions gridOptions = null)
        {
            var repository = _unitOfWork.GetAsyncRepository<CostCenter>();
            var count = await repository
                .GetCountByCriteriaAsync(
                    cc => cc.FiscalPeriod.Id == fpId && cc.Branch.Id == branchId,
                    gridOptions);
            return count;
        }

        /// <summary>
        /// به روش آسنکرون، مرکز هزینه با شناسه عددی مشخص شده را از دیتابیس خوانده و برمی گرداند
        /// </summary>
        /// <param name="costCenterId">شناسه عددی یکی از مراکز هزینه موجود</param>
        /// <returns>مرکز هزینه مشخص شده با شناسه عددی</returns>
        public async Task<CostCenterViewModel> GetCostCenterAsync(int costCenterId)
        {
            CostCenterViewModel item = null;
            var repository = _unitOfWork.GetAsyncRepository<CostCenter>();
            var costCenter = await repository.GetByIDAsync(
                costCenterId, cc => cc.FiscalPeriod, cc => cc.Branch, cc => cc.Parent, cc => cc.Children);
            if (costCenter != null)
            {
                item = _mapper.Map<CostCenterViewModel>(costCenter);
            }

            return item;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات فراداده ای تعریف شده برای مرکز هزینه را از دیتابیس خوانده و برمی گرداند
        /// </summary>
        /// <returns>اطلاعات فراداده ای تعریف شده برای مرکز هزینه</returns>
        public async Task<EntityItemViewModel<CostCenterViewModel>> GetCostCenterMetadataAsync()
        {
            return await _decorator.GetDecoratedItemAsync<CostCenter, CostCenterViewModel>(null);
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
            var repository = _unitOfWork.GetAsyncRepository<CostCenter>();
            if (costCenter.Id == 0)
            {
                costCenterModel = _mapper.Map<CostCenter>(costCenter);
                repository.Insert(costCenterModel);
            }
            else
            {
                costCenterModel = await repository.GetByIDAsync(
                    costCenter.Id, cc => cc.FiscalPeriod, cc => cc.Branch);
                if (costCenterModel != null)
                {
                    UpdateExistingCostCenter(costCenter, costCenterModel);
                    repository.Update(costCenterModel);
                }
            }

            await _unitOfWork.CommitAsync();
            return _mapper.Map<CostCenterViewModel>(costCenterModel);
        }

        /// <summary>
        /// به روش آسنکرون، مرکز هزینه مشخص شده با شناسه عددی را از دیتابیس حذف می کند
        /// </summary>
        /// <param name="costCenterId">شناسه عددی مرکز هزینه مورد نظر برای حذف</param>
        public async Task DeleteCostCenterAsync(int costCenterId)
        {
            var repository = _unitOfWork.GetAsyncRepository<CostCenter>();
            var costCenter = await repository.GetByIDAsync(costCenterId);
            if (costCenter != null)
            {
                costCenter.FiscalPeriod = null;
                costCenter.Branch = null;
                costCenter.Parent = null;
                repository.Delete(costCenter);
                await _unitOfWork.CommitAsync();
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
            var repository = _unitOfWork.GetAsyncRepository<CostCenter>();
            var costCenters = await repository
                .GetByCriteriaAsync(
                    cc => cc.Id != costCenter.Id
                        && cc.FiscalPeriod.Id == costCenter.FiscalPeriodId
                        && cc.Code == costCenter.Code);
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
            var repository = _unitOfWork.GetAsyncRepository<VoucherLine>();
            var articles = await repository
                .GetByCriteriaAsync(art => art.CostCenter.Id == costCenterId);
            return (articles.Count != 0);
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
            var repository = _unitOfWork.GetAsyncRepository<CostCenter>();
            var costCenter = await repository.GetByIDAsync(costCenterId, cc => cc.Children);
            if (costCenter != null)
            {
                hasChildren = costCenter.Children.Count > 0;
            }

            return hasChildren;
        }

        private static void UpdateExistingCostCenter(CostCenterViewModel costCenterViewModel, CostCenter costCenter)
        {
            costCenter.Code = costCenterViewModel.Code;
            costCenter.FullCode = costCenterViewModel.FullCode;
            costCenter.Name = costCenterViewModel.Name;
            costCenter.Level = costCenterViewModel.Level;
            costCenter.Description = costCenterViewModel.Description;
        }

        private IUnitOfWork _unitOfWork;
        private IDomainMapper _mapper;
        private IMetadataDecorator _decorator;
    }
}
