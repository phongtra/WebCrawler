using System;
using System.Threading.Tasks;
using CrawlerEngine._4_Storage.PageModels.Data;
using CrawlerEngine.Abstraction;
using CrawlerEngine.Abstraction._1_Scheduler;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using UriDB.Data.Entities;


namespace CrawlerEngine
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            Framework.SetMultiThread();

            var builder = new HostBuilder()
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddJsonFile("Config/appsettings.json", optional: true);
                    config.AddEnvironmentVariables();

                    if (args != null)
                    {
                        config.AddCommandLine(args);
                    }
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddOptions();
                    ServicesConfigurationHelper.ConfigureDefaultServices(hostContext, services);
                    services.AddHostedService(serviceProvider => serviceProvider.GetService<ISchedulerService>());
                    services.AddDbContextPool<ContentContext>((serviceProvider, options) =>
                    {
                        
                        options.UseSqlite(hostContext.Configuration.GetSection("AppConfig")["ConnectionString"]);
                    }, poolSize:200);

                    ServicesConfigurationHelper.ConfigureDefaultDownloaderServices(hostContext, services);
                })
                .ConfigureLogging((hostingContext, logging) =>
                {
                    logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                    logging.AddConsole();
                });

            using var host = builder.Build();

            Console.WriteLine("Application Starting!");
            await host.StartAsync();
            Console.WriteLine("Application Started! Press <enter> to stop.");

            using (var uriBucket = host.Services.GetService<IUriBucket<WaitingPage>>())
            {
                uriBucket.Add(new WaitingPage {Uri = "https://google.com/"});
            }


            Console.ReadLine();


            Console.WriteLine("Application Stopping!");
            await host.StopAsync();
            Console.WriteLine("Main thread Stopped!");

            // await builder.RunConsoleAsync();
        }
    }
}
