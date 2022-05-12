using System.Net;
using System.Text;
using SPPC.Framework.Common;
using SPPC.Framework.Helpers;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Domain;

namespace SPPC.Tadbir.Web.Api
{
    /// <summary>
    /// Test class for building on-the-fly encoded GridOptions instance (used in HTTP client tools)
    /// </summary>
    public static class TestGridOptionsBuilder
    {
        /// <summary>
        /// Builds and returns a GridOptions instance in an encoded format used by TadbirNG app
        /// </summary>
        /// <returns>Encoded GridOptions instance</returns>
        public static string GetEncodedGridOptions()
        {
            var builder = new FilterExpressionBuilder();
            var gridOptions = new GridOptions();
            var dateFrom = new GridFilter()
            {
                FieldName = "Date",
                FieldTypeName = "System.DateTime",
                Operator = GridFilterOperator.IsGreaterOrEqualTo,
                Value = "2018-09-11"
            };
            var dateTo = new GridFilter()
            {
                FieldName = "Date",
                FieldTypeName = "System.DateTime",
                Operator = GridFilterOperator.IsLessOrEqualTo,
                Value = "2019-01-14"
            };
            gridOptions.QuickFilter = builder
                .New(dateFrom)
                .And(dateTo)
                .Build();
            var json = JsonHelper.From(gridOptions, false);
            var urlEncoded = WebUtility.UrlEncode(json);
            return Transform.ToBase64String(Encoding.UTF8.GetBytes(urlEncoded));
        }

        /// <summary>
        /// Builds and returns a GridOptions instance in an encoded format used by TadbirNG app
        /// </summary>
        /// <returns>Encoded GridOptions instance</returns>
        public static string GetOtherEncodedGridOptions()
        {
            var builder = new FilterExpressionBuilder();
            var gridOptions = new GridOptions();
            var numFrom = new GridFilter()
            {
                FieldName = "No",
                FieldTypeName = "System.Int32",
                Operator = GridFilterOperator.IsGreaterOrEqualTo,
                Value = "10"
            };
            var numTo = new GridFilter()
            {
                FieldName = "No",
                FieldTypeName = "System.Int32",
                Operator = GridFilterOperator.IsLessOrEqualTo,
                Value = "20"
            };
            gridOptions.QuickFilter = builder
                .New(numFrom)
                .And(numTo)
                .Build();
            var json = JsonHelper.From(gridOptions, false);
            var urlEncoded = WebUtility.UrlEncode(json);
            return Transform.ToBase64String(Encoding.UTF8.GetBytes(urlEncoded));
        }

        /// <summary>
        /// Builds and returns a GridOptions instance that has no filter, no sorting and reads first page
        /// </summary>
        /// <returns>Encoded GridOptions instance</returns>
        public static string GetBlankGridOptions()
        {
            var gridOptions = new GridOptions();
            gridOptions.Paging.PageSize = AppConstants.DefaultPageSize;
            var json = JsonHelper.From(gridOptions, false);
            var urlEncoded = WebUtility.UrlEncode(json);
            return Transform.ToBase64String(Encoding.UTF8.GetBytes(urlEncoded));
        }

    }
}
