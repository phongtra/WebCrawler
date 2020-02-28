// using System;
// using System.Collections.Generic;
// using System.Text;
// using System.Threading;
// using System.Threading.Tasks;
// using Microsoft.Extensions.Hosting;
// using Microsoft.Extensions.Logging;
//
// namespace CrawlerEngine.Abstraction._1_Scheduler
// {
//     public class DataMaintenanceService: BackgroundService
//     {
//         public DataMaintenanceService(ILoggerFactory loggerFactory)
//         {
//             Logger = loggerFactory.CreateLogger<DataMaintenanceService>();
//         }
//
//         public ILogger Logger { get; }
//
//         protected override async Task ExecuteAsync(CancellationToken stoppingToken)
//         {
//             // Logger.LogInformation("MyServiceA is starting.");
//
//             stoppingToken.Register(() => Logger.LogInformation("DataMaintenanceService is stopping."));
//
//             while (!stoppingToken.IsCancellationRequested)
//             {
//                 Logger.LogInformation("MyServiceA is doing background work.");
//
//                 await Task.Delay(20, stoppingToken);
//             }
//
//             // Logger.LogInformation("MyServiceA background task is stopping.");
//         }
//     }
// }
