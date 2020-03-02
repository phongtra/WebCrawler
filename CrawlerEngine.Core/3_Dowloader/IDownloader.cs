using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace CrawlerEngine.Core._3_Dowloader
{
    public interface IDownloader
    {
        Task<Tuple<HttpStatusCode, string>> GetPage(HttpRequestMessage requestMessage);
    }
}
