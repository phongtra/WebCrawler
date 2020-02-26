using System;
using System.Text;
using System.Threading.Tasks;
using AngleSharp;
using CrawlerEngine._2_Downloader;
using CrawlerEngine.Abstraction._1_Scheduler;
using CrawlerEngine.Abstraction._2_Downloader;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using UriDB.Data;
using UriDB.Data.Entities;

namespace CrawlerEngine.Abstraction
{
    public class ServicesConfigurationHelper
    {
        public static void ConfigureDefaultServices(HostBuilderContext hostContext, IServiceCollection services)
        {
            services.Configure<CrawlerConfig>(hostContext.Configuration.GetSection(CrawlerConfig.ConfigSection));
            services.AddTransient<ISchedulerService, SchedulerServiceCore>();
            services.AddTransient<IUriBucket<WaitingPage>, DefaultWaitingUriBucket>();
            // services.AddSingleton<IHostedService, PrintInfoToConsoleService>();

            services.AddTransient<ParallelOptions>(serviceProvider => new ParallelOptions
            {
                MaxDegreeOfParallelism = Environment.ProcessorCount - 1
            });

            services.AddHttpClient();
            services.AddTransient<IDownloader, Downloader>();

            // services.AddDbContext<UriDBContext>(options =>
            // {
            //     options.UseSqlite(@"Data Source=./1_Scheduler/DB/UriDB.db;");
            // }, ServiceLifetime.Scoped);

            services.AddDbContextPool<UriDBContext>(options =>
            {
                options.UseSqlite(@"Data Source=./1_Scheduler/DB/UriDB.db;");
            }, 128);

            // services.AddSingleton<ObjectPool<PooledObjectWrapper<UriDBContext>>>(provider =>
            // {
            //     return new ObjectPool<PooledObjectWrapper<UriDBContext>>(25,() => 
            //         new PooledObjectWrapper<UriDBContext>(provider.GetService<UriDBContext>())
            //         {
            //             OnReleaseResources = uriDBContext => {},
            //             OnResetState       = uriDBContext => {}
            //         });
            // });
        }

        public static void ConfigureDefaultDownloaderServices(HostBuilderContext hostContext, IServiceCollection services)
        {
            services.AddTransient<IBrowsingContext>(serviceProvider =>
            {
                //Use the default configuration for AngleSharp
                var config = Configuration.Default;

                //Create a new context for evaluating webpages with the given config
                var context = BrowsingContext.New(config);
                return context;
            });
            

        }
    }
}
