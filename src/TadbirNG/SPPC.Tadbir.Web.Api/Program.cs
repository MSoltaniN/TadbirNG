using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using SPPC.Framework.Common;

namespace SPPC.Tadbir.Web.Api
{
    /// <summary>
    ///
    /// </summary>
    public class Program
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
#if DEBUG
            AnalyzeCurrentApi();
#endif
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="args"></param>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        private static void AnalyzeCurrentApi()
        {
            var types = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(type => type.Name.EndsWith("Controller"));
            foreach (var type in types)
            {
                var httpGets = type
                    .GetMethods()
                    .Where(method => method.Name.StartsWith("Get")
                        && method.Name != "GetType" && method.Name != "GetHashCode");
                foreach (var httpGet in httpGets)
                {
                    if (Reflector.GetAttribute(httpGet, typeof(HttpGetAttribute)) is not HttpGetAttribute attrib)
                    {
                        Debug.WriteLine("WARN: Missing HttpGet attribute on Controller method.");
                        Debug.WriteLine("{0}.{1}", type.Name, httpGet.Name);
                        Debug.WriteLine(Environment.NewLine);
                    }
                }
            }
        }
    }
}
