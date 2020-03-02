using System;
using System.Collections.Generic;
using System.Text;
using AngleSharp.Html.Dom;
using CrawlerEngine._3_PageProcessor;
using CrawlerEngine.Abstraction._1_Scheduler;
using UriDB.Data.Entities;

namespace CrawlerEngine.Abstraction._3_PageProcessor
{
    public class DefaultPageProcessor : IPageProcessor
    {
        public void ProcessPageContent(IHtmlDocument document, Uri requestUri)
        {
            //Dont do anything
        }

        public void Dispose()
        {
        }
    }
}
