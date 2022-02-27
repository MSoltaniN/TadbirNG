using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace SPPC.Framework.KeyServer
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            using var host = Host.CreateDefaultBuilder(args)
                .UseWindowsService(options =>
                {
                    options.ServiceName = "SPPC Key Server";
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();
                })
                .Build();

            await host.RunAsync();
        }
    }
}
