using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Domain;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Configuration;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Model.Auth;
using SPPC.Tadbir.Model.Corporate;
using SPPC.Tadbir.Model.Metadata;

namespace SPPC.Tadbir.Persistence
{
    public class SecureCacheRepository : RepositoryBase, ISecureCacheRepository
    {
        public SecureCacheRepository(IRepositoryContext context)
            : base(context)
        {
        }

        public IEnumerable<TModel> ApplyOperationBranchFilter<TModel>(IEnumerable<TModel> items)
            where TModel : class, IFiscalEntity
        {
            var childTree = GetChildTree(UserContext.BranchId);
            return items
                .Where(entity => entity.FiscalPeriodId == UserContext.FiscalPeriodId &&
                    (entity.BranchId == UserContext.BranchId || childTree.Contains(entity.BranchId)));
        }

        /// <summary>
        /// تنظیمات موجود برای فیلتر سطرهای اطلاعاتی را روی مجموعه ای از اطلاعات اعمال می کند
        /// </summary>
        /// <typeparam name="TModel">نوع موجودیتی که سطرهای آن باید فیلتر شود</typeparam>
        /// <param name="items">مجوعه سطرهای اطلاعاتی اولیه</param>
        /// <param name="viewId">شناسه نمای اطلاعاتی اصلی موجودیت پایه</param>
        /// <returns>مجوعه سطرهای اطلاعاتی فیلتر شده</returns>
        public IEnumerable<TModel> ApplyRowFilter<TModel>(IEnumerable<TModel> items, int viewId)
            where TModel : class, IEntity
        {
            if (UserContext.Roles.Contains(AppConstants.AdminRoleId))
            {
                return items;
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
                var filter = GetRowFilter(items, permission);
                if (filter != null)
                {
                    filters.Add(filter);
                }
            }

            UnitOfWork.UseCompanyContext();
            string compoundFilter = String.Join(FilterExpressionOperator.Or, filters.Select(f => f.ToString()));
            return !String.IsNullOrEmpty(compoundFilter)
                ? items.AsQueryable().Where(compoundFilter)
                : items;
        }

        private FilterExpression GetRowFilter<TModel>(
            IEnumerable<TModel> items, ViewRowPermission permission)
            where TModel : class, IEntity
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
                items = ApplySpecificRecords(items, permission);
            }
            else if (permission.AccessMode == RowAccessOptions.AllExceptSpecificRecords)
            {
                items = ApplySpecificRecords(items, permission, true);
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

        private FilterExpression GetSpecificReferenceFilter(
            ViewRowPermission permission, bool isExcept = false)
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

        private IEnumerable<TModel> ApplySpecificRecords<TModel>(
            IEnumerable<TModel> items, ViewRowPermission permission, bool isExcept = false)
            where TModel : class, IEntity
        {
            var recordIds = permission.Items
                .Split(',')
                .Select(item => Int32.Parse(item.Trim()))
                .ToList();
            items = isExcept
                ? items.Where(entity => !recordIds.Contains(entity.Id))
                : items.Where(entity => recordIds.Contains(entity.Id));
            return items;
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
