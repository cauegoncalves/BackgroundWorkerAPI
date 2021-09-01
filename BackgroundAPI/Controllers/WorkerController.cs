using BackgroundAPI.Models;
using BackgroundAPI.Services;
using BackgroundAPI.Services.Abstractions;
using BackgroundAPI.Services.Worker.ConsoleWrite.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackgroundAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkerController : ControllerBase
    {

        private readonly IWorkerService<ConsoleMessage> _workerService;

        public WorkerController(IWorkerService<ConsoleMessage> workerService)
        {
            _workerService = workerService;
        }

        [HttpPost("start")]
        public async Task<IActionResult> StartProcess([FromBody]StartProcessRequest request)
        {
            try
            {
                await _workerService.StartAsync(new ConsoleMessage
                {
                    Message = request.Message
                });
                return new StatusCodeResult(StatusCodes.Status201Created);
            }
            catch(InvalidOperationException)
            {
                return Conflict("Process is already running");
            }
        }

        [HttpPost("stop")]
        public async Task<IActionResult> StopProcess()
        {
            await _workerService.StopAsync();
            return Ok();
        }

        [HttpGet()]
        public async Task<IActionResult> GetProcessStatus()
        {
            var status = await _workerService.GetStatus();
            return Ok(status);
        }

    }
}
