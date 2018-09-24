﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Common;
using SPPC.Framework.Domain;
using SPPC.Framework.Extensions;
using SPPC.Framework.Helpers;
using SPPC.Framework.Mapper;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Configuration;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Model.Auth;
using SPPC.Tadbir.Model.Corporate;
using SPPC.Tadbir.Model.Metadata;
using SPPC.Tadbir.Values;
using SPPC.Tadbir.ViewModel.Auth;

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
        /// <param name="unitOfWork">پیاده سازی اینترفیس واحد کاری برای انجام عملیات دیتابیسی</param>
        /// <param name="mapper">نگاشت مورد استفاده برای تبدیل کلاس های مدل اطلاعاتی</param>
        /// <param name="metadata">امکان خواندن متادیتا برای یک موجودیت را فراهم می کند</param>
        public SecureRepository(
            IAppUnitOfWork unitOfWork, IDomainMapper mapper, IMetadataRepository metadata)
            : base(unitOfWork, mapper, metadata)
        {
        }

        /// <summary>
        /// به روش آسنکرون، کلیه سطرهای یک موجودیت پایه را که در دوره مالی و شعبه مشخص شده تعریف شده اند،
        /// پس از اعمال محدودیت های تعریف شده برای شعب و دسترسی به رکوردها از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <typeparam name="TEntity">نوع موجودیتی که سطرهای آن باید خوانده شود</typeparam>
        /// <param name="userContext">
        /// اطلاعات دسترسی کاربر به منابع محدود شده مانند نقش ها، دوره های مالی و شعبه ها
        /// </param>
        /// <param name="fpId">شناسه عددی یکی از دوره های مالی موجود</param>
        /// <param name="branchId">شناسه عددی یکی از شعب موجود</param>
        /// <param name="viewId">شناسه نمای اطلاعاتی اصلی موجودیت پایه</param>
        /// <param name="relatedProperties">اطلاعات مرتبط مورد نیاز در موجودیت</param>
        /// <returns>لیست فیلتر شده از سطرهای اطلاعاتی موجودیت مورد نظر</returns>
        public async Task<IList<TEntity>> GetAllAsync<TEntity>(
            UserContextViewModel userContext, int fpId, int branchId, int viewId,
            params Expression<Func<TEntity, object>>[] relatedProperties)
            where TEntity : class, IBaseEntity
        {
            var query = GetFilteredQuery(userContext, fpId, branchId, viewId, relatedProperties);
            return await query
                .ToListAsync();
        }

        /// <summary>
        /// به روش آسنکرون، کلیه سطرهای یک موجودیت عملیاتی را که در دوره مالی و شعبه مشخص شده تعریف شده اند،
        /// پس از اعمال محدودیت های تعریف شده برای شعب و دسترسی به رکوردها از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <typeparam name="TEntity">نوع موجودیتی که سطرهای آن باید خوانده شود</typeparam>
        /// <param name="userContext">
        /// اطلاعات دسترسی کاربر به منابع محدود شده مانند نقش ها، دوره های مالی و شعبه ها
        /// </param>
        /// <param name="fpId">شناسه عددی یکی از دوره های مالی موجود</param>
        /// <param name="branchId">شناسه عددی یکی از شعب موجود</param>
        /// <param name="viewId">شناسه نمای اطلاعاتی اصلی موجودیت پایه</param>
        /// <param name="relatedProperties">اطلاعات مرتبط مورد نیاز در موجودیت</param>
        /// <returns>لیست فیلتر شده از سطرهای اطلاعاتی موجودیت مورد نظر</returns>
        public async Task<IList<TEntity>> GetAllOperationAsync<TEntity>(
            UserContextViewModel userContext, int fpId, int branchId, int viewId,
            params Expression<Func<TEntity, object>>[] relatedProperties)
            where TEntity : class, IFiscalEntity
        {
            var query = GetFilteredOperationQuery(userContext, fpId, branchId, viewId, relatedProperties);
            return await query
                .ToListAsync();
        }

        /// <summary>
        /// به روش آسنکرون، کلیه سطرهای یک موجودیت را که در دوره مالی و شعبه مشخص شده تعریف شده اند،
        /// پس از اعمال محدودیت های تعریف شده برای شعب و دسترسی به رکوردها به صورت کد و نام خوانده و برمی گرداند
        /// </summary>
        /// <typeparam name="TEntity">نوع موجودیتی که سطرهای آن باید خوانده شود</typeparam>
        /// <param name="userContext">
        /// اطلاعات دسترسی کاربر به منابع محدود شده مانند نقش ها، دوره های مالی و شعبه ها
        /// </param>
        /// <param name="fpId">شناسه عددی یکی از دوره های مالی موجود</param>
        /// <param name="branchId">شناسه عددی یکی از شعب موجود</param>
        /// <param name="viewId">شناسه نمای اطلاعاتی اصلی موجودیت پایه</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns></returns>
        public async Task<IList<KeyValue>> GetAllLookupAsync<TEntity>(
            UserContextViewModel userContext, int fpId, int branchId, int viewId, GridOptions gridOptions = null)
            where TEntity : class, IBaseEntity
        {
            Verify.ArgumentNotNull(userContext, "userContext");
            var repository = UnitOfWork.GetAsyncRepository<TEntity>();
            var query = repository.GetEntityQuery();
            query = ApplyBranchFilterForLookup(query, fpId, branchId);
            query = ApplyRowFilter(ref query, userContext, viewId);
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
        /// <param name="userContext">
        /// اطلاعات دسترسی کاربر به منابع محدود شده مانند نقش ها، دوره های مالی و شعبه ها
        /// </param>
        /// <param name="fpId">شناسه عددی یکی از دوره های مالی موجود</param>
        /// <param name="branchId">شناسه عددی یکی از شعب موجود</param>
        /// <param name="viewId">شناسه نمای اطلاعاتی اصلی موجودیت پایه</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>تعداد سطرهای اطلاعاتی موجودیت مورد نظر</returns>
        public async Task<int> GetCountAsync<TEntity>(
            UserContextViewModel userContext, int fpId, int branchId, int viewId, GridOptions gridOptions = null)
            where TEntity : class, IBaseEntity
        {
            var query = GetFilteredQuery<TEntity>(userContext, fpId, branchId, viewId);
            return await query
                .Apply(gridOptions, false)
                .CountAsync();
        }

        /// <summary>
        /// به روش آسنکرون، تعداد سطرهای یک موجودیت عملیاتی را که در دوره مالی و شعبه مشخص شده تعریف شده اند،
        /// پس از اعمال محدودیت های تعریف شده برای شعب و دسترسی به رکوردها از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <typeparam name="TEntity">نوع موجودیتی که تعداد سطرهای آن باید خوانده شود</typeparam>
        /// <param name="userContext">
        /// اطلاعات دسترسی کاربر به منابع محدود شده مانند نقش ها، دوره های مالی و شعبه ها
        /// </param>
        /// <param name="fpId">شناسه عددی یکی از دوره های مالی موجود</param>
        /// <param name="branchId">شناسه عددی یکی از شعب موجود</param>
        /// <param name="viewId">شناسه نمای اطلاعاتی اصلی موجودیت پایه</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>تعداد سطرهای اطلاعاتی موجودیت مورد نظر</returns>
        public async Task<int> GetOperationCountAsync<TEntity>(
            UserContextViewModel userContext, int fpId, int branchId, int viewId, GridOptions gridOptions = null)
            where TEntity : class, IFiscalEntity
        {
            var query = GetFilteredOperationQuery<TEntity>(userContext, fpId, branchId, viewId);
            return await query
                .Apply(gridOptions, false)
                .CountAsync();
        }

        /// <summary>
        /// تنظیمات موجود برای فیلتر سطرهای اطلاعاتی را روی مجموعه ای از اطلاعات اعمال می کند
        /// </summary>
        /// <typeparam name="TEntity">نوع موجودیتی که سطرهای آن باید فیلتر شود</typeparam>
        /// <param name="records">مجوعه سطرهای اطلاعاتی اولیه</param>
        /// <param name="userContext">
        /// اطلاعات دسترسی کاربر به منابع محدود شده مانند نقش ها، دوره های مالی و شعبه ها
        /// </param>
        /// <param name="viewId">شناسه نمای اطلاعاتی اصلی موجودیت پایه</param>
        /// <returns>مجوعه سطرهای اطلاعاتی فیلتر شده</returns>
        public IQueryable<TEntity> ApplyRowFilter<TEntity>(
            ref IQueryable<TEntity> records, UserContextViewModel userContext, int viewId)
            where TEntity : class, IEntity
        {
            UnitOfWork.UseSystemContext();
            var repository = UnitOfWork.GetAsyncRepository<ViewRowPermission>();
            var filters = new List<FilterExpression>();
            foreach (int roleId in userContext.Roles)
            {
                var permission = repository
                    .GetEntityQuery()
                    .Where(perm => perm.Role.Id == roleId
                        && perm.View.Id == viewId)
                    .Include(perm => perm.View)
                        .ThenInclude(view => view.Columns)
                    .SingleOrDefault();
                var filter = GetRowFilter(ref records, permission, userContext.Id);
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

        private IQueryable<TEntity> GetFilteredQuery<TEntity>(
            UserContextViewModel userContext, int fpId, int branchId, int viewId,
            params Expression<Func<TEntity, object>>[] relatedProperties)
            where TEntity : class, IBaseEntity
        {
            Verify.ArgumentNotNull(userContext, "userContext");
            var repository = UnitOfWork.GetAsyncRepository<TEntity>();
            var query = repository.GetEntityQuery(relatedProperties);
            query = ApplyBranchFilter(query, fpId, branchId);
            query = ApplyRowFilter(ref query, userContext, viewId);
            return query;
        }

        private IQueryable<TEntity> GetFilteredOperationQuery<TEntity>(
            UserContextViewModel userContext, int fpId, int branchId, int viewId,
            params Expression<Func<TEntity, object>>[] relatedProperties)
            where TEntity : class, IFiscalEntity
        {
            Verify.ArgumentNotNull(userContext, "userContext");
            var repository = UnitOfWork.GetAsyncRepository<TEntity>();
            var query = repository.GetEntityQuery(relatedProperties);
            query = ApplyOperationBranchFilter(query, fpId, branchId);
            query = ApplyRowFilter(ref query, userContext, viewId);
            return query;
        }

        private IQueryable<TEntity> ApplyBranchFilter<TEntity>(IQueryable<TEntity> records, int fpId, int branchId)
            where TEntity : class, IBaseEntity
        {
            var repository = UnitOfWork.GetAsyncRepository<TEntity>();
            var tree = GetParentTree(branchId);
            var childTree = GetChildTree(branchId);
            var queryable = records
                .Where(entity => entity.FiscalPeriodId == fpId &&
                    (entity.BranchScope == (short)BranchScope.AllBranches ||
                    (entity.BranchScope == (short)BranchScope.CurrentBranch && entity.BranchId == branchId) ||
                    (entity.BranchScope == (short)BranchScope.CurrentBranchAndChildren &&
                        (tree.Contains(entity.BranchId) || childTree.Contains(entity.BranchId)))));
            return queryable;
        }

        private IQueryable<TEntity> ApplyBranchFilterForLookup<TEntity>(
            IQueryable<TEntity> records, int fpId, int branchId)
            where TEntity : class, IBaseEntity
        {
            var repository = UnitOfWork.GetAsyncRepository<TEntity>();
            var tree = GetParentTree(branchId);
            var queryable = records
                .Where(entity => entity.FiscalPeriodId == fpId &&
                    (entity.BranchScope == (short)BranchScope.AllBranches ||
                    (entity.BranchScope == (short)BranchScope.CurrentBranch && entity.BranchId == branchId) ||
                    (entity.BranchScope == (short)BranchScope.CurrentBranchAndChildren &&
                        tree.Contains(entity.BranchId))));
            return queryable;
        }

        private IQueryable<TEntity> ApplyOperationBranchFilter<TEntity>(
            IQueryable<TEntity> records, int fpId, int branchId)
            where TEntity : class, IFiscalEntity
        {
            var repository = UnitOfWork.GetAsyncRepository<TEntity>();
            var childTree = GetChildTree(branchId);
            var queryable = records
                .Where(entity => entity.FiscalPeriodId == fpId &&
                    (entity.BranchId == branchId || childTree.Contains(entity.BranchId)));
            return queryable;
        }

        private FilterExpression GetRowFilter<TEntity>(
            ref IQueryable<TEntity> records, ViewRowPermission permission, int userId)
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
                expression = GetCreatedByFilter(userId);
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

        private IEnumerable<int> GetParentTree(int branchId)
        {
            var tree = new List<int>();
            var repository = UnitOfWork.GetAsyncRepository<Branch>();
            int? parentId = branchId;
            while (parentId != null)
            {
                tree.Add(parentId.Value);
                var parent = repository.GetByID(parentId.Value);
                parentId = parent.ParentId;
            }

            return tree;
        }

        private IEnumerable<int> GetChildTree(int branchId)
        {
            var tree = new List<int>();
            var repository = UnitOfWork.GetAsyncRepository<Branch>();
            var branch = repository.GetByID(branchId, br => br.Children);
            AddChildren(branch, tree);
            return tree;
        }

        private void AddChildren(Branch branch, IList<int> children)
        {
            var repository = UnitOfWork.GetAsyncRepository<Branch>();
            foreach (var child in branch.Children)
            {
                children.Add(child.Id);
                var item = repository.GetByID(child.Id, br => br.Children);
                AddChildren(item, children);
            }
        }
    }
}
