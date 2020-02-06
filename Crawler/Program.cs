using System;
using System.Threading.Tasks;
using PuppeteerSharp;

namespace Crawler
{
    class Program
    {
        private const int ChromiumRevision = BrowserFetcher.DefaultRevision;

        static async Task Main(string[] args)
        {
            //Download chromium browser revision package
            await new BrowserFetcher().DownloadAsync(ChromiumRevision);
            string pageContent;
            using (var browser = await Puppeteer.LaunchAsync(new LaunchOptions
            {
                Headless = true
            }))
            {
                var url = "http://vnexpress.net";

                pageContent = await GetPageContent(browser, url, 1024, 768);


                //Close headless browser, all pages will be closed here.
                await browser.CloseAsync();

            }

            Console.WriteLine(pageContent);
        }

        private static async Task<string> GetPageContent(Browser browser, string url, int ViewPortWidth, int ViewPortHeight)
        {
            var page = await browser.NewPageAsync();
            await page.SetViewportAsync(new ViewPortOptions
            {
                Width  = ViewPortWidth,
                Height = ViewPortHeight
            });
            await page.GoToAsync(url);
            var outputFile = "capture.jpg";
//            await page.ScreenshotAsync(outputFile, new ScreenshotOptions {FullPage = true});
            var pageContent = await page.GetContentAsync();
            //Close tab page
            await page.CloseAsync();
            return pageContent;
        }
    }
}
