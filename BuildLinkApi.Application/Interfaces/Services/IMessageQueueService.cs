using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuildLinkApi.Application.Interfaces.Services
{
    public interface IMessageQueueService
    {
        Task PublishEmailVerificationAsync(string email, string code);
    }
}