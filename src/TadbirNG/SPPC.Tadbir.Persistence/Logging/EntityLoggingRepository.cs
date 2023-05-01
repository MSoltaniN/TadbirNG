using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer.Management.Assessment.Expressions;
using SPPC.Framework.Common;
using SPPC.Framework.Domain;
using SPPC.Framework.Persistence;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Model;
using SPPC.Tadbir.Model.Auth;
using SPPC.Tadbir.Model.CashFlow;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.ViewModel;
using SPPC.Tadbir.ViewModel.Core;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات کمکی برای ایجاد لاگ های عملیاتی موجودیت ها همزمان با عملیات ذخیره و بازیابی را پیاده سازی می کند
    /// </summary>
    /// <typeparam name="TEntity">نوع مدل اطلاعاتی موجودیت</typeparam>
    /// <typeparam name="TEntityView">نوع مدل نمایشی موجودیت</typeparam>
    public abstract class EntityLoggingRepository<TEntity, TEntityView> : LoggingRepositoryBase
            where TEntity : class, IEntity
            where TEntityView : class, new()
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="context">امکانات مشترک مورد نیاز را برای عملیات دیتابیسی فراهم می کند</param>
        /// <param name="logRepository">امکان ایجاد لاگ های عملیاتی و سیستمی را فراهم می کند</param>
        protected EntityLoggingRepository(IRepositoryContext context, IOperationLogRepository logRepository)
            : base(context, logRepository)
        {
        }

        /// <summary>
        /// تمام ارتباطات موجود در نمونه داده شده را پاک کرده و در عمل، این ارتباطات را قطع می کند
        /// </summary>
        /// <param name="entity">نمونه ای که ارتباطات آن باید قطع شوند</param>
        protected static void DisconnectEntity(object entity)
        {
            var relations = Reflector
                .GetPropertyNames(entity)
                .Where(prop => Reflector.GetPropertyType(entity, prop)
                    .Namespace.StartsWith(ModelNamespace))
                .ToArray();
            Array.ForEach(relations, prop => Reflector.SetProperty(entity, prop, null));
        }

        /// <summary>
        /// به روش آسنکرون، لاگ عملیاتی را در صورت نیاز برای عملیات خواندن لیست موجودیت ها ایجاد می کند
        /// </summary>
        /// <param name="gridOptions">اطلاعات مورد نیاز برای ایجاد لاگ</param>
        /// <param name="description">شرح اختیاری برای رویداد</param>
        protected override async Task ReadAsync(GridOptions gridOptions, string description = null)
        {
            var options = gridOptions ?? new GridOptions();
            OnEntityAction((OperationId)options.Operation);
            Log.Description = description;
            if (options.ListChanged)
            {
                await TrySaveLogAsync();
            }
        }

        /// <summary>
        /// به روش آسنکرون، سطر اطلاعاتی جدید را در دیتابیس جاری برنامه و سطر لاگ عملیاتی را
        /// در دیتابیس سیستمی ذخیره می کند
        /// </summary>
        /// <param name="repository">اتصال دیتابیسی به دیتابیس شرکت جاری در برنامه</param>
        /// <param name="entity">سطر اطلاعاتی که باید ذخیره شود</param>
        /// <param name="operation">کد عملیات انجام شده که به صورت پیش فرض ایجاد موجودیت است</param>
        protected virtual async Task InsertAsync(IRepository<TEntity> repository,
            TEntity entity, OperationId operation = OperationId.Create)
        {
            OnEntityAction(operation);
            Log.Description = Context.Localize(GetState(entity));
            repository.Insert(entity);
            await FinalizeActionAsync(entity);
        }

        /// <summary>
        /// به روش آسنکرون، سطر اطلاعاتی اصلاح شده را در دیتابیس جاری برنامه و سطر لاگ عملیاتی را
        /// در دیتابیس سیستمی ذخیره می کند
        /// </summary>
        /// <param name="repository">اتصال دیتابیسی به دیتابیس شرکت جاری در برنامه</param>
        /// <param name="entity">سطر اطلاعاتی که تغییرات آن باید ذخیره شود</param>
        /// <param name="entityView">مدل نمایشی شامل آخرین تغییرات سطر اطلاعاتی</param>
        /// <param name="operation">کد عملیات انجام شده که به صورت پیش فرض اصلاح موجودیت است</param>
        protected virtual async Task UpdateAsync(IRepository<TEntity> repository,
            TEntity entity, TEntityView entityView, OperationId operation = OperationId.Edit)
        {
            string oldState = GetState(entity);
            OnEntityAction(operation);
            UpdateExisting(entityView, entity);
            Log.Description = Context.Localize(
                String.Format("{0} : ({1}) , {2} : ({3})",
                AppStrings.Old, Context.Localize(oldState),
                AppStrings.New, Context.Localize(GetState(entity))));
            repository.Update(entity);
            await FinalizeActionAsync(entity);
        }

        /// <summary>
        /// به روش آسنکرون، سطر اطلاعاتی قابل حذف را از دیتابیس جاری برنامه حذف و سطر لاگ عملیاتی را
        /// در دیتابیس سیستمی ذخیره می کند
        /// </summary>
        /// <param name="repository">اتصال دیتابیسی به دیتابیس شرکت جاری در برنامه</param>
        /// <param name="entity">سطر اطلاعاتی که باید حذف شود</param>
        /// <param name="operation">کد عملیات انجام شده که به صورت پیش فرض حذف موجودیت است</param>
        protected virtual async Task DeleteAsync(IRepository<TEntity> repository,
            TEntity entity, OperationId operation = OperationId.Delete)
        {
            OnEntityAction(operation);
            Log.Description = Context.Localize(GetState(entity));
            DisconnectEntity(entity);
            repository.Delete(entity);
            await FinalizeActionAsync(entity);
        }

        /// <summary>
        /// به روش آسنکرون، سطر اطلاعاتی قابل حذف را از دیتابیس جاری برنامه حذف می کند
        /// </summary>
        /// <param name="repository">اتصال دیتابیسی به دیتابیس شرکت جاری در برنامه</param>
        /// <param name="entity">سطر اطلاعاتی که باید حذف شود</param>
        protected virtual async Task DeleteNoLogAsync(IRepository<TEntity> repository, TEntity entity)
        {
            DisconnectEntity(entity);
            repository.Delete(entity);
            await UnitOfWork.CommitAsync();
        }

        /// <summary>
        /// به روش آسنکرون، سطر اطلاعاتی لاگ عملیاتی را برای تخصیص آیتم ها به منابع 
        /// درج می کند
        /// </summary>
        /// <param name="newItemIds">آیتم های جدید انتخابی</param>
        /// <param name="removeItemIds">آیتم های از قدیمی حذف شده</param>
        /// <param name="resourceId">شناسه منبع تخصیص یافته</param>
        /// <param name="operationId">کد عملیاتی تخصیص منبع</param>
        /// <param name="relatedProperties">اطلاعات مرتبط مورد نیاز در موجودیت</param>
        protected async Task InsertAssignedItemsLogAsync(int[] newItemIds, int[] removeItemIds,
            int resourceId, OperationId operationId, params Expression<Func<TEntity, object>>[] relatedProperties)
        {
            var repository = UnitOfWork.GetAsyncRepository<TEntity>();
            object resource = null;
            if (relatedProperties.Length == 0)
            {
                resource = await repository.GetByIDAsync(resourceId);
            }
            else
            {
                resource = await repository
                    .GetEntityQuery(relatedProperties)
                    .Where(e => e.Id == resourceId)
                    .Select(e => Mapper.Map<RelatedItemViewModel>(e))
                    .SingleOrDefaultAsync();
            }
            if (resource != null)
            {
                string resourceName = Reflector.GetProperty(resource, "Name").ToString();
                if (typeof(TEntity) == typeof(Role))
                {
                    resourceName = Context.Localize(resourceName);
                }
                OnEntityAction(operationId);
                Log.Description = await GetAssignedItemsDescriptionAsync(newItemIds, removeItemIds,
                    resourceName, operationId);
                await TrySaveLogAsync();
            }
        }

        /// <summary>
        /// به روش آسنکرون، لیست رشته ای از عناوین آیتم های ورودی را بر اساس کد عملیاتی برمی گرداند 
        /// </summary>
        /// <param name="itemIds">لیستی از شناسه آیتم های مورد نظر</param>
        /// <param name="operationId">کد عملیاتی مورد نظر</param>
        /// <returns>لیست رشته ای از عناوین آیتم ها</returns>
        protected virtual async Task<string[]> GetItemNamesAsync(int[] itemIds, OperationId operationId)
        {
            return await Task.Run(() => Array.Empty<string>());
        }

        private async Task<string> GetAssignedItemsDescriptionAsync(int[] newItemIds,
            int[] removeItemIds, string resourceName, OperationId operationId)
        {
            StringBuilder description = new StringBuilder();
            if (newItemIds.Length > 0)
            {
                description.Append(await LocalizeAssignedItemsDescriptionAsync(newItemIds,
                    AppStrings.AssignEntityToResource, resourceName, operationId));
            }

            if (removeItemIds.Length > 0)
            {
                if (description.Length > 0)
                {
                    description.Append(" - ");
                }

                description.Append(await LocalizeAssignedItemsDescriptionAsync(removeItemIds,
                    AppStrings.UnassignEntityToResource, resourceName, operationId));
            }

            return description.ToString();
        }

        private async Task<string> LocalizeAssignedItemsDescriptionAsync(int[] itemIds,
            string template, string resourceName, OperationId operationId)
        {
            string itemNamesStr = String.Empty;
            string localizeTemp = Context.Localize(template);
            var itemNames = await GetItemNamesAsync(itemIds, operationId);
            if (operationId == OperationId.RoleAccess || operationId == OperationId.AssignRole)
            {
                itemNamesStr = String.Join(", ", itemNames.Select(i => $"'{Context.Localize(i)}'"));
            }
            else
            {
                itemNamesStr = String.Join(", ", itemNames.Select(i => $"'{i}'"));
            }
            var itemsTitle = GetItemsTitle(itemNames.Length, operationId);

            List<OperationId> accessedOprations = new() { OperationId.AssignRole, OperationId.BranchAccess,
                OperationId.CompanyAccess, OperationId.FiscalPeriodAccess };
            if (accessedOprations.Contains(operationId))
            {
                var assignedTitle = Context.Localize(typeof(TEntity).Name).ToLower();
                return String.Format(localizeTemp, itemsTitle, itemNamesStr, assignedTitle, resourceName);
            }
            else
            {
                var accessedTitle = Context.Localize(typeof(TEntity).Name).ToLower();
                return String.Format(localizeTemp, accessedTitle, resourceName, itemsTitle, itemNamesStr);
            }
        }

        private string GetItemsTitle(int itemsLength, OperationId operationId)
        {
            string itemsTitle = String.Empty;
            switch (operationId)
            {
                case OperationId.AssignCashRegisterUser:
                case OperationId.AssignUser:
                    itemsTitle = Context.Localize(itemsLength > 1
                        ? AppStrings.Users
                        : AppStrings.User).ToLower();
                    break;
                case OperationId.RoleAccess:
                case OperationId.AssignRole:
                    itemsTitle = Context.Localize(itemsLength > 1
                        ? AppStrings.Roles
                        : AppStrings.Role).ToLower();
                    break;
                case OperationId.BranchAccess:
                    itemsTitle = Context.Localize(itemsLength > 1
                        ? AppStrings.Branches
                        : AppStrings.Branch).ToLower();
                    break;
                case OperationId.CompanyAccess:
                    itemsTitle = Context.Localize(itemsLength > 1
                        ? AppStrings.Companies
                        : AppStrings.Company).ToLower();
                    break;
                case OperationId.FiscalPeriodAccess:
                    itemsTitle = Context.Localize(itemsLength > 1
                        ? AppStrings.FiscalPeriods
                        : AppStrings.FiscalPeriod).ToLower();
                    break;
            }

            return itemsTitle;
        }

        internal virtual int? EntityType
        {
            get { return null; }
        }

        internal virtual void OnEntityAction(OperationId operation)
        {
            Log = new OperationLogViewModel()
            {
                BranchId = UserContext.BranchId,
                FiscalPeriodId = UserContext.FiscalPeriodId,
                CompanyId = UserContext.CompanyId,
                UserId = UserContext.Id,
                Date = DateTime.Now.Date,
                Time = DateTime.Now.TimeOfDay,
                OperationId = (int)operation,
                EntityTypeId = EntityType
            };
        }

        internal virtual async Task OnEntityGroupInserted(
            IEnumerable<int> insertedIds, OperationId operation)
        {
            OnEntityAction(operation);
            Log.Description = Context.Localize(String.Format(
                "{0} : {1}", AppStrings.InsertedItemCount, insertedIds.Count()));
            await TrySaveLogAsync();
        }

        internal virtual async Task OnEntityGroupDeleted(
            IEnumerable<int> deletedIds, OperationId operation = OperationId.GroupDelete)
        {
            OnEntityAction(operation);
            Log.Description = Context.Localize(String.Format(
                "{0} : {1}", AppStrings.DeletedItemCount, deletedIds.Count()));
            await TrySaveLogAsync();
        }

        /// <summary>
        /// اطلاعات خلاصه سطر اطلاعاتی داده شده را به صورت یک رشته متنی برمی گرداند
        /// </summary>
        /// <param name="entity">یکی از سطرهای اطلاعاتی موجود</param>
        /// <returns>اطلاعات خلاصه سطر اطلاعاتی داده شده به صورت رشته متنی</returns>
        protected virtual string GetState(TEntity entity)
        {
            return String.Empty;
        }

        /// <summary>
        /// آخرین تغییرات موجودیت را از مدل نمایشی به سطر اطلاعاتی موجود کپی می کند
        /// </summary>
        /// <param name="entityView">مدل نمایشی شامل آخرین تغییرات</param>
        /// <param name="entity">سطر اطلاعاتی موجود</param>
        protected virtual void UpdateExisting(TEntityView entityView, TEntity entity)
        {
        }

        /// <summary>
        /// تغییرات انجام شده را اعمال کرده و در صورت نیاز، لاگ عملیاتی را ایجاد می کند
        /// </summary>
        /// <param name="entity">موجودیتی که عملیات روی آن در حال انجام است</param>
        protected virtual async Task FinalizeActionAsync(TEntity entity)
        {
            await UnitOfWork.CommitAsync();
            Log.EntityId = entity.Id;
            CopyEntityDataToLog(entity);
            await TrySaveLogAsync();
        }

        /// <summary>
        /// به روش آسنکرون، رکورد مشخص شده با شناسه دیتابیسی داده شده را
        /// به همراه کلیه اطلاعات وابسته به آن از دیتابیس حذف می کند
        /// </summary>
        /// <param name="id">شناسه دیتابیسی رکورد مورد نظر برای حذف</param>
        protected async Task DeleteWithCascadeAsync(int id)
        {
            DbConsole.ConnectionString = UnitOfWork.CompanyConnection;
            var dependentTypes = ModelCatalogue.GetAllDependentsOfType(typeof(TEntity));
            foreach (var dependentType in dependentTypes)
            {
                var items = GetReferencedItems(id, typeof(TEntity), dependentType);
                DeleteWithCascade(typeof(TEntity), id, dependentType, items);
            }

            var repository = UnitOfWork.GetAsyncRepository<TEntity>();
            var entity = await repository.GetByIDAsync(id);
            await DeleteAsync(repository, entity);
        }

        /// <summary>
        /// یک رکورد لاگ عملیاتی برای عملیات تغییر وضعیت موجودیت عملیاتی ایجاد می کند
        /// </summary>
        /// <param name="newStatus">وضعیت ثبتی جدید برای موجودیت عملیاتی</param>
        /// <param name="oldStatus"></param>
        protected void OnDocumentStatus(DocumentStatusId newStatus, DocumentStatusId oldStatus)
        {
            OperationId operation = OperationId.None;
            switch (newStatus)
            {
                case DocumentStatusId.NotChecked:
                    operation = OperationId.UndoCheck;
                    break;
                case DocumentStatusId.Checked:
                    if (oldStatus == DocumentStatusId.Finalized)
                    {
                        operation = OperationId.UndoFinalize;
                    }
                    else
                    {
                        operation = OperationId.Check;
                    }

                    break;
                case DocumentStatusId.Finalized:
                    operation = OperationId.Finalize;
                    break;
                default:
                    break;
            }

            OnEntityAction(operation);
        }

        /// <summary>
        /// یک رکورد لاگ عملیاتی برای عملیات تایید یا برگشت از تایید موجودیت عملیاتی ایجاد می کند
        /// </summary>
        /// <param name="isConfirmed">مشخص می کند که وضعیت تایید جدید، تایید شده است یا نه؟</param>
        protected void OnDocumentConfirmation(bool isConfirmed)
        {
            OperationId operation = isConfirmed ? OperationId.Confirm : OperationId.UndoConfirm;
            OnEntityAction(operation);
        }

        /// <summary>
        /// یک رکورد لاگ عملیاتی برای عملیات تصویب یا برگشت از تصویب موجودیت عملیاتی ایجاد می کند
        /// </summary>
        /// <param name="isApproved">مشخص می کند که وضعیت تصویب جدید، تصویب شده است یا نه؟</param>
        protected void OnDocumentApproval(bool isApproved)
        {
            OperationId operation = isApproved ? OperationId.Approve : OperationId.UndoApprove;
            OnEntityAction(operation);
        }

        /// <summary>
        /// یک رکورد لاگ عملیاتی برای عملیات تغییر وضعیت های گروهی موجودیت های عملیاتی ایجاد می کند
        /// </summary>
        /// <param name="itemIds">شناسه اسناد</param>
        /// <param name="operation">شناسه عملیات گروهی</param>
        protected async Task OnEntityGroupChangeAsync(IEnumerable<int> itemIds, OperationId operation)
        {
            OnEntityAction(operation);
            Log.Description = Context.Localize(String.Format(
                "{0} : {1}", AppStrings.VoucherCount, itemIds.Count()));
            await TrySaveLogAsync();
        }

        /// <summary>
        /// اطلاعات مشترک موجودیت های پایه و عملیاتی را در رکورد لاگ کپی می کند
        /// </summary>
        /// <param name="entity">موجودیتی که عملیات جاری روی آن انجام شده است</param>
        protected void CopyEntityDataToLog(object entity)
        {
            var dataFields = new string[]
                {
                    AppStrings.FullCode, AppStrings.Name, AppStrings.Description, AppStrings.No,
                    AppStrings.Date, AppStrings.Reference, AppStrings.Association
                };
            foreach (string dataField in dataFields)
            {
                string propertyName = String.Format("Entity{0}", dataField);
                object value = Reflector.GetSimpleProperty(entity, dataField, false);
                if (dataField == AppStrings.No)
                {
                    int? no = (value != null) ? Int32.Parse(value.ToString()) : (int?)null;
                    Reflector.SetProperty(Log, propertyName, no);
                }
                else if (dataField == AppStrings.Date)
                {
                    DateTime? date = (value != null) ? DateTime.Parse(value.ToString()) : (DateTime?)null;
                    Reflector.SetProperty(Log, propertyName, date);
                }
                else if (dataField == AppStrings.FullCode)
                {
                    Reflector.SetProperty(Log, AppStrings.EntityCode, value?.ToString());
                }
                else
                {
                    Reflector.SetProperty(Log, propertyName, Context.Localize(value?.ToString()));
                }
            }
        }

        /// <summary>
        /// کد عملیات گروهی اسناد را با توجه به وضعیت قدیم و جدید آنها به دست می آورد
        /// </summary>
        /// <param name="newStatus">وضعیت  جدید سند حسابداری</param>
        /// <param name="oldStatus">وضعیت قبلی سند حسابداری</param>
        protected OperationId GetGroupOperationCode(DocumentStatusId newStatus, DocumentStatusId oldStatus)
        {
            OperationId operation = OperationId.None;
            switch (newStatus)
            {
                case DocumentStatusId.NotChecked:
                    operation = OperationId.GroupUndoCheck;
                    break;
                case DocumentStatusId.Checked:
                    if (oldStatus == DocumentStatusId.Finalized)
                    {
                        operation = OperationId.GroupUndoFinalize;
                    }
                    else
                    {
                        operation = OperationId.GroupCheck;
                    }

                    break;
                case DocumentStatusId.Finalized:
                    operation = OperationId.GroupFinalize;
                    break;
                default:
                    break;
            }

            return operation;
        }

        private void DeleteWithCascade(Type parentType, int parentId, Type type, IEnumerable<int> ids)
        {
            if (!ids.Any())
            {
                return;
            }

            var dependentTypes = ModelCatalogue.GetAllDependentsOfType(type);
            foreach (var dependentType in dependentTypes)
            {
                foreach (int id in ids)
                {
                    var items = GetReferencedItems(id, type, dependentType);
                    DeleteWithCascade(type, id, dependentType, items);
                }
            }

            string keyName = parentType == typeof(DetailAccount)
                ? "Detail"
                : parentType.Name;
            var idItems = ModelCatalogue.GetModelTypeItems(type);
            string command = String.Format("DELETE FROM [{0}].[{1}] WHERE {2}ID = {3}",
                idItems[0], idItems[1], keyName, parentId);
            DbConsole.ExecuteNonQuery(command);
        }

        private const string ModelNamespace = "SPPC.Tadbir.Model";
    }
}
