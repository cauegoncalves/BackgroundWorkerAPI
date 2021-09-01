using BackgroundAPI.Services.Abstractions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackgroundAPI.Services.Abstractions
{
    public interface IWorkerService<T> where T : BaseTaskModel
    {

        Task StartAsync(T task);
        Task<string> GetStatus();
        Task StopAsync();

    }
}
