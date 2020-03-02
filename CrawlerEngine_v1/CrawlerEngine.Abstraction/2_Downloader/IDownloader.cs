using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CrawlerEngine._2_Downloader
{
    public interface IDownloader
    {
        Task<string> GetPage(HttpRequestMessage requestMessage);
    }
}
