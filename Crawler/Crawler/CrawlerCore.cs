using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using PuppeteerSharp;

namespace Crawler
{
    public class CrawlerCore
    {
        public static async Task Crawl()
        {
            //1. Retrieve page
            //2. Extract info
            //3. Add links
        }

        public static async Task DemoCrawl()
        {
            var browser = Program.ServiceProvider.GetService<IBrowserController>();
            var          url             = "https://vnexpress.net/";
            var          page            = await browser.NewPageAsync();
            var          mainPageContent = await GetPageContent(page, url, 1024, 768);
            // Program.LogInfo(mainPageContent);
            Program.LogInfo("Done");
        }

        private static async Task<string> GetPageContent(Page page, string url, int ViewPortWidth, int ViewPortHeight)
        {
            await page.SetViewportAsync(new ViewPortOptions
            {
                Width  = ViewPortWidth,
                Height = ViewPortHeight
            });
            await page.GoToAsync(url);

            // var outputFile = "capture.jpg";
            //            await page.ScreenshotAsync(outputFile, new ScreenshotOptions {FullPage = true});
            var pageContent = await page.GetContentAsync();

            //Close tab page
            await page.CloseAsync();
            return pageContent;
        }
    }
}
