using System;
using System.Threading.Tasks;
using AngleSharp.Html.Dom;

namespace CrawlerEngine.Core._5_ContentExtractor
{
    public interface IContentExtractor : IDisposable
    {
        Task ProcessPageContent(IHtmlDocument document, Uri requestUri);
    }
}
