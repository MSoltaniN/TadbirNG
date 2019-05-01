using System;
using System.Net;
using System.Text;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using SPPC.Framework.Common;
using SPPC.Framework.Helpers;
using SPPC.Framework.Presentation;

namespace SPPC.Tadbir.Web.Api
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var builder = new FilterExpressionBuilder();
            var gridOptions = new GridOptions();
            var branch = new GridFilter() { FieldName = "BranchId", FieldTypeName = "System.Int32", Operator = GridFilterOperator.IsEqualTo, Value = "1" };
            var status = new GridFilter() { FieldName = "VoucherStatusId", FieldTypeName = "System.Int32", Operator = GridFilterOperator.IsEqualTo, Value = "2" };
            var dateFrom = new GridFilter() { FieldName = "Date", FieldTypeName = "System.DateTime", Operator = GridFilterOperator.IsGreaterOrEqualTo, Value = "2018-03-21" };
            var dateTo = new GridFilter() { FieldName = "Date", FieldTypeName = "System.DateTime", Operator = GridFilterOperator.IsLessOrEqualTo, Value = "2018-05-21" };
            gridOptions.Filter = builder
                .New(status)
                ////.And(status)
                ////.New(dateFrom)
                ////.And(dateTo)
                .Build();
            ////gridOptions.SortColumns.Add(new GridOrderBy() { FieldName = "Id", Direction = "DESC" });
            var json = JsonHelper.From(gridOptions, false);
            var urlEncoded = WebUtility.UrlEncode(json);
            var base64 = Transform.ToBase64String(Encoding.UTF8.GetBytes(urlEncoded));
           BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
