using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Domain;
using SPPC.Framework.Mapper;
using SPPC.Framework.Persistence;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Configuration;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Model;
using SPPC.Tadbir.Model.Auth;
using SPPC.Tadbir.Model.Metadata;
using SPPC.Tadbir.Values;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای اعمال دسترسی امنیتی در سطح سطرهای اطلاعاتی را پیاده سازی می کند
    /// </summary>
    public abstract class SecureRepository : ISecureRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="unitOfWork">پیاده سازی اینترفیس واحد کاری برای انجام عملیات دیتابیسی</param>
        /// <param name="mapper">نگاشت مورد استفاده برای تبدیل کلاس های مدل اطلاعاتی</param>
        protected SecureRepository(IUnitOfWork unitOfWork, IDomainMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// محدودیت دسترسی به سطرهای اطلاعاتی را در سطح شعب سازمانی اعمال می کند
        /// </summary>
        /// <typeparam name="TEntity">نوع موجودیتی که فیلتر روی سطرهای آن باید اعمال شود</typeparam>
        /// <param name="records">مجموعه سطرهای اطلاعاتی</param>
        /// <param name="fpId">شناسه عددی یکی از دوره های مالی موجود</param>
        /// <param name="branchId">شناسه عددی یکی از شعب موجود</param>
        /// <returns>مجموعه سطرهای اطلاعاتی فیلتر شده</returns>
        public IQueryable<TEntity> ApplyBranchFilter<TEntity>(IQueryable<TEntity> records, int fpId, int branchId)
            where TEntity : class, IBaseEntity
        {
            var repository = UnitOfWork.GetAsyncRepository<TEntity>();
            var queryable = records
                .Where(entity => entity.FiscalPeriodId == fpId &&
                    (entity.BranchScope == (short)BranchScope.AllBranches ||
                    (entity.BranchScope == (short)BranchScope.CurrentBranch && entity.BranchId == branchId) ||
                    (entity.BranchScope == (short)BranchScope.CurrentBranchAndChildren &&
                        (entity.BranchId == branchId ||
                            (entity as FiscalEntity).Branch
                                .Children
                                .Select(br => br.Id)
                                .Contains(branchId)))));
            return queryable;
        }

        /// <summary>
        /// محدودیت دسترسی به سطرهای اطلاعاتی را با توجه به تنظیمات موجود اعمال می کند
        /// </summary>
        /// <typeparam name="TEntity">نوع موجودیتی که فیلتر روی سطرهای آن باید اعمال شود</typeparam>
        /// <param name="records">مجموعه سطرهای اطلاعاتی</param>
        /// <param name="roleId">شناسه دیتابیسی نقش امنیتی دارای محدودیت دسترسی</param>
        /// <returns>مجموعه سطرهای اطلاعاتی فیلتر شده</returns>
        public async Task<IQueryable<TEntity>> ApplyRowFilterAsync<TEntity>(IQueryable<TEntity> records, int roleId)
            where TEntity : class, IEntity
        {
            var permissionRepository = UnitOfWork.GetAsyncRepository<ViewRowPermission>();
            var permission = await permissionRepository
                .GetEntityQuery()
                .Where(perm => perm.Role.Id == roleId
                    && perm.View.Id == ViewId)
                .Include(perm => perm.View)
                    .ThenInclude(view => view.Properties)
                .SingleOrDefaultAsync();
            return ApplyRowFilter(records, roleId, permission);
        }

        /// <summary>
        /// پیاده سازی اینترفیس واحد کاری برای انجام عملیات دیتابیسی
        /// </summary>
        protected IUnitOfWork UnitOfWork
        {
            get { return _unitOfWork; }
        }

        /// <summary>
        /// نگاشت مورد استفاده برای تبدیل کلاس های مدل اطلاعاتی
        /// </summary>
        protected IDomainMapper Mapper
        {
            get { return _mapper; }
        }

        /// <summary>
        /// شناسه دیتابیسی نمای اصلی در ساختار اطلاعاتی متادیتا
        /// </summary>
        protected abstract int ViewId { get; }

        private IQueryable<TEntity> ApplyRowFilter<TEntity>(
            IQueryable<TEntity> records, int roleId, ViewRowPermission permission)
            where TEntity : class, IEntity
        {
            var rowPermission = permission ?? new ViewRowPermission()
            {
                Role = new Role() { Id = roleId },
                View = new Entity() { Id = ViewId }
            };
            if (rowPermission.AccessMode == RowAccessOptions.SpecificReference)
            {
                records = ApplySpecificReference(records, rowPermission);
            }
            else if (rowPermission.AccessMode == RowAccessOptions.AllExceptSpecificReference)
            {
                records = ApplySpecificReference(records, rowPermission, true);
            }
            else if (rowPermission.AccessMode == RowAccessOptions.SpecificRecords)
            {
                records = ApplySpecificRecords(records, rowPermission);
            }
            else if (rowPermission.AccessMode == RowAccessOptions.AllExceptSpecificRecords)
            {
                records = ApplySpecificRecords(records, rowPermission, true);
            }
            else if (rowPermission.AccessMode == RowAccessOptions.MaxMoneyValue)
            {
                records = ApplyMaxNumericValue(records, rowPermission, "Money");
            }
            else if (rowPermission.AccessMode == RowAccessOptions.MaxQuantityValue)
            {
                records = ApplyMaxNumericValue(records, rowPermission, "Quantity");
            }

            return records;
        }

        private IQueryable<TEntity> ApplySpecificReference<TEntity>(
            IQueryable<TEntity> queryable, ViewRowPermission permission, bool isExcept = false)
            where TEntity : class, IEntity
        {
            var references = permission.View.Properties
                .Where(prop => prop.Type == "Reference")
                .ToList();
            foreach (var reference in references)
            {
                var filter = new GridFilter()
                {
                    FieldName = reference.Name,
                    FieldTypeName = reference.DotNetType,
                    Operator = isExcept ? GridFilterOperator.NotContains : GridFilterOperator.Contains,
                    Value = permission.TextValue
                };
                queryable = queryable.Where(filter.ToString());
            }

            return queryable;
        }

        private IQueryable<TEntity> ApplySpecificRecords<TEntity>(
            IQueryable<TEntity> queryable, ViewRowPermission permission, bool isExcept = false)
            where TEntity : class, IEntity
        {
            var recordIds = permission.Items
                .Split(',')
                .Select(item => Int32.Parse(item.Trim()))
                .ToList();
            queryable = isExcept
                ? queryable.Where(entity => !recordIds.Contains(entity.Id))
                : queryable.Where(entity => recordIds.Contains(entity.Id));
            return queryable;
        }

        private IQueryable<TEntity> ApplyMaxNumericValue<TEntity>(
            IQueryable<TEntity> queryable, ViewRowPermission permission, string type)
            where TEntity : class, IEntity
        {
            var properties = permission.View.Properties
                .Where(prop => prop.Type == type)
                .ToList();
            foreach (var property in properties)
            {
                var filter = new GridFilter()
                {
                    FieldName = property.Name,
                    FieldTypeName = property.DotNetType,
                    Operator = GridFilterOperator.IsLessOrEqualTo,
                    Value = permission.Value.ToString()
                };
                queryable = queryable.Where(filter.ToString());
            }

            return queryable;
        }

        private readonly IUnitOfWork _unitOfWork;
        private readonly IDomainMapper _mapper;
    }
}
