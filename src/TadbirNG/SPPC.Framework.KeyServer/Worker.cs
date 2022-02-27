using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using NetMQ;
using NetMQ.Sockets;
using SPPC.Framework.Licensing;

namespace SPPC.Framework.KeyServer
{
    public class Worker : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var responder = new ResponseSocket();
            responder.Bind(DefaultEndpoint);

            while (!stoppingToken.IsCancellationRequested)
            {
                string response;
                string request = responder.ReceiveFrameString();
                if (IsRequestSupported(request))
                {
                    response = HardwareKey.Query();
                    responder.SendFrame(response);
                }
                else
                {
                    response = "ERROR: Invalid request.";
                    responder.SendFrame(response);
                }
            }

            responder.Unbind(DefaultEndpoint);
            await Task.Delay(2000, stoppingToken);
        }

        private static bool IsRequestSupported(string request)
        {
            return request.ToLower() == "hwkey";
        }

        private const string DefaultEndpoint = "tcp://*:5555";
    }
}
