using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Common;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Model.ProductScope;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.Utility;
using SPPC.Tadbir.ViewModel.ProductScope;

namespace SPPC.Tadbir.Persistence.Repository.ProductScope
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت واحدها را پیاده سازی می کند
    /// </summary>
    public class UnitRepository : EntityLoggingRepository<Unit, UnitViewModel>, IUnitRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="context">امکانات مشترک مورد نیاز را برای عملیات دیتابیسی فراهم می کند</param>
        /// <param name="system">امکانات مورد نیاز در دیتابیس های سیستمی را فراهم می کند</param>
        public UnitRepository(IRepositoryContext context, ISystemRepository system)
            : base(context, system.Logger)
        {
            _system = system;
        }

        /// <inheritdoc/>
        public async Task<PagedList<UnitViewModel>> GetUnitsAsync(GridOptions gridOptions)
        {
            Verify.ArgumentNotNull(gridOptions, nameof(gridOptions));
            var units = new List<UnitViewModel>();
            if (gridOptions.Operation != (int)OperationId.Print)
            {
                var query = Repository.GetAllQuery<Unit>(ViewId.Unit);
                units = await query
                    .Select(item => Mapper.Map<UnitViewModel>(item))
                    .ToListAsync();
            }

            await ReadAsync(gridOptions);
            return new PagedList<UnitViewModel>(units, gridOptions);
        }

        /// <inheritdoc/>
        public async Task<UnitViewModel> GetUnitAsync(int unitId)
        {
            UnitViewModel item = null;
            var repository = UnitOfWork.GetAsyncRepository<Unit>();
            var unit = await repository.GetByIDAsync(unitId);
            if (unit != null)
            {
                item = Mapper.Map<UnitViewModel>(unit);
            }

            return item;
        }

        /// <inheritdoc/>
        public async Task<UnitViewModel> SaveUnitAsync(UnitViewModel unit)
        {
            Verify.ArgumentNotNull(unit, nameof(unit));
            Unit unitModel;
            var repository = UnitOfWork.GetAsyncRepository<Unit>();
            if (unit.Id == 0)
            {
                unitModel = Mapper.Map<Unit>(unit);
                await InsertAsync(repository, unitModel);
            }
            else
            {
                unitModel = await repository.GetByIDAsync(unit.Id);
                if (unitModel != null)
                {
                    await UpdateAsync(repository, unitModel, unit);
                }
            }

            return Mapper.Map<UnitViewModel>(unitModel);
        }

        /// <inheritdoc/>
        public async Task DeleteUnitAsync(int unitId)
        {
            var repository = UnitOfWork.GetAsyncRepository<Unit>();
            var unit = await repository.GetByIDAsync(unitId);
            if (unit != null)
            {
                await DeleteAsync(repository, unit);
            }
        }

        /// <inheritdoc/>
        public async Task DeleteUnitsAsync(IList<int> unitIds)
        {
            var repository = UnitOfWork.GetAsyncRepository<Unit>();
            foreach (int unitId in unitIds)
            {
                var unit = await repository.GetByIDAsync(unitId);
                if (unit != null)
                {
                    await DeleteNoLogAsync(repository, unit);
                }
            }

            await OnEntityGroupDeleted(unitIds);
        }

        internal override int? EntityType
        {
            get { return (int?)EntityTypeId.Unit; }
        }

        /// <inheritdoc/>
        protected override void UpdateExisting(UnitViewModel unitViewModel, Unit unit)
        {
            unit.Name = unitViewModel.Name;
            unit.EnName = unitViewModel.EnName;
            unit.Description = unitViewModel.Description;
            unit.Symbol = unitViewModel.Symbol;
            unit.Status = unitViewModel.Status;
            unit.IsActive = unitViewModel.IsActive;
        }

        /// <inheritdoc/>
        protected override string GetState(Unit entity)
        {
            return entity == null
                ?string.Empty
                :$"{AppStrings.UnitName} : {entity.Name}" +
                 $"{AppStrings.Description} : {entity.Description}";
        }

        private ISecureRepository Repository
        {
            get { return _system.Repository; }
        }

        private readonly ISystemRepository _system;
    }
}
