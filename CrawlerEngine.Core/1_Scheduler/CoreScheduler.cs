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
using CrawlerEngine.Core._1_Scheduler.DB.Data.Entities;
using CrawlerEngine.Core._3_Dowloader;
using CrawlerEngine.Core._5_ContentExtractor;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace CrawlerEngine.Core._1_Scheduler
{
    public class CoreScheduler :  BackgroundService, IScheduler
    {
        private int maxThreads = 32;
        private ILogger          Logger   { get; }
        private IServiceProvider Services { get; }
        private IConfiguration Configuration { get; }

        public ObservableConcurrentQueue<WaitingPageModel> PagesQueue { get; }
        // private ConcurrentQueue<WaitingPageModel> InternalPagesQueue { get; }

        readonly ManualResetEventSlim _canExecute = new ManualResetEventSlim();
        private bool _stopping = false;

        public CoreScheduler(IServiceProvider services, IConfiguration configuration)
        {
            Logger   = services.GetService<ILogger<CoreScheduler>>();
            Services = services;
            Configuration = configuration;
            PagesQueue                =  services.GetRequiredService<ObservableConcurrentQueue<WaitingPageModel>>();
            PagesQueue.ContentChanged += PagesQueueOnContentChanged;
        }

        private void Stop()
        {
            _stopping = true;
        }

        private async void PagesQueueOnContentChanged(object sender,
            ObservableConcurrentQueue<WaitingPageModel>.NotifyConcurrentQueueChangedEventArgs<WaitingPageModel> args)
        {
            if (args.Action == ObservableConcurrentQueue<WaitingPageModel>.NotifyConcurrentQueueChangedAction.Enqueue)
            {
                // PagesQueue.TryDequeue(out var item);
                // InternalPagesQueue.Enqueue(item);
                _canExecute.Set();
            }
        }

        private async Task Process(CancellationToken stoppingToken)
        {
            var allTasks  = new List<Task>();
            var throttler = new SemaphoreSlim(initialCount: maxThreads);
            while (!StoppingTokenIsCancellationRequested(stoppingToken))
            {
                if (PagesQueue.Count == 0)
                {
                    _canExecute.Wait(stoppingToken);
                    _canExecute.Reset();
                }

                if (PagesQueue.Count > 0 || allTasks.Count > 0)
                {
                    await throttler.WaitAsync(stoppingToken); //Important: need stoopingToken or will have exception when do cancellation
                    // await throttler.WaitAsync(); //Important: need stoopingToken or will have exception when do cancellation
                    if (allTasks.Count > maxThreads || StoppingTokenIsCancellationRequested(stoppingToken))
                    {
                        allTasks.Remove(await Task.WhenAny(allTasks));
                    }

                    if (allTasks.Count > maxThreads)
                    {
                        //Error, should not over maxThreads
                        throw new Exception("Never reach here");
                    }

                    if (StoppingTokenIsCancellationRequested(stoppingToken))
                    {
                        throttler.Release();
                        break;
                    }

                    // if (PagesQueue.Count > 0)
                    if (PagesQueue.TryDequeue(out var item))
                    {
                        //Dequeue item and add into uriBucket
                        //UriBucket may decide to get a different item than the dequeued to process (GetNextUri at CrawlTask)
                        //TODO: in uriBucket there may have old uris which are not retrieved last time, need to sync these item back to PageQueue at starting
                        using var uriBucket = Services.GetService<IUriBucket<WaitingPage>>();
                        uriBucket.Add(new WaitingPage {Uri = item.Uri.ToString()});

                        allTasks.Add(
                            Task.Run(() =>
                            {
                                try
                                {
                                    CrawlTask(stoppingToken);
                                }
                                finally
                                {
                                    throttler.Release();
                                }
                            }, stoppingToken));
                    }
                }

                await Task.Delay(5, stoppingToken);
            }

            await Task.WhenAll(allTasks);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Run(async () => await Process(stoppingToken));
            // await Process(stoppingToken);
        }

        public new async Task StopAsync(CancellationToken cancellationToken)
        {
            Stop();
            await base.StopAsync(cancellationToken);
        }

        private async void CrawlTask(CancellationToken stoppingToken)
        {
            if (StoppingTokenIsCancellationRequested(stoppingToken)) 
                return;
            // if (!PagesQueue.TryDequeue(out var pageToCrawl)) return;
            using var uriBucket = Services.GetService<IUriBucket<WaitingPage>>();
            var pageToCrawl = uriBucket.GetNextUri();
            //Since uriBucket use it logic to get the page to crawl, it may decide not crawl any page
            if (pageToCrawl == null)
            {
                return;
            }

            Logger.LogInformation("Downloading " + pageToCrawl.Uri);
            var downloader = Services.GetRequiredService<IDownloader>();
            var method     = new HttpMethod(pageToCrawl.Verb);
            // var requestUri = pageToCrawl.Uri;
            var requestUri = new Uri(pageToCrawl.Uri);

            if (!(requestUri.Scheme.ToLower() == "http" || requestUri.Scheme.ToLower() == "https"))
            {
                return;
            }

            Tuple<HttpStatusCode, string> downloadedContent;
            try
            {
                downloadedContent = await downloader.GetPage(new HttpRequestMessage(method, pageToCrawl.Uri));
            }
            catch (Exception e)
            {
                Logger.LogError(e.ToString());
                //TODO:Enqueue it again
                
                return;
            }
            if (StoppingTokenIsCancellationRequested(stoppingToken)) return;

            pageToCrawl.DownloadedTime = DateTime.UtcNow.ToString(Tools.StrDateTimeFormat);

            //3. Process the page (IPageProcessor)
            if (downloadedContent.Item1 == HttpStatusCode.OK)
            {
                var browsingContext = Services.GetService<IBrowsingContext>();
                
                // var credentials = new NetworkCredential("user", "pass", "domain");
                // var handler     = new HttpClientHandler { Credentials = credentials };
                // var config = Configuration.Default
                //     .WithRequesters(handler)
                //     .WithCookies()
                //     .WithDefaultLoader();
                // var context  = BrowsingContext.New(config);
                var parser   = browsingContext.GetService<IHtmlParser>();
                var document = parser.ParseDocument(downloadedContent.Item2); 
                
                Logger.LogInformation("Document parsed");

                if (StoppingTokenIsCancellationRequested(stoppingToken)) return;
                using var contentExtractor = Services.GetService<IContentExtractor>();
                try
                {
                    await contentExtractor.ProcessPageContent(document, requestUri);
                }
                catch (Exception e)
                {
                    pageToCrawl.Error = e.ToString();
                    pageToCrawl.RetrieveErrorAtStep = "Content Extraction";
                    pageToCrawl.NeedUpdate = 0;
                    uriBucket.Add(pageToCrawl);
                }

                var links = document
                    .Links
                    .OfType<IHtmlAnchorElement>()
                    .Where(e => e.Href.StartsWith("https://www.webtoons.com"))
                    .Select(e => new Uri(e.Href));
            
                foreach (var link in links.Where(item => item.Scheme.ToLower() == "http" || item.Scheme.ToLower() == "https"))
                {
                    if (StoppingTokenIsCancellationRequested(stoppingToken))
                    {
                        break;
                    }
                    PagesQueue.Enqueue(new WaitingPageModel {Uri = link, Verb = "GET"});
                }
            }
            else
            {
                Logger.LogInformation("Can not download, Http code " + downloadedContent.Item1 + "Page " + pageToCrawl.Uri);
            }
        }

        private bool StoppingTokenIsCancellationRequested(CancellationToken stoppingToken)
        {
            return stoppingToken.IsCancellationRequested || _stopping;
        }
    }
}
