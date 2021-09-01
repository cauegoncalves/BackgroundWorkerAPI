using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BackgroundAPI.Services.Abstractions
{
    public interface ITaskQueue
    {

        Task QueueTaskAsync(Func<Task> task);

        Task<Func<Task>> DequeueTaskAsync();

    }
}