using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SPPC.Framework.Common;
using SPPC.Tadbir.Model.Finance;

namespace SPPC.Tadbir.Model
{
    /// <summary>
    /// اطلاعات موردی را درباره انواع موجودیت های تعریف شده در سیستم به دست می آورد
    /// </summary>
    public sealed class ModelCatalogue
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public ModelCatalogue()
        {
        }

        /// <summary>
        /// مجموعه ای از انواع دات نتی مشتق شده از نوع داده شده را خوانده و برمی گرداند
        /// </summary>
        /// <typeparam name="TEntity">نوع پایه مورد نظر</typeparam>
        /// <returns>مجموعه انواع دات نتی مشتق شده</returns>
        public static Type[] GetAllOfType<TEntity>()
        {
            return Assembly.GetExecutingAssembly()
                .GetExportedTypes()
                .Where(type => Reflector.DerivesFrom(type, typeof(TEntity)))
                .ToArray();
        }

        /// <summary>
        /// انواع دات نتی مدل های اطلاعاتی وابسته به شعبه را برمی گرداند
        /// </summary>
        /// <returns>مجموعه انواع دات نتی وابسته به شعبه</returns>
        public static Type[] GetBranchDependentTypes()
        {
            var fiscalTypes = GetAllOfType<FiscalEntity>();

            // TODO: The types AccountCurrency and CurrencyRate also reference Branch but are
            // currently not returned by catalogue, because they don't have FiscalPeriodID and
            // so CANNOT be derived from FiscalEntity.
            // The following block can be removed when the two types are upgraded to FiscalEntity.
            var dependentTypes = new List<Type>(fiscalTypes)
            {
                typeof(AccountCurrency),
                typeof(CurrencyRate)
            };

            return dependentTypes.ToArray();
        }

        /// <summary>
        /// نام های مشخصه یکی از مدل های اطلاعاتی سیستم را برمی گرداند
        /// </summary>
        /// <param name="entityType">نوع دات نتی مدل اطلاعاتی مورد نظر</param>
        /// <returns>نام های مشخصه به دست آمده به صورت آرایه دوتایی از رشته ها
        /// که مقدار اول نام شِما و مقدار دوم نام جدول دیتابیسی مدل اطلاعاتی است</returns>
        public static string[] GetModelTypeItems(Type entityType)
        {
            Verify.ArgumentNotNull(entityType, nameof(entityType));
            var idItems = entityType.FullName.Split('.');

            // Subsystem-specific model types are expected to have full type name like below :
            // SPPC.Tadbir.Model.[Schema].[Table]
            if (idItems.Count() != 5)
            {
                return null;
            }

            return idItems.Skip(3).ToArray();
        }
    }
}
