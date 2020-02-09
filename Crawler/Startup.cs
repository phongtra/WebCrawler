using System.Threading.Tasks;
using Abot2.Core;
using Abot2.Crawler;
using Abot2.Poco;
using Abot2.Util;
using Crawler.Crawler;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PuppeteerSharp;

namespace Crawler
{
    public class Startup
    {
        IConfigurationRoot Configuration { get; }

        public Startup()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional:true);
            
            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(Configuration);

            //Custom code
            services
                .AddTransient<LaunchOptions>(provider => new LaunchOptions
                {
                    Headless = false
                })
            .AddSingleton<IBrowserController, BrowserController>();

            services.AddTransient<CrawlConfiguration>(provider => new CrawlConfiguration
                {
                    MaxPagesToCrawl                    = 10,  //Only crawl 10 pages
                    MinCrawlDelayPerDomainMilliSeconds = 3000 //Wait this many millisecs between requests
                })
                .AddTransient<IPageRequester, PageRequester>()
                .AddTransient<IPoliteWebCrawler, PoliteWebCrawler>(provider =>
                {
                    var crawlConfiguration = provider.GetRequiredService<CrawlConfiguration>();
                    ICrawlDecisionMaker crawlDecisionMaker = null;
                    // var crawlDecisionMaker = provider.GetRequiredService<ICrawlDecisionMaker>();
                    IThreadManager threadManager = null;
                    // var threadManager = provider.GetRequiredService<IThreadManager>();
                    IScheduler scheduler = null;
                    // var scheduler = provider.GetRequiredService<IScheduler>();
                    IPageRequester pageRequester = null;
                    // var pageRequester = provider.GetRequiredService<IPageRequester>();
                    IHtmlParser htmlParser = null;
                    // var htmlParser = provider.GetRequiredService<IHtmlParser>();
                    IMemoryManager memoryManager = null;
                    // var memoryManager = provider.GetRequiredService<IMemoryManager>();
                    IDomainRateLimiter domainRateLimiter = null;
                    // var domainRateLimiter = provider.GetRequiredService<IDomainRateLimiter>();
                    IRobotsDotTextFinder robotsDotTextFinder = null;
                    // var robotsDotTextFinder = provider.GetRequiredService<IRobotsDotTextFinder>();

                    return new PoliteWebCrawler(crawlConfiguration, crawlDecisionMaker, threadManager, scheduler,
                        pageRequester, htmlParser, memoryManager, domainRateLimiter, robotsDotTextFinder);
                });
        }

        public async Task StartApp()
        {
            
            //Custom code
            // await CrawlerCore.DemoCrawl();
            await CrawlerHandler.DemoSimpleCrawler();
        }
    }
}
