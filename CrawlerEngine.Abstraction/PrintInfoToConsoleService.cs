using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CrawlerEngine.Abstraction
{
        public class PrintInfoToConsoleService : IHostedService, IDisposable
        {
            private readonly ILogger _logger;
            private readonly IOptions<CrawlerConfig> _appConfig;
            private Timer _timer;

            public PrintInfoToConsoleService(ILogger<PrintInfoToConsoleService> logger, IOptions<CrawlerConfig> appConfig)
            {
                _logger    = logger;
                _appConfig = appConfig;
            }

            public Task StartAsync(CancellationToken cancellationToken)
            {
                _logger.LogInformation("Service Starting");

                _timer = new Timer(DoWork, null, TimeSpan.Zero,
                    TimeSpan.FromSeconds(5));

                return Task.CompletedTask;
            }

            private void DoWork(object state)
            {
                _logger.LogInformation($"Background work with text: {_appConfig.Value.TextToPrint}");
            }

            public Task StopAsync(CancellationToken cancellationToken)
            {
                _logger.LogInformation("Service Stopping");

                _timer?.Change(Timeout.Infinite, 0);

                return Task.CompletedTask;
            }

            public void Dispose()
            {
                _timer?.Dispose();
                _logger.LogInformation("Service Disposed");
            }
        }
}