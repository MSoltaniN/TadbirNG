using System;
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
            ////var gridOptions = new GridOptions() { Paging = new GridPaging() { PageIndex = 1, PageSize = 20 } };
            ////gridOptions.Filters.Add(new GridFilter() { FieldName = "Name", FieldTypeName = "System.String", Operator = ".IndexOf({0}) == -1", Value = "های" });
            ////gridOptions.SortColumns.Add(new GridOrderBy() { FieldName = "Code", Direction = "ASC" });
            ////var json = Json.From(gridOptions, false);
            ////var base64 = Transform.ToBase64String(Encoding.UTF8.GetBytes(json));
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
