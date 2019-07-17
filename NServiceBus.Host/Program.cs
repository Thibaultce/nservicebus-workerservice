using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NServiceBus.Host.Configuration;
using System.IO;

namespace NServiceBus.Host
{
    public class Program
    {
        static IConfiguration Configuration;

        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.SetBasePath(Directory.GetCurrentDirectory());

                    Configuration = config.Build();
                })
                .ConfigureServices(services =>
                {
                    services.AddHostedService<Worker>();
                    services.AddTransient<NServiceBusHost>();

                    services.Configure<NServiceBusOptions>(Configuration.GetSection("NServiceBus"));
                });
    }
}
