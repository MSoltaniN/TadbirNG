﻿using System;
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
        /// <returns>This object</returns>
        public static IQueryable<T> Apply<T>(this IQueryable<T> queryable, GridOptions gridOptions)
        {
            Verify.ArgumentNotNull(queryable, "queryable");
            var options = gridOptions ?? new GridOptions();
            foreach (var filter in options.Filters)
            {
                queryable = queryable.Where(filter.ToString());
            }

            if (options.SortColumns.Count > 0)
            {
                string ordering = String.Join(", ", options.SortColumns.Select(col => col.ToString()));
                queryable = queryable.OrderBy(ordering);
            }

            options.Paging = options.Paging ?? new GridPaging();
            options.Paging.PageIndex = Math.Max(1, options.Paging.PageIndex);   // Prevent zero or negative page index
            options.Paging.PageSize = Math.Max(1, options.Paging.PageSize);     // Prevent zero or negative page size
            queryable = (GridPaging.IsPagingEnabled(options.Paging))
                ? queryable
                    .Skip((options.Paging.PageIndex - 1) * options.Paging.PageSize)
                    .Take(options.Paging.PageSize)
                : queryable;

            return queryable;
        }
    }
}
