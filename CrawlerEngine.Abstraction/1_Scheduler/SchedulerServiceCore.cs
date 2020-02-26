using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using CrawlerEngine._2_Downloader;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using UriDB.Data;
using UriDB.Data.Entities;

namespace CrawlerEngine.Abstraction._1_Scheduler
{
    public class SchedulerServiceCore : ISchedulerService
    {
        public ILogger Logger { get; }
        public IServiceProvider Services { get; }

        private CancellationTokenSource _cancellationTokenSource;

        private Task[] tasks;
        private Task _getPagesTask;
        private Task _updatePriorityTask;

        public SchedulerServiceCore(/*ILoggerFactory loggerFactory,*/ IServiceProvider services)
        {
            // Logger = loggerFactory.CreateLogger<SchedulerServiceCore>();
            Logger = services.GetService<ILogger<PrintInfoToConsoleService>>();

            Services = services;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            Logger.LogInformation("MyServiceB is starting.");
            stoppingToken.Register(() => Logger.LogInformation("Scheduler service is stopping."));
            // Create a linked token so we can trigger cancellation outside of this token's cancellation
            _cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(stoppingToken);

            // tasks = new List<Task>
            // {
            //     BackgroundTask(_cancellationTokenSource.Token),
            //     BackgroundTask2_UpdatePagePriority(_cancellationTokenSource.Token)
            // };

            // tasks = new Task[2];
            //
            // Parallel.Invoke(() => { tasks[0] = BackgroundTask(_cancellationTokenSource.Token); }
            //     , () => { tasks[1] = BackgroundTask2_UpdatePagePriority(_cancellationTokenSource.Token); });
            
            _getPagesTask = BackgroundTask(_cancellationTokenSource.Token);
            // _updatePriorityTask = BackgroundTask2_UpdatePagePriority(_cancellationTokenSource.Token);

            return Task.CompletedTask;
        }

        private async Task BackgroundTask(CancellationToken stoppingToken)
        {
            using (var uriBucket = Services.GetService<IUriBucket<WaitingPage>>())
            {
                // while (!_stopping)
                while (!stoppingToken.IsCancellationRequested)
                {
                    // Logger.LogInformation("MyServiceB is doing background work.");

                    // using var scope        = Services.CreateScope();
                    // var       uriDbContext = scope.ServiceProvider.GetService<UriDBContext>();

                    var pageToCrawl = uriBucket.GetNextUri();
                    if (pageToCrawl != null)
                    {
                        await DoCrawling(pageToCrawl, uriBucket);
                        uriBucket.Remove(pageToCrawl);
                    }

                    // Check stopping token trigger, wait 1 ms
                    await Task.Delay(1, stoppingToken);
                }
            }

            Logger.LogInformation("MyServiceB background task is stopping.");
        }

        private async Task BackgroundTask2_UpdatePagePriority(CancellationToken stoppingToken)
        {
            // using (var uriBucket = Services.GetService<IUriBucket<WaitingPage>>())
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    // await DoPriorityUpDate(uriBucket);
        
                    // Check stopping token trigger, wait 1 ms
                    await Task.Delay(1, stoppingToken);
                }
            }
        }

        public async Task StopAsync(CancellationToken stoppingToken)
        {
            Logger.LogInformation("MyServiceB is stopping.");
            //TODO: using quartz to set resume at time .StopAsync(new System.Threading.CancellationToken());

            // Stop called without start
            // if (tasks == null)
            if (_getPagesTask == null)
            {
                return;
            }

            // Signal cancellation to the executing method
            _cancellationTokenSource.Cancel();

            // Wait until the task completes or the stop token triggers, delay -1: wait infinite
            // await Task.WhenAny(
            //     tasks[0],
            //     tasks[1],
            //     Task.Delay(-1, stoppingToken));

            await Task.WhenAny(
                _getPagesTask,
                // _updatePriorityTask,
                Task.Delay(-1, stoppingToken));
        }

        public void Dispose()
        {
            Logger.LogInformation("MyServiceB is disposing.");
        }

        public async Task DoCrawling(WaitingPage pageToCrawl, IUriBucket<WaitingPage> uriBucket)
        {
            //1. Get page url

            //4. Get parsed data into:
            //   Page url -> Scheduler
            //   Data Model -> Storage
            Logger.LogInformation("Start crawling " + pageToCrawl.Uri);

            //2. Download the page (IDownloader)
            var downloader = Services.GetService<IDownloader>();
            var method = new HttpMethod(pageToCrawl.Verb);
            var requestUri = pageToCrawl.Uri;
            var downloadedContent = await downloader.GetPage(new HttpRequestMessage(method, requestUri));
            pageToCrawl.DownloadedTime = DateTime.UtcNow.ToString(Tools.StrDateTimeFormat);

            //3. Process the page (IPageProcessor)
            var browsingContext = Services.GetService<IBrowsingContext>();

            // var credentials = new NetworkCredential("user", "pass", "domain");
            // var handler     = new HttpClientHandler { Credentials = credentials };
            // var config = Configuration.Default
            //     .WithRequesters(handler)
            //     .WithCookies()
            //     .WithDefaultLoader();
            // var context  = BrowsingContext.New(config);
            var parser   = browsingContext.GetService<IHtmlParser>();
            var document = parser.ParseDocument(downloadedContent);
            // var document = await browsingContext.OpenAsync(downloadedContent);
            var links = document
                .Links
                .OfType<IHtmlAnchorElement>()
                .Select(e => new Uri(e.Href));

            foreach (var link in links.Where(item => item.Scheme.ToLower() == "http" || item.Scheme.ToLower() == "https"))
            {
                uriBucket.Add(new WaitingPage{Uri = link.ToString()});
            }
        }

        private async Task DoPriorityUpDate(IUriBucket<WaitingPage> uriBucket)
        {
            // DateTime.ParseExact("2009-05-08 14:40:52,531", "yyyy-MM-dd HH:mm:ss,fff",
            //     System.Globalization.CultureInfo.InvariantCulture);
            
        }
    }
}
