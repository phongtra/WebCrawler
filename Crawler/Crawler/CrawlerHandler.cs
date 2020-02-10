using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abot2.Crawler;
using Abot2.Poco;
using Microsoft.Extensions.DependencyInjection;

namespace Crawler
{
    public class CrawlerHandler
    {
        private static int count = 0;
        private static List<string> genres = new List<string>(){"drama", "fantasy", "comedy", "action", "slice-of-life", "romance", "superhero", "historical", "thriller", "sports", "heartwarming", "sci-fi", "horror", "informative"};
        public static async Task DemoSimpleCrawler()
        {
            var crawler = Program.ServiceProvider.GetService<IPoliteWebCrawler>();
            crawler.ShouldCrawlPageDecisionMaker += (crawl, context) =>
            {
                if (genres.Any(genre => crawl.Uri.AbsoluteUri.Contains(genre) ||
                                        crawl.Uri.AbsoluteUri == "https://www.webtoons.com/en/dailySchedule"))
                {
                    return new CrawlDecision { Allow = true };
                }
            
                return new CrawlDecision { Allow = false, Reason = "Invalid site" };
            };

            crawler.PageCrawlCompleted += Crawler_PageCrawlCompleted; ; //Several events available...

            var crawlResult = await crawler.CrawlAsync(new Uri("https://www.webtoons.com/en/dailySchedule"));
        }

        private static void Crawler_PageCrawlCompleted(object sender, PageCrawlCompletedArgs e)
        {
            count++;
            var httpStatus  = e.CrawledPage.HttpResponseMessage.StatusCode;
            var rawPageText = e.CrawledPage.Content.Text;
            Program.LogInfo(count.ToString());
        }
    }
}
