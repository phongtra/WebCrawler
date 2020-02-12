using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Abot2.Crawler;
using Abot2.Poco;
using AngleSharp;
using AngleSharp.Html.Parser;
using Content.Data;
using Crawler.Data;
using Crawler.Domain.Mapping;
using Crawler.Domain.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Crawler.Crawler
{
    public class CrawlerHandler
    {
        private static int count = 0;
        private static List<string> genres = new List<string>(){"romance", "fantasy"};
        public static async Task DemoSimpleCrawler()
        {
            var crawler = Program.ServiceProvider.GetService<IPoliteWebCrawler>();
            crawler.ShouldCrawlPageDecisionMaker += (crawl, context) =>
            {
                if (genres.Any(genre => crawl.Uri.AbsoluteUri.Contains(genre)  ||
                                         crawl.Uri.AbsoluteUri.Contains("viewer")))
                {
                    return new CrawlDecision { Allow = true };
                }
            
                return new CrawlDecision { Allow = false, Reason = "Invalid site" };
            };

            crawler.PageCrawlCompleted += Crawler_PageCrawlCompleted; ; //Several events available...

            var crawlResult = await crawler.CrawlAsync(new Uri("https://www.webtoons.com/en/romance/subzero/list?title_no=1468"));
        }

        private static async void Crawler_PageCrawlCompleted(object sender, PageCrawlCompletedArgs e)
        {
            count++;
            var httpStatus  = e.CrawledPage.HttpResponseMessage.StatusCode;
            var rawPageText = e.CrawledPage.Content.Text;
            var _context = Program.ServiceProvider.GetService<ContentContext>();
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
                    var genre = comicCard.QuerySelector(".genre").InnerHtml;
                    var subject = comicCard.QuerySelector(".subj").InnerHtml;
                    var author = comicCard.QuerySelector(".author").InnerHtml;
                  
                    var cardModel = createWebToonModel(link, titleNo, imageLink, genre, subject, author);
                    var cardEntity = WebtoonProfile.MapCreateModelToEntity(cardModel);
                    await _context.WebToons.AddAsync(cardEntity);
                    // await _context.SaveChangesAsync();
                }
            }
            // else if (e.CrawledPage.Uri.AbsoluteUri.Contains("list"))
            // {
            //     var context = BrowsingContext.New(Configuration.Default);
            //     var parser = context.GetService<IHtmlParser>();
            //     var document = parser.ParseDocument(rawPageText);
            //     var list = document.QuerySelectorAll("#_listUl li");
            //     Console.WriteLine(list.Length);
            //     foreach (var item in list)
            //     {
            //         var link = new Uri(e.CrawledPage.Uri.AbsoluteUri);
            //         var titleNo = HttpUtility.ParseQueryString(link.Query).Get("title_no");
            //         var episodeName = item.QuerySelector("a .subj").InnerHtml;
            //         var episodeThumb = item.QuerySelector("a .thmb img").GetAttribute("src");
            //         var episodeDate = item.QuerySelector("a .date").InnerHtml;
            //         var episodeLink = item.QuerySelector("a").GetAttribute("href");
            //         var episodeLinkHash = ComputeSha256Hash(episodeLink);
            //         var episodeModel = createEpisodeModel(titleNo, episodeName, episodeThumb, episodeDate, episodeLink,
            //             episodeLinkHash);
            //         var episodeEntity = EpisodeProfile.MapCreateModelToEntity(episodeModel);
            //         // Console.WriteLine(episodeThumb);
            //         // var _context = Program.ServiceProvider.GetService<ContentContext>();
            //         var existed = await _context.Episodes.FindAsync(episodeLinkHash);
            //         if (existed == null) { await _context.AddAsync(episodeEntity); }
            //         
            //         // Console.WriteLine(episodeLinkHash);
            //         
            //     }
            // }
            else if (e.CrawledPage.Uri.AbsoluteUri.Contains("viewer"))
            {
                var context = BrowsingContext.New(Configuration.Default);
                var parser = context.GetService<IHtmlParser>();
                var document = parser.ParseDocument(rawPageText);
                var content = document.QuerySelector(".viewer_img");
                Console.WriteLine(content.InnerHtml);
                // var contentLinks = content.Select(cont => cont.GetAttribute("data-url")).ToList();
                // foreach (var cont in content)
                // {
                //     var linkData = cont.InnerHtml;
                //     Console.WriteLine(linkData);
                // }
                // var episodeLink = e.CrawledPage.Uri.AbsoluteUri;
                // var epsiodeLinkHash = ComputeSha256Hash(episodeLink);
                // var contentJson = JsonSerializer.Serialize(contentLinks);
                // var pageModel = createPageModel(epsiodeLinkHash, contentJson);
                // var pageEntity = PageProfile.MapCreateModelToEntity(pageModel);
                // var _context = Program.ServiceProvider.GetService<ContentContext>();
                // await _context.AddAsync(pageEntity);
                // await _context.SaveChangesAsync();
            }
            Program.LogInfo(count.ToString());
            // await _context.SaveChangesAsync();
        }

        private static EpisodeCreateModel createEpisodeModel(string titleNo, string episodeName,
            string episodeThumbnail, string episodeDate, string episodeLink, string episodeLinkHash)
        {
            return new EpisodeCreateModel(){TitleNo = titleNo, EpisodeName = episodeName, EpisodeDate = episodeDate, EpisodeThumbnail = episodeThumbnail, EpisodeLink = episodeLink, EpisodeLinkHash = episodeLinkHash, Updated = DateTime.Now.ToString("f"), ContentHash = "Update Later"};
        }
        private static WebToonCreateModel createWebToonModel(string uri, string titleNo, string imageLink, string genre,
            string subject, string author)
        {
            return new WebToonCreateModel(){Uri = uri, TitleNo = titleNo, ImageLink = imageLink, Genre = genre, Subject = subject, Author = author, Updated = DateTime.Now.ToString("f"), ContentHash = "Update Later"};
        }

        private static PageCreateModel createPageModel(string episodeLinkHash, string content)
        {
            return new PageCreateModel(){Content = content, EpisodeLinkHash = episodeLinkHash, Updated = DateTime.Now.ToString("f")};
        }
        private static string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256   
            using var sha256Hash = SHA256.Create();
            // ComputeHash - returns byte array  
            var bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

            // Convert byte array to a string   
            var builder = new StringBuilder();
            foreach (var t in bytes)
            {
                builder.Append(t.ToString("x2"));
            }
            return builder.ToString();
        }
    }
}
