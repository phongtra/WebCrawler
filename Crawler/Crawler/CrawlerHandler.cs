using System;
using System.Collections.Generic;
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
        public static async Task DemoSimpleCrawler()
        {
            var crawler = Program.ServiceProvider.GetService<IPoliteWebCrawler>();

            crawler.PageCrawlCompleted += Crawler_PageCrawlCompleted; ; //Several events available...

            var crawlResult = await crawler.CrawlAsync(new Uri("https://vnexpress.net"));
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
