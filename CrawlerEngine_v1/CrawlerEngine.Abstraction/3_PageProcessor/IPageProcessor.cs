using System;
using System.Collections.Generic;
using System.Text;
using AngleSharp.Html.Dom;
using CrawlerEngine.Abstraction._1_Scheduler;
using UriDB.Data.Entities;

namespace CrawlerEngine._3_PageProcessor
{
    public interface IPageProcessor : IDisposable
    {
        void ProcessPageContent(IHtmlDocument document, Uri requestUri);
    }
}
