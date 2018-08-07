using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Common;
using SPPC.Framework.Domain;
using SPPC.Framework.Extensions;
using SPPC.Framework.Mapper;
using SPPC.Framework.Persistence;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Configuration;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Model;
using SPPC.Tadbir.Model.Auth;
using SPPC.Tadbir.Model.Metadata;
using SPPC.Tadbir.Values;
using SPPC.Tadbir.ViewModel.Auth;

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
            UnitOfWork = unitOfWork;
            Mapper = mapper;
        }

        /// <summary>
        /// به روش آسنکرون، کلیه سطرهای یک موجودیت را که در دوره مالی و شعبه مشخص شده تعریف شده اند،
        /// پس از اعمال محدودیت های تعریف شده برای شعب و دسترسی به رکوردها از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <typeparam name="TEntity">نوع موجودیتی که سطرهای آن باید خوانده شود</typeparam>
        /// <param name="userAccess">
        /// اطلاعات دسترسی کاربر به منابع محدود شده مانند نقش ها، دوره های مالی و شعبه ها
        /// </param>
        /// <param name="fpId">شناسه عددی یکی از دوره های مالی موجود</param>
        /// <param name="branchId">شناسه عددی یکی از شعب موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <param name="relatedProperties">اطلاعات مرتبط مورد نیاز در موجودیت</param>
        /// <returns>لیست فیلتر شده از سطرهای اطلاعاتی موجودیت مورد نظر</returns>
        public async Task<IList<TEntity>> GetAllAsync<TEntity>(
            UserAccessViewModel userAccess, int fpId, int branchId, GridOptions gridOptions = null,
            params Expression<Func<TEntity, object>>[] relatedProperties)
            where TEntity : class, IBaseEntity
        {
            Verify.ArgumentNotNull(userAccess, "userAccess");
            var repository = UnitOfWork.GetAsyncRepository<TEntity>();
            var query = repository.GetEntityQuery(relatedProperties);
            query = ApplyBranchFilter(query, fpId, branchId);
            query = await ApplyRowFilterAsync(query, userAccess);
            return await query
                .Apply(gridOptions)
                .ToListAsync();
        }

        /// <summary>
        /// به روش آسنکرون، تعداد کل سطرهای یک موجودیت را که در دوره مالی و شعبه مشخص شده تعریف شده اند،
        /// پس از اعمال محدودیت های تعریف شده برای شعب و دسترسی به رکوردها از دیتابیس خوانده و برمی گرداند
        /// </summary>
        /// <typeparam name="TEntity">نوع موجودیتی که تعداد سطرهای آن باید خوانده شود</typeparam>
        /// <param name="userAccess">
        /// اطلاعات دسترسی کاربر به منابع محدود شده مانند نقش ها، دوره های مالی و شعبه ها
        /// </param>
        /// <param name="fpId">شناسه عددی یکی از دوره های مالی موجود</param>
        /// <param name="branchId">شناسه عددی یکی از شعب موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>تعداد سطرهای اطلاعاتی موجودیت مورد نظر</returns>
        public async Task<int> GetCountAsync<TEntity>(
            UserAccessViewModel userAccess, int fpId, int branchId, GridOptions gridOptions = null)
            where TEntity : class, IBaseEntity
        {
            Verify.ArgumentNotNull(userAccess, "userAccess");
            var repository = UnitOfWork.GetAsyncRepository<TEntity>();
            var query = repository.GetEntityQuery();
            query = ApplyBranchFilter(query, fpId, branchId);
            query = await ApplyRowFilterAsync(query, userAccess);
            return await query
                .Apply(gridOptions, false)
                .CountAsync();
        }

        /// <summary>
        /// پیاده سازی اینترفیس واحد کاری برای انجام عملیات دیتابیسی
        /// </summary>
        protected IUnitOfWork UnitOfWork { get; }

        /// <summary>
        /// نگاشت مورد استفاده برای تبدیل کلاس های مدل اطلاعاتی
        /// </summary>
        protected IDomainMapper Mapper { get; }

        /// <summary>
        /// شناسه دیتابیسی نمای اصلی در ساختار اطلاعاتی متادیتا
        /// </summary>
        protected abstract int ViewId { get; }

        /// <summary>
        /// محدودیت دسترسی به سطرهای اطلاعاتی را در سطح شعب سازمانی اعمال می کند
        /// </summary>
        /// <typeparam name="TEntity">نوع موجودیتی که فیلتر روی سطرهای آن باید اعمال شود</typeparam>
        /// <param name="records">مجموعه سطرهای اطلاعاتی</param>
        /// <param name="fpId">شناسه عددی یکی از دوره های مالی موجود</param>
        /// <param name="branchId">شناسه عددی یکی از شعب موجود</param>
        /// <returns>مجموعه سطرهای اطلاعاتی فیلتر شده</returns>
        private IQueryable<TEntity> ApplyBranchFilter<TEntity>(IQueryable<TEntity> records, int fpId, int branchId)
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
        /// <param name="userAccess">
        /// اطلاعات دسترسی کاربر به منابع محدود شده مانند نقش ها، دوره های مالی و شعبه ها
        /// </param>
        /// <returns>مجموعه سطرهای اطلاعاتی فیلتر شده</returns>
        private async Task<IQueryable<TEntity>> ApplyRowFilterAsync<TEntity>(
            IQueryable<TEntity> records, UserAccessViewModel userAccess)
            where TEntity : class, IEntity
        {
            var repository = UnitOfWork.GetAsyncRepository<ViewRowPermission>();
            var filters = new List<FilterExpression>();
            foreach (int roleId in userAccess.Roles)
            {
                var permission = await repository
                    .GetEntityQuery()
                    .Where(perm => perm.Role.Id == roleId
                        && perm.View.Id == ViewId)
                    .Include(perm => perm.View)
                        .ThenInclude(view => view.Properties)
                    .SingleOrDefaultAsync();
                var filter = GetRowFilter(records, permission);
                if (filter != null)
                {
                    filters.Add(filter);
                }
            }

            string compoundFilter = String.Join(FilterExpressionOperator.Or, filters.Select(f => f.ToString()));
            return records.Where(compoundFilter);
        }

        private FilterExpression GetRowFilter<TEntity>(IQueryable<TEntity> records, ViewRowPermission permission)
            where TEntity : class, IEntity
        {
            FilterExpression expression = null;
            if (permission == null)
            {
                return expression;
            }

            if (permission.AccessMode == RowAccessOptions.SpecificReference)
            {
                expression = GetSpecificReferenceFilter(permission);
            }
            else if (permission.AccessMode == RowAccessOptions.AllExceptSpecificReference)
            {
                expression = GetSpecificReferenceFilter(permission, true);
            }
            else if (permission.AccessMode == RowAccessOptions.SpecificRecords)
            {
                records = ApplySpecificRecords(records, permission);
            }
            else if (permission.AccessMode == RowAccessOptions.AllExceptSpecificRecords)
            {
                records = ApplySpecificRecords(records, permission, true);
            }
            else if (permission.AccessMode == RowAccessOptions.MaxMoneyValue)
            {
                expression = GetMaxNumericValueFilter(permission, "Money");
            }
            else if (permission.AccessMode == RowAccessOptions.MaxQuantityValue)
            {
                expression = GetMaxNumericValueFilter(permission, "Quantity");
            }

            return expression;
        }

        private FilterExpression GetSpecificReferenceFilter(ViewRowPermission permission, bool isExcept = false)
        {
            var builder = new FilterExpressionBuilder();
            var references = permission.View.Properties
                .Where(prop => prop.Type == "Reference")
                .ToList();
            if (references.Count > 0)
            {
                builder.New(GetReferenceFilter(references[0], permission.TextValue, isExcept));
            }

            for (int i = 1; i < references.Count; i++)
            {
                var reference = references[i];
                builder.And(GetReferenceFilter(reference, permission.TextValue, isExcept));
            }

            return builder.Build();
        }

        private FilterExpression GetMaxNumericValueFilter(ViewRowPermission permission, string type)
        {
            var builder = new FilterExpressionBuilder();
            var properties = permission.View.Properties
                .Where(prop => prop.Type == type)
                .ToList();
            if (properties.Count > 0)
            {
                builder.New(GetNumericValueFilter(properties[0], permission.Value));
            }

            for (int i = 1; i < properties.Count; i++)
            {
                var reference = properties[i];
                builder.And(GetNumericValueFilter(reference, permission.Value));
            }

            return builder.Build();
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

        private GridFilter GetReferenceFilter(Property reference, string value, bool isExcept)
        {
            return new GridFilter()
            {
                FieldName = reference.Name,
                FieldTypeName = reference.DotNetType,
                Operator = isExcept ? GridFilterOperator.NotContains : GridFilterOperator.Contains,
                Value = value
            };
        }

        private GridFilter GetNumericValueFilter(Property property, double value)
        {
            return new GridFilter()
            {
                FieldName = property.Name,
                FieldTypeName = property.DotNetType,
                Operator = GridFilterOperator.IsLessOrEqualTo,
                Value = value.ToString()
            };
        }
    }
}
