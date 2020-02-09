using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Html.Parser;
using Content.Data;
using Content.Domain.Mapping;
using Content.Domain.Models;
using Microsoft.EntityFrameworkCore;
using PuppeteerSharp;

namespace ContentCrawler
{
    public class ContentCrawler
    {
        private const int ChromiumRevision = BrowserFetcher.DefaultRevision;
        public static async Task Crawler()
        {
            //Download chromium browser revision package
            await new BrowserFetcher().DownloadAsync(ChromiumRevision);
            using var browser = await Puppeteer.LaunchAsync(new LaunchOptions
            {
                // Headless = true
                Headless = false
            });
            var optionsBuilder = new DbContextOptionsBuilder<ContentContext>();
            var options = optionsBuilder
                .UseSqlite("Data Source=D:\\WebCrawlerPrj\\Crawler\\DB\\content.db;",
                    providerOptions => providerOptions.CommandTimeout(60));
            await using var _context = new ContentContext(options.Options);
            var articles = _context.CrawledLinks.Select(c => new { c.Id, c.Url }).ToList();
            foreach (var article in articles)
            {
                var page = await browser.NewPageAsync();
                var pageContent = await GetPageContent(page, article.Url, 1024, 768);
                var context = BrowsingContext.New(Configuration.Default);
                var parser = context.GetService<IHtmlParser>();
                var document = parser.ParseDocument(pageContent);
                var articleContent = document.QuerySelector(".sidebar_1").InnerHtml;
                var articleModel = CreatePageModel(article.Url, articleContent, article.Id);
                var articleEntity = PageProfile.MapCreateModelToEntity(articleModel);
                await _context.Pages.AddAsync(articleEntity);
                await _context.SaveChangesAsync();
                await page.CloseAsync();
            }
            await browser.CloseAsync();
        }
        private static PageCreateModel CreatePageModel(string url, string pageContent, long crawledLinkId)
        {
            var page = new PageCreateModel { Url = url, Content = pageContent, CrawledLinkId = crawledLinkId };
            return page;
        }
        private static async Task<string> GetPageContent(Page page, string url, int ViewPortWidth, int ViewPortHeight)
        {

            await page.SetViewportAsync(new ViewPortOptions
            {
                Width = ViewPortWidth,
                Height = ViewPortHeight
            });
            await page.GoToAsync(url);

            // var outputFile = "capture.jpg";
            //            await page.ScreenshotAsync(outputFile, new ScreenshotOptions {FullPage = true});
            var pageContent = await page.GetContentAsync();

            //Close tab page
            // await page.CloseAsync();
            return pageContent;
        }
    }
}
