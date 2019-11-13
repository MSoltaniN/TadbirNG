using System;
using System.Collections.Generic;
using System.Linq;
using SPPC.Framework.Common;
using SPPC.Framework.Domain;

namespace SPPC.Tadbir.Model
{
    /// <summary>
    /// امکان مقایسه دو نوع دات نتی را بر مبنای وابستگی آنها به یکدیگر فراهم می کند
    /// </summary>
    /// <remarks>
    /// وابستگی به شکلی که در این کلاس کمکی پیاده سازی شده، به معنی داشتن یک یا مجموعه ای از اشیاء در یک نوع است.
    /// یعنی اگر نوع 1 یک عضو از نوع 2 یا مجموعه ای از نوع 2 داشته باشد، بنابراین نوع 1 از نوع 2 وابسته تر است و
    /// در نتیجه از نظر وابستگی نوع 1 بزرگتر از نوع 2 است.
    /// </remarks>
    public class EntityComparerByDependency : IComparer<Type>
    {
        /// <summary>
        /// دو نوع دات نتی را از نظر وابستگی با هم مقایسیه می کند
        /// </summary>
        /// <param name="x">نوع دات نتی اول</param>
        /// <param name="y">نوع دات نتی دوم</param>
        /// <returns>اگر نوغ اول به نوع دوم وابسته باشد، مقدار 1
        /// و اگر نوع دوم به نوع اول وابسته باشد، مقدار منفی 1
        /// و در غیر این صورت مقدار صفر را برمی گرداند</returns>
        /// <remarks>
        /// وابستگی به شکلی که در این کلاس کمکی پیاده سازی شده، به معنی داشتن یک یا مجموعه ای از اشیاء در یک نوع است.
        /// یعنی اگر نوع 1 یک عضو از نوع 2 یا مجموعه ای از نوع 2 داشته باشد، بنابراین نوع 1 از نوع 2 وابسته تر است و
        /// در نتیجه از نظر وابستگی نوع 1 بزرگتر از نوع 2 است.
        /// </remarks>
        public int Compare(Type x, Type y)
        {
            int result = 0;
            if (HasReference(x, y) || HasCollection(x, y))
            {
                result = 1;
            }
            else if (HasReference(y, x) || HasCollection(y, x))
            {
                result = -1;
            }

            return result;
        }

        private static bool HasReference(Type x, Type y)
        {
            return Reflector
                .GetPropertyNames(x)
                .Any(name => Reflector.GetPropertyType(x, name) == y);
        }

        private static bool HasCollection(Type x, Type y)
        {
            var collectionType = Reflector.GetGenericType(typeof(IList<>), y);
            return Reflector
                .GetPropertyNames(x)
                .Any(name => Reflector.GetPropertyType(x, name) == collectionType);
        }
    }
}
