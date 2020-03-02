using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CrawlerEngine.Core._1_Scheduler
{
    public class EmptyScheduler : BackgroundService, IScheduler
    {
        private int maxThreads = 16;
        private ILogger          Logger   { get; }
        private IServiceProvider Services { get; }
        private IConfiguration Configuration { get; }

        public ObservableConcurrentQueue<WaitingPageModel> PagesQueue { get; }
        // private ConcurrentQueue<WaitingPageModel> InternalPagesQueue { get; }

        ManualResetEventSlim _canExecute = new ManualResetEventSlim();

        public EmptyScheduler(IServiceProvider services, IConfiguration configuration)
        {
            Logger   = services.GetService<ILogger<EmptyScheduler>>();
            Services = services;
            Configuration = configuration;
            PagesQueue                =  services.GetRequiredService<ObservableConcurrentQueue<WaitingPageModel>>();
            PagesQueue.ContentChanged += PagesQueueOnContentChanged;
            // InternalPagesQueue = new ConcurrentQueue<WaitingPageModel>();


            //For the 1st time
            // PagesQueue.TryDequeue(out var item);
            // InternalPagesQueue.Enqueue(item);
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
            while (!stoppingToken.IsCancellationRequested/* && (InternalPagesQueue.Count > 0 || allTasks.Count > 0)*/)
            {
                if (PagesQueue.Count == 0)
                {
                    Console.WriteLine("paused");
                    _canExecute.Wait(stoppingToken);
                    _canExecute.Reset();
                }
                Console.WriteLine("resumed");
                
                if (PagesQueue.Count > 0 || allTasks.Count > 0)
                {
                    await throttler.WaitAsync(stoppingToken); //Important: need stoopingToken or will have exception when do cancellation
                    // await throttler.WaitAsync(); //Important: need stoopingToken or will have exception when do cancellation
                    if (allTasks.Count > maxThreads || stoppingToken.IsCancellationRequested)
                    {
                        allTasks.Remove(await Task.WhenAny(allTasks));
                    }

                    if (stoppingToken.IsCancellationRequested)
                    {
                        throttler.Release();
                        break;
                    }



                    PagesQueue.TryDequeue(out var item);
                    
                    if (item != null)
                    {
                        allTasks.Add(
                            Task.Run(async () =>
                            {
                                try
                                {


                                    // var html = await client.GetStringAsync(url);
                                    Console.WriteLine("aaaaaaaaaaaaa");
                                    // Console.WriteLine($"retrieved {html.Length} characters from {url}");
                                    // Logger.LogInformation("Scheduler start");
                                }
                                finally
                                {
                                    throttler.Release();
                                }
                            }));
                    }

                }
            }

            await Task.WhenAll(allTasks);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // while (!stoppingToken.IsCancellationRequested)
            // {
            //     Logger.LogInformation("MyServiceA is doing background work.");
            //
            //     await Task.Delay(1, stoppingToken);
            // }

            Task.Run(async () => await Process(stoppingToken));
        }
    }
}
