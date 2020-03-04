using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Abot2.Core;
using Abot2.Poco;

namespace Crawler.Crawler
{
    public class ChromiumPageRequester:IPageRequester
    {
        public void Dispose()
        {
            
        }

        public async Task<CrawledPage> MakeRequestAsync(Uri uri)
        {
            return await MakeRequestAsync(uri, (x) => new CrawlDecision {Allow = true}).ConfigureAwait(false);
        }

        public async Task<CrawledPage> MakeRequestAsync(Uri uri, Func<CrawledPage, CrawlDecision> shouldDownloadContent)
        {
            //Follow this: https://github.com/sjdirect/abot/blob/master/Abot2/Core/PageRequester.cs
            if (uri == null)
                throw new ArgumentNullException(nameof(uri));
            var crawledPage = new CrawledPage(uri);
            crawledPage.RequestStarted = DateTime.Now;


            return crawledPage;
        }
    }
}
