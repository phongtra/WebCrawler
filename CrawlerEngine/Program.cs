using System;
using System.Diagnostics;
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
                    }, 128);

                    ServicesConfigurationHelper.ConfigureDefaultDownloaderServices(hostContext, services);
                })
                .ConfigureLogging((hostingContext, logging) =>
                {
                    logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                    logging.AddConsole();
                });
                // .UseConsoleLifetime();

            using var host = builder.Build();

            try
            {
                Console.WriteLine("Application Starting!");
                await host.StartAsync();
                Console.WriteLine("Application Started! Press <enter> to stop.");

                using (var uriBucket = host.Services.GetService<IUriBucket<WaitingPage>>())
                {
                    uriBucket.Add(new WaitingPage {Uri = "https://www.webtoons.com"});
                }


                Console.ReadLine();


                Console.WriteLine("Application Stopping!");
                await host.StopAsync();
                Console.WriteLine("Main thread Stopped!");
            }
            catch (Exception e)
            {
                var trace = new StackTrace(e, true);
                var line = trace.GetFrame(trace.FrameCount -1).GetFileLineNumber();
                Console.WriteLine("Exception at line " + line );
                Console.WriteLine(e);
            }

            // await builder.RunConsoleAsync();
        }
    }
}
