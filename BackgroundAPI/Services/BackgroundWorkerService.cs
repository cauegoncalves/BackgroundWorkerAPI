using BackgroundAPI.Services.Abstractions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BackgroundAPI.Services
{

    public class BackgroundWorkerService : BackgroundService
    {

        private readonly ILogger<BackgroundWorkerService> _logger;
        private readonly ITaskQueue _taskQueueService;

        public BackgroundWorkerService(ITaskQueue taskQueueService)
        {
            _taskQueueService = taskQueueService;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while(!cancellationToken.IsCancellationRequested)
            {
                var task = await _taskQueueService.DequeueTaskAsync();
                if (task != null)
                {
                    try
                    {
                        await task();
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error executing the background task");
                    }
                }
                await Task.Delay(5000);
            }
        }
    }
}
