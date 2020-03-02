using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp;
using CrawlerEngine._5_ContentExtractor;
using CrawlerEngine._6_Storage.PageModels.Data;
using CrawlerEngine.Core;
using CrawlerEngine.Core._1_Scheduler;
using CrawlerEngine.Core._1_Scheduler.DB.Data;
using CrawlerEngine.Core._1_Scheduler.DB.Data.Entities;
using CrawlerEngine.Core._3_Dowloader;
using CrawlerEngine.Core._5_ContentExtractor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Bulkhead;

namespace CrawlerEngine
{
    class Program
    {
        static async Task Main(string[] args)
        {
            ThreadPool.SetMaxThreads(100, Int32.MaxValue);
            // Create the token source.
            var cts = new CancellationTokenSource();

            await using var crawlerHost = new CrawlerHost();
            var hostBuilder = CrawlerHost.CreateHostBuilder(args, (hostContext, services) =>
            {
                //Add more services to the Dependency Injection here

                //1. Scheduler
                // services.AddTransient<ISchedulerService, DefaultScheduler>();
                // services.AddHostedService(serviceProvider => serviceProvider.GetService<ISchedulerService>());
                services.AddTransient<IScheduler, CoreScheduler>();
                services.AddHostedService(serviceProvider => serviceProvider.GetService<IScheduler>());

                services.AddSingleton<ObservableConcurrentQueue<WaitingPageModel>>();
                // services.AddSingleton<AsyncBulkheadPolicy>(serviceProvider =>
                //     Policy.BulkheadAsync(Convert.ToInt32(hostContext.Configuration.GetSection("AppConfig")["MaxThreads"]),
                //         Int32.MaxValue));
                services.AddTransient<IUriBucket<WaitingPage>, DefaultWaitingUriBucket>();
                services.AddDbContextPool<UriDBContext>(options =>
                {
                    options.UseSqlite(@"Data Source=./1_Scheduler/DB/UriDB.db;");
                }, 16);

                //2. Crawler
                services.AddSingleton<IBrowsingContext>(serviceProvider =>
                {
                    //Use the default configuration for AngleSharp
                    var config = Configuration.Default
                        .WithRequesters()     // from AngleSharp.Io
                        .WithDefaultLoader(); // from AngleSharp

                    //Create a new context for evaluating webpages with the given config
                    var context = BrowsingContext.New(config);
                    return context;
                });

                //3. Downloader
                services.AddHttpClient();
                services.AddTransient<IDownloader, Downloader>();

                //4. UriPolicies

                //5. Content Extractor
                services.AddTransient<IContentExtractor, ContentExtractor>();

                //6. Storage
                services.AddDbContextPool<ContentContext>((serviceProvider, options) =>
                {
                    options.UseSqlite(hostContext.Configuration.GetSection("AppConfig")["ConnectionString"]);
                    // options.UseSqlite(@"Data Source=./4_Storage/PageModels/DB/content.db;");
                }, 32);
            });

            try
            {
                Console.WriteLine("Application Starting!");
                
                using var host = crawlerHost.RunCrawlerEngine(hostBuilder, cts.Token);
                
                // var waitingQueue = host.Services.GetRequiredService<ObservableConcurrentQueue<WaitingPageModel>>();
                // waitingQueue.Enqueue(new WaitingPageModel {Uri = new Uri("https://www.webtoons.com"), Verb = "GET"});

                await host.StartAsync(cts.Token);

                Console.WriteLine("Application Started! Press <enter> to stop.");

                var waitingQueue = host.Services.GetRequiredService<ObservableConcurrentQueue<WaitingPageModel>>();
                waitingQueue.Enqueue(new WaitingPageModel {Uri = new Uri("https://www.webtoons.com"), Verb = "GET"});

                // Console.WriteLine("Enter");

                Console.ReadLine();

                // waitingQueue = host.Services.GetRequiredService<ObservableConcurrentQueue<WaitingPageModel>>();
                // waitingQueue.Enqueue(new WaitingPageModel {Uri = new Uri("https://www.webtoons.com"), Verb = "GET"});
                //
                // Console.ReadLine();

                // cts.Cancel();

                await host.StopAsync(cts.Token);

                Console.WriteLine("Application Stopping!");
                Console.WriteLine("Main thread Stopped!");
            }
            catch (Exception e)
            {
                var trace = new StackTrace(e, true);
                var line  = trace.GetFrame(trace.FrameCount - 1).GetFileLineNumber();
                Console.WriteLine("Exception at line " + line);
                Console.WriteLine(e);
            }
            finally
            {
                cts.Dispose();
            }
        }
    }
}