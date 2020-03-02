using System;
using System.Linq;
using System.Text.Json;
using System.Web;
using AngleSharp;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using Crawler.Data.Entities;
using Crawler.Domain.Mapping;
using CrawlerEngine._4_Storage.PageModels.Data;
using CrawlerEngine._4_Storage.PageModels.Data.Entities;
using CrawlerEngine._4_Storage.PageModels.Domain.Mapping;
using CrawlerEngine.Abstraction;
using CrawlerEngine.Abstraction._1_Scheduler;
using UriDB.Data.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace CrawlerEngine._3_PageProcessor
{
    public class PageProcessor:IPageProcessor
    {
        private ContentContext _context;
        IServiceScope _scope;

        public PageProcessor(IServiceProvider services)
        {
            _scope = services.CreateScope();
            _context = _scope.ServiceProvider.GetService<ContentContext>();
        }

        public async void ProcessPageContent(IHtmlDocument document, Uri requestUri)
        {
            var uri = requestUri.ToString();
            if (uri == "https://www.webtoons.com/en/dailySchedule")
            {
                var comicCards = document.QuerySelectorAll(".daily_card_item");
            
                foreach (var comicCard in comicCards)
                {
                    var link = comicCard.GetAttribute("href");
                    var webToon = new WebToon
                    {
                        Author      = comicCard.QuerySelector(".author").InnerHtml,
                        ContentHash = "",
                        Genre       = comicCard.QuerySelector(".genre").InnerHtml,
                        ImageLink   = new Uri(comicCard.QuerySelector("img").GetAttribute("src")).PathAndQuery,
                        Subject     = comicCard.QuerySelector(".subj").InnerHtml,
                        TitleNo     = HttpUtility.ParseQueryString(new Uri(link).Query).Get("title_no"),
                        Updated     = DateTime.UtcNow.ToString(Tools.StrDateTimeFormat),
                        Uri         = comicCard.GetAttribute("href")
                    };
                    var existingWeb = await _context.WebToons.FindAsync(webToon.TitleNo);
                    if (existingWeb != null) continue;
                    await _context.WebToons.AddAsync(webToon);
                }
            }
            else if (uri.Contains("/list?title_no"))
            {
                var list = document.QuerySelectorAll("#_listUl li");
                Console.WriteLine(list.Length);
                foreach (var item in list)
                {
                    var episode = new Episode
                    {
                        ContentHash      = "",
                        EpisodeDate      = item.QuerySelector("a .date").InnerHtml,
                        EpisodeLink      = item.QuerySelector("a").GetAttribute("href"),
                        EpisodeName      = item.QuerySelector("a .subj").InnerHtml,
                        EpisodeThumbnail = new Uri(item.QuerySelector("a .thmb img").GetAttribute("src")).PathAndQuery,
                        TitleNo          = HttpUtility.ParseQueryString(requestUri.Query).Get("title_no"),
                        Updated          = DateTime.UtcNow.ToString(Tools.StrDateTimeFormat)
                    };
                    episode.EpisodeLinkHash = Tools.ComputeSha256Hash(episode.EpisodeLink);

                    var existed = await _context.Episodes.FindAsync(episode.EpisodeLinkHash);
                    if (existed != null) continue;
                    await _context.AddAsync(episode);
                }
            }
            else if (uri.Contains("viewer"))
            {
                var content      = document.QuerySelectorAll(".viewer_img img");
                var contentLinks = content.Select(cont => new Uri(cont.GetAttribute("data-url")).PathAndQuery).ToList();

                var page = new Page
                {
                    Content = JsonSerializer.Serialize(contentLinks),
                    EpisodeLink = uri,
                    Updated = DateTime.UtcNow.ToString(Tools.StrDateTimeFormat)
                };
                page.EpisodeLinkHash = Tools.ComputeSha256Hash(page.EpisodeLink);

                var existingPage = _context.Pages.Where(p => p.Content == page.Content).ToList();
                if (existingPage.Count == 0)
                {
                    await _context.AddAsync(page);
                }

            }

            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _scope?.Dispose();
            _context = null;
        }
    }
}
