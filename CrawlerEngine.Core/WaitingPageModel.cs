using System;
using System.Collections.Generic;
using System.Text;

namespace CrawlerEngine.Core
{
    public class WaitingPageModel
    {
        public Uri Uri { get; set; }

        public string UriHash { get; set; }

        public double? Priority { get; set; }

        public string RequestTime { get; set; }

        public string Verb { get; set; }

        public string DownloadedTime { get; set; }

        public long? NeedUpdate { get; set; }
    }
}
