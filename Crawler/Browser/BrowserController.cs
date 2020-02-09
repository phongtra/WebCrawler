using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using PuppeteerSharp;

namespace Crawler
{
    public class BrowserController : IBrowserController
    {
        private readonly Browser _browser;
        private const int ChromiumRevision = BrowserFetcher.DefaultRevision;

        public BrowserController(LaunchOptions options)
        {
            //Download chromium browser revision package
            new BrowserFetcher().DownloadAsync(ChromiumRevision).GetAwaiter().GetResult();

            _browser = Puppeteer.LaunchAsync(options).GetAwaiter().GetResult();
        }

        public Task<Page> NewPageAsync()
        {
            return _browser.NewPageAsync();
        }

        public Target[] Targets()
        {
            return _browser.Targets();
        }

        public Task<BrowserContext> CreateIncognitoBrowserContextAsync()
        {
            return _browser.CreateIncognitoBrowserContextAsync();
        }

        public BrowserContext[] BrowserContexts()
        {
            return _browser.BrowserContexts();
        }

        public Task<Page[]> PagesAsync()
        {
            return _browser.PagesAsync();
        }

        public Task<string> GetVersionAsync()
        {
            return _browser.GetVersionAsync();
        }

        public Task<string> GetUserAgentAsync()
        {
            return _browser.GetUserAgentAsync();
        }

        public void Disconnect()
        {
            _browser.Disconnect();
        }

        public Task CloseAsync()
        {
            return _browser.CloseAsync();
        }

        public Task<Target> WaitForTargetAsync(Func<Target, bool> predicate, WaitForOptions options = null)
        {
            return _browser.WaitForTargetAsync(predicate, options);
        }

        public void Dispose()
        {
            if (!_browser.IsClosed)
            {
                _browser.CloseAsync().Wait();
            }

            _browser.Dispose();
        }

        public string WebSocketEndpoint => _browser.WebSocketEndpoint;

        public Process Process => _browser.Process;

        public bool IgnoreHTTPSErrors
        {
            get => _browser.IgnoreHTTPSErrors;
            set => _browser.IgnoreHTTPSErrors = value;
        }

        public bool IsClosed => _browser.IsClosed;

        public BrowserContext DefaultContext => _browser.DefaultContext;

        public int DefaultWaitForTimeout
        {
            get => _browser.DefaultWaitForTimeout;
            set => _browser.DefaultWaitForTimeout = value;
        }

        public bool IsConnected => _browser.IsConnected;

        public Target Target => _browser.Target;

        public event EventHandler Closed
        {
            add => _browser.Closed += value;
            remove => _browser.Closed -= value;
        }

        public event EventHandler Disconnected
        {
            add => _browser.Disconnected += value;
            remove => _browser.Disconnected -= value;
        }

        public event EventHandler<TargetChangedArgs> TargetChanged
        {
            add => _browser.TargetChanged += value;
            remove => _browser.TargetChanged -= value;
        }

        public event EventHandler<TargetChangedArgs> TargetCreated
        {
            add => _browser.TargetCreated += value;
            remove => _browser.TargetCreated -= value;
        }

        public event EventHandler<TargetChangedArgs> TargetDestroyed
        {
            add => _browser.TargetDestroyed += value;
            remove => _browser.TargetDestroyed -= value;
        }
    }
}
