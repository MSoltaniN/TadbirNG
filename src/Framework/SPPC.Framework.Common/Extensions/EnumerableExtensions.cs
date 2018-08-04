using System;
using System.Collections.Generic;
using System.Linq;
using SPPC.Framework.Common;
using SPPC.Framework.Presentation;

namespace SPPC.Framework.Extensions
{
    /// <summary>
    /// Provides framework-specific extension methods for generic IEnumerable instances
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Applies optional filtering, sorting and paging (in that order) to items in an enumerable instance
        /// </summary>
        /// <typeparam name="T">Type of items in enumerable instance</typeparam>
        /// <param name="enumerable">Self reference (this) of enumerable instance</param>
        /// <param name="gridOptions">Options for filtering, sorting and paging items</param>
        /// <param name="withPaging">Indicates if paging needs to be applied; default is true.</param>
        /// <returns>This object</returns>
        public static IEnumerable<T> Apply<T>(this IEnumerable<T> enumerable, GridOptions gridOptions, bool withPaging = true)
        {
            Verify.ArgumentNotNull(enumerable, "enumerable");
            return enumerable
                .AsQueryable()
                .Apply(gridOptions, withPaging);
        }
    }
}
