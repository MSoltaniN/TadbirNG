using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Model;
using SPPC.Tadbir.Model.Core;
using SPPC.Tadbir.Resources;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مرتبط با فعال و غیرفعال کردن یک موجودیت پایه را پیاده سازی می کند
    /// </summary>
    /// <typeparam name="TEntity">نوع کلاس مدل اطلاعاتی برای موجودیت پایه</typeparam>
    /// <typeparam name="TEntityView">نوع کلاس مدل نمایشی برای موجودیت پایه</typeparam>
    public class ActiveStateRepository<TEntity, TEntityView>
        : EntityLoggingRepository<TEntity, TEntityView>, IActiveStateRepository<TEntityView>
        where TEntity : BaseEntity
        where TEntityView : class, IBaseEntityView, new()
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="context">امکانات مشترک مورد نیاز را برای عملیات دیتابیسی فراهم می کند</param>
        /// <param name="logRepository">امکان ایجاد لاگ های عملیاتی و سیستمی را فراهم می کند</param>
        protected ActiveStateRepository(IRepositoryContext context, IOperationLogRepository logRepository)
            : base(context, logRepository)
        {
        }

        /// <inheritdoc/>
        public async Task SetActiveStatusAsync(TEntityView entity, bool isActive)
        {
            var entityModel = Mapper.Map<TEntity>(entity);
            var entityName = typeof(TEntity).Name;
            if (isActive)
            {
                await ReactivateAsync(entityModel, entityName);
            }
            else
            {
                await DeactivateAsync(entityModel, entityName);
            }
        }

        /// <inheritdoc/>
        public async Task<bool> IsDeactivatedAsync(int itemId)
        {
            var repository = UnitOfWork.GetAsyncRepository<InactiveEntity>();
            var isDeactivated = await repository
                .GetEntityQuery()
                .Where(item => item.FiscalPeriodId == UserContext.FiscalPeriodId
                    && item.BranchId == UserContext.BranchId
                    && item.EntityId == itemId
                    && item.EntityName == typeof(TEntity).Name)
                .AnyAsync();
            return isDeactivated;
        }

        /// <summary>
        /// به روش آسنکرون، سطرهای اطلاعاتی غیرفعال را در فهرست اطلاعاتی ورودی به روزرسانی می کند
        /// </summary>
        /// <param name="items">مجموعه سطرهای اطلاعاتی مورد نظر برای به روزرسانی وضعیت</param>
        protected async Task UpdateInactiveItemsAsync(IEnumerable<TEntityView> items)
        {
            var repository = UnitOfWork.GetAsyncRepository<InactiveEntity>();
            var inactiveIds = await repository
                .GetEntityQuery()
                .Where(item => item.FiscalPeriodId == UserContext.FiscalPeriodId
                    && item.BranchId == UserContext.BranchId
                    && item.EntityName == typeof(TEntity).Name)
                .Select(item => item.EntityId)
                .ToListAsync();
            foreach (var item in items)
            {
                if (inactiveIds.Contains(item.Id))
                {
                    item.State = AppStrings.Inactive;
                }
            }
        }

        /// <summary>
        /// به روش آسنکرون، فهرست ورودی را بر اساس فیلد وضعیت فعال یا غیر فعال اطلاعات پایه فیلتر می‌کند 
        /// </summary>
        /// <param name="items">مجموعه سطرهای اطلاعاتی مورد نظر</param>
        /// <param name="activeState">وضعیت درخواستی مورد نظر بر اساس فیلد فعال و غیر فعال</param>
        /// <returns>لیست فیلتر شده فهرست اطلاعاتی ورودی</returns>
        protected List<TEntityView> FilterItemsByActiveState(List<TEntityView> items, ActiveState activeState)
        {
            switch (activeState)
            {
                case ActiveState.All:
                    return items;
                case ActiveState.Inactive:
                    return items.
                        Where(a => a.State == AppStrings.Inactive)
                        .ToList();
                case (int)ActiveState.Active:
                default:
                    return items.
                        Where(a => a.State == AppStrings.Active)
                        .ToList();
            }
        }

        /// <summary>
        /// به روش آسنکرون، سطر اطلاعات پایه را پیش از حذف از حالت غیرفعال خارج می کند
        /// </summary>
        /// <param name="itemId">شناسه دیتابیسی سطر اطلاعاتی مورد نظر برای حذف</param>
        protected async Task OnDeleteItemAsync(int itemId)
        {
            var repository = UnitOfWork.GetAsyncRepository<InactiveEntity>();
            var inactiveItems = await repository.GetByCriteriaAsync(
                item => item.EntityId == itemId);
            foreach (var item in inactiveItems)
            {
                repository.Delete(item);
            }
        }

        private async Task DeactivateAsync(TEntity entity, string entityName)
        {
            var repository = UnitOfWork.GetAsyncRepository<InactiveEntity>();
            var inactive = new InactiveEntity()
            {
                FiscalPeriodId = UserContext.FiscalPeriodId,
                BranchId = UserContext.BranchId,
                EntityId = entity.Id,
                EntityName = entityName
            };
            repository.Insert(inactive);
            OnEntityAction(OperationId.Deactivate);
            await FinalizeActionAsync(entity);
        }

        private async Task ReactivateAsync(TEntity entity, string entityName)
        {
            var repository = UnitOfWork.GetAsyncRepository<InactiveEntity>();
            var inactive = await repository.GetSingleByCriteriaAsync(
                item => item.FiscalPeriodId == UserContext.FiscalPeriodId
                    && item.BranchId == UserContext.BranchId
                    && item.EntityId == entity.Id
                    && item.EntityName == entityName);
            if (inactive != null)
            {
                repository.Delete(inactive);
                OnEntityAction(OperationId.Reactivate);
                await FinalizeActionAsync(entity);
            }
        }
    }
}
