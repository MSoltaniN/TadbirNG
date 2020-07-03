using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using SPPC.Framework.Common;
using SPPC.Framework.Domain;
using SPPC.Framework.Persistence;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.ViewModel.Auth;
using SPPC.Tadbir.ViewModel.Core;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات پایه مورد نیاز برای ایجاد لاگ های عملیاتی را پیاده سازی می کند
    /// </summary>
    /// <typeparam name="TEntity">نوع مدل دیتابیسی که برای آن لاگ عملیاتی ایجاد می شود</typeparam>
    /// <typeparam name="TEntityView">نوع مدل نمایشی که برای آن لاگ عملیاتی ایجاد می شود</typeparam>
    public abstract class LoggingRepositoryBase<TEntity, TEntityView> : RepositoryBase, ILoggingRepository<TEntity, TEntityView>
        where TEntity : class, IEntity
        where TEntityView : class, new()
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="context">امکانات مشترک مورد نیاز را برای عملیات دیتابیسی فراهم می کند</param>
        public LoggingRepositoryBase(IRepositoryContext context)
            : base(context)
        {
        }

        /// <summary>
        /// به روش آسنکرون، سطر اطلاعاتی جدید را در دیتابیس جاری برنامه و سطر لاگ عملیاتی را
        /// در دیتابیس سیستمی ذخیره می کند
        /// </summary>
        /// <param name="repository">اتصال دیتابیسی به دیتابیس شرکت جاری در برنامه</param>
        /// <param name="entity">سطر اطلاعاتی که باید ذخیره شود</param>
        /// <param name="operation">کد عملیات انجام شده که به صورت پیش فرض ایجاد موجودیت است</param>
        public virtual async Task InsertAsync(IRepository<TEntity> repository,
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
        public virtual async Task UpdateAsync(IRepository<TEntity> repository,
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
        public virtual async Task DeleteAsync(IRepository<TEntity> repository,
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
        public virtual async Task DeleteNoLogAsync(IRepository<TEntity> repository, TEntity entity)
        {
            DisconnectEntity(entity);
            repository.Delete(entity);
            await UnitOfWork.CommitAsync();
        }

        internal virtual int? EntityType
        {
            get { return null; }
        }

        internal virtual OperationSourceId OperationSource
        {
            get { return OperationSourceId.None; }
        }

        /// <summary>
        /// به روش آسنکرون، لاگ عملیاتی را در صورت نیاز برای عملیات خواندن لیست موجودیت ها ایجاد می کند
        /// </summary>
        /// <param name="gridOptions">اطلاعات مورد نیاز برای ایجاد لاگ</param>
        /// <param name="description">شرح اختیاری برای رویداد</param>
        protected async Task ReadAsync(GridOptions gridOptions, string description = null)
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
        /// مدل نمایشی لاگ عملیاتی برای عملیات جاری
        /// </summary>
        protected OperationLogViewModel Log { get; private set; }

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

        internal async Task OnSourceActionAsync(OperationId operation, string description = null)
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
                SourceId = (int)OperationSource,
                Description = description ?? String.Empty
            };
            await TrySaveLogAsync();
        }

        internal async Task OnSourceActionAsync(GridOptions gridOptions, SourceListId list = SourceListId.None)
        {
            await OnSourceActionAsync(gridOptions, OperationSource, list);
        }

        internal async Task OnSourceActionAsync(
            GridOptions gridOptions, OperationSourceId source, SourceListId list = SourceListId.None)
        {
            int? listId = (list != SourceListId.None) ? (int?)list : null;
            if (gridOptions.ListChanged)
            {
                Log = new OperationLogViewModel()
                {
                    BranchId = UserContext.BranchId,
                    FiscalPeriodId = UserContext.FiscalPeriodId,
                    CompanyId = UserContext.CompanyId,
                    UserId = UserContext.Id,
                    Date = DateTime.Now.Date,
                    Time = DateTime.Now.TimeOfDay,
                    OperationId = gridOptions.Operation,
                    SourceId = (int)source,
                    SourceListId = listId
                };
                await TrySaveLogAsync();
            }
        }

        internal virtual async Task OnEntityGroupDeleted(
            IEnumerable<int> deletedIds, OperationId operation = OperationId.GroupDelete)
        {
            OnEntityAction(operation);
            Log.Description = Context.Localize(String.Format(
                "{0} : {1}", AppStrings.DeletedItemCount, deletedIds.Count()));
            await TrySaveLogAsync();
        }

        internal async Task OnSystemLoginAsync(int? userId, string description)
        {
            Log = new OperationLogViewModel()
            {
                Date = DateTime.Now.Date,
                Time = DateTime.Now.TimeOfDay,
                SourceId = (int)OperationSourceId.AppLogin,
                OperationId = (int)OperationId.FailedLogin,
                UserId = userId,
                Description = description
            };
            await TrySaveLogAsync();
        }

        internal async Task OnEnvironmentChangeAsync(
            CompanyLoginViewModel currentLogin, CompanyLoginViewModel newLogin)
        {
            string description = String.Empty;
            Log = new OperationLogViewModel()
            {
                Date = DateTime.Now.Date,
                Time = DateTime.Now.TimeOfDay,
                SourceId = (int)OperationSourceId.AppEnvironment,
                CompanyId = currentLogin.CompanyId > 0
                    ? currentLogin.CompanyId
                    : null,
                UserId = currentLogin.UserId
            };

            if (currentLogin.CompanyId == 0
                || currentLogin.CompanyId != newLogin.CompanyId)
            {
                Log.CompanyId = newLogin.CompanyId;
                Log.OperationId = (int)OperationId.CompanyLogin;
                description = String.Format("{0} : '{1}', {2} : '{3}', {4} : '{5}'",
                    AppStrings.Company, newLogin.CompanyName,
                    AppStrings.FiscalPeriod, newLogin.FiscalPeriodName,
                    AppStrings.Branch, newLogin.BranchName);
                Log.Description = Context.Localize(description);
                await TrySaveLogAsync();
            }
            else
            {
                if (currentLogin.FiscalPeriodId != newLogin.FiscalPeriodId)
                {
                    Log.OperationId = (int)OperationId.SwitchFiscalPeriod;
                    description = String.Format("{0} : '{1}', {2} : '{3}'",
                        AppStrings.CurrentValue, currentLogin.FiscalPeriodName,
                        AppStrings.NewValue, newLogin.FiscalPeriodName);
                    Log.Description = Context.Localize(description);
                    await TrySaveLogAsync();
                }

                if (currentLogin.BranchId != newLogin.BranchId)
                {
                    Log.OperationId = (int)OperationId.SwitchBranch;
                    description = String.Format("{0} : '{1}', {2} : '{3}'",
                        AppStrings.CurrentValue, currentLogin.BranchName,
                        AppStrings.NewValue, newLogin.BranchName);
                    Log.Description = Context.Localize(description);
                    await TrySaveLogAsync();
                }
            }
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
        /// رکورد لاگ عملیاتی را در جدول مرتبط ایجاد می کند.
        /// </summary>
        /// <remarks>توجه : هر گونه خطای زمان اجرا حین عملیات، نادیده گرفته می‌شود</remarks>
        protected abstract Task TrySaveLogAsync();

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
        /// در صورت بروز خطا هنگام ایجاد رکورد لاگ، جزییات خطا را در کنسول ویژوال استودیو گزارش می دهد
        /// </summary>
        /// <param name="ex">خطای ایجاد شده</param>
        protected void ReportLoggingError(Exception ex)
        {
#if DEBUG
            Debug.WriteLine(Environment.NewLine);
            Debug.WriteLine("WARNING: Could not create operation log.");
            Debug.WriteLine("    More Info : {0}", ex);
#endif
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

        private const string ModelNamespace = "SPPC.Tadbir.Model";
    }
}
