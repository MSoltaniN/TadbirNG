using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using SPPC.Framework.Common;
using SPPC.Framework.Domain;
using SPPC.Framework.Persistence;
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
        public virtual async Task InsertAsync(IRepository<TEntity> repository, TEntity entity)
        {
            OnEntityAction(OperationId.Create);
            Log.Description = GetState(entity);
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
        public virtual async Task UpdateAsync(IRepository<TEntity> repository, TEntity entity, TEntityView entityView)
        {
            var clone = GetEntityCopy(entity);
            OnEntityAction(OperationId.Edit);
            UpdateExisting(entityView, entity);
            Log.Description = String.Format("(Old) => {1}{0}(New) => {2}",
                Environment.NewLine, GetState(clone), GetState(entity));
            repository.Update(entity);
            await FinalizeActionAsync(entity);
        }

        /// <summary>
        /// به روش آسنکرون، سطر اطلاعاتی قابل حذف را از دیتابیس جاری برنامه حذف و سطر لاگ عملیاتی را
        /// در دیتابیس سیستمی ذخیره می کند
        /// </summary>
        /// <param name="repository">اتصال دیتابیسی به دیتابیس شرکت جاری در برنامه</param>
        /// <param name="entity">سطر اطلاعاتی که باید حذف شود</param>
        public virtual async Task DeleteAsync(IRepository<TEntity> repository, TEntity entity)
        {
            OnEntityAction(OperationId.Delete);
            Log.Description = GetState(entity);
            DisconnectEntity(entity);
            repository.Delete(entity);
            await FinalizeActionAsync(entity);
        }

        internal virtual int? EntityType
        {
            get { return null; }
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
        protected abstract string GetState(TEntity entity);

        /// <summary>
        /// آخرین تغییرات موجودیت را از مدل نمایشی به سطر اطلاعاتی موجود کپی می کند
        /// </summary>
        /// <param name="entityView">مدل نمایشی شامل آخرین تغییرات</param>
        /// <param name="entity">سطر اطلاعاتی موجود</param>
        protected abstract void UpdateExisting(TEntityView entityView, TEntity entity);

        /// <summary>
        /// رکورد لاگ عملیاتی را در جدول مرتبط ایجاد می کند.
        /// </summary>
        /// <remarks>توجه : هر گونه خطای زمان اجرا حین عملیات، نادیده گرفته می‌شود</remarks>
        protected abstract Task TrySaveLogAsync();

        /// <summary>
        /// تغییرات انجام شده را اعمال کرده و در صورت امکان لاگ عملیاتی را ایجاد می کند
        /// </summary>
        /// <param name="entity">موجودیتی که عملیات روی آن در حال انجام است</param>
        protected async Task FinalizeActionAsync(TEntity entity)
        {
            await UnitOfWork.CommitAsync();
            Log.EntityId = entity.Id;
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
        /// از آبجکت داده شده یک رونوشت تهیه کرده و رونوشت را به صورت آبجکت جدیدی برمی گرداند
        /// </summary>
        /// <param name="entity">آبجکتی که باید از آن رونوشت یا کپی تهیه شود</param>
        /// <returns>رونوشت تهیه شده از آبجکت داده شده</returns>
        protected TEntity GetEntityCopy(TEntity entity)
        {
            var mapped = Mapper.Map<TEntityView>(entity);
            return Mapper.Map<TEntity>(mapped);
        }

        private const string ModelNamespace = "SPPC.Tadbir.Model";
    }
}
