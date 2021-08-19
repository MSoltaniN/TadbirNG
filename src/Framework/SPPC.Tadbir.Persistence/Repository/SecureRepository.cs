using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Domain;
using SPPC.Framework.Extensions;
using SPPC.Framework.Helpers;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Configuration;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Model.Auth;
using SPPC.Tadbir.Model.Metadata;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای اعمال دسترسی امنیتی در سطح سطرهای اطلاعاتی را پیاده سازی می کند
    /// </summary>
    public class SecureRepository : RepositoryBase, ISecureRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="context">امکانات مشترک مورد نیاز برای عملیات دیتابیسی را فراهم می کند</param>
        public SecureRepository(IRepositoryContext context)
            : base(context)
        {
        }

        /// <summary>
        /// به روش آسنکرون، کلیه سطرهای یک موجودیت پایه را که در دوره مالی و شعبه مشخص شده تعریف شده اند،
        /// پس از اعمال محدودیت های تعریف شده برای شعب و دسترسی به رکوردها از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <typeparam name="TEntity">نوع موجودیتی که سطرهای آن باید خوانده شود</typeparam>
        /// <param name="viewId">شناسه نمای اطلاعاتی اصلی موجودیت پایه</param>
        /// <param name="relatedProperties">اطلاعات مرتبط مورد نیاز در موجودیت</param>
        /// <returns>لیست فیلتر شده از سطرهای اطلاعاتی موجودیت مورد نظر</returns>
        public async Task<IList<TEntity>> GetAllAsync<TEntity>(int viewId,
            params Expression<Func<TEntity, object>>[] relatedProperties)
            where TEntity : class, IBaseEntity
        {
            var query = GetAllQuery(viewId, relatedProperties);
            return await query
                .ToListAsync();
        }

        /// <summary>
        /// کوئری فیلترشده مورد نیاز برای خواندن اطلاعات دوره مالی و شعبه جاری برنامه را
        /// پس از اعمال محدودیت های تعریف شده برای شعب و دسترسی به رکوردها برمی گرداند
        /// </summary>
        /// <typeparam name="TEntity">نوع موجودیتی که سطرهای آن باید خوانده شود</typeparam>
        /// <param name="viewId">شناسه نمای اطلاعاتی اصلی موجودیت پایه</param>
        /// <param name="relatedProperties">اطلاعات مرتبط مورد نیاز در موجودیت</param>
        /// <returns>کوئری فیلترشده خواندن اطلاعات دوره مالی و شعبه جاری برنامه</returns>
        public IQueryable<TEntity> GetAllQuery<TEntity>(int viewId,
            params Expression<Func<TEntity, object>>[] relatedProperties)
            where TEntity : class, IBaseEntity
        {
            var repository = UnitOfWork.GetAsyncRepository<TEntity>();
            var query = repository.GetEntityQuery(relatedProperties);
            query = ApplyBranchFilter(query);
            query = ApplyRowFilter(ref query, viewId);
            return query;
        }

        /// <summary>
        /// کوئری فیلترشده مورد نیاز برای خواندن اطلاعات عملیاتی دوره مالی و شعبه جاری برنامه را
        /// پس از اعمال محدودیت های تعریف شده برای شعب و دسترسی به رکوردها برمی گرداند
        /// </summary>
        /// <typeparam name="TEntity">نوع موجودیت عملیاتی که سطرهای آن باید خوانده شود</typeparam>
        /// <param name="viewId">شناسه نمای اطلاعاتی اصلی موجودیت عملیاتی</param>
        /// <param name="relatedProperties">اطلاعات مرتبط مورد نیاز در موجودیت</param>
        /// <returns>کوئری فیلترشده خواندن اطلاعات عملیاتی دوره مالی و شعبه جاری برنامه</returns>
        public IQueryable<TEntity> GetAllOperationQuery<TEntity>(int viewId,
            params Expression<Func<TEntity, object>>[] relatedProperties)
            where TEntity : class, IFiscalEntity
        {
            var repository = UnitOfWork.GetAsyncRepository<TEntity>();
            var query = repository.GetEntityQuery(relatedProperties);
            query = ApplyOperationBranchFilter(query);
            query = ApplyRowFilter(ref query, viewId);
            return query;
        }

        /// <summary>
        /// به روش آسنکرون، کلیه سطرهای یک موجودیت عملیاتی را که در دوره مالی و شعبه جاری تعریف شده اند،
        /// پس از اعمال محدودیت های تعریف شده برای شعب و دسترسی به رکوردها از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <typeparam name="TEntity">نوع موجودیتی که سطرهای آن باید خوانده شود</typeparam>
        /// <param name="viewId">شناسه نمای اطلاعاتی اصلی موجودیت پایه</param>
        /// <param name="relatedProperties">اطلاعات مرتبط مورد نیاز در موجودیت</param>
        /// <returns>لیست فیلتر شده از سطرهای اطلاعاتی موجودیت مورد نظر</returns>
        public async Task<IList<TEntity>> GetAllOperationAsync<TEntity>(int viewId,
            params Expression<Func<TEntity, object>>[] relatedProperties)
            where TEntity : class, IFiscalEntity
        {
            var query = GetAllOperationQuery(viewId, relatedProperties);
            return await query
                .ToListAsync();
        }

        /// <summary>
        /// به روش آسنکرون، کلیه سطرهای یک موجودیت را که در دوره مالی و شعبه مشخص شده تعریف شده اند،
        /// پس از اعمال محدودیت های تعریف شده برای شعب و دسترسی به رکوردها به صورت کد و نام خوانده و برمی گرداند
        /// </summary>
        /// <typeparam name="TEntity">نوع موجودیتی که سطرهای آن باید خوانده شود</typeparam>
        /// <param name="viewId">شناسه نمای اطلاعاتی اصلی موجودیت پایه</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns></returns>
        public async Task<IList<KeyValue>> GetAllLookupAsync<TEntity>(int viewId, GridOptions gridOptions = null)
            where TEntity : class, IBaseEntity
        {
            var repository = UnitOfWork.GetAsyncRepository<TEntity>();
            var query = repository.GetEntityQuery();
            query = ApplyBranchFilterForLookup(query);
            query = ApplyRowFilter(ref query, viewId);
            return await query
                .Select(entity => Mapper.Map<KeyValue>(entity))
                .Apply(gridOptions)
                .ToListAsync();
        }

        /// <summary>
        /// به روش آسنکرون، تعداد کل سطرهای یک موجودیت پایه را که در دوره مالی و شعبه مشخص شده تعریف شده اند،
        /// پس از اعمال محدودیت های تعریف شده برای شعب و دسترسی به رکوردها از دیتابیس خوانده و برمی گرداند
        /// </summary>
        /// <typeparam name="TEntity">نوع موجودیتی که تعداد سطرهای آن باید خوانده شود</typeparam>
        /// <typeparam name="TEntityView">نوع مدل نمایشی که برای نمایش اطلاعات موجودیت استفاده می شود</typeparam>
        /// <param name="viewId">شناسه نمای اطلاعاتی اصلی موجودیت پایه</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>تعداد سطرهای اطلاعاتی موجودیت مورد نظر</returns>
        public async Task<int> GetCountAsync<TEntity, TEntityView>(int viewId, GridOptions gridOptions = null)
            where TEntity : class, IBaseEntity
            where TEntityView : class, new()
        {
            var query = GetAllQuery<TEntity>(viewId);
            return await query
                .Select(item => Mapper.Map<TEntityView>(item))
                .Apply(gridOptions, false)
                .CountAsync();
        }

        /// <summary>
        /// به روش آسنکرون، تعداد سطرهای یک موجودیت عملیاتی را که در دوره مالی و شعبه جاری تعریف شده اند،
        /// پس از اعمال محدودیت های تعریف شده برای شعب و دسترسی به رکوردها از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <typeparam name="TEntity">نوع موجودیتی که تعداد سطرهای آن باید خوانده شود</typeparam>
        /// <typeparam name="TEntityView">نوع مدل نمایشی که برای نمایش اطلاعات موجودیت استفاده می شود</typeparam>
        /// <param name="viewId">شناسه نمای اطلاعاتی اصلی موجودیت پایه</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>تعداد سطرهای اطلاعاتی موجودیت مورد نظر</returns>
        public async Task<int> GetOperationCountAsync<TEntity, TEntityView>(int viewId, GridOptions gridOptions = null)
            where TEntity : class, IFiscalEntity
            where TEntityView : class, new()
        {
            var query = GetAllOperationQuery<TEntity>(viewId);
            return await query
                .Select(item => Mapper.Map<TEntityView>(item))
                .Apply(gridOptions, false)
                .CountAsync();
        }

        /// <summary>
        /// تنظیمات موجود برای فیلتر سطرهای اطلاعاتی را روی مجموعه ای از اطلاعات اعمال می کند
        /// </summary>
        /// <typeparam name="TEntity">نوع موجودیتی که سطرهای آن باید فیلتر شود</typeparam>
        /// <param name="records">مجوعه سطرهای اطلاعاتی اولیه</param>
        /// <param name="viewId">شناسه نمای اطلاعاتی اصلی موجودیت پایه</param>
        /// <returns>مجوعه سطرهای اطلاعاتی فیلتر شده</returns>
        public IQueryable<TEntity> ApplyRowFilter<TEntity>(
            ref IQueryable<TEntity> records, int viewId)
            where TEntity : class, IEntity
        {
            if (UserContext.Roles.Contains(AppConstants.AdminRoleId))
            {
                return records;
            }

            UnitOfWork.UseSystemContext();
            var repository = UnitOfWork.GetAsyncRepository<ViewRowPermission>();
            var filters = new List<FilterExpression>();
            foreach (int roleId in UserContext.Roles)
            {
                var permission = repository
                    .GetEntityQuery()
                    .Where(perm => perm.Role.Id == roleId
                        && perm.View.Id == viewId)
                    .Include(perm => perm.View)
                        .ThenInclude(view => view.Columns)
                    .SingleOrDefault();
                var filter = GetRowFilter(ref records, permission);
                if (filter != null)
                {
                    filters.Add(filter);
                }
            }

            UnitOfWork.UseCompanyContext();
            string compoundFilter = String.Join(FilterExpressionOperator.Or, filters.Select(f => f.ToString()));
            var filteredQuery = !String.IsNullOrEmpty(compoundFilter)
                ? records.Where(compoundFilter)
                : records;
            return filteredQuery;
        }

        private IQueryable<TEntity> ApplyBranchFilter<TEntity>(IQueryable<TEntity> records)
            where TEntity : class, IBaseEntity
        {
            var tree = GetParentTree(UserContext.BranchId);
            var queryable = records
                .Where(entity => entity.FiscalPeriodId <= UserContext.FiscalPeriodId &&
                    (entity.BranchScope == (short)BranchScope.AllBranches ||
                    (entity.BranchScope == (short)BranchScope.CurrentBranch &&
                        entity.BranchId == UserContext.BranchId) ||
                    (entity.BranchScope == (short)BranchScope.CurrentBranchAndChildren &&
                        tree.Contains(entity.BranchId))));
            return queryable;
        }

        private IQueryable<TEntity> ApplyBranchFilterForLookup<TEntity>(IQueryable<TEntity> records)
            where TEntity : class, IBaseEntity
        {
            var childTree = GetChildTree(UserContext.BranchId);
            var tree = GetParentTree(UserContext.BranchId);
            var queryable = records
                .Where(entity => entity.FiscalPeriodId == UserContext.FiscalPeriodId &&
                    (entity.BranchScope == (short)BranchScope.AllBranches ||
                    (entity.BranchScope == (short)BranchScope.CurrentBranch &&
                        entity.BranchId == UserContext.BranchId) ||
                    (entity.BranchScope == (short)BranchScope.CurrentBranchAndChildren &&
                        (tree.Contains(entity.BranchId) || childTree.Contains(entity.BranchId)))));
            return queryable;
        }

        private IQueryable<TEntity> ApplyOperationBranchFilter<TEntity>(
            IQueryable<TEntity> records)
            where TEntity : class, IFiscalEntity
        {
            var childTree = GetChildTree(UserContext.BranchId);
            var queryable = records
                .Where(entity => entity.FiscalPeriodId == UserContext.FiscalPeriodId &&
                    (entity.BranchId == UserContext.BranchId || childTree.Contains(entity.BranchId)));
            return queryable;
        }

        private FilterExpression GetRowFilter<TEntity>(
            ref IQueryable<TEntity> records, ViewRowPermission permission)
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
                records = ApplySpecificRecords(ref records, permission);
            }
            else if (permission.AccessMode == RowAccessOptions.AllExceptSpecificRecords)
            {
                records = ApplySpecificRecords(ref records, permission, true);
            }
            else if (permission.AccessMode == RowAccessOptions.MaxMoneyValue)
            {
                expression = GetMaxNumericValueFilter(permission, "Money");
            }
            else if (permission.AccessMode == RowAccessOptions.MaxQuantityValue)
            {
                expression = GetMaxNumericValueFilter(permission, "Quantity");
            }
            else if (permission.AccessMode == RowAccessOptions.AllRecordsCreatedByUser)
            {
                expression = GetCreatedByFilter(UserContext.Id);
            }

            return expression;
        }

        private FilterExpression GetSpecificReferenceFilter(ViewRowPermission permission, bool isExcept = false)
        {
            var builder = new FilterExpressionBuilder();
            var references = permission.View.Columns
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
            var properties = permission.View.Columns
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

        private FilterExpression GetCreatedByFilter(int userId)
        {
            var filter = new GridFilter()
            {
                FieldName = "CreatedById",
                FieldTypeName = "System.Int32",
                Operator = GridFilterOperator.IsEqualTo,
                Value = userId.ToString()
            };

            return new FilterExpressionBuilder()
                .New(filter)
                .Build();
        }

        private IQueryable<TEntity> ApplySpecificRecords<TEntity>(
            ref IQueryable<TEntity> queryable, ViewRowPermission permission, bool isExcept = false)
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

        private GridFilter GetReferenceFilter(Column reference, string value, bool isExcept)
        {
            return new GridFilter()
            {
                FieldName = reference.Name,
                FieldTypeName = reference.DotNetType,
                Operator = isExcept ? GridFilterOperator.NotContains : GridFilterOperator.Contains,
                Value = value
            };
        }

        private GridFilter GetNumericValueFilter(Column property, double value)
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
