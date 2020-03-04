using System;
using System.Threading.Tasks;
using Abot2.Crawler;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using StructureMap;

namespace Crawler
{
    /// <summary>
    /// Do not modify anything here
    /// </summary>
    class Program
    {
        //https://stackoverflow.com/questions/41407221/startup-cs-in-a-self-hosted-net-core-console-application
        public static IServiceProvider ServiceProvider { get; private set; }
        static async Task Main(string[] args)
        {
            // add the framework services
            var services = new ServiceCollection();
            //Configure for common
            ConfigureServices(services);
            //Customized configuring
            var startup = new Startup();
            startup.ConfigureServices(services);
            //Start the code here with 1 service provider
            await using var serviceProvider = services.BuildServiceProvider();
            ServiceProvider = serviceProvider;
            
            LogInfo("Starting application");

            await startup.StartApp();
        }

        public static void LogInfo(string info)
        {
            var logger = ServiceProvider.GetService<ILogger<Program>>();
            logger.LogInformation(info);
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();

            services
                .AddLogging(configure => configure.AddSerilog());

            // add StructureMap
            var container = new StructureMap.Container();
            container.Configure(config =>
            {
                // Register stuff in container, using the StructureMap APIs...
                config.Scan(_ =>
                {
                    _.AssemblyContainingType(typeof(Program));
                    _.WithDefaultConventions();
                });
                // Populate the container using the service collection
                config.Populate(services);
            });
        }
    }
}