using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace CrawlerEngine.Abstraction
{
    public class Framework
    {
        public static void SetMultiThread()
        {
            ThreadPool.SetMinThreads(256, 256);
        }
    }
}
