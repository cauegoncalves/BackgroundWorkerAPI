using BackgroundAPI.Services.Abstractions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BackgroundAPI.Services.TaskQueue.InMemory
{
    public class TaskQueueService : ITaskQueue
    {

        private ConcurrentQueue<Func<Task>> taskQueue = new ConcurrentQueue<Func<Task>>();

        public Task<Func<Task>> DequeueTaskAsync()
        {
            taskQueue.TryDequeue(out var task);
            return Task.FromResult(task);
        }

        public Task QueueTaskAsync(Func<Task> task)
        {
            taskQueue.Enqueue(task);
            return Task.CompletedTask;
        }
    }
}
