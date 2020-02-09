using System;
using System.Diagnostics;
using System.Threading.Tasks;
using PuppeteerSharp;

namespace Crawler
{
    public interface IBrowserController : IDisposable
    {
        Task<Page> NewPageAsync();
        Target[] Targets();
        Task<BrowserContext> CreateIncognitoBrowserContextAsync();
        BrowserContext[] BrowserContexts();
        Task<Page[]> PagesAsync();
        Task<string> GetVersionAsync();
        Task<string> GetUserAgentAsync();
        void Disconnect();
        Task CloseAsync();
        Task<Target> WaitForTargetAsync(Func<Target, bool> predicate, WaitForOptions options = null);
        string WebSocketEndpoint { get; }
        Process Process { get; }
        bool IgnoreHTTPSErrors { get; set; }
        bool IsClosed { get; }
        BrowserContext DefaultContext { get; }
        int DefaultWaitForTimeout { get; set; }
        bool IsConnected { get; }
        Target Target { get; }
        event EventHandler Closed;
        event EventHandler Disconnected;
        event EventHandler<TargetChangedArgs> TargetChanged;
        event EventHandler<TargetChangedArgs> TargetCreated;
        event EventHandler<TargetChangedArgs> TargetDestroyed;
    }
}