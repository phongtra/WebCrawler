using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CrawlerEngine.Core
{
    public class CrawlerHost : IAsyncDisposable
    {
        private IHost _host;
        private CancellationToken _cancellationToken;

        public static IHostBuilder CreateHostBuilder(string[] args,
            Action<HostBuilderContext, IServiceCollection> configServicesAction)
        {
            var builder = new HostBuilder()
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddJsonFile("0_Config/appsettings.json", optional: true);
                    config.AddEnvironmentVariables();

                    if (args != null)
                    {
                        config.AddCommandLine(args);
                    }
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddOptions();
                    configServicesAction(hostContext, services);
                })
                .ConfigureLogging((hostingContext, logging) =>
                {
                    logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                    logging.AddConsole();
                })
            .UseConsoleLifetime();

            return builder;
        }

        public IHost RunCrawlerEngine(IHostBuilder builder, CancellationToken cancellationToken)
        {
            _host = builder.Build();
            _cancellationToken = cancellationToken;
            return _host;
        }

        public async ValueTask DisposeAsync()
        {
            // if (_host != null)
            // {
            //     await _host.StopAsync(_cancellationToken);
            //     _host.Dispose();
            // }
        }
    }
}
