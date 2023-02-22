using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Common;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Model.CashFlow;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.Utility;
using SPPC.Tadbir.ViewModel.CashFlow;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت صندوق ها را پیاده سازی می کند
    /// </summary>
    public class CashRegisterRepository : EntityLoggingRepository<CashRegister, CashRegisterViewModel>, ICashRegisterRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="context">امکانات مشترک مورد نیاز را برای عملیات دیتابیسی فراهم می کند</param>
        /// <param name="system">امکانات مورد نیاز در دیتابیس های سیستمی را فراهم می کند</param>
        public CashRegisterRepository(IRepositoryContext context, ISystemRepository system)
            : base(context, system.Logger)
        {
            _system = system;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات کلیه صندوق ها را خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از صندوق ها تعریف شده</returns>
        public async Task<PagedList<CashRegisterViewModel>> GetCashRegistersAsync(GridOptions gridOptions = null)
        {
            var options = gridOptions ?? new GridOptions();
            var cashRegisters = new List<CashRegisterViewModel>();
            if (options.Operation != (int)OperationId.Print)
            {
                var query = Repository.GetAllQuery<CashRegister>(ViewId.CashRegister);
                cashRegisters = await query
                    .Select(item => Mapper.Map<CashRegisterViewModel>(item))
                    .ToListAsync();
            }

            await ReadAsync(options);
            return new PagedList<CashRegisterViewModel>(cashRegisters, options);
        }

        /// <summary>
        /// به روش آسنکرون، صندوق با شناسه عددی مشخص شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="cashRegisterId">شناسه عددی یکی از صندوق ها موجود</param>
        /// <returns>صندوق مشخص شده با شناسه عددی</returns>
        public async Task<CashRegisterViewModel> GetCashRegisterAsync(int cashRegisterId)
        {
            CashRegisterViewModel item = null;
            var repository = UnitOfWork.GetAsyncRepository<CashRegister>();
            var cashRegister = await repository.GetByIDAsync(cashRegisterId);
            if (cashRegister != null)
            {
                item = Mapper.Map<CashRegisterViewModel>(cashRegister);
            }

            return item;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات یک صندوق را ایجاد یا اصلاح می کند
        /// </summary>
        /// <param name="cashRegister">صندوق مورد نظر برای ایجاد یا اصلاح</param>
        /// <returns>اطلاعات نمایشی صندوق ایجاد یا اصلاح شده</returns>
        public async Task<CashRegisterViewModel> SaveCashRegisterAsync(CashRegisterViewModel cashRegister)
        {
            Verify.ArgumentNotNull(cashRegister, nameof(cashRegister));
            CashRegister cashRegisterModel;
            var repository = UnitOfWork.GetAsyncRepository<CashRegister>();
            if (cashRegister.Id == 0)
            {
                cashRegisterModel = Mapper.Map<CashRegister>(cashRegister);
                await InsertAsync(repository, cashRegisterModel);
            }
            else
            {
                cashRegisterModel = await repository.GetByIDAsync(cashRegister.Id);
                if (cashRegisterModel != null)
                {
                    await UpdateAsync(repository, cashRegisterModel, cashRegister);
                }
            }

            return Mapper.Map<CashRegisterViewModel>(cashRegisterModel);
        }

        /// <summary>
        /// به روش آسنکرون، صندوق مشخص شده با شناسه عددی را حذف می کند
        /// </summary>
        /// <param name="cashRegisterId">شناسه عددی صندوق مورد نظر برای حذف</param>
        public async Task DeleteCashRegisterAsync(int cashRegisterId)
        {
            var repository = UnitOfWork.GetAsyncRepository<CashRegister>();
            var cashRegister = await repository.GetByIDAsync(cashRegisterId);
            if (cashRegister != null)
            {
                await DeleteAsync(repository, cashRegister);
            }
        }

        /// <summary>
        /// به روش آسنکرون، صندوق ها مشخص شده با شناسه عددی را حذف می کند
        /// </summary>
        /// <param name="cashRegisterIds">مجموعه ای از شناسه های عددی صندوق ها مورد نظر برای حذف</param>
        public async Task DeleteCashRegistersAsync(IList<int> cashRegisterIds)
        {
            var repository = UnitOfWork.GetAsyncRepository<CashRegister>();
            foreach (int cashRegisterId in cashRegisterIds)
            {
                var cashRegister = await repository.GetByIDAsync(cashRegisterId);
                if (cashRegister != null)
                {
                    await DeleteNoLogAsync(repository, cashRegister);
                }
            }

            await OnEntityGroupDeleted(cashRegisterIds);
        }

        internal override int? EntityType
        {
            get { return (int?)EntityTypeId.CashRegister; }
        }

        /// <summary>
        /// آخرین تغییرات موجودیت را از مدل نمایشی به سطر اطلاعاتی موجود کپی می کند
        /// </summary>
        /// <param name="cashRegisterViewModel">مدل نمایشی شامل آخرین تغییرات</param>
        /// <param name="cashRegister">سطر اطلاعاتی موجود</param>
        protected override void UpdateExisting(CashRegisterViewModel cashRegisterViewModel, CashRegister cashRegister)
        {
            cashRegister.Name = cashRegisterViewModel.Name;
            cashRegister.BranchScope = cashRegisterViewModel.BranchScope;
            cashRegister.Description = cashRegisterViewModel.Description;
        }

        /// <summary>
        /// اطلاعات خلاصه سطر اطلاعاتی داده شده را به صورت یک رشته متنی برمی گرداند
        /// </summary>
        /// <param name="entity">یکی از سطرهای اطلاعاتی موجود</param>
        /// <returns>اطلاعات خلاصه سطر اطلاعاتی داده شده به صورت رشته متنی</returns>
        protected override string GetState(CashRegister entity)
        {
            return entity != null
                ? $"{AppStrings.Name}: {entity.Name}, {AppStrings.Description}: {entity.Description}"
                : String.Empty;
        }

        private ISecureRepository Repository
        {
            get { return _system.Repository; }
        }

        private readonly ISystemRepository _system;
    }
}
