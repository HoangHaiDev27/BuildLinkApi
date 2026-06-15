using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuildLinkApi.Application.Interfaces.Services
{
    public interface ITurnstileService
    {
        Task<bool> VerifyAsync(string token, string? remoteIp);

    }
}