using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Abot2.Crawler;
using Abot2.Poco;
using AngleSharp;
using AngleSharp.Html.Parser;
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
            if (e.CrawledPage.Uri.AbsoluteUri == "https://www.webtoons.com/en/dailySchedule")
            {
                var context = BrowsingContext.New(Configuration.Default);
                var parser = context.GetService<IHtmlParser>();
                var document = parser.ParseDocument(rawPageText);
                var comicCards = document.QuerySelectorAll(".daily_card_item");
                foreach (var comicCard in comicCards)
                {
                    var imageLink = comicCard.QuerySelector("img").GetAttribute("src");
                    var link = comicCard.GetAttribute("href");
                    var titleNo = HttpUtility.ParseQueryString(new Uri(link).Query).Get("title_no");
                    var genre = comicCard.QuerySelector(".genre");
                    var subject = comicCard.QuerySelector("subj");
                    var author = comicCard.QuerySelector("author");

                }
            }
            else if (e.CrawledPage.Uri.AbsoluteUri.Contains("list"))
            {
                var context = BrowsingContext.New(Configuration.Default);
                var parser = context.GetService<IHtmlParser>();
                var document = parser.ParseDocument(rawPageText);
                var list = document.QuerySelectorAll("#_listUl");
                foreach (var item in list)
                {
                    var link = new Uri(e.CrawledPage.Uri.AbsoluteUri);
                    var titleNo = HttpUtility.ParseQueryString(link.Query).Get("title_no");
                    var subject = item.QuerySelector("a .subj").InnerHtml;
                    Console.WriteLine(subject);
                }
            }
            else if (e.CrawledPage.Uri.AbsoluteUri.Contains("ep-"))
            {
                var context = BrowsingContext.New(Configuration.Default);
                var parser = context.GetService<IHtmlParser>();
                var document = parser.ParseDocument(rawPageText);
                var content = document.QuerySelector(".viewer_img").InnerHtml;
            }
            Program.LogInfo(count.ToString());
        }
    }
}
