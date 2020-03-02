using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using UriDB.Data.Entities;

namespace CrawlerEngine.Abstraction._1_Scheduler
{
    public interface ISchedulerService : IHostedService
    {
        Task DoCrawling(WaitingPage pageToCrawl, IUriBucket<WaitingPage> uriBucket);
        void Dispose();
    }
}