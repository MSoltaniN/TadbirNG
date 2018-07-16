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
            var gridOptions = new GridOptions() { Paging = new GridPaging() { PageIndex = 1, PageSize = 5 } };
            var codeLike = new GridFilter() { FieldName = "Code", FieldTypeName = "System.String", Operator = GridFilterOperator.Contains, Value = "DET1" };
            var nameLike = new GridFilter() { FieldName = "Name", FieldTypeName = "System.String", Operator = GridFilterOperator.Contains, Value = "DET1" };
            gridOptions.Filter = builder
                .New(codeLike)
                .Or(nameLike)
                .Build();
            gridOptions.SortColumns.Add(new GridOrderBy() { FieldName = "Id", Direction = "DESC" });
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
