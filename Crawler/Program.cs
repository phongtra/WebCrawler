﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using Content.Domain.Models;
using PuppeteerSharp;

namespace Crawler
{
    public class Program
    {
        private const int ChromiumRevision = BrowserFetcher.DefaultRevision;


        public static async Task Main(string[] args)
        {
            var linkConcurrentQueue = new ConcurrentQueue<string>();
            //Download chromium browser revision package
            await new BrowserFetcher().DownloadAsync(ChromiumRevision);
            using var browser = await Puppeteer.LaunchAsync(new LaunchOptions
            {
                // Headless = true
                Headless = false
            });
            var url = "https://vnexpress.net/";
            var page = await browser.NewPageAsync();
            var mainPageContent = await GetPageContent(page, url, 1024, 768);
            const string element = "//nav[@id='main_menu']/a[contains(., 'Kinh doanh')]";
            await Navigate(page, element);
                
            var kinhDoanhPageContent = await GetPageContent(page, page.Url, 1024, 768);
            ExtractKinhDoanhPage(kinhDoanhPageContent, ".container .sidebar_1 .list_news", page, linkConcurrentQueue);
            while (linkConcurrentQueue.Count > 0)
            {
                if (!linkConcurrentQueue.TryDequeue(out var link)) continue;
                var childPage = await browser.NewPageAsync();
                await OpenArticlePage(childPage, link);
                await childPage.CloseAsync();
            }
                
            var pageModel = CreatePageModel(url, mainPageContent);
            await page.CloseAsync();
            //Close headless browser, all pages will be closed here.
            await browser.CloseAsync();
        }

        private static async Task OpenArticlePage(Page childPage, string link)
        {
            var articlePageContent = await GetPageContent(childPage, link, 1024, 768);
            var context = BrowsingContext.New(Configuration.Default);
            ExtractArticlePage(context, articlePageContent);
        }

        private static void ExtractArticlePage(IBrowsingContext context, string articlePageContent)
        {
            var parser = context.GetService<IHtmlParser>();
            var document = parser.ParseDocument(articlePageContent);
            var articlePageTitle = document.QuerySelector(".title_news_detail").InnerHtml;
            var articlePageDescription = document.QuerySelector(".description").InnerHtml;
            var paragraphs = document.QuerySelectorAll(".Normal");

            Console.WriteLine(paragraphs.Length);
            var articlePageParagraphs = paragraphs.Select(paragraph => paragraph.InnerHtml).ToList();

            // var author = childParagraphs.ElementAt(childParagraphs.Count - 1);
            // childParagraphs.RemoveAt(childParagraphs.Count - 1);
            var articlePageImages =
                document.QuerySelectorAll("table img");
            var articlePageImageLinks =
                articlePageImages.Select(childPageImage => childPageImage?.GetAttribute("src")).ToList();
            Console.WriteLine(articlePageTitle);
            Console.WriteLine(articlePageDescription);

            foreach (var articlePageParagraph in articlePageParagraphs)
            {
                Console.WriteLine(articlePageParagraph);
            }

            foreach (var articlePageImageLink in articlePageImageLinks)
            {
                Console.WriteLine(articlePageImageLink);
            }
        }

        private static void ExtractKinhDoanhPage(string kinhDoanhPageContent, string selector, Page page, ConcurrentQueue<string> linkConcurrentQueue)
        {
            var context = BrowsingContext.New(Configuration.Default);
            var parser = context.GetService<IHtmlParser>();
            var document = parser.ParseDocument(kinhDoanhPageContent);
            var articles = document.QuerySelectorAll(selector);
            foreach (var article in articles)
            {
                var anchors = article.QuerySelectorAll("a");
                var link =  anchors[0].GetAttribute("href");
                var title = anchors[0].InnerHtml;
                var image = (IHtmlImageElement) anchors[2].QuerySelector("img");
                var imageLink = image.Source;
                var description = anchors[3].InnerHtml;
                linkConcurrentQueue.Enqueue(link);
                // foreach (var anchor in anchors)
                // {
                //     var unbox = (IHtmlAnchorElement) anchor;
                //     unbox.h
                //  Console.WriteLine("Begins:\n" + anchor.InnerHtml);   
                // }
            }
        }                                                                                                                                                                                                                                                                                                                                        

        private static async Task Navigate(Page page, string element)
        {
            var linkHandlers = await page.XPathAsync(element);

            if (linkHandlers.Length > 0)
            {
                await linkHandlers[0].ClickAsync();
                await page.WaitForNavigationAsync();
                Console.WriteLine(page.Url);
            }
            else
            {
                throw new Exception("Link not found");
            }
        }

        private static PageCreateModel CreatePageModel(string url, string pageContent)
        {
            var page = new PageCreateModel();
            page.Url = url;
            page.Content = pageContent;
            return page;
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
            
            return pageContent;
        }
    }
}
