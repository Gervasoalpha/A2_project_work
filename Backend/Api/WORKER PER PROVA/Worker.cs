using System.Diagnostics;
using System.Net.Mime;
using System.Text;
using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;

namespace WORKER_PER_PROVA
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private string? AuthCode;
        private string? UnlockCode;
        private string? info;
        private string? status;
        private int PortNumber;
        private int BuildingNumber;
        private readonly IConfiguration conf;
        public Worker(ILogger<Worker> logger, IConfiguration configuration)
        {
            _logger = logger;
            status = "alive";
            BuildingNumber = 0;
            PortNumber = 0;
            conf = configuration;

        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var deviceclient = DeviceClient.CreateFromConnectionString(conf.GetConnectionString("Device"));

            while (!stoppingToken.IsCancellationRequested)
            {
                Console.WriteLine("Insert command to send message:");
                var con = Console.ReadLine();
                if (con == "send")
                {
                    var message = JsonConvert.SerializeObject(new
                    {
                        AuthCode,
                        UnlockCode,
                        status,
                        info,
                        BuildingNumber,
                        PortNumber,
                    });
                   
                    using var message_body = new Message(Encoding.ASCII.GetBytes(message))
                    {
                        ContentType = "application/json",
                        ContentEncoding = "utf-8"
                    };
                    await deviceclient.SendEventAsync(message_body);
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                    _logger.LogInformation(message);
                    await Task.Delay(1000, stoppingToken);
                }
                
            }
        }
    }
}