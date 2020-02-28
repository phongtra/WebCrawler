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
        private bool _stopping = false;

        // private CancellationTokenSource _cancellationTokenSource;

        // private Task[] tasks;
        private Task _getPagesTask1;
        private Task _getPagesTask2;
        private Task _getPagesTask3;
        private Task _getPagesTask4;
        // private Task _updatePriorityTask;

        public SchedulerServiceCore(/*ILoggerFactory loggerFactory,*/ IServiceProvider services)
        {
            // Logger = loggerFactory.CreateLogger<SchedulerServiceCore>();
            Logger = services.GetService<ILogger<SchedulerServiceCore>>();

            Services = services;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            Logger.LogInformation("MyServiceB is starting.");
            stoppingToken.Register(() => Logger.LogInformation("Scheduler service is stopping."));
            // Create a linked token so we can trigger cancellation outside of this token's cancellation
            // _cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(stoppingToken);

            // tasks = new List<Task>
            // {
            //     BackgroundTask(_cancellationTokenSource.Token),
            //     BackgroundTask2_UpdatePagePriority(_cancellationTokenSource.Token)
            // };

            // tasks = new Task[2];
            //
            // Parallel.Invoke(() => { tasks[0] = BackgroundTask(_cancellationTokenSource.Token); }
            //     , () => { tasks[1] = BackgroundTask2_UpdatePagePriority(_cancellationTokenSource.Token); });
            
            // _getPagesTask = BackgroundTask(_cancellationTokenSource.Token);
            // _updatePriorityTask = BackgroundTask2_UpdatePagePriority(_cancellationTokenSource.Token);

            // _getPagesTask = Task.Run(() => BackgroundTask(_cancellationTokenSource.Token));
            _getPagesTask1 = Task.Run(BackgroundTask, stoppingToken);
            // _getPagesTask2 = Task.Run(BackgroundTask, stoppingToken);
            // _getPagesTask3 = Task.Run(BackgroundTask, stoppingToken);
            // _getPagesTask4 = Task.Run(BackgroundTask, stoppingToken);

            return Task.CompletedTask;
        }

        // private async Task BackgroundTask(CancellationToken stoppingToken)
        private async Task BackgroundTask()
        {
            
                // while (true)
                while (!_stopping)
                // while (!stoppingToken.IsCancellationRequested)
                {
                    using var uriBucket = Services.GetService<IUriBucket<WaitingPage>>();
                    // Logger.LogInformation("MyServiceB is doing background work.");

                    // using var scope        = Services.CreateScope();
                    // var       uriDbContext = scope.ServiceProvider.GetService<UriDBContext>();

                    var pageToCrawl = uriBucket.GetNextUri();
                    if (pageToCrawl == null) continue;
                    await DoCrawling(pageToCrawl, uriBucket);
                    uriBucket.Remove(pageToCrawl);

                    //Do data maintenance here

                    // Check stopping token trigger, wait 1 ms
                    // await Task.Delay(1, stoppingToken);
                    //Delay a little bit, this is optional
                    // await Task.Delay(5);
                }

                Logger.LogInformation("MyServiceB background task is stopping.");

        }

        // private async Task BackgroundTask2_UpdatePagePriority(CancellationToken stoppingToken)
        // {
        //     // using (var uriBucket = Services.GetService<IUriBucket<WaitingPage>>())
        //     {
        //         while (!stoppingToken.IsCancellationRequested)
        //         {
        //             // await DoPriorityUpDate(uriBucket);
        //
        //             // Check stopping token trigger, wait 1 ms
        //             await Task.Delay(1, stoppingToken);
        //         }
        //     }
        // }

        public async Task StopAsync(CancellationToken stoppingToken)
        {
            Logger.LogInformation("MyServiceB is stopping.");
            //TODO: using quartz to set resume at time .StopAsync(new System.Threading.CancellationToken());

            _stopping = true;
            // Stop called without start
            // if (tasks == null)
            if (_getPagesTask1 == null)
            {
                return;
            }

            // Signal cancellation to the executing method
            // _cancellationTokenSource.Cancel();

            // Wait until the task completes or the stop token triggers, delay -1: wait infinite
            // await Task.WhenAny(
            //     tasks[0],
            //     tasks[1],
            //     Task.Delay(-1, stoppingToken));

            await Task.WhenAny(
                _getPagesTask1,
                // _getPagesTask2,
                // _getPagesTask3,
                // _getPagesTask4,
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
                .Where(e => e.Href.StartsWith("https://www.webtoons.com"))
                .Select(e => new Uri(e.Href));

            foreach (var link in links.Where(item => item.Scheme.ToLower() == "http" || item.Scheme.ToLower() == "https"))
            {
                uriBucket.Add(new WaitingPage{Uri = link.ToString()});
            }

            // DoPriorityUpDate();
        }

        private void DoPriorityUpDate(IUriBucket<WaitingPage> uriBucket)
        {
            // DateTime.ParseExact("2009-05-08 14:40:52,531", "yyyy-MM-dd HH:mm:ss,fff",
            //     System.Globalization.CultureInfo.InvariantCulture);
            
        }
    }
}
