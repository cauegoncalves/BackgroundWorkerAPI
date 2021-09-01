using BackgroundAPI.Services;
using BackgroundAPI.Services.Abstractions;
using BackgroundAPI.Services.TaskQueue.InMemory;
using BackgroundAPI.Services.Worker.ConsoleWrite;
using BackgroundAPI.Services.Worker.ConsoleWrite.Models;
using Microsoft.Extensions.DependencyInjection;

namespace BackgroundAPI.Extensions
{
    public static class ServiceCollectionExtensions
    {

        public static IServiceCollection AddBackgroundServices(this IServiceCollection services)
        {
            services.AddSingleton<ITaskQueue, TaskQueueService>();
            services.AddSingleton<IWorkerService<ConsoleMessage>, ConsoleMessageWriteWorkerService>();
            services.AddHostedService<BackgroundWorkerService>();
            return services;
        }

    }
}
