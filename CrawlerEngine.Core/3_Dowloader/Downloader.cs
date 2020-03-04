using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace CrawlerEngine.Core._3_Dowloader
{
    public class Downloader : IDownloader
    {
        private IHttpClientFactory ClientFactory { get; }

        public Downloader(IHttpClientFactory clientFactory)
        {
            ClientFactory = clientFactory;
        }

        public async Task<Tuple<HttpStatusCode, string>> GetPage(HttpRequestMessage requestMessage)
        {
            using var client   = ClientFactory.CreateClient();
            var       response = await client.SendAsync(requestMessage);

            return response.IsSuccessStatusCode
                ? new Tuple<HttpStatusCode, string>(response.StatusCode, await response.Content.ReadAsStringAsync())
                : new Tuple<HttpStatusCode, string>(response.StatusCode, "");

        }
    }
}
