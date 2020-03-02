using System;
using System.Collections.Generic;
using System.Text;

namespace CrawlerEngine.Core._2_Crawler
{
    public interface ICrawler
    {
        void CrawlAPage(Uri uri);
    }
}
