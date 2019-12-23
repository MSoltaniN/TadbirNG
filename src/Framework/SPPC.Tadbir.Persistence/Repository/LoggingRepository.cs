﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using SPPC.Framework.Common;
using SPPC.Framework.Domain;
using SPPC.Framework.Persistence;
using SPPC.Tadbir.Model;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.ViewModel.Core;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات کمکی برای ایجاد لاگ های عملیاتی همزمان با عملیات ذخیره و بازیابی را پیاده سازی می کند
    /// </summary>
    /// <typeparam name="TEntity">نوع مدل اطلاعاتی که عملیات روی آن انجام می شود</typeparam>
    /// <typeparam name="TEntityView">نوع مدل نمایشی که برای اصلاح اطلاعات استفاده می شود</typeparam>
    public abstract class LoggingRepository<TEntity, TEntityView> : RepositoryBase, ILoggingRepository<TEntity, TEntityView>
        where TEntity : class, IEntity
        where TEntityView : class, new()
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="context">امکانات مشترک مورد نیاز را برای عملیات دیتابیسی فراهم می کند</param>
        /// <param name="logRepository">امکان ایجاد لاگ های عملیاتی را در دیتابیس سیستمی برنامه فراهم می کند</param>
        public LoggingRepository(IRepositoryContext context, IOperationLogRepository logRepository)
            : base(context)
        {
            _logRepository = logRepository;
        }

        /// <summary>
        /// به روش آسنکرون، سطر اطلاعاتی جدید را در دیتابیس جاری برنامه و سطر لاگ عملیاتی را
        /// در دیتابیس سیستمی ذخیره می کند
        /// </summary>
        /// <param name="repository">اتصال دیتابیسی به دیتابیس شرکت جاری در برنامه</param>
        /// <param name="entity">سطر اطلاعاتی که باید ذخیره شود</param>
        public async Task<bool> InsertAsync(IRepository<TEntity> repository, TEntity entity)
        {
            OnAction("Create", null, entity);
            repository.Insert(entity);
            return await FinalizeActionAsync();
        }

        /// <summary>
        /// به روش آسنکرون، سطر اطلاعاتی اصلاح شده را در دیتابیس جاری برنامه و سطر لاگ عملیاتی را
        /// در دیتابیس سیستمی ذخیره می کند
        /// </summary>
        /// <param name="repository">اتصال دیتابیسی به دیتابیس شرکت جاری در برنامه</param>
        /// <param name="entity">سطر اطلاعاتی که تغییرات آن باید ذخیره شود</param>
        /// <param name="entityView">مدل نمایشی شامل آخرین تغییرات سطر اطلاعاتی</param>
        public async Task<bool> UpdateAsync(IRepository<TEntity> repository, TEntity entity, TEntityView entityView)
        {
            var clone = Mapper.Map<TEntity>(entity);
            OnAction("Edit", clone, null);
            UpdateExisting(entityView, entity);
            repository.Update(entity);
            return await FinalizeActionAsync();
        }

        /// <summary>
        /// به روش آسنکرون، سطر اطلاعاتی قابل حذف را از دیتابیس جاری برنامه حذف و سطر لاگ عملیاتی را
        /// در دیتابیس سیستمی ذخیره می کند
        /// </summary>
        /// <param name="repository">اتصال دیتابیسی به دیتابیس شرکت جاری در برنامه</param>
        /// <param name="entity">سطر اطلاعاتی که باید حذف شود</param>
        public async Task<bool> DeleteAsync(IRepository<TEntity> repository, TEntity entity)
        {
            var clone = Mapper.Map<TEntity>(entity);
            OnAction("Delete", clone, null);
            DisconnectEntity(entity);
            repository.Delete(entity);
            return await FinalizeActionAsync();
        }

        /// <summary>
        /// مدل نمایشی لاگ عملیاتی برای عملیات جاری
        /// </summary>
        protected OperationLogViewModel Log { get; private set; }

        /// <summary>
        /// تغییرات انجام شده را اعمال کرده و در صورت امکان لاگ عملیاتی را ایجاد می کند
        /// </summary>
        protected async Task<bool> FinalizeActionAsync()
        {
            bool succeeded = false;
            try
            {
                await UnitOfWork.CommitAsync();
                succeeded = true;
                await TrySaveLogAsync();
            }
            catch (Exception ex)
            {
                await TrySaveLogAsync();
                throw;
            }

            return succeeded;
        }

        /// <summary>
        /// رکورد لاگ عملیاتی را با توجه به وضعیت قدیم و جدید سطر اطلاعاتی آماده می کند
        /// </summary>
        /// <param name="action">نوع عملیات مورد استفاده در لاگ عملیاتی</param>
        /// <param name="before">وضعیت قدیم سطر اطلاعاتی</param>
        /// <param name="after">وضعیت جدید سطر اطلاعاتی</param>
        protected void OnAction(string action, TEntity before, TEntity after)
        {
            Log = GetOperationLog(action);
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

        private static void DisconnectEntity(TEntity entity)
        {
            var relations = Reflector
                .GetPropertyNames(entity)
                .Where(prop => Reflector.GetPropertyType(entity, prop)
                    .Namespace.StartsWith(ModelNamespace))
                .ToArray();
            Array.ForEach(relations, prop => Reflector.SetProperty(entity, prop, null));
        }

        private void DeleteWithCascade(Type parentType, int parentId, Type type, IEnumerable<int> ids)
        {
            if (ids.Count() == 0)
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

        private OperationLogViewModel GetOperationLog(string action)
        {
            var log = new OperationLogViewModel()
            {
                CompanyId = UserContext.CompanyId,
                Date = DateTime.Now.Date,
                Time = DateTime.Now.TimeOfDay,
                UserId = UserContext.Id
            };
            return log;
        }

        private async Task TrySaveLogAsync()
        {
            try
            {
                await _logRepository.SaveLogAsync(Log);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(Environment.NewLine);
                Debug.WriteLine("WARNING: Could not create operation log.");
                Debug.WriteLine("    More Info : {0}", ex);

                // Ignored (logging should not throw exception)
            }
        }

        private const string ModelNamespace = "SPPC.Tadbir.Model";
        private readonly IOperationLogRepository _logRepository;
    }
}
