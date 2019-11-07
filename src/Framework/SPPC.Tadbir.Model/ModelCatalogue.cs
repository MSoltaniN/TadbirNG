using System;
using System.Linq;
using System.Reflection;
using SPPC.Framework.Common;

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
    }
}
