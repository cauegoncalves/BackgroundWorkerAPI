using BackgroundAPI.Services.Abstractions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BackgroundAPI.Services.Worker.ConsoleWrite.Models
{
    public class ConsoleMessage : BaseTaskModel
    {
        public string Message { get; set; } = string.Empty;
    }
}
