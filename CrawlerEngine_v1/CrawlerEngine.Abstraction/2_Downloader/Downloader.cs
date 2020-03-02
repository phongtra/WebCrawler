using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CrawlerEngine._2_Downloader;
using CrawlerEngine._4_Storage.PageModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CrawlerEngine.Abstraction._2_Downloader
{
    public class Downloader : IDownloader
    {
        public IHttpClientFactory ClientFactory { get; }

        public Downloader(IHttpClientFactory clientFactory)
        {
            ClientFactory = clientFactory;
        }

        public async Task<string> GetPage(HttpRequestMessage requestMessage)
        {
            var client   = ClientFactory.CreateClient();
            var response = await client.SendAsync(requestMessage);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }

            return $"StatusCode: {response.StatusCode}";
        }
    }
}
