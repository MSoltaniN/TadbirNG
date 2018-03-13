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
    public class Program
    {
        public static void Main(string[] args)
        {
            ////var gridOptions = new GridOptions() { Paging = new GridPaging() { PageIndex = 1, PageSize = 3 } };
            ////gridOptions.Filters.Add(new GridFilter() { FieldName = "Document.Id", FieldTypeName = "System.Int32", Operator = " >= {0}", Value = "1000" });
            ////gridOptions.SortColumns.Add(new GridOrderBy() { FieldName = "No", Direction = "ASC" });
            ////var json = Json.From(gridOptions, false);
            ////var urlEncoded = WebUtility.UrlEncode(json);
            ////var base64 = Transform.ToBase64String(Encoding.UTF8.GetBytes(urlEncoded));
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
