using System;
using Microsoft.Extensions.DependencyInjection;

namespace CrawlerEngine
{
    /// <summary>
    /// 001. Host Builder, starting code of the crawler here
    /// </summary>
    public class CrawlerHostBuilder : IDisposable
    {
        private readonly ServiceCollection _services;
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
