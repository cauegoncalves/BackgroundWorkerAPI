using BackgroundAPI.Services.Abstractions;
using BackgroundAPI.Services.Worker.ConsoleWrite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BackgroundAPI.Services.Worker.ConsoleWrite
{
    public class ConsoleMessageWriteWorkerService : IWorkerService<ConsoleMessage>
    {

        private readonly ITaskQueue _taskQueueService;
        private CancellationTokenSource? _cancellationTokenSource;

        public ConsoleMessageWriteWorkerService(ITaskQueue taskQueueService)
        {
            _taskQueueService = taskQueueService;
        }

        public async Task StartAsync(ConsoleMessage task)
        {
            if (IsRunning())
                throw new InvalidOperationException();

            _cancellationTokenSource = new CancellationTokenSource();
            task.CancellationToken = _cancellationTokenSource.Token;
            await _taskQueueService.QueueTaskAsync(async () =>
            {
                int i = 0;
                Console.WriteLine($"Start of message => {task.Message}");
                while (!task.CancellationToken.IsCancellationRequested)
                {
                    Console.WriteLine($"{i++} => {task.Message}");
                    try
                    {
                        await Task.Delay(1000, task.CancellationToken);
                    }
                    catch(TaskCanceledException){ }
                    catch (OperationCanceledException) { }
                }
                Console.WriteLine($"End of message => {task.Message}");
            });
        }

        private bool IsRunning()
        {
            return (_cancellationTokenSource != null && !_cancellationTokenSource.Token.IsCancellationRequested);
        }

        public Task StopAsync()
        {
            if (_cancellationTokenSource != null)
                _cancellationTokenSource.Cancel();

            return Task.CompletedTask;
        }

        public Task<string> GetStatus()
        {
            var status = "stopped";
            if (IsRunning())
                status = "running";

            return Task.FromResult(status);
        }
    }

}
