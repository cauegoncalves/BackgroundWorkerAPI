using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace BackgroundAPI.Services.Abstractions.Models
{
    public abstract class BaseTaskModel
    {
        public CancellationToken CancellationToken { get; set; }
    }
}
