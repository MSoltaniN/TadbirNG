using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using SPPC.Framework.Common;
using SPPC.Framework.Presentation;

namespace SPPC.Framework.Extensions
{
    /// <summary>
    /// Provides framework-specific extension methods for generic IQueryable instances
    /// </summary>
    public static class QueryableExtensions
    {
        /// <summary>
        /// Applies optional filtering, sorting and paging (in that order) to items in a queryable instance
        /// </summary>
        /// <typeparam name="T">Type of items in queryable instance</typeparam>
        /// <param name="queryable">Self reference (this) of queryable instance</param>
        /// <param name="gridOptions">Options for filtering, sorting and paging items</param>
        /// <param name="withPaging">Indicates if paging needs to be applied; default is true.</param>
        /// <returns>This object</returns>
        public static IQueryable<T> Apply<T>(this IQueryable<T> queryable, GridOptions gridOptions, bool withPaging = true)
        {
            Verify.ArgumentNotNull(queryable, nameof(queryable));
            var options = gridOptions ?? new GridOptions();
            if (options.Filter != null)
            {
                queryable = queryable.Where(options.Filter.ToString());
            }

            if (options.SortColumns.Count > 0)
            {
                string ordering = String.Join(", ", options.SortColumns.Select(col => col.ToString()));
                queryable = queryable.OrderBy(ordering);
            }

            options.Paging ??= new GridPaging();
            options.Paging.PageIndex = Math.Max(1, options.Paging.PageIndex);   // Prevent zero or negative page index
            options.Paging.PageSize = Math.Max(1, options.Paging.PageSize);     // Prevent zero or negative page size
            queryable = (withPaging)
                ? queryable
                    .Skip((options.Paging.PageIndex - 1) * options.Paging.PageSize)
                    .Take(options.Paging.PageSize)
                : queryable;

            return queryable;
        }

        /// <summary>
        /// Applies filtering specified as quick filter to items in a queryable instance
        /// </summary>
        /// <typeparam name="T">Type of items in queryable instance</typeparam>
        /// <param name="queryable">Self reference (this) of queryable instance</param>
        /// <param name="gridOptions">Options for filtering, sorting and paging items</param>
        /// <returns>This object</returns>
        public static IQueryable<T> ApplyQuickFilter<T>(this IQueryable<T> queryable, GridOptions gridOptions)
        {
            Verify.ArgumentNotNull(queryable, nameof(queryable));
            var options = gridOptions ?? new GridOptions();
            if (options.QuickFilter != null)
            {
                queryable = queryable.Where(options.QuickFilter.ToString());
            }

            return queryable;
        }

        /// <summary>
        /// Applies only paging to items in a queryable instance
        /// </summary>
        /// <typeparam name="T">Type of items in queryable instance</typeparam>
        /// <param name="queryable">Self reference (this) of queryable instance</param>
        /// <param name="gridOptions">Options for filtering, sorting and paging items</param>
        /// <returns>This object</returns>
        public static IQueryable<T> ApplyPaging<T>(this IQueryable<T> queryable, GridOptions gridOptions)
        {
            Verify.ArgumentNotNull(queryable, nameof(queryable));
            Verify.ArgumentNotNull(gridOptions, nameof(gridOptions));
            gridOptions.Paging ??= new GridPaging();
            gridOptions.Paging.PageIndex = Math.Max(1, gridOptions.Paging.PageIndex);   // Prevent zero or negative page index
            gridOptions.Paging.PageSize = Math.Max(1, gridOptions.Paging.PageSize);     // Prevent zero or negative page size
            return queryable
                .Skip((gridOptions.Paging.PageIndex - 1) * gridOptions.Paging.PageSize)
                .Take(gridOptions.Paging.PageSize);
        }
    }
}
